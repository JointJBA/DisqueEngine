using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Math;

namespace Disque.Raytracer.Shapes
{
    public class Cone : Shape
    {
        public Cone(Transform o2w, Transform w2o, bool ro, float height, float rad, float tm)
            : base(o2w, w2o, ro)
        {
            Radius = rad;
            Height = height;
            PhiMax = MathHelper.ToRadians(MathHelper.Clamp(tm, 0, 360.0f));
        }
        public override BBox ObjectBound()
        {
            Point p1 = new Point(-Radius, -Radius, 0);
            Point p2 = new Point(Radius, Radius, Height);
            return new BBox(p1, p2);
        }
        public override bool Intersect(Ray r, Pointer<float> tHit, Pointer<float> rayEpsilon, Pointer<DifferentialGeometry> dg)
        {
            float phi;
            Point phit = new Point();
            Ray ray = (WorldToObject[r]);
            float k = Radius / Height;
            k = k * k;
            float A = ray.Direction.X * ray.Direction.X + ray.Direction.Y * ray.Direction.Y -
                k * ray.Direction.Z * ray.Direction.Z;
            float B = 2 * (ray.Direction.X * ray.Origin.X + ray.Direction.Y * ray.Origin.Y -
                k * ray.Direction.Z * (ray.Origin.Z - Height));
            float C = ray.Origin.X * ray.Origin.X + ray.Origin.Y * ray.Origin.Y -
                k * (ray.Origin.Z - Height) * (ray.Origin.Z - Height);
            float t0 = 0, t1 = 0;
            if (!MathHelper.Quadratic(A, B, C, ref t0, ref t1))
                return false;
            if (t0 > ray.MaxT || t1 < ray.MinT)
                return false;
            float thit = t0;
            if (t0 < ray.MinT)
            {
                thit = t1;
                if (thit > ray.MaxT) return false;
            }
            phit = ray[thit];
            phi = MathHelper.Atan2(phit.Y, phit.X);
            if (phi < 0) phi += 2.0f * MathHelper.PI;
            if (phit.Z < 0 || phit.Z > Height || phi > PhiMax)
            {
                if (thit == t1) return false;
                thit = t1;
                if (t1 > ray.MaxT) return false;
                phit = ray[thit];
                phi = MathHelper.Atan2(phit.Y, phit.X);
                if (phi < 0) phi += 2.0f * MathHelper.PI;
                if (phit.Z < 0 || phit.Z > Height || phi > PhiMax)
                    return false;
            }
            float u = phi / PhiMax;
            float v = phit.Z / Height;
            Vector dpdu = new Vector(-PhiMax * phit.Y, PhiMax * phit.X, 0);
            Vector dpdv = new Vector(-phit.X / (1 - v),
                        -phit.Y / (1 - v), Height);
            Vector d2Pduu = -PhiMax * PhiMax *
                            new Vector(phit.X, phit.Y, 0);
            Vector d2Pduv = PhiMax / (1.0f - v) *
                            new Vector(-phit.Y, -phit.X, 0);
            Vector d2Pdvv = new Vector(0, 0, 0);
            float E = Vector.Dot(dpdu, dpdu);
            float F = Vector.Dot(dpdu, dpdv);
            float G = Vector.Dot(dpdv, dpdv);
            Vector N = Vector.Normalize(Vector.Cross(dpdu, dpdv));
            float e = Vector.Dot(N, d2Pduu);
            float f = Vector.Dot(N, d2Pduv);
            float g = Vector.Dot(N, d2Pdvv);
            float invEGF2 = 1.0f / (E * G - F * F);
            Normal dndu = new Normal((f * F - e * G) * invEGF2 * dpdu +
                                 (e * F - f * E) * invEGF2 * dpdv);
            Normal dndv = new Normal((g * F - f * G) * invEGF2 * dpdu +
                                 (f * F - g * E) * invEGF2 * dpdv);
            Transform o2w = ObjectToWorld;
            dg[0] = new DifferentialGeometry(o2w[phit], o2w[dpdu], o2w[dpdv], o2w[dndu], o2w[dndv], u, v, this);
            tHit[0] = thit;
            rayEpsilon[0] = 5e-4f * ~tHit;
            return true;
        }
        public override bool IntersectP(Ray r)
        {
            float phi;
            Point phit = new Point();
            Ray ray = (WorldToObject)[r];
            float k = Radius / Height;
            k = k * k;
            float A = ray.Direction.X * ray.Direction.X + ray.Direction.Y * ray.Direction.Y -
                k * ray.Direction.Z * ray.Direction.Z;
            float B = 2 * (ray.Direction.X * ray.Origin.X + ray.Direction.Y * ray.Origin.Y -
                k * ray.Direction.Z * (ray.Origin.Z - Height));
            float C = ray.Origin.X * ray.Origin.X + ray.Origin.Y * ray.Origin.Y -
                k * (ray.Origin.Z - Height) * (ray.Origin.Z - Height);
            float t0 = 0, t1 = 0;
            if (!MathHelper.Quadratic(A, B, C, ref t0, ref t1))
                return false;
            if (t0 > ray.MaxT || t1 < ray.MinT)
                return false;
            float thit = t0;
            if (t0 < ray.MinT)
            {
                thit = t1;
                if (thit > ray.MaxT) return false;
            }
            phit = ray[thit];
            phi = MathHelper.Atan2(phit.Y, phit.X);
            if (phi < 0) phi += 2.0f * MathHelper.PI;
            if (phit.Z < 0 || phit.Z > Height || phi > PhiMax)
            {
                if (thit == t1) return false;
                thit = t1;
                if (t1 > ray.MaxT) return false;
                phit = ray[thit];
                phi = MathHelper.Atan2(phit.Y, phit.X);
                if (phi < 0) phi += 2.0f * MathHelper.PI;
                if (phit.Z < 0 || phit.Z > Height || phi > PhiMax)
                    return false;
            }
            return true;
        }
        public override float Area
        {
            get
            {
                return PhiMax * Height * Height * MathHelper.Sqrt((Height * Height) + (Radius * Radius)) / (2.0f * Radius);
            }
        }
        public float Radius { get; set; }
        public float Height { get; set; }
        public float PhiMax { get; set; }
    }
}
