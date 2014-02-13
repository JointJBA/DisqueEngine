using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Math;

namespace Disque.Raytracer
{
    public static class Extensions
    {
        public static void CoordinateSystem(Vector v1, out Vector v2, out Vector v3)
        {
            if (MathHelper.Abs(v1.X) > MathHelper.Abs(v1.Y))
            {
                float invLen = 1.0f / MathHelper.Sqrt(v1.X * v1.X + v1.Z * v1.Z);
                v2 = new Vector(-v1.Z * invLen, 0.0f, v1.X * invLen);
            }
            else
            {
                float invLen = 1.0f / MathHelper.Sqrt(v1.Y * v1.Y + v1.Z * v1.Z);
                v2 = new Vector(0.0f, v1.Z * invLen, -v1.Y * invLen);
            }
            v3 = Vector.Cross(v1, v2);
        }
        public static Vector UniformSampleCone(float u1, float u2, float costhetamax, Vector x, Vector y, Vector z)
        {
            float costheta = MathHelper.Lerp(u1, costhetamax, 1.0f);
            float sintheta = MathHelper.Sqrt(1.0f - costheta * costheta);
            float phi = u2 * 2.0f * MathHelper.PI;
            return MathHelper.Cos(phi) * sintheta * x + MathHelper.Sin(phi) * sintheta * y + costheta * z;
        }
        public static void ConcentricSampleDisk(float u1, float u2, ref float dx, ref float dy)
        {
            float r, theta;
            float sx = 2 * u1 - 1;
            float sy = 2 * u2 - 1;
            if (sx == 0.0 && sy == 0.0)
            {
                dx = 0.0f;
                dy = 0.0f;
                return;
            }
            if (sx >= -sy)
            {
                if (sx > sy)
                {
                    r = sx;
                    if (sy > 0.0) theta = sy / r;
                    else theta = 8.0f + sy / r;
                }
                else
                {
                    r = sy;
                    theta = 2.0f - sx / r;
                }
            }
            else
            {
                if (sx <= sy)
                {
                    r = -sx;
                    theta = 4.0f - sy / r;
                }
                else
                {
                    r = -sy;
                    theta = 6.0f + sx / r;
                }
            }
            theta *= MathHelper.PI / 4.0f;
            dx = r * MathHelper.Cos(theta);
            dy = r * MathHelper.Sin(theta);
        }
        public static void UniformSampleTriangle(float u1, float u2, ref float u, ref float v)
        {
            float su1 = MathHelper.Sqrt(u1);
            u = 1.0f - su1;
            v = u2 * su1;
        }
    }
}
