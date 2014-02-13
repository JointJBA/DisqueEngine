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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Disque.Raytracer.Worlds;
using System.IO;

namespace DisqueRenderToy
{
    /// <summary>
    /// Interaction logic for ScreenImage.xaml
    /// </summary>
    public partial class ScreenImage : UserControl
    {
        public ScreenImage()
        {
            InitializeComponent();
        }
        public void RenderImage(World screen)
        {
            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(screen.ViewPlane.HRes, screen.ViewPlane.VRes);
            for (int i = 0; i < screen.Screen.Pixels.Count; i++)
            {
                Pixel p = screen.Screen.Pixels[i];
                Color r = Color.FromScRgb(1.0f, p.Color.R, p.Color.G, p.Color.B);
                bm.SetPixel((int)p.X, (int)p.Y, System.Drawing.Color.FromArgb(r.R, r.G, r.B));
            }
            string name = DateTime.Now.Millisecond.ToString() + ".png";
            while (File.Exists(name))
            {
                name = DateTime.Now.Millisecond.ToString() + ".png";
            }
            string file = @"C:\Users\Belal\Pictures\3D\" + name;
            bm.Save(file, System.Drawing.Imaging.ImageFormat.Png);
            mainimage.Source = new BitmapImage(new Uri(file));
            mainimage.Height = bm.Height;
            mainimage.Width = bm.Width;
        }
        public static string RenderImageFromScreen(PixelScreen screen, int height, int width, string folder, int framenumber)
        {
            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(width, height);
            for (int i = 0; i < screen.Pixels.Count; i++)
            {
                Pixel p = screen.Pixels[i];
                Color r = Color.FromScRgb(1.0f, p.Color.R, p.Color.G, p.Color.B);
                bm.SetPixel((int)p.X, (int)p.Y, System.Drawing.Color.FromArgb(r.R, r.G, r.B));
            }
            if (!Directory.Exists(@"C:\Users\Belal\Pictures\Movies\" + folder))
            {
                Directory.CreateDirectory(@"C:\Users\Belal\Pictures\Movies\" + folder);
            }
            string file = @"C:\Users\Belal\Pictures\Movies\" + folder + @"\" + framenumber + ".png";
            bm.Save(file, System.Drawing.Imaging.ImageFormat.Png);
            return file;
        }
    }
}
