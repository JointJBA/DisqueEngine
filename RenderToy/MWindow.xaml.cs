using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Disque.Raytracer.Worlds;
using Disque.Raytracer.Rml;
using System.ComponentModel;
using System.IO;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Folding;
using Microsoft.Win32;
using System.Diagnostics;

namespace RenderToy
{
    /// <summary>
    /// Interaction logic for MWindow.xaml
    /// </summary>
    public partial class MWindow : Window
    {
        #region variables
        World world;

        BackgroundWorker worker = new BackgroundWorker();

        DispatcherTimer dt = new DispatcherTimer();

        FoldingManager foldingManager;

        XmlFoldingStrategy foldingStrategy;

        OpenFileDialog openDialog = new OpenFileDialog() { Multiselect = false };

        SaveFileDialog saveDialog = new SaveFileDialog() { Filter = "RenderToy Markup Language (*.rtml)|*.rtml" };

        FileManager fileManager = new FileManager();

        LogManager logManager = new LogManager();

        bool fileSaved = true;

        bool logSaved = true;

        string saveDirectory;

        Stopwatch renderTimer = new Stopwatch();
        #endregion

        public MWindow()
        {
            try
            {
                InitializeComponent();
                Closing += new CancelEventHandler(MWindow_Closing);
                worker.DoWork += new DoWorkEventHandler(worker_DoWork);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                dt.Interval = TimeSpan.FromSeconds(1);
                dt.Tick += new EventHandler(dt_Tick);
                foldingManager = FoldingManager.Install(textEditor.TextArea);
                foldingStrategy = new XmlFoldingStrategy();
                textEditor.TextChanged += new EventHandler(textEditor_TextChanged);
                if (App.StartUpCommand != "" && App.StartUpCommand != null)
                {
                    openFile(App.StartUpCommand);
                }
                KeyGesture renderKeyGesture = new KeyGesture(Key.F5);
                KeyBinding renderCmdKeybinding = new KeyBinding(Commands.Render, renderKeyGesture);
                this.InputBindings.Add(renderCmdKeybinding);
                status.Text = "Ready!";
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        void MWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!closeFile())
                e.Cancel = true;
        }

        void dt_Tick(object sender, EventArgs e)
        {
            render_progress.Value = ((double)world.Screen.Pixels.Count / (double)(world.ViewPlane.HRes * world.ViewPlane.VRes)) * 100.0;
            render_progress_text.Text = world.Screen.Pixels.Count + " / " + (world.ViewPlane.HRes * world.ViewPlane.VRes) + " pixels rendered.";
            if (render_progress.Value == render_progress.Maximum)
            {
                dt.Stop();
                render_progress_text.Text = "";
                world.Screen.Pixels.Clear();
            }
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            status.Text = "Ready";
            renderTimer.Stop();
            rendered_image.Source = new BitmapImage(new Uri(RenderImage(world)));
            textEditor.IsEnabled = true;
            logSaved = false;
            updateImageList();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            renderTimer.Reset();
            renderTimer.Start();
            world.RenderScene();
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            string file = createFile();
            if (file != null)
            {
                newFile(file);
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            saveFile();
        }

        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           bool? open = openDialog.ShowDialog(this);
           if (open == true)
           {
               openFile(openDialog.FileName);
           }
        }

        private void Render_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                saveFile();
                Disque.Raytracer.GeometricObjects.GeometricObject.Clear();
                world = RmlReader.Compile(textEditor.Text, ref saveDirectory);
                worker.RunWorkerAsync();
                dt.Start();
                textEditor.IsEnabled = false;
                status.Text = "Rendering...";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void AddImage_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddImage add = new AddImage();
            add.ShowDialog();
        }

        string RenderImage(World screen)
        {
            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(screen.ViewPlane.HRes, screen.ViewPlane.VRes);
            for (int i = 0; i < screen.Screen.Pixels.Count; i++)
            {
                Pixel p = screen.Screen.Pixels[i];
                System.Windows.Media.Color r = System.Windows.Media.Color.FromScRgb(1.0f, p.Color.R, p.Color.G, p.Color.B);
                bm.SetPixel((int)p.X, (int)p.Y, System.Drawing.Color.FromArgb(r.R, r.G, r.B));
            }
            string name = getLogCount() + ".png";
            string file = saveDirectory + @"\";
            bm.Save(file + name, System.Drawing.Imaging.ImageFormat.Png);
            addToLog(file + name, world.ViewPlane.NumSamples, DateTime.Now, renderTimer.Elapsed);
            return file + name;
        }

        void textEditor_TextChanged(object sender, EventArgs e)
        {
            foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
            fileSaved = false;
            setTitle();
            textEditor.IsEnabled = true;
            render_progress.Value = 0;
        }

        void imageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (imageList.SelectedItem != null)
            {
                if (e.RemovedItems.Count > 0)
                    ((ImageItem)e.RemovedItems[0]).UnSelect();
                ImageItem ii = (ImageItem)imageList.SelectedItem;
                ii.Select();
                rendered_image.Source = ii.mainImage.Source;
            }
        }

        #region Window functions
        void newFile(string uri)
        {
            closeFile();
            File.WriteAllText(uri, "<World></World>");
            File.WriteAllText(uri + ".log", "<Log></Log>");
            openFile(uri);
        }

        bool closeFile()
        {
            if (!fileSaved)
            {
                MessageBoxResult mbr = MessageBox.Show(this, "Are you sure you want to close without saving?", "Render Toy", MessageBoxButton.YesNoCancel);
                if (mbr == MessageBoxResult.No)
                {
                    saveFile();
                    return true;
                }
                else if (mbr == MessageBoxResult.Yes)
                {
                    return true;
                }
                return false;
            }
            saveLog();
            return true;
        }

        void saveFile()
        {
            if (!fileSaved)
            {
                if (fileManager.CurrentFileUri != "")
                {
                    fileManager.CurrentFile = textEditor.Text;
                    fileManager.Save();
                    fileSaved = true;
                }
                else
                {
                    fileManager.CurrentFileUri = createFile();
                    fileManager.CurrentFile = textEditor.Text;
                    fileManager.Save();
                    logManager.CurrentLogUri = fileManager.CurrentFileUri + ".log";
                    fileSaved = true;
                }
                setTitle();
            }
        }

        void setTitle()
        {
            Title = "RenderToy - " + fileManager.Title;
            if (!fileSaved)
                Title += "*";
        }

        string createFile()
        {
            bool? d = saveDialog.ShowDialog(this);
            if (d == true)
            {
                return saveDialog.FileName;
            }
            return null;
        }

        void openFile(string uri)
        {
            fileManager.Open(uri);
            textEditor.Text = fileManager.CurrentFile;
            setTitle();
            openLog(uri + ".log");
            saveFile();
        }

        void openLog(string uri)
        {
            logManager.Open(uri);
            updateImageList();
        }

        void addToLog(string uri, int ns, DateTime dt, TimeSpan st)
        {
            logManager.Images.Add(new ImageData() { Uri = uri, NumSamples = ns, RenderDate= dt, RenderTime = st });
        }

        void saveLog()
        {
            if (!logSaved)
                logManager.Save();
        }

        int getLogCount()
        {
            return logManager.Images.Count + 1;
        }

        void updateImageList()
        {
            imageList.Items.Clear();
            foreach (ImageData im in logManager.Images)
            {
                ImageItem ii = new ImageItem();
                ii.SetImage(im);
                imageList.Items.Add(ii);
            }
        }
        #endregion
    }
}
