using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Math;

namespace Disque.Raytracer.Shapes
{
    public class Disk : Shape
    {
        public Disk(Transform o2w, Transform w2o, bool ro, float ht, float r, float ri, float tmax)
            : base(o2w, w2o, ro)
        {
            Height = ht;
            InnerRadius = ri;
            Radius = r;
            PhiMax = MathHelper.ToRadians(MathHelper.Clamp(tmax, 0, 360f));
        }
        public override BBox ObjectBound()
        {
            return new BBox(new Point(-Radius, -Radius, Height), new Point(Radius, Radius, Height));
        }
        public override bool Intersect(Ray r, Pointer<float> tHit, Pointer<float> rayEpsilon, Pointer<DifferentialGeometry> dg)
        {
            Ray ray = (WorldToObject)[r];
            if (MathHelper.Abs(ray.Direction.Z) < 1e-7) return false;
            float thit = (Height - ray.Origin.Z) / ray.Direction.Z;
            if (thit < ray.MinT || thit > ray.MaxT)
                return false;
            Point phit = ray[thit];
            float dist2 = phit.X * phit.X + phit.Y * phit.Y;
            if (dist2 > Radius * Radius || dist2 < InnerRadius * InnerRadius)
                return false;
            float phi = MathHelper.Atan2(phit.Y, phit.X);
            if (phi < 0) phi += 2.0f * MathHelper.PI;
            if (phi > PhiMax)
                return false;
            float u = phi / PhiMax;
            float oneMinusV = ((MathHelper.Sqrt(dist2) - InnerRadius) /
                               (Radius - InnerRadius));
            float invOneMinusV = (oneMinusV > 0.0f) ? (1.0f / oneMinusV) : 0.0f;
            float v = 1.0f - oneMinusV;
            Vector dpdu = new Vector(-PhiMax * phit.Y, PhiMax * phit.X, 0);
            Vector dpdv = new Vector(-phit.X * invOneMinusV, -phit.Y * invOneMinusV, 0);
            dpdu *= PhiMax * MathHelper.InvTwoPI;
            dpdv *= (Radius - InnerRadius) / Radius;
            Normal dndu = new Normal(0, 0, 0), dndv = new Normal(0, 0, 0);
            Transform o2w = ObjectToWorld;
            dg[0] = new DifferentialGeometry(o2w[phit], o2w[dpdu], o2w[dpdv],
                                       o2w[dndu], o2w[dndv], u, v, this);
            tHit[0] = thit;
            rayEpsilon[0] = 5e-4f * ~tHit;
            return true;
        }
        public override bool IntersectP(Ray r)
        {
            Ray ray = (WorldToObject)[r];
            if (MathHelper.Abs(ray.Direction.Z) < 1e-7) return false;
            float thit = (Height - ray.Origin.Z) / ray.Direction.Z;
            if (thit < ray.MinT || thit > ray.MaxT)
                return false;
            Point phit = ray[thit];
            float dist2 = phit.X * phit.X + phit.Y * phit.Y;
            if (dist2 > Radius * Radius || dist2 < InnerRadius * InnerRadius)
                return false;
            float phi = MathHelper.Atan2(phit.Y, phit.X);
            if (phi < 0) phi += 2.0f * MathHelper.PI;
            if (phi > PhiMax)
                return false;
            return true;
        }
        public override float Area
        {
            get
            {
                return PhiMax * 0.5f * (Radius * Radius - InnerRadius * InnerRadius);
            }
        }
        public override Point Sample(float u1, float u2, Pointer<Normal> n)
        {
            Point p = new Point();
            float x = 0, y = 0;
            Extensions.ConcentricSampleDisk(u1, u2, ref x, ref y);
            p.X = x;
            p.Y = y;
            p.X *= Radius;
            p.Y *= Radius;
            p.Z = Height;
            n[0] = Normal.Normalize((ObjectToWorld)[new Normal(0, 0, 1)]);
            if (ReverseOrientation) n[0] *= -1.0f;
            return (ObjectToWorld)[p];
        }
        public float Radius { get; set; }
        public float InnerRadius { get; set; }
        public float Height { get; set; }
        public float PhiMax { get; set; }
    }
}
