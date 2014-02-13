using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Raytracer.Math
{
    public struct Vector
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
        public Vector(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        public Vector(float value)
        {
            _x = _y = _z = value;
        }
        public float Dot(Vector v)
        {
            return X * v.X + Y * v.Y + Z * v.Z;
        }
        public float AbsDot(Vector v)
        {
            return MathHelper.Abs(Dot(v));
        }
        public static float AbsDot(Vector v1, Vector v2)
        {
            return MathHelper.Abs(Dot(v1, v2));
        }
        public Vector Cross(Vector vector)
        {
            return new Vector(Y * vector.Z - Z * vector.Y, Z * vector.X - X * vector.Z, X * vector.Y - Y * vector.X);
        }
        public void Invert()
        {
            X = -X;
            Y = -Y;
            Z = -Z;
        }
        public void Normalize()
        {
            float l = Magnitude;
            if (l > 0)
            {
                this *= (1 / l);
            }
        }
        public static float Dot(Vector vector1, Vector vector2)
        {
            return vector1.Dot(vector2);
        }
        public static Vector Cross(Vector v1, Vector v2)
        {
            return v1.Cross(v2);
        }
        public static Vector Cross(Vector v1, Normal v2)
        {
            return v2.Cross(v1);
        }
        public static Vector Cross(Normal v1, Vector v2)
        {
            return v1.Cross(v2);
        }
        public static Vector Invert(Vector vector)
        {
            vector.Invert();
            return vector;
        }
        public static Vector Normalize(Vector vector)
        {
            vector.Normalize();
            return vector;
        }
        public static Vector operator *(Vector v, float scale)
        {
            return new Vector(v.X * scale, v.Y * scale, v.Z * scale);
        }
        public static Vector operator *(float scale, Vector v)
        {
            return v * scale;
        }
        public static Vector operator /(Vector v, float s)
        {
            return new Vector(v.X / s, v.Y / s, v.Z / s);
        }
        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        public static bool operator ==(Vector v1, Vector v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
        }
        public static bool operator !=(Vector v1, Vector v2)
        {
            return v1.X != v2.X || v1.Y != v2.Y || v1.Z != v2.Z;
        }
        public static float Distance(Vector v1, Vector v2)
        {
            return (v2 - v1).Magnitude;
        }
        public static float DistanceSquared(Vector v1, Vector v2)
        {
            return (v2 - v1).SquaredMagnitude;
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
