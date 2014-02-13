using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Textures;
using Disque.Math;

namespace DisqueRenderToy
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
        public static Image CreateFromBitmap(string uri)
        {
            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(uri);
            return CreateFromBitmap(bm);
        }
        public static byte[] GetHeightMap(System.Drawing.Bitmap bm)
        {
            byte[] result = new byte[bm.Height * bm.Width];
            for (int x = 0; x < bm.Width; x++)
            {
                for (int y = 0; y < bm.Height; y++)
                {
                    result[x + y * bm.Width] = bm.GetPixel(x, y).R;
                }
            }
            return result;
        }
    }
}
