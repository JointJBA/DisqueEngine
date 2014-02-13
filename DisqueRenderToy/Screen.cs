using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Disque.Raytracer.Worlds;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DisqueRenderToy
{
    public class Screen : FrameworkElement
    {
        VisualCollection collection;
        public Screen()
        {
            collection = new VisualCollection(this);
        }
        public void Render(PixelScreen screen)
        {
            if (screen != null)
            {
                collection.Clear();
                DrawingVisual dv = new DrawingVisual();
                DrawingContext dc = dv.RenderOpen();
                for (int i = 0; i < screen.Pixels.Count; i++)
                {
                    Pixel p = screen.Pixels[i];
                    Color r = Color.FromScRgb(1.0f, p.Color.R, p.Color.G, p.Color.B);
                    dc.DrawRectangle(new SolidColorBrush(r), null, new Rect(p.X, p.Y, 1, 1));
                }
                dc.Close();
                collection.Add(dv);
            }
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
                name = DateTime.Now.Millisecond.ToString() +".png";
            }
            string file = @"C:\Users\Belal\Pictures\3D\" + name;
            bm.Save(file, System.Drawing.Imaging.ImageFormat.Png);
            collection.Clear();
            collection.Add(new Image() { Source = new BitmapImage(new Uri(file)), Height = bm.Height, Width = bm.Width });
        }
        protected override Visual GetVisualChild(int index)
        {
            return collection[index];
        }
        protected override int VisualChildrenCount
        {
            get
            {
                return collection.Count;
            }
        }
    }
}
