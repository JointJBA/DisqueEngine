using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Math;

namespace Disque.Raytracer.Shapes
{
    public class Hyperboloid : Shape
    {
        float zmin, zmax, phiMax, rmax, a, c;
        Point p1, p2;
        public Hyperboloid(Transform o2w, Transform w2o, bool ro, Point point1, Point point2, float tm)
            : base(o2w, w2o, ro)
        {
            p1 = point1;
            p2 = point2;
            phiMax = MathHelper.ToRadians(MathHelper.Clamp(tm, 0.0f, 360.0f));
            float radius1 = MathHelper.Sqrt(p1.X * p1.X + p1.Y * p1.Y);
            float radius2 = MathHelper.Sqrt(p2.X * p2.X + p2.Y * p2.Y);
            rmax = MathHelper.Max(radius1, radius2);
            zmin = MathHelper.Min(p1.Z, p2.Z);
            zmax = MathHelper.Max(p1.Z, p2.Z);
            if (p2.Z == 0) MathHelper.Swap<Point>(ref p1, ref p2);
            Point pp = p1;
            float xy1, xy2;
            do
            {
                pp += 2.0f * (p2 - p1);
                xy1 = pp.X * pp.X + pp.Y * pp.Y;
                xy2 = p2.X * p2.X + p2.Y * p2.Y;
                a = (1.0f / xy1 - (pp.Z * pp.Z) / (xy1 * p2.Z * p2.Z)) /
                    (1 - (xy2 * pp.Z * pp.Z) / (xy1 * p2.Z * p2.Z));
                c = (a * xy2 - 1) / (p2.Z * p2.Z);
            } while (float.IsInfinity(a) || float.IsNaN(a));
        }
        public override BBox ObjectBound()
        {
            Point p1 = new Point(-rmax, -rmax, zmin);
            Point p2 = new Point(rmax, rmax, zmax);
            return new BBox(p1, p2);
        }
        public override bool Intersect(Ray r, Pointer<float> tHit, Pointer<float> rayEpsilon, Pointer<DifferentialGeometry> dg)
        {
            float phi, v;
            Point phit;
            Ray ray = (WorldToObject)[r];
            float A = a * ray.Direction.X * ray.Direction.X +
                      a * ray.Direction.Y * ray.Direction.Y -
                      c * ray.Direction.Z * ray.Direction.Z;
            float B = 2.0f * (a * ray.Direction.X * ray.Origin.X +
                             a * ray.Direction.Y * ray.Origin.Y -
                             c * ray.Direction.Z * ray.Origin.Z);
            float C = a * ray.Origin.X * ray.Origin.X +
                      a * ray.Origin.Y * ray.Origin.Y -
                      c * ray.Origin.Z * ray.Origin.Z - 1;
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
            v = (phit.Z - p1.Z) / (p2.Z - p1.Z);
            Point pr = (1.0f - v) * p1 + v * p2;
            phi = MathHelper.Atan2(pr.X * phit.Y - phit.X * pr.Y,
                phit.X * pr.X + phit.Y * pr.Y);
            if (phi < 0)
                phi += 2 * MathHelper.PI;
            if (phit.Z < zmin || phit.Z > zmax || phi > phiMax)
            {
                if (thit == t1) return false;
                thit = t1;
                if (t1 > ray.MaxT) return false;
                phit = ray[thit];
                v = (phit.Z - p1.Z) / (p2.Z - p1.Z);
                Point pr2 = (1.0f - v) * p1 + v * p2;
                phi = MathHelper.Atan2(pr2.X * phit.Y - phit.X * pr2.Y,
                    phit.X * pr2.X + phit.Y * pr2.Y);
                if (phi < 0)
                    phi += 2 * MathHelper.PI;
                if (phit.Z < zmin || phit.Z > zmax || phi > phiMax)
                    return false;
            }
            float u = phi / phiMax;
            float cosphi = MathHelper.Cos(phi), sinphi = MathHelper.Sin(phi);
            Vector dpdu = new Vector(-phiMax * phit.Y, phiMax * phit.X, 0);
            Vector dpdv = new Vector((p2.X - p1.X) * cosphi - (p2.Y - p1.Y) * sinphi,
                (p2.X - p1.X) * sinphi + (p2.Y - p1.Y) * cosphi,
                p2.Z - p1.Z);
            Vector d2Pduu = -phiMax * phiMax *
                            new Vector(phit.X, phit.Y, 0);
            Vector d2Pduv = phiMax *
                            new Vector(-dpdv.Y, dpdv.X, 0);
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
            dg[0] = new DifferentialGeometry(o2w[phit], o2w[dpdu], o2w[dpdv],
                                       o2w[dndu], o2w[dndv], u, v, this);
            tHit[0] = thit;
            rayEpsilon[0] = 5e-4f * tHit;
            return true;
        }
        public override bool IntersectP(Ray r)
        {
            float phi, v;
            Point phit;
            Ray ray = (WorldToObject)[r];
            float A = a * ray.Direction.X * ray.Direction.X +
                      a * ray.Direction.Y * ray.Direction.Y -
                      c * ray.Direction.Z * ray.Direction.Z;
            float B = 2.0f * (a * ray.Direction.X * ray.Origin.X +
                             a * ray.Direction.Y * ray.Origin.Y -
                             c * ray.Direction.Z * ray.Origin.Z);
            float C = a * ray.Origin.X * ray.Origin.X +
                      a * ray.Origin.Y * ray.Origin.Y -
                      c * ray.Origin.Z * ray.Origin.Z - 1;
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
            v = (phit.Z - p1.Z) / (p2.Z - p1.Z);
            Point pr = (1.0f - v) * p1 + v * p2;
            phi = MathHelper.Atan2(pr.X * phit.Y - phit.X * pr.Y,
                phit.X * pr.X + phit.Y * pr.Y);
            if (phi < 0)
                phi += 2 * MathHelper.PI;
            if (phit.Z < zmin || phit.Z > zmax || phi > phiMax)
            {
                if (thit == t1) return false;
                thit = t1;
                if (t1 > ray.MaxT) return false;
                phit = ray[thit];
                v = (phit.Z - p1.Z) / (p2.Z - p1.Z);
                Point pr2 = (1.0f - v) * p1 + v * p2;
                phi = MathHelper.Atan2(pr2.X * phit.Y - phit.X * pr2.Y,
                    phit.X * pr2.X + phit.Y * pr2.Y);
                if (phi < 0)
                    phi += 2 * MathHelper.PI;
                if (phit.Z < zmin || phit.Z > zmax || phi > phiMax)
                    return false;
            }
            return true;
        }
        public override float Area
        {
            get
            {
                return phiMax / 6.0f * (2.0f * QUAD(p1.X) - 2.0f * p1.X * p1.X * p1.X * p2.X + 2.0f * QUAD(p2.X) +
                       2.0f * (p1.Y * p1.Y + p1.Y * p2.Y + p2.Y * p2.Y) * (SQR(p1.Y - p2.Y) + SQR(p1.Z - p2.Z)) +
                       p2.X * p2.X * (5.0f * p1.Y * p1.Y + 2.0f * p1.Y * p2.Y - 4.0f * p2.Y * p2.Y + 2.0f * SQR(p1.Z - p2.Z)) +
                       p1.X * p1.X * (-4.0f * p1.Y * p1.Y + 2.0f * p1.Y * p2.Y + 5.0f * p2.Y * p2.Y + 2.0f * SQR(p1.Z - p2.Z)) -
                       2.0f * p1.X * p2.X * (p2.X * p2.X - p1.Y * p1.Y + 5.0f * p1.Y * p2.Y - p2.Y * p2.Y - p1.Z * p1.Z +
                       2.0f * p1.Z * p2.Z - p2.Z * p2.Z));
            }
        }
        static float SQR(float a)
        {
            return a * a;
        }
        static float QUAD(float a)
        {
            return SQR(a) * SQR(a);
        }
    }
}
