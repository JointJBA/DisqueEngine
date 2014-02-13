using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Math;

namespace Disque.Raytracer.Shapes
{
    public class Cylinder : Shape
    {
        public Cylinder(Transform o2w, Transform w2o, bool ro, float rad, float z0, float z1, float pm)
            : base(o2w, w2o, ro)
        {
            Radius = rad;
            ZMin = MathHelper.Min(z0, z1);
            ZMax = MathHelper.Max(z0, z1);
            PhiMax = MathHelper.ToRadians(MathHelper.Clamp(pm, 0.0f, 360.0f));
        }
        public override bool Intersect(Ray r, Pointer<float> tHit, Pointer<float> rayEpsilon, Pointer<DifferentialGeometry> dg)
        {
            float phi;
            Point phit;
            Ray ray = (WorldToObject)[r];
            float A = ray.Direction.X * ray.Direction.X + ray.Direction.Y * ray.Direction.Y;
            float B = 2 * (ray.Direction.X * ray.Origin.X + ray.Direction.Y * ray.Origin.Y);
            float C = ray.Origin.X * ray.Origin.X + ray.Origin.Y * ray.Origin.Y - Radius * Radius;
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
            if (phit.Z < ZMin || phit.Z > ZMax || phi > PhiMax)
            {
                if (thit == t1) return false;
                thit = t1;
                if (t1 > ray.MaxT) return false;
                phit = ray[thit];
                phi = MathHelper.Atan2(phit.Y, phit.X);
                if (phi < 0) phi += 2.0f * MathHelper.PI;
                if (phit.Z < ZMin || phit.Z > ZMax || phi > PhiMax)
                    return false;
            }
            float u = phi / PhiMax;
            float v = (phit.Z - ZMin) / (ZMax - ZMin);
            Vector dpdu = new Vector(-PhiMax * phit.Y, PhiMax * phit.X, 0);
            Vector dpdv = new Vector(0, 0, ZMax - ZMin);
            Vector d2Pduu = -PhiMax * PhiMax * new Vector(phit.X, phit.Y, 0);
            Vector d2Pduv = new Vector(0, 0, 0), d2Pdvv = new Vector(0, 0, 0);
            float E = Vector.Dot(dpdu, dpdu);
            float F = Vector.Dot(dpdu, dpdv);
            float G = Vector.Dot(dpdv, dpdv);
            Vector N = Vector.Normalize(Vector.Cross(dpdu, dpdv));
            float e = Vector.Dot(N, d2Pduu);
            float f = Vector.Dot(N, d2Pduv);
            float g = Vector.Dot(N, d2Pdvv);
            float invEGF2 = 1.0f / (E * G - F * F);
            Normal dndu = new Normal((f * F - e * G) * invEGF2 * dpdu + (e * F - f * E) * invEGF2 * dpdv);
            Normal dndv = new Normal((g * F - f * G) * invEGF2 * dpdu + (f * F - g * E) * invEGF2 * dpdv);
            Transform o2w = ObjectToWorld;
            dg[0] = new DifferentialGeometry(o2w[phit], o2w[dpdu], o2w[dpdv],
                                       o2w[dndu], o2w[dndv], u, v, this);
            tHit[0] = thit;
            rayEpsilon[0] = 5e-4f * ~tHit;
            return true;
        }
        public override bool IntersectP(Ray r)
        {
            float phi;
            Point phit = new Point();
            Ray ray = (WorldToObject)[r];
            float A = ray.Direction.X * ray.Direction.X + ray.Direction.Y * ray.Direction.Y;
            float B = 2 * (ray.Direction.X * ray.Origin.X + ray.Direction.Y * ray.Origin.Y);
            float C = ray.Origin.X * ray.Origin.X + ray.Origin.Y * ray.Origin.Y - Radius * Radius;
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
            if (phit.Z < ZMin || phit.Z > ZMax || phi > PhiMax)
            {
                if (thit == t1) return false;
                thit = t1;
                if (t1 > ray.MaxT) return false;
                phit = ray[thit];
                phi = MathHelper.Atan2(phit.Y, phit.X);
                if (phi < 0) phi += 2.0f * MathHelper.PI;
                if (phit.Z < ZMin || phit.Z > ZMax || phi > PhiMax)
                    return false;
            }
            return true;
        }
        public override BBox ObjectBound()
        {
            Point p1 = new Point(-Radius, -Radius, ZMin);
            Point p2 = new Point(Radius, Radius, ZMax);
            return new BBox(p1, p2);
        }
        public override Point Sample(float u1, float u2, Pointer<Normal> n)
        {
            float z = MathHelper.Lerp(u1, ZMin, ZMax);
            float t = u2 * PhiMax;
            Point p = new Point(Radius * MathHelper.Cos(t), Radius * MathHelper.Sin(t), z);
            n[0] = Normal.Normalize((ObjectToWorld)[new Normal(p.X, p.Y, 0.0f)]);
            if (ReverseOrientation) n[0] *= -1.0f;
            return (ObjectToWorld)[p];
        }
        public float Radius { get; set; }
        public float ZMin { get; set; }
        public float ZMax { get; set; }
        public float PhiMax { get; set; }
        public override float Area
        {
            get
            {
                return (ZMax - ZMin) * PhiMax * Radius;
            }
        }
    }
}
