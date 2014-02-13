using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using SRenderToy.RenderService;
using System.Windows.Media.Imaging;
using System.Windows.Browser;
using System.Windows.Threading;

namespace SRenderToy
{
    public partial class MainPage : UserControl
    {            
        DispatcherTimer dt = new DispatcherTimer();

        #region rml
        string ffile = @"<World Directory=""None"" Tracer=""Whitted"">
<ViewPlane Height=""200"" Width=""800"" Samples=""1"" PixelSize=""1"" MaxDepth=""5"" Sampler=""Regular""/>
<EnviromentalLight>
<Ambient Color=""1,1,1"" Radiance=""0"" Shadows=""True""/>
</EnviromentalLight>
<Camera>
<Pinhole Position=""40,10,0"" Target=""0.1,0.5,0"" Zoom=""1.0"" Distance=""5000"" ExposureTime=""1"" RollAngle=""0""/>
</Camera>
<Lights>
<PointLight Color=""1,1,1"" Position=""4,4,0"" Shadows=""True"" Radiance=""7""/>
</Lights>
<Objects>
<Sphere Position=""0,0.5,0"" Radius=""0.5"" Name=""sphere1"" Shadows=""True"">
<Reflective SpecularColor=""1,0.1,0.1"" SpecularCoff=""0.2"" ReflectiveColor=""1,0.1,0.1"" ReflectiveCoff=""0.8"" Exponent=""20"" AmbientRelfectionCoff=""0.8"" DiffuseRelfectionCoff=""0.2"" Color=""1,0.1,0.1"" Shadows=""True""/>
</Sphere>
<Sphere Position=""0,0.5,-1.2"" Name=""Disk1"" Radius=""0.5"" Shadows=""True"">
<Matte Color=""0.1,1,0.1"" ReflectionCoff=""0.5"" DiffuseCoff=""0.5"" Shadows=""True""/>
</Sphere>
<Box Min=""-0.5,0,1"" Max=""0.5,1,2"" Name=""Box1"" Shadows=""False"">
<Transparent SpecularColor=""0.1,0.1,1"" SpecularCoff=""0.2"" ReflectiveColor=""0.1,0.1,1"" ReflectiveCoff=""0.1"" Exponent=""2000"" AmbientRelfectionCoff=""0.3"" DiffuseRelfectionCoff=""0.3"" Color=""0.1,0.1,1"" IOR=""1.3"" TransCoff=""1.0"" Shadows=""True""/>
</Box>
<Plane Type=""Plane"" Position=""0,0,0"" Direction=""0,1,0"" Name=""MainPlane"" Shadows=""True"">
<Matte ReflectionCoff=""0.5"" DiffuseCoff=""0.5"" Color=""1,1,1"" Shadows=""True""/>
</Plane>
</Objects>
</World>";
        #endregion

        string directory = "", uri = "", imgurl = "";

        public List<string> names = new List<string>();

        public MainPage()
        {
            InitializeComponent();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += new EventHandler(dt_Tick);
            xmlEditor.Text = ffile;
        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Result == "c")
            {
                status.Text = "Render completed.";
                renderedImage.Source = new BitmapImage(new Uri(imgurl));
                link.NavigateUri = new Uri(imgurl);
                enableUsables();
                dt.Stop();
            }
            else if (e.Result == "r")
            {                
                status.Text = "Rendering...";
            }
            else if (e.Result == "")
            {
            }
            else if (e.Result.Contains("Progress"))
            {
                string res = e.Result.Replace("Progress: ", "");
                progressText.Text = res;
                string[] d = res.Split(new char[] { '/' });
                renderProgress.Value = ((float.Parse(d[0]) / float.Parse(d[1])) * 100);
            }
            else
            {
                enableUsables();
                MessageBox.Show(e.Result);
            }
        }

        void dt_Tick(object sender, EventArgs e)
        {
            link.NavigateUri = new Uri(imgurl);
            WebClient wc = new WebClient();
            wc.DownloadStringAsync(new Uri(uri + "ImageRend.ashx?world=" + directory + "&cmnd=status"));
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            WebClient wc2 = new WebClient();
            wc2.DownloadStringAsync(new Uri(uri + "ImageRend.ashx?world=" + directory + "&cmnd=progress"));
            wc2.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
        }

        private void renderButton_Click(object sender, RoutedEventArgs e)
        {
            RenderServiceSoapClient rssc = new RenderServiceSoapClient();
            rssc.RenderCompleted += new EventHandler<RenderCompletedEventArgs>(rssc_RenderCompleted);
            rssc.RenderAsync(xmlEditor.Text);
        }

        void rssc_RenderCompleted(object sender, RenderCompletedEventArgs e)
        {
            uri = HtmlPage.Document.DocumentUri.OriginalString.Replace(HtmlPage.Document.DocumentUri.AbsolutePath, "") + "/";
            if (!e.Result.Contains("Error"))
            {
                directory = e.Result;
                imgurl = uri + "ImageRend.ashx?image=" + directory;
                WebClient wc = new WebClient();
                wc.DownloadStringAsync(new Uri(uri + "ImageRend.ashx?world=" + e.Result + "&cmnd=render"));
                dt.Start();
                disableUsables();
            }
            else
            {
                MessageBox.Show(e.Result);
            }
        }

        private void addimage_button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddImage ai = new AddImage(this);
                ai.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void xmlEditor_ContentChanged(object sender, ContentChangedEventArgs e)
        {
            renderProgress.Value = 0;
            status.Text = "";
        }

        private void imageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int ri = 0; ri < e.AddedItems.Count; ri++)
            {
                ((ImageItem)e.AddedItems[ri]).Select();
            }
            for (int ri = 0; ri < e.RemovedItems.Count; ri++)
            {
                ((ImageItem)e.RemovedItems[ri]).UnSelect();
            }
        }

        void enableUsables()
        {
            renderButton.IsEnabled = true;
            xmlEditor.IsReadOnly = false;
        }

        void disableUsables()
        {
            renderButton.IsEnabled = false;
            xmlEditor.IsReadOnly = true;
        }

        public void updateImageList()
        {
            imageList.Items.Clear();
            foreach (string s in names)
            {
                ImageItem ii = new ImageItem();
                ii.SetImage(s);
                imageList.Items.Add(ii);
            }
        }
    }
}
