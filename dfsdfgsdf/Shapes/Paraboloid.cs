using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Math;

namespace Disque.Raytracer.Shapes
{
    public class Paraboloid : Shape
    {
        public Paraboloid(Transform o2w, Transform w2o, bool ro, float rad, float z0, float z1, float tm)
            : base(o2w, w2o, ro)
        {
            Radius = rad;
            ZMin = z0;
            ZMax = z1;
            PhiMax = MathHelper.ToRadians(MathHelper.Clamp(tm, 0, 360));
        }
        public override BBox ObjectBound()
        {
            Point p1 = new Point(-Radius, -Radius, ZMin);
            Point p2 = new Point(Radius, Radius, ZMax);
            return new BBox(p1, p2);
        }
        public override bool Intersect(Ray r, Pointer<float> tHit, Pointer<float> rayEpsilon, Pointer<DifferentialGeometry> dg)
        {
            float phi;
            Point phit = new Point();
            Ray ray = (WorldToObject[r]);
            float k = ZMax / (Radius * Radius);
            float A = k * (ray.Direction.X * ray.Direction.X + ray.Direction.Y * r.Direction.Y);
            float B = 2.0f * k * (ray.Direction.X * ray.Origin.X + ray.Direction.Y * r.Origin.Y) - ray.Direction.Z;
            float C = k * (ray.Origin.X * ray.Origin.X + ray.Origin.Y * r.Origin.Y) - ray.Origin.Z;
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
                if (phi < 0.0f) phi += 2.0f * MathHelper.PI;
                if (phit.Z < ZMin || phit.Z > ZMax || phi > PhiMax)
                    return false;
            }
            float u = phi / PhiMax;
            float v = (phit.Z - ZMin) / (ZMax - ZMin);
            Vector dpdu = new Vector(-PhiMax * phit.Y, PhiMax * phit.X, 0.0f);
            Vector dpdv = (ZMax - ZMin) *
                new Vector(phit.X / (2.0f * phit.Z), phit.Y / (2.0f * phit.Z), 1);
            Vector d2Pduu = -PhiMax * PhiMax *
                            new Vector(phit.X, phit.Y, 0);
            Vector d2Pduv = (ZMax - ZMin) * PhiMax *
                            new Vector(-phit.Y / (2.0f * phit.Z),
                                   phit.X / (2.0f * phit.Z),
                                   0);
            Vector d2Pdvv = -(ZMax - ZMin) * (ZMax - ZMin) *
                            new Vector(phit.X / (4.0f * phit.Z * phit.Z),
                                   phit.Y / (4.0f * phit.Z * phit.Z),
                                   0.0f);
            float E = Vector.Dot(dpdu, dpdu);
            float F = Vector.Dot(dpdu, dpdv);
            float G = Vector.Dot(dpdv, dpdv);
            Vector N = Vector.Normalize(Vector.Cross(dpdu, dpdv));
            float e = Vector.Dot(N, d2Pduu);
            float f = Vector.Dot(N, d2Pduv);
            float g = Vector.Dot(N, d2Pdvv);
            float invEGF2 = 1.0f / (E * G - F * F);
            Normal dndu = new Normal(((f * F - e * G) * invEGF2 * dpdu +
                                 (e * F - f * E) * invEGF2 * dpdv));
            Normal dndv = new Normal(((g * F - f * G) * invEGF2 * dpdu +
                                 (f * F - g * E) * invEGF2 * dpdv));
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
            Point phit;
            Ray ray = (WorldToObject)[r];
            float k = ZMax / (Radius * Radius);
            float A = k * (ray.Direction.X * ray.Direction.X + ray.Direction.Y * ray.Direction.Y);
            float B = 2 * k * (ray.Direction.X * ray.Origin.X + ray.Direction.Y * ray.Origin.Y) - ray.Direction.Z;
            float C = k * (ray.Origin.X * ray.Origin.X + ray.Origin.Y * ray.Origin.Y) - ray.Origin.Z;
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
        public override float Area
        {
            get
            {
                return PhiMax / 12.0f * (MathHelper.Pow(1.0f + 4.0f * ZMin, 1.5f) - MathHelper.Pow(1.0f + 4.0f * ZMax, 1.5f));
            }
        }
        public float Radius { get; set; }
        public float ZMin { get; set; }
        public float ZMax { get; set; }
        public float PhiMax { get; set; }
    }
}
