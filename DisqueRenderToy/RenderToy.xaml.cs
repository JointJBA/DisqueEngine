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
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Folding;
using Microsoft.Xna.Framework;
using System.ComponentModel;
using Disque.Raytracer.Worlds;
using Disque;

namespace DisqueRenderToy
{
    /// <summary>
    /// Interaction logic for RenderToy.xaml
    /// </summary>
    public partial class RenderToy : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        GameScreen gscreen;
        FoldingManager foldingManager;
        XmlFoldingStrategy foldingStrategy;
        public RenderToy()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(RenderToy_Loaded);
            gscreen = new GameScreen(screen.Handle);
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Start();
            dt.Tick += new EventHandler(dt_Tick);
            status.Text = Variables.StatusString + Variables.LoadingString;
            foldingManager = FoldingManager.Install(textEditor.TextArea);
            foldingStrategy = new XmlFoldingStrategy();
            textEditor.TextChanged += new EventHandler(textEditor_TextChanged);
            screen.SizeChanged += new SizeChangedEventHandler(screen_SizeChanged);
        }
        void screen_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            gscreen.AdjustCamera((float)screen.ActualWidth, (float)screen.ActualHeight);
        }
        void textEditor_TextChanged(object sender, EventArgs e)
        {
            foldingStrategy.UpdateFoldings(foldingManager, textEditor.Document);
        }
        void dt_Tick(object sender, EventArgs e)
        {
            fps.Text = "FPS: " + gscreen.FPS;
        }
        void RenderToy_Loaded(object sender, RoutedEventArgs e)
        {
            status.Text = Variables.StatusString + Variables.ReadyString;
        }
        private void image_render_button_Click(object sender, RoutedEventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.RunWorkerAsync();
            bw.DoWork += (f, g) => 
            {
                g.Result = prepareobjects();
            };
            bw.RunWorkerCompleted += (s, f) =>
            {
                EditWindow window = new EditWindow(EditMode.Image, (World)f.Result);
                window.Show();
            };
        }
        World prepareobjects()
        {
            return new World();
        }
        Vector3 project(Vector newv)
        {
            double x = newv.X / (screen.ActualWidth / 2.0);
            double y = newv.Y / (screen.ActualHeight / 2.0);
            x = x - 1;
            y = 1 - y;
            double z2 = 1 - x * x - y * y;
            double z = z2 > 0 ? Math.Sqrt(z2) : 0;
            return Vector3.Normalize(new Vector3((float)x, (float)y, (float)z));
        }
        void closeProject()
        {
        }
        static double AngleBetween(Vector3 vector1, Vector3 vector2)
        {
            vector1.Normalize();
            vector2.Normalize();
            double num = Vector3.Dot(vector1, vector2);
            double radians;
            if (num < 0.0)
            {
                radians = 3.1415926535897931 - 2.0 * Math.Asin((-vector1 - vector2).Length() / 2.0);
            }
            else
            {
                radians = 2.0 * Math.Asin((vector1 - vector2).Length() / 2.0);
            }
            return radians;
        }
        private void Close_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            closeProject();
            Close();
        }
        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            NewWindow window = new NewWindow();
            window.ShowDialog();
        }
        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void SaveAs_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }
        private void screen_GotFocus(object sender, RoutedEventArgs e)
        {
            gscreen.Active = true;
        }
        private void screen_LostFocus(object sender, RoutedEventArgs e)
        {
            gscreen.Active = false;
        }
        private void add_light_button_Click(object sender, RoutedEventArgs e)
        {
            NewLightWindow window = new NewLightWindow(Type.Light);
            if (window.ShowDialog() == true)
            {
            }
        }
    }
}
