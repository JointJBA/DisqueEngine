using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Math;

namespace Disque.Raytracer.Shapes
{
    public class Triangle : Shape
    {
        public Triangle(Transform o2w, Transform w2o, bool ro, TriangleMesh m, int n)
            : base(o2w, w2o, ro)
        {
            mesh = m;
            int i = 3 * n;
            v[0] = mesh.vertexIndex[i];
            v[1] = mesh.vertexIndex[i+1];
            v[2] = mesh.vertexIndex[i+2];
        }
        public override BBox ObjectBound()
        {
            Point p1 = mesh.p[v[0]];
            Point p2 = mesh.p[v[1]];
            Point p3 = mesh.p[v[2]];
            return BBox.Union(new BBox(WorldToObject[p1], WorldToObject[p2]), WorldToObject[p3]);
        }
        public override BBox WorldBound()
        {
            Point p1 = mesh.p[v[0]];
            Point p2 = mesh.p[v[1]];
            Point p3 = mesh.p[v[2]];
            return BBox.Union(new BBox(p1, p2), p3);
        }
        public override bool Intersect(Ray ray, Pointer<float> tHit, Pointer<float> rayEpsilon, Pointer<DifferentialGeometry> dg)
        {
            Point p1 = mesh.p[v[0]];
            Point p2 = mesh.p[v[1]];
            Point p3 = mesh.p[v[2]];
            Vector e1 = p2 - p1;
            Vector e2 = p3 - p1;
            Vector s1 = Vector.Cross(ray.Direction, e2);
            float divisor = Vector.Dot(s1, e1);
            if (divisor == 0)
                return false;
            float invDivisor = 1.0f / divisor;
            Vector d = ray.Origin - p1;
            float b1 = Vector.Dot(d, s1) * invDivisor;
            if (b1 < 0 || b1 > 1)
                return false;
            Vector s2 = Vector.Cross(d, e1);
            float b2 = Vector.Dot(ray.Direction, s2) * invDivisor;
            if (b2 < 0 || b1 + b2 > 1)
                return false;
            float t = Vector.Dot(e2, s2) * invDivisor;
            if (t < ray.MinT || t > ray.MaxT)
                return false;
            Vector dpdu, dpdv;
            float[][] uvs = new float[3][];
            uvs[0] = new float[2];
            uvs[1] = new float[2];
            uvs[2] = new float[2];
            GetUVs(uvs);
            float du1 = uvs[0][0] - uvs[2][0];
            float du2 = uvs[1][0] - uvs[2][0];
            float dv1 = uvs[0][1] - uvs[2][1];
            float dv2 = uvs[1][1] - uvs[2][1];
            Vector dp1 = p1 - p3, dp2 = p2 - p3;
            float determinant = du1 * dv2 - dv1 * du2;
            if (determinant == 0.0f)
            {
                Extensions.CoordinateSystem(Vector.Normalize(Vector.Cross(e2, e1)), out dpdu, out dpdv);
            }
            else
            {
                float invdet = 1.0f / determinant;
                dpdu = (dv2 * dp1 - dv1 * dp2) * invdet;
                dpdv = (-du2 * dp1 + du1 * dp2) * invdet;
            }
            float b0 = 1 - b1 - b2;
            float tu = b0 * uvs[0][0] + b1 * uvs[1][0] + b2 * uvs[2][0];
            float tv = b0 * uvs[0][1] + b1 * uvs[1][1] + b2 * uvs[2][1];
            if (ray.Depth != -1)
            {
                if (mesh.alphaTexture != null)
                {
                    DifferentialGeometry dgLocal = new DifferentialGeometry(ray[t], dpdu, dpdv, new Normal(0, 0, 0), new Normal(0, 0, 0), tu, tv, this);
                    if (mesh.alphaTexture.Evaluate(dgLocal) == 0.0f)
                        return false;
                }
            }
            dg[0] = new DifferentialGeometry(ray[t], dpdu, dpdv, new Normal(0,0,0), new Normal(0,0,0), tu, tv, this);
            tHit[0] = t;
            rayEpsilon[0] = 1e-3f * ~tHit;
            return true;
        }
        public override bool IntersectP(Ray ray)
        {
            Point p1 = mesh.p[v[0]];
            Point p2 = mesh.p[v[1]];
            Point p3 = mesh.p[v[2]];
            Vector e1 = p2 - p1;
            Vector e2 = p3 - p1;
            Vector s1 = Vector.Cross(ray.Direction, e2);
            float divisor = Vector.Dot(s1, e1);
            if (divisor == 0.0f)
                return false;
            float invDivisor = 1.0f / divisor;
            Vector d = ray.Origin - p1;
            float b1 = Vector.Dot(d, s1) * invDivisor;
            if (b1 < 0 || b1 > 1)
                return false;
            Vector s2 = Vector.Cross(d, e1);
            float b2 = Vector.Dot(ray.Direction, s2) * invDivisor;
            if (b2 < 0 || b1 + b2 > 1)
                return false;
            float t = Vector.Dot(e2, s2) * invDivisor;
            if (t < ray.MinT || t > ray.MaxT)
                return false;
            if (ray.Depth != -1 && mesh.alphaTexture != null)
            {
                Vector dpdu, dpdv;
                float[][] uvs = new float[3][];
                uvs[0] = new float[2];
                uvs[1] = new float[2];
                uvs[2] = new float[2];
                GetUVs(uvs);
                float du1 = uvs[0][0] - uvs[2][0];
                float du2 = uvs[1][0] - uvs[2][0];
                float dv1 = uvs[0][1] - uvs[2][1];
                float dv2 = uvs[1][1] - uvs[2][1];
                Vector dp1 = p1 - p3, dp2 = p2 - p3;
                float determinant = du1 * dv2 - dv1 * du2;
                if (determinant == 0.0f)
                {
                    Extensions.CoordinateSystem(Vector.Normalize(Vector.Cross(e2, e1)), out dpdu, out dpdv);
                }
                else
                {
                    float invdet = 1.0f / determinant;
                    dpdu = (dv2 * dp1 - dv1 * dp2) * invdet;
                    dpdv = (-du2 * dp1 + du1 * dp2) * invdet;
                }
                float b0 = 1 - b1 - b2;
                float tu = b0 * uvs[0][0] + b1 * uvs[1][0] + b2 * uvs[2][0];
                float tv = b0 * uvs[0][1] + b1 * uvs[1][1] + b2 * uvs[2][1];
                DifferentialGeometry dgLocal = new DifferentialGeometry(ray[t], dpdu, dpdv, new Normal(0,0,0), new Normal(0,0,0), tu, tv, this);
                if (mesh.alphaTexture.Evaluate(dgLocal) == 0.0f)
                    return false;
            }
            return true;
        }
        public override Point Sample(float u1, float u2, Pointer<Normal> n)
        {
            float b1 = 0, b2 = 0;
            Extensions.UniformSampleTriangle(u1, u2, ref b1, ref b2);
            Point p1 = mesh.p[v[0]];
            Point p2 = mesh.p[v[1]];
            Point p3 = mesh.p[v[2]];
            Point p = b1 * p1 + b2 * p2 + (1.0f - b1 - b2) * p3;
            Normal n2 = new Normal(Vector.Cross(p2 - p1, p3 - p1));
            n[0] = Normal.Normalize(n2);
            if (ReverseOrientation) n[0] *= -1.0f;
            return p;
        }
        public override void GetShadingGeometry(Transform obj2world, DifferentialGeometry dg, Pointer<DifferentialGeometry> dgShading)
        {
            if (mesh.n == null && mesh.s == null)
            {
                dgShading[0] = dg;
                return;
            }
            float[] b = new float[3];
            float[][] uv = new float[3][];
            uv[0] = new float[2];
            uv[1] = new float[2];
            uv[2] = new float[2];
            GetUVs(uv);
            float[][] A = { new float[2]
    { uv[1][0] - uv[0][0], uv[2][0] - uv[0][0] },
    new float[2]
    { uv[1][1] - uv[0][1], uv[2][1] - uv[0][1] } 
    };
            float[] C = { dg.u - uv[0][0], dg.v - uv[0][1] };
            if (!Transform.SolveLinearSystem2x2(A, C, ref b[1], ref b[2]))
            {
                b[0] = b[1] = b[2] = 1.0f / 3.0f;
            }
            else
                b[0] = 1.0f - b[1] - b[2];
            Normal ns;
            Vector ss, ts;
            if (mesh.n != null) ns = Normal.Normalize(obj2world[b[0] * mesh.n[v[0]] +
                                                  b[1] * mesh.n[v[1]] +
                                                  b[2] * mesh.n[v[2]]]);
            else ns = dg.nn;
            if (mesh.s != null) ss = Vector.Normalize(obj2world[b[0] * mesh.s[v[0]] +
                                                  b[1] * mesh.s[v[1]] +
                                                  b[2] * mesh.s[v[2]]]);
            else ss = Vector.Normalize(dg.dpdu);

            ts = Vector.Cross(ss, ns);
            if (ts.SquaredMagnitude > 0.0f)
            {
                ts = Vector.Normalize(ts);
                ss = Vector.Cross(ts, ns);
            }
            else
                Extensions.CoordinateSystem((Vector)ns, out ss, out ts);
            Normal dndu, dndv;
            if (mesh.n != null)
            {
                float[][] uvs = new float[3][];
                uvs[0] = new float[2];
                uvs[1] = new float[2];
                uvs[2] = new float[2];
                GetUVs(uvs);
                float du1 = uvs[0][0] - uvs[2][0];
                float du2 = uvs[1][0] - uvs[2][0];
                float dv1 = uvs[0][1] - uvs[2][1];
                float dv2 = uvs[1][1] - uvs[2][1];
                Normal dn1 = mesh.n[v[0]] - mesh.n[v[2]];
                Normal dn2 = mesh.n[v[1]] - mesh.n[v[2]];
                float determinant = du1 * dv2 - dv1 * du2;
                if (determinant == 0.0f)
                    dndu = dndv = new Normal(0, 0, 0);
                else
                {
                    float invdet = 1.0f / determinant;
                    dndu = ((dv2 * dn1 - dv1 * dn2) * invdet);
                    dndv = ((-du2 * dn1 + du1 * dn2) * invdet);
                }
            }
            else
                dndu = dndv = new Normal(0, 0, 0);
            dgShading[0] = new DifferentialGeometry(dg.p, ss, ts, (ObjectToWorld)[dndu], (ObjectToWorld)[dndv],
                dg.u, dg.v, dg.shape);
            dgShading[0].Setdudx(dg.dudx); 
            dgShading[0].Setdvdx(dg.dvdx);
            dgShading[0].Setdudy(dg.dudy); 
            dgShading[0].Setdvdy(dg.dvdy);
            dgShading[0].Setdpdx(dg.dpdx); 
            dgShading[0].Setdpdy(dg.dpdy);
        }
        void GetUVs(float[][] uv)
        {
          if (mesh.uvs != null) {
            uv[0][0] = mesh.uvs[2*v[0]];
            uv[0][1] = mesh.uvs[2*v[0]+1];
            uv[1][0] = mesh.uvs[2*v[1]];
            uv[1][1] = mesh.uvs[2*v[1]+1];
            uv[2][0] = mesh.uvs[2*v[2]];
            uv[2][1] = mesh.uvs[2*v[2]+1];
        }
        else {
            uv[0][0] = 0.0f; uv[0][1] = 0.0f;
            uv[1][0] = 1.0f; uv[1][1] = 0.0f;
            uv[2][0] = 1.0f; uv[2][1] = 1.0f;
        }
        }
        public override float Area
        {
            get
            {
                Point p1 = mesh.p[v[0]];
                Point p2 = mesh.p[v[1]];
                Point p3 = mesh.p[v[2]];
                return 0.5f * Vector.Cross(p2 - p1, p3 - p1).Magnitude;
            }
        }
        internal TriangleMesh mesh;
        internal int[] v;
    }
}
