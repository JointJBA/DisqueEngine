using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Shapes;
using Disque.Raytracer.Math;

namespace Disque.Raytracer
{
    public struct DifferentialGeometry
    {
        public Point p;
        public Normal nn;
        public float u;
        public float v;
        public Shape shape;
        public Vector dpdu;
        public Vector dpdv;
        public Normal dndu;
        public Normal dndv;
        public Vector dpdx;
        public Vector dpdy;
        public float dudx;
        public float dudy;
        public float dvdx;
        public float dvdy;
        public void Setp(Point p)
        {
            this.p = p;
        }
        public void Setnn(Normal nn)
        {
            this.nn = nn;
        }
        public void Setu(float u)
        {
            this.u = u;
        }
        public void Setv(float _v)
        {
            v = _v;
        }
        public void Setshape(Shape s)
        {
            shape = s;
        }
        public void Setdpdu(Vector dpdu)
        {
            this.dpdu = dpdu;
        }
        public void Setdpdv(Vector dpdv)
        {
            this.dpdv = dpdv;
        }
        public void Setdpdx(Vector dpdu)
        {
            this.dpdx = dpdu;
        }
        public void Setdpdy(Vector dpdv)
        {
            this.dpdy = dpdv;
        }
        public void Setdndu(Normal n)
        {
            dndu = n;
        }
        public void Setdndv(Normal n)
        {
            dndv = n;
        }
        public void Setdudx(float d)
        {
            dudx = d;
        }
        public void Setdudy(float d)
        {
            dudy = d;
        }
        public void Setdvdx(float d)
        {
            dvdx = d;
        }
        public void Setdvdy(float d)
        {
            dvdy = d;
        }
        public DifferentialGeometry(Point P, Vector DPDU, Vector DPDV, Normal DNDU, Normal DNDV, float uu, float vv, Shape sh)
        {
            p = (P);
            dpdu = (DPDU);
            dpdv = (DPDV);
            dndu = (DNDU);
            dndv = (DNDV);
            nn = new Normal(Vector.Normalize(Vector.Cross(dpdu, dpdv)));
            u = uu;
            v = vv;
            shape = sh;
            dudx = dvdx = dudy = dvdy = 0;
            dpdx = dpdy = new Vector();
            if (shape != null && (shape.ReverseOrientation ^ shape.TransformSwapsHandedness))
                nn *= -1.0f;
        }
        public void ComputeDifferentials(RayDifferential ray)
        {
            if (ray.HasDifferentials)
            {
                float d = -Vector.Dot((Vector)nn, new Vector(p.X, p.Y, p.Z));
                Vector rxv = new Vector(ray.RXOrigin.X, ray.RXOrigin.Y, ray.RXOrigin.Z);
                float tx = -(Vector.Dot((Vector)nn, rxv) + d) / Vector.Dot((Vector)nn, ray.RXDirection);
                if (float.IsNaN(tx))
                {
                    dudx = dvdx = 0.0f;
                    dudy = dvdy = 0.0f;
                    dpdx = dpdy = new Vector(0, 0, 0);
                    goto end;
                }
                Point px = ray.RXOrigin + tx * ray.RXDirection;
                Vector ryv = new Vector(ray.RYOrigin.X, ray.RYOrigin.Y, ray.RYOrigin.Z);
                float ty = -(Vector.Dot((Vector)nn, ryv) + d) / Vector.Dot((Vector)nn, ray.RYDirection);
                if (float.IsNaN(ty))
                {
                    dudx = dvdx = 0.0f;
                    dudy = dvdy = 0.0f;
                    dpdx = dpdy = new Vector(0, 0, 0);
                    goto end;
                }
                Point py = ray.RYOrigin + ty * ray.RYDirection;
                dpdx = px - p;
                dpdy = py - p;
                float[][] A = new float[2][];
                float[] Bx = new float[2];
                float[] By = new float[2];
                int[] axes = new int[2];
                if (MathHelper.Abs(nn.X) > MathHelper.Abs(nn.Y) && MathHelper.Abs(nn.X) > MathHelper.Abs(nn.Z))
                {
                    axes[0] = 1; axes[1] = 2;
                }
                else if (MathHelper.Abs(nn.Y) > MathHelper.Abs(nn.Z))
                {
                    axes[0] = 0; axes[1] = 2;
                }
                else
                {
                    axes[0] = 0; axes[1] = 1;
                }
                A[0][0] = dpdu[axes[0]];
                A[0][1] = dpdv[axes[0]];
                A[1][0] = dpdu[axes[1]];
                A[1][1] = dpdv[axes[1]];
                Bx[0] = px[axes[0]] - p[axes[0]];
                Bx[1] = px[axes[1]] - p[axes[1]];
                By[0] = py[axes[0]] - p[axes[0]];
                By[1] = py[axes[1]] - p[axes[1]];
                float a = 0, b = 0, c = 0, f = 0;
                if (!Transform.SolveLinearSystem2x2(A, Bx, ref a, ref b))
                {
                    dudx = 0.0f; 
                    dvdx = 0.0f;
                }
                else
                {
                    dudx = a;
                    dvdx = b;
                }
                if (!Transform.SolveLinearSystem2x2(A, By, ref c, ref f))
                {
                    dudy = 0.0f; dvdy = 0.0f;
                }
                else
                {
                    dudy = c;
                    dvdy = f;
                }
            end:
                float j = 0;
            }
            else
            {
                dudx = dvdx = 0.0f;
                dudy = dvdy = 0.0f;
                dpdx = dpdy = new Vector(0, 0, 0);
            }
        }
    }
}
