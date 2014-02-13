using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Raytracer.Math
{
    public struct Normal
    {
        float _x;
        float _y;
        float _z;
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
        public Normal(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        public Normal(Vector v)
        {
            _x = v.X;
            _y = v.Y;
            _z = v.Z;
        }
        public Normal(float value)
        {
            _x = _y = _z = value;
        }
        public float Magnitude
        {
            get
            {
                return MathHelper.Sqrt(X * X + Y * Y + Z * Z);
            }
        }
        public float SquaredMagnitude
        {
            get
            {
                return (X * X + Y * Y + Z * Z);
            }
        }
        public static Normal operator *(Normal v, float scale)
        {
            return new Normal(v.X * scale, v.Y * scale, v.Z * scale);
        }
        public static Normal operator *(float scale, Normal v)
        {
            return v * scale;
        }
        public static Normal operator /(Normal v, float s)
        {
            return new Normal(v.X / s, v.Y / s, v.Z / s);
        }
        public static Normal operator +(Normal v1, Normal v2)
        {
            return new Normal(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Normal operator -(Normal v1, Normal v2)
        {
            return new Normal(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        public Vector Cross(Vector v)
        {
            return v.Cross((Vector)this);
        }
        public void Normalize()
        {
            float l = Magnitude;
            if (l > 0)
            {
                this *= (1 / l);
            }
        }
        public static Normal Normalize(Normal vector)
        {
            vector.Normalize();
            return vector;
        }
        public static explicit operator Vector(Normal n)
        {
            return new Vector(n.X, n.Y, n.Z);
        }
        public static bool operator ==(Normal v1, Normal v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
        }
        public static bool operator !=(Normal v1, Normal v2)
        {
            return v1.X != v2.X || v1.Y != v2.Y || v1.Z != v2.Z;
        }
        public float this[int i]
        {
            get
            {
                if (i == 0)
                    return X;
                else if (i == 1)
                    return Y;
                return Z;
            }

        }
    }
}
