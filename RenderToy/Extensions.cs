using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Textures;
using Disque.Math;

namespace RenderToy
{
    public static class Extensions
    {
        public static string testFile = 
@"<World Tracer='Whitted'>
<World.ViewPlane Width='400' Height='400' Samples='1' Sampler='Regular' MaxDepth='1'/>
<World.AmbientLight>
<Ambient Color='1,1,1' Radiance='1' Shadows='True'/>
</World.AmbientLight>
<World.Camera>
<Pinhole Position='4,4,4' Target='0,0,0' Distance='5000' Zoom='1' RollAngle='0'/>
</World.Camera>
<Lights>
<PointLight Position='0,4,0' Color='1,1,1' Radiance='1'/>
</Lights>
<Objects>
<Sphere Position='0,0.5,0' Radius='0.5' Name='sphere1'>
<Sphere.Material>
<Matte AmbientCoeff='0.5' DiffuseCoeff='0.5' Color='1,0,0' Shadows='True'/>
</Sphere.Material>
</Sphere>
</Objects>
</World>";

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
    }
}
