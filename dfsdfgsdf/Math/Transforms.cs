using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Math;

namespace Disque.Raytracer.Math
{
    public struct Transform
    {
        Matrix4x4 mat, minv;
        public Transform(Matrix4x4 m)
        {
            mat = m;
            minv = Matrix4x4.Inverse(m);
        }
        public Transform(Matrix4x4 m, Matrix4x4 minv)
        {
            mat = m;
            this.minv = minv;
        }
        public void Inverse()
        {
            MathHelper.Swap<Matrix4x4>(ref mat, ref minv);
        }
        public void Transpose()
        {
            mat = Matrix4x4.Transpose(mat);
            minv = Matrix4x4.Transpose(minv);
        }
        public bool IsIdentity()
        {
            return mat == Matrix4x4.Identity;
        }
        public bool HasScale()
        {
            float la2 = new Vector(1, 0, 0).SquaredMagnitude;
            float lb2 = new Vector(0, 1, 0).SquaredMagnitude;
            float lc2 = new Vector(0, 0, 1).SquaredMagnitude;
            return ((la2 < 0.999f || la2 > 1.001f) || (lb2 < 0.999f || lb2 > 1.001f) || (lc2 < 0.999f || lc2 > 1.001f));
        }
        public bool SwitchHandedness()
        {
            float det = ((mat[0, 0] *
              (mat[1, 1] * mat[2, 2] -
               mat[1, 2] * mat[2, 1])) -
             (mat[0, 1] *
              (mat[1, 0] * mat[2, 2] -
               mat[1, 2] * mat[2, 0])) +
             (mat[0, 2] *
              (mat[1, 0] * mat[2, 1] -
               mat[1, 1] * mat[2, 0])));
            return det < 0.0f;
        }
        public static bool SolveLinearSystem2x2(float[][] A, float[] B, ref float x0, ref float x1)
        {
            float det = A[0][0] * A[1][1] - A[0][1] * A[1][0];
            if (MathHelper.Abs(det) < 1e-10f)
                return false;
            x0 = (A[1][1] * B[0] - A[0][1] * B[1]) / det;
            x1 = (A[0][0] * B[1] - A[1][0] * B[0]) / det;
            if (float.IsNaN(x0) || float.IsNaN(x1))
                return false;
            return true;
        }
        public static Transform Inverse(Transform t)
        {
            t.Inverse();
            return t;
        }
        public static Transform Transpose(Transform t)
        {
            t.Transpose();
            return t;
        }
        public static Transform Translate(Vector delta)
        {
            Matrix4x4 m = new Matrix4x4(new float[4, 4]{
                {1,0,0,delta.X},
                {0,1,0,delta.Y},
                {0,0,1,delta.Z},
                {0,0,0,1}});
            Matrix4x4 minv = new Matrix4x4(new float[4, 4]{
                {1,0,0,-delta.X},
                {0,1,0,-delta.Y},
                {0,0,1,-delta.Z},
                {0,0,0,1}});
            return new Transform(m, minv);
        }
        public static Transform Scale(float x, float y, float z)
        {
            Matrix4x4 m = new Matrix4x4(new float[4, 4]{
                {x,0,0,0},
                {0,y,0,0},
                {0,0,z,0},
                {0,0,0,1}});
            Matrix4x4 minv = new Matrix4x4(new float[4, 4]{
                {1.0f/x,0,0,0},
                {0,1.0f/y,0,0},
                {0,0,1.0f/z,0},
                {0,0,0,1}});
            return new Transform(m, minv);
        }
        public static Transform RotateX(float angle)
        {
            float sin_t = MathHelper.Sin(MathHelper.ToRadians(angle));
            float cos_t = MathHelper.Cos(MathHelper.ToRadians(angle));
            Matrix4x4 m = new Matrix4x4(new float[4, 4]{
                {1,0,0,0},
                {0,cos_t,-sin_t,0},
                {0,sin_t,cos_t,0},
                {0,0,0,1}});
            return new Transform(m, Matrix4x4.Transpose(m));
        }
        public static Transform RotateY(float angle)
        {
            float sin_t = MathHelper.Sin(MathHelper.ToRadians(angle));
            float cos_t = MathHelper.Cos(MathHelper.ToRadians(angle));
            Matrix4x4 m = new Matrix4x4(new float[4, 4]{
                {cos_t,0,sin_t,0},
                {0,1,0,0},
                {-sin_t,0,cos_t,0},
                {0,0,0,1}});
            return new Transform(m, Matrix4x4.Transpose(m));
        }
        public static Transform RotateZ(float angle)
        {
            float sin_t = MathHelper.Sin(MathHelper.ToRadians(angle));
            float cos_t = MathHelper.Cos(MathHelper.ToRadians(angle));
            Matrix4x4 m = new Matrix4x4(new float[4, 4]{
                {cos_t,-sin_t,0,0},
                {sin_t,cos_t,0,0},
                {0,0,1,0},
                {0,0,0,1}});
            return new Transform(m, Matrix4x4.Transpose(m));
        }
        public static Transform Rotate(float angle, Vector axis)
        {
            Vector a = Vector.Normalize(axis);
            float s = MathHelper.Sin(MathHelper.ToRadians(angle));
            float c = MathHelper.Cos(MathHelper.ToRadians(angle));
            float[,] m = new float[4, 4];
            m[0, 0] = a.X * a.X + (1.0f - a.X * a.X) * c;
            m[0, 1] = a.X * a.Y * (1.0f - c) - a.Z * s;
            m[0, 2] = a.X * a.Z * (1.0f - c) + a.Y * s;
            m[0, 3] = 0;
            m[1, 0] = a.X * a.Y * (1.0f - c) + a.Z * s;
            m[1, 1] = a.Y * a.Y + (1.0f - a.Y * a.Y) * c;
            m[1, 2] = a.Y * a.Z * (1.0f - c) - a.X * s;
            m[1, 3] = 0;
            m[2, 0] = a.X * a.Z * (1.0f - c) - a.Y * s;
            m[2, 1] = a.Y * a.Z * (1.0f - c) + a.X * s;
            m[2, 2] = a.Z * a.Z + (1.0f - a.Z * a.Z) * c;
            m[2, 3] = 0;
            m[3, 0] = 0;
            m[3, 1] = 0;
            m[3, 2] = 0;
            m[3, 3] = 1;
            Matrix4x4 mat = new Matrix4x4(m);
            return new Transform(mat, Matrix4x4.Transpose(mat));
        }
        public static Transform LookAt(Point pos, Point target, Vector up)
        {
            float[,] m = new float[4, 4];
            m[0, 3] = pos.X;
            m[1, 3] = pos.Y;
            m[2, 3] = pos.Z;
            m[3, 3] = 1;
            Vector dir = Vector.Normalize(target - pos);
            Vector left = Vector.Normalize(Vector.Cross(Vector.Normalize(up), dir));
            Vector newUp = Vector.Cross(dir, left);
            m[0, 0] = left.X;
            m[1, 0] = left.Y;
            m[2, 0] = left.Z;
            m[3, 0] = 0.0f;
            m[0, 1] = newUp.X;
            m[1, 1] = newUp.Y;
            m[2, 1] = newUp.Z;
            m[3, 1] = 0.0f;
            m[0, 2] = dir.X;
            m[1, 2] = dir.Y;
            m[2, 2] = dir.Z;
            m[3, 2] = 0.0f;
            Matrix4x4 camToWorld = new Matrix4x4(m);
            return new Transform(Matrix4x4.Inverse(camToWorld), camToWorld);
        }
        public static Transform Orthographic(float znear, float zfar)
        {
            return Scale(1.0f, 1.0f, 1.0f / (zfar - znear)) * Translate(new Vector(0, 0, -znear));
        }
        public static Transform Perspective(float fov, float n, float f)
        {
            Matrix4x4 persp = new Matrix4x4(new float[4, 4] {
            { 1, 0, 0, 0},
            {0, 1, 0, 0},
            {0, 0, f / (f - n), -f * n / (f - n)},
            {0, 0, 1, 0}
            });
            float invTanAng = 1.0f / MathHelper.Tan(MathHelper.ToRadians(fov) / 2.0f);
            return Scale(invTanAng, invTanAng, 1) * new Transform(persp);
        }
        public static Transform CreateTranformFromString(string matrix)
        {
            return new Transform(new Matrix4x4(matrix));
        }
        public static Transform operator *(Transform t1, Transform t2)
        {
            Matrix4x4 m1 = t1.Matrix * t2.Matrix;
            Matrix4x4 m2 = t2.InverseMatrix * t1.InverseMatrix;
            return new Transform(m1, m2);
        }
        public static bool operator ==(Transform t1, Transform t2)
        {
            return ((t1.mat == t2.mat) && (t1.minv == t2.minv));
        }
        public static bool operator !=(Transform t1, Transform t2)
        {
            return ((t1.mat != t2.mat) || (t1.minv != t2.minv));
        }
        public static bool operator <(Transform t1, Transform t2)
        {
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    if (t1.mat[i, j] < t2.mat[i, j]) return true;
                    if (t1.mat[i, j] > t2.mat[i, j]) return false;
                }
            return false;
        }
        public static bool operator >(Transform t1, Transform t2)
        {
            for (int i = 0; i < 4; ++i)
                for (int j = 0; j < 4; ++j)
                {
                    if (t1.mat[i, j] > t2.mat[i, j]) return true;
                    if (t1.mat[i, j] < t2.mat[i, j]) return false;
                }
            return false;
        }
        public Point this[Point pt]
        {
            get
            {
                float x = pt.X, y = pt.Y, z = pt.Z;
                float xp = mat[0, 0] * x + mat[0, 1] * y + mat[0, 2] * z + mat[0, 3];
                float yp = mat[1, 0] * x + mat[1, 1] * y + mat[1, 2] * z + mat[1, 3];
                float zp = mat[2, 0] * x + mat[2, 1] * y + mat[2, 2] * z + mat[2, 3];
                float wp = mat[3, 0] * x + mat[3, 1] * y + mat[3, 2] * z + mat[3, 3];
                if (wp != 0) throw new Exception();
                if (wp == 1.0f) return new Point(xp, yp, zp);
                else return new Point(xp, yp, zp) / wp;
            }
        }
        public Vector this[Vector v]
        {
            get
            {
                float x = v.X, y = v.Y, z = v.Z;
                return new Vector(mat[0, 0] * x + mat[0, 1] * y + mat[0, 2] * z,
                              mat[1, 0] * x + mat[1, 1] * y + mat[1, 2] * z,
                              mat[2, 0] * x + mat[2, 1] * y + mat[2, 2] * z);
            }
        }
        public Normal this[Normal n]
        {
            get
            {
                float x = n.X, y = n.Y, z = n.Z;
                return new Normal(minv[0, 0] * x + minv[1, 0] * y + minv[2, 0] * z,
                              minv[0, 1] * x + minv[1, 1] * y + minv[2, 1] * z,
                              minv[0, 2] * x + minv[1, 2] * y + minv[2, 2] * z);
            }
        }
        public Ray this[Ray ray]
        {
            get
            {
                Ray r = new Ray();
                r.Origin = this[ray.Origin];
                r.Direction = this[ray.Direction];
                return r;
            }
        }
        public RayDifferential this[RayDifferential ray]
        {
            get
            {
                RayDifferential ret = (RayDifferential)this[new Ray(ray)];
                ret.HasDifferentials = ray.HasDifferentials;
                ret.RXOrigin = this[ray.RXOrigin];
                ret.RYOrigin = this[ray.RYOrigin];
                ret.RXDirection = this[ray.RXDirection];
                ret.RYDirection = this[ray.RYDirection];
                return ret;
            }
        }
        public BBox this[BBox b]
        {
            get
            {
                Transform M = this;
                BBox ret = new BBox(M[new Point(b.Min.X, b.Min.Y, b.Min.Z)]);
                ret = BBox.Union(ret, M[new Point(b.Max.X, b.Min.Y, b.Min.Z)]);
                ret = BBox.Union(ret, M[new Point(b.Min.X, b.Max.Y, b.Min.Z)]);
                ret = BBox.Union(ret, M[new Point(b.Min.X, b.Min.Y, b.Max.Z)]);
                ret = BBox.Union(ret, M[new Point(b.Min.X, b.Max.Y, b.Max.Z)]);
                ret = BBox.Union(ret, M[new Point(b.Max.X, b.Max.Y, b.Min.Z)]);
                ret = BBox.Union(ret, M[new Point(b.Max.X, b.Min.Y, b.Max.Z)]);
                ret = BBox.Union(ret, M[new Point(b.Max.X, b.Max.Y, b.Max.Z)]);
                return ret;
            }
        }
        public Matrix4x4 Matrix
        {
            get
            {
                return mat;
            }
        }
        public Matrix4x4 InverseMatrix
        {
            get
            {
                return minv;
            }
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }
    public class AnimatedTransform
    {
        Transform startTransform, endTransform;
        float startTime, endTime;
        bool actuallyAnimated;
        Vector[] T = new Vector[2];
        Quaternion[] R = new Quaternion[2];
        Matrix4x4[] S = new Matrix4x4[2];
        public AnimatedTransform(Transform t1, float time1, Transform t2, float time2)
        {
            startTime = time1;
            endTime = time2;
            startTransform = t1;
            endTransform = t2;
            actuallyAnimated = t1 != t2;
            Decompose(startTransform.Matrix, out T[0], out R[0], out S[0]);
            Decompose(startTransform.Matrix, out T[1], out R[1], out S[1]);
        }
        public static void Decompose(Matrix4x4 m, out Vector T, out Quaternion Rquat, out Matrix4x4 S)
        {
            T = new Vector();
            T.X = m[0, 3];
            T.Y = m[1, 3];
            T.Z = m[2, 3];
            Matrix4x4 M = m;
            for (int i = 0; i < 3; ++i)
                M[i, 3] = M[3, i] = 0.0f;
            M[3, 3] = 1.0f;
            float norm;
            int count = 0;
            Matrix4x4 R = M;
            do
            {
                Matrix4x4 Rnext = new Matrix4x4();
                Matrix4x4 Rit = Matrix4x4.Inverse(Matrix4x4.Transpose(R));
                for (int i = 0; i < 4; ++i)
                    for (int j = 0; j < 4; ++j)
                        Rnext[i, j] = 0.5f * (R[i, j] + Rit[i, j]);
                norm = 0.0f;
                for (int i = 0; i < 3; ++i)
                {
                    float n = MathHelper.Abs(R[i, 0] - Rnext[i, 0]) +
                              MathHelper.Abs(R[i, 1] - Rnext[i, 1]) +
                              MathHelper.Abs(R[i, 2] - Rnext[i, 2]);
                    norm = MathHelper.Max(norm, n);
                }
                R = Rnext;
            } while (++count < 100 && norm > .0001f);
            Rquat = new Quaternion(new Transform(R));
            S = (Matrix4x4.Inverse(R) * M);
        }
        public void Interpolate(float time, Transform t)
        {
            if (!actuallyAnimated || time <= startTime)
            {
                t = startTransform;
                return;
            }
            if (time >= endTime)
            {
                t = endTransform;
                return;
            }
            float dt = (time - startTime) / (endTime - startTime);
            Vector trans = (1.0f - dt) * T[0] + dt * T[1];
            Quaternion rotate = Quaternion.Slerp(dt, R[0], R[1]);
            Matrix4x4 scale = new Matrix4x4();
            for (int i = 0; i < 3; ++i)
                for (int j = 0; j < 3; ++j)
                    scale[i, j] = MathHelper.Lerp(dt, S[0][i, j], S[1][i, j]);
            t = Transform.Translate(trans) * rotate.ToTransform() * new Transform(scale);
        }
        public BBox MotionBounds(BBox b, bool useInverse)
        {
            if (!actuallyAnimated) return Transform.Inverse(startTransform)[b];
            BBox ret = new BBox();
            int nSteps = 128;
            for (int i = 0; i < nSteps; ++i)
            {
                Transform t = new Transform();
                float time = MathHelper.Lerp(((float)i) / ((float)nSteps - 1), startTime, endTime);
                Interpolate(time, t);
                if (useInverse) t = Transform.Inverse(t);
                ret = BBox.Union(ret, t[b]);
            }
            return ret;
        }
        public Ray this[Ray r]
        {
            get
            {
                Ray tr = new Ray();
                if (!actuallyAnimated || r.Time <= startTime)
                    tr = (startTransform)[r];
                else if (r.Time >= endTime)
                    tr = (endTransform)[r];
                else
                {
                    Transform t = new Transform();
                    Interpolate(r.Time, t);
                    tr = t[r];
                }
                tr.Time = r.Time;
                return tr;
            }
        }
        public RayDifferential this[RayDifferential r]
        {
            get
            {
                RayDifferential tr = new RayDifferential();
                if (!actuallyAnimated || r.Time <= startTime)
                    tr = (startTransform)[r];
                else if (r.Time >= endTime)
                    tr = (endTransform)[r];
                else
                {
                    Transform t = new Transform();
                    Interpolate(r.Time, t);
                    tr = t[r];
                }
                tr.Time = r.Time;
                return tr;
            }
        }
        public Point this[float time, Point p]
        {
            get
            {
                if (!actuallyAnimated || time <= startTime)
                    return (startTransform)[p];
                else if (time >= endTime)
                    return (endTransform)[p];
                Transform t = new Transform();
                Interpolate(time, t);
                return t[p];
            }
        }
        public Vector this[float time, Vector v]
        {
            get
            {
                if (!actuallyAnimated || time <= startTime)
                    return (startTransform)[v];
                else if (time >= endTime)
                    return (endTransform)[v];
                Transform t = new Transform();
                Interpolate(time, t);
                return t[v];
            }
        }
    }
}
