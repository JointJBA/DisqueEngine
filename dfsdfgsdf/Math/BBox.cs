using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Raytracer.Math
{
    public struct BBox
    {
        Point max, min;
        public Point Max { get { return max; } set { max = value; } }
        public Point Min { get { return min; } set { min = value; } }
        public float SurfaceArea
        {
            get
            {
                Vector d = Max - Min;
                return (2.0f * (d.X * d.Y + d.X * d.Z + d.Y * d.Z));
            }
        }
        public float Volume
        {
            get
            {
                Vector d = Max - Min;
                return d.X * d.Y * d.Z;
            }
        }
        public int MaximumExtent
        {
            get
            {
                Vector diag = Max - Min;
                if (diag.X > diag.Y && diag.X > diag.Z)
                    return 0;
                else if (diag.Y > diag.Z)
                    return 1;
                else
                    return 2;
            }
        }
        public BBox(Point p)
        {
            max = min = p;
        }
        public BBox(Point p1, Point p2)
        {
            min = new Point(MathHelper.Min(p1.X, p2.X), MathHelper.Min(p1.Y, p2.Y), MathHelper.Min(p1.Z, p2.Z));
            max = new Point(MathHelper.Max(p1.X, p2.X), MathHelper.Max(p1.Y, p2.Y), MathHelper.Max(p1.Z, p2.Z));
        }
        public static BBox Union(BBox box, Point point)
        {
            BBox ret = box;
            var min = new Point();
            min.X = MathHelper.Min(box.Min.X, point.X);
            min.Y = MathHelper.Min(box.Min.Y, point.Y);
            min.Z = MathHelper.Min(box.Min.Z, point.Z);
            ret.Min = min;
            var max = new Point();
            max.X = MathHelper.Max(box.Max.X, point.X);
            max.Y = MathHelper.Max(box.Max.Y, point.Y);
            max.Z = MathHelper.Max(box.Max.Z, point.Z);
            ret.Max = max;
            return ret;
        }
        public void Union(BBox b2)
        {
            var min = new Point();
            min.X = MathHelper.Min(Min.X, b2.Min.X);
            min.Y = MathHelper.Min(Min.Y, b2.Min.Y);
            min.Z = MathHelper.Min(Min.Z, b2.Min.Z);
            Min = min;
            var max = new Point();
            max.X = MathHelper.Max(Max.X, b2.Max.X);
            max.Y = MathHelper.Max(Max.Y, b2.Max.Y);
            max.Z = MathHelper.Max(Max.Z, b2.Max.Z);
            Max = max;
        }
        public static BBox Union(BBox b1, BBox b2)
        {
            b1.Union(b2);
            return b1;
        }
        public void Union(Point point)
        {
            BBox b = BBox.Union(this, point);
            Max = b.Max;
            Min = b.Min;
        }
        public bool Overlaps(BBox b)
        {
            bool x = (Max.X >= b.Min.X) && (Min.X <= b.Max.X);
            bool y = (Max.Y >= b.Min.Y) && (Min.Y <= b.Max.Y);
            bool z = (Max.Z >= b.Min.Z) && (Min.Z <= b.Max.Z);
            return (x && y && z);
        }
        public bool Inside(Point pt)
        {
            return (
                pt.X >= Min.X && pt.X <= Max.X &&
                pt.Y >= Min.Y && pt.Y <= Max.Y && 
                pt.Z >= Min.Z && pt.Z <= Max.Z);
        }
        public void Expand(float pad)
        {
            Min -= new Vector(pad);
            Min += new Vector(pad);
        }
        public Point Lerp(float tx, float ty, float tz)
        {
            return new Point(MathHelper.Lerp(tx, Min.X, Max.X), MathHelper.Lerp(ty, Min.Y, Max.Y), MathHelper.Lerp(tz, Min.Z, Max.Z));
        }
        public Vector Offset(Point p)
        {
            return new Vector((p.X - Min.X) / (Max.X - Min.X), (p.Y - Min.Y) / (Max.Y - Min.Y), (p.Z - Min.Z) / (Max.Z - Min.Z));
        }
        public void BoundingSphere(out Point center, out float radius)
        {
            center = new Point();
            center = 0.5f * Min + 0.5f * Max;
            radius = Inside(center) ? Point.Distance(center, Max) : 0.0f;
        }
        public bool IntersectRay(Ray ray, ref float hitt0, ref float hitt1)
        {
            float t0 = ray.MinT, t1 = ray.MaxT;
            for (int i = 0; i < 3; ++i)
            {
                float invRayDir = 1.0f / ray.Direction[i];
                float tNear = (Min[i] - ray.Origin[i]) * invRayDir;
                float tFar = (Max[i] - ray.Origin[i]) * invRayDir;

                if (tNear > tFar) MathHelper.Swap<float>(ref tNear,ref tFar);
                t0 = tNear > t0 ? tNear : t0;
                t1 = tFar < t1 ? tFar : t1;
                if (t0 > t1) return false;
            }
            hitt0 = t0;
            hitt1 = t1;
            return true;
        }
        public static bool operator ==(BBox a, BBox b)
        {
            return a.Max == b.Max && a.Min == b.Min;
        }
        public static bool operator !=(BBox a, BBox b)
        {
            return !(a == b);
        }
    }
}
