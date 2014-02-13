using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Textures;
using Disque.Math;
using System.IO;

namespace WebRenderToy
{
    public static class Extensions
    {
        public static Image CreateFromBitmap(System.Drawing.Bitmap bm)
        {
            Image imm = new Image();
            imm.HRes = bm.Width;
            imm.VRes = bm.Height;
            imm.Pixels = new Vector3[imm.VRes * imm.HRes];
            for (int x = 0; x < imm.HRes; x++)
            {
                for (int y = 0; y < imm.VRes; y++)
                {
                    var c = bm.GetPixel(x, y);
                    imm.Pixels[x + y * imm.HRes] = new Vector3((((float)c.R) / 255.0f), (((float)c.G) / 255.0f), (((float)c.B) / 255.0f));
                }
            }
            return imm;
        }

        public static MemoryStream CreateFromImage(Image im)
        {
            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(im.HRes, im.VRes);
            for (int x = 0; x < im.HRes; x++)
            {
                for (int y = 0; y < im.VRes; y++)
                {
                    Vector3 v = im.Pixels[x + y * im.HRes];
                    bm.SetPixel(x, y, System.Drawing.Color.FromArgb((int)(v.X * 255.0f), (int)(v.Y * 255.0f), (int)(v.Z * 255.0f)));
                }
            }
            MemoryStream ms = new MemoryStream();
            bm.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms;
        }
    }
}
