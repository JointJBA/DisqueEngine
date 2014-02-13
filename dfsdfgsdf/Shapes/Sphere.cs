using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Rml;
using Disque.Raytracer.Math;

namespace Disque.Raytracer.Shapes
{
    public class Sphere : Shape
    {
        float zmin = 0, zmax = 0, phiMax = 0, thetaMax = 0, thetaMin = 0;
        public Sphere(Transform o2w, Transform w2o, bool ro, float rad, float z0, float z1, float pm)
            : base(o2w, w2o, ro)
        {
            Radius = rad;
            zmin = MathHelper.Clamp(MathHelper.Min(z0, z1), -Radius, Radius);
            zmax = MathHelper.Clamp(MathHelper.Max(z0, z1), -Radius, Radius);
            thetaMin = MathHelper.Acos(MathHelper.Clamp(zmin / Radius, -1.0f, 1.0f));
            thetaMax = MathHelper.Acos(MathHelper.Clamp(zmax / Radius, -1.0f, 1.0f));
            phiMax = MathHelper.ToRadians(MathHelper.Clamp(pm, 0.0f, 360.0f));
        }
        public override BBox ObjectBound()
        {
            return new BBox(new Point(-Radius, -Radius, zmin), new Point(Radius, Radius, zmax));
        }
        public override bool Intersect(Ray r, Pointer<float> tHit, Pointer<float> rayEpsilon, Pointer<DifferentialGeometry> dg)
        {
            float phi;
            Point phit = new Point();
            Ray ray = (WorldToObject)[r];
            float A = ray.Direction.X * ray.Direction.X + ray.Direction.Y * ray.Direction.Y + ray.Direction.Z * ray.Direction.Z;
            float B = 2 * (ray.Direction.X * ray.Origin.X + ray.Direction.Y * ray.Origin.Y + ray.Direction.Z * ray.Origin.Z);
            float C = ray.Origin.X * ray.Origin.X + ray.Origin.Y * ray.Origin.Y +
                      ray.Origin.Z * ray.Origin.Z - Radius * Radius;
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
            if (phit.X == 0.0f && phit.Y == 0.0f) phit.X = 1e-5f * Radius;
            phi = MathHelper.Atan2(phit.Y, phit.X);
            if (phi < 0.0f) phi += 2.0f * MathHelper.PI;
            if ((zmin > -Radius && phit.Z < zmin) ||
                (zmax < Radius && phit.Z > zmax) || phi > phiMax)
            {
                if (thit == t1) return false;
                if (t1 > ray.MaxT) return false;
                thit = t1;
                phit = ray[thit];
                if (phit.X == 0.0f && phit.Y == 0.0f) phit.X = 1e-5f * Radius;
                phi = MathHelper.Atan2(phit.Y, phit.X);
                if (phi < 0.0f) phi += 2.0f * MathHelper.PI;
                if ((zmin > -Radius && phit.Z < zmin) ||
                    (zmax < Radius && phit.Z > zmax) || phi > phiMax)
                    return false;
            }
            float u = phi / phiMax;
            float theta = MathHelper.Acos(MathHelper.Clamp(phit.Z / Radius, -1.0f, 1.0f));
            float v = (theta - thetaMin) / (thetaMax - thetaMin);
            float zradius = MathHelper.Sqrt(phit.X * phit.X + phit.Y * phit.Y);
            float invzradius = 1.0f / zradius;
            float cosphi = phit.X * invzradius;
            float sinphi = phit.Y * invzradius;
            Vector dpdu = new Vector(-phiMax * phit.Y, phiMax * phit.X, 0);
            Vector dpdv = (thetaMax - thetaMin) *
                new Vector(phit.Z * cosphi, phit.Z * sinphi,
                       -Radius * MathHelper.Sin(theta));
            Vector d2Pduu = -phiMax * phiMax * new Vector(phit.X, phit.Y, 0);
            Vector d2Pduv = (thetaMax - thetaMin) * phit.Z * phiMax *
                            new Vector(-sinphi, cosphi, 0.0f);
            Vector d2Pdvv = -(thetaMax - thetaMin) * (thetaMax - thetaMin) *
                            new Vector(phit.X, phit.Y, phit.Z);
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
            dg[0] = new DifferentialGeometry(o2w[phit], o2w[dpdu], o2w[dpdv], o2w[dndu], o2w[dndv], u, v, this);
            tHit[0] = thit;
            rayEpsilon[0] = 5e-4f * ~tHit;
            return true;
        }
        public override bool IntersectP(Ray r)
        {
            float phi;
            Point phit = new Point();
            Ray ray = WorldToObject[r];
            float A = ray.Direction.X * ray.Direction.X + ray.Direction.Y * ray.Direction.Y + ray.Direction.Z * ray.Direction.Z;
            float B = 2 * (ray.Direction.X * ray.Origin.X + ray.Direction.Y * ray.Origin.Y + ray.Direction.Z * ray.Origin.Z);
            float C = ray.Origin.X * ray.Origin.X + ray.Origin.Y * ray.Origin.Y +
                      ray.Origin.Z * ray.Origin.Z - Radius * Radius;
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
            if (phit.X == 0.0f && phit.Y == 0.0f) phit.X = 1e-5f * Radius;
            phi = MathHelper.Atan2(phit.Y, phit.X);
            if (phi < 0.0f) phi += 2.0f * MathHelper.PI;
            if ((zmin > -Radius && phit.Z < zmin) ||
                (zmax < Radius && phit.Z > zmax) || phi > phiMax)
            {
                if (thit == t1) return false;
                if (t1 > ray.MaxT) return false;
                thit = t1;
                phit = ray[thit];
                if (phit.X == 0.0f && phit.Y == 0.0f) phit.X = 1e-5f * Radius;
                phi = MathHelper.Atan2(phit.Y, phit.X);
                if (phi < 0.0f) phi += 2.0f * MathHelper.PI;
                if ((zmin > -Radius && phit.Z < zmin) ||
                    (zmax < Radius && phit.Z > zmax) || phi > phiMax)
                    return false;
            }
            return true;
        }
        public override float Area
        {
            get
            {
                return phiMax * Radius * (zmax - zmin);
            }
        }
        public override Point Sample(float u1, float u2, Pointer<Normal> ns)
        {
            Point p = new Point(0, 0, 0) + Radius * UniformSampleSphere(u1, u2);
            ns[0] = Normal.Normalize(ObjectToWorld[new Normal((Vector)p)]);
            if (ReverseOrientation) ns[0] *= -1.0f;
            return (ObjectToWorld)[(p)];
        }
        public override Point Sample(Point p, float u1, float u2, Pointer<Normal> ns)
        {
            Point Pcenter = (ObjectToWorld)[(new Point(0, 0, 0))];
            Vector wc = Vector.Normalize(Pcenter - p);
            Vector wcX, wcY;
            Extensions.CoordinateSystem(wc, out wcX, out wcY);
            if (Point.DistanceSquared(p, Pcenter) - Radius * Radius < 1e-4f)
                return Sample(u1, u2, ns);
            float sinThetaMax2 = Radius * Radius / Point.DistanceSquared(p, Pcenter);
            float cosThetaMax = MathHelper.Sqrt(MathHelper.Max(0.0f, 1.0f - sinThetaMax2));
            Pointer<DifferentialGeometry> dgSphere = new Pointer<DifferentialGeometry>(new DifferentialGeometry());
            Pointer<float> thit = (Pointer<float>)0, rayEpsilon = (Pointer<float>)0;
            Point ps;
            Ray r = new Ray(p, Extensions.UniformSampleCone(u1, u2, cosThetaMax, wcX, wcY, wc), 1e-3f);
            if (!Intersect(r, thit, rayEpsilon, dgSphere))
                thit[0] = Vector.Dot(Pcenter - p, Vector.Normalize(r.Direction));
            ps = r[thit];
            ns[0] = new Normal(Vector.Normalize(ps - Pcenter));
            if (ReverseOrientation) ns[0] *= -1.0f;
            return ps;
        }
        public static Vector UniformSampleSphere(float u1, float u2)
        {
            float z = 1.0f - 2.0f * u1;
            float r = MathHelper.Sqrt(MathHelper.Max(0.0f, 1.0f - z * z));
            float phi = 2.0f * MathHelper.PI * u2;
            float x = r * MathHelper.Cos(phi);
            float y = r * MathHelper.Sin(phi);
            return new Vector(x, y, z);
        }
        public float Radius { get; set; }
        public Sphere GetSphere(Element element)
        {
            if (element.Name == "Sphere")
            {
                return new Sphere(Transform.CreateTranformFromString(element.GetParameter("ObjectToWorld").Value), Transform.CreateTranformFromString(element.GetParameter("WorldToObject").Value), bool.Parse(element.GetParameter("ReverseOrientation").Value), float.Parse(element.GetParameter("Radius").Value), float.Parse(element.GetParameter("ZMin").Value), float.Parse(element.GetParameter("ZMax").Value), float.Parse(element.GetParameter("PhiMax").Value));
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
