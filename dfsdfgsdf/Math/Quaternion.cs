using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Raytracer.Math
{
    public struct Quaternion
    {
        float _x, _y, _z, _w;
        public Quaternion(Transform t)
        {
            Matrix4x4 m = t.Matrix;
            float trace = m[0, 0] + m[1, 1] + m[2, 2];
            if (trace > 0.0f)
            {
                float s = MathHelper.Sqrt(trace + 1.0f);
                _w = s / 2.0f;
                s = 0.5f / s;
                _x = (m[2, 1] - m[1, 2]) * s;
                _y = (m[0, 2] - m[2, 0]) * s;
                _z = (m[1, 0] - m[0, 1]) * s;
            }
            else
            {
                int[] nxt = { 1, 2, 0 };
                float[] q = new float[3];
                int i = 0;
                if (m[1, 1] > m[0, 0]) i = 1;
                if (m[2, 2] > m[i, i]) i = 2;
                int j = nxt[i];
                int k = nxt[j];
                float s = MathHelper.Sqrt((m[i, i] - (m[j, j] + m[k, k])) + 1.0f);
                q[i] = s * 0.5f;
                if (s != 0.0f) s = 0.5f / s;
                _w = (m[k, j] - m[j, k]) * s;
                q[j] = (m[j, i] + m[i, j]) * s;
                q[k] = (m[k, i] + m[i, k]) * s;
                _x = q[0];
                _y = q[1];
                _z = q[2];
            }
        }
        public Quaternion(float x, float y, float z, float w)
        {
            _x = x;
            _y = y;
            _z = z;
            _w = w;
        }
        public float X
        {
            get { return _x; }
            set { _x = value; }
        }
        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public float Z
        {
            get { return _z; }
            set { _z = value; }
        }
        public float W
        {
            get { return _w; }
            set { _w = value; }
        }
        public Transform ToTransform()
        {
            float xx = _x * _x, yy = _y * _y, zz = _z * _z;
            float xy = _x * _y, xz = _x * _z, yz = _y * _z;
            float wx = _x * _w, wy = _y * _w, wz = _z * _w;
            Matrix4x4 m = new Matrix4x4();
            m[0, 0] = 1.0f - 2.0f * (yy + zz);
            m[0, 1] = 2.0f * (xy + wz);
            m[0, 2] = 2.0f * (xz - wy);
            m[1, 0] = 2.0f * (xy - wz);
            m[1, 1] = 1.0f - 2.0f * (xx + zz);
            m[1, 2] = 2.0f * (yz + wx);
            m[2, 0] = 2.0f * (xz + wy);
            m[2, 1] = 2.0f * (yz - wx);
            m[2, 2] = 1.0f - 2.0f * (xx + yy);
            return new Transform(Matrix4x4.Transpose(m), m);
        }
        public float Dot(Quaternion q)
        {
            return _x * q._x + _y * q._y + _z * q._z + _w * q._w;
        }
        public static Quaternion operator +(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1._x + q2._x, q1._y + q2._y, q1._z + q2._z, q1._w + q2._w);
        }
        public static Quaternion operator -(Quaternion q1, Quaternion q2)
        {
            return new Quaternion(q1._x - q2._x, q1._y - q2._y, q1._z - q2._z, q1._w - q2._w);
        }
        public static Quaternion operator *(Quaternion q, float scale)
        {
            return new Quaternion(q._x * scale, q._y * scale, q._z * scale, q._w * scale);
        }
        public static Quaternion operator *(float scale, Quaternion q)
        {
            return new Quaternion(q._x * scale, q._y * scale, q._z * scale, q._w * scale);
        }
        public static Quaternion operator /(Quaternion q, float s)
        {
            float scale = 1.0f / s;
            return new Quaternion(q._x * scale, q._y * scale, q._z * scale, q._w * scale);
        }
        public static float Dot(Quaternion q1, Quaternion q2)
        {
            return q1.Dot(q2);
        }
        public static Quaternion Normalize(Quaternion q)
        {
            return q / MathHelper.Sqrt(Dot(q, q));
        }
        public static Quaternion Slerp(float t, Quaternion q1, Quaternion q2)
        {
            float cosTheta = Dot(q1, q2);
            if (cosTheta > .9995f)
                return Normalize((1.0f - t) * q1 + t * q2);
            else
            {
                float theta = MathHelper.Acos(MathHelper.Clamp(cosTheta, -1.0f, 1.0f));
                float thetap = theta * t;
                Quaternion qperp = Normalize(q2 - q1 * cosTheta);
                return q1 * MathHelper.Cos(thetap) + qperp * MathHelper.Sin(thetap);
            }
        }
    }
}
