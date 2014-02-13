using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Raytracer.Math
{
    public struct Point
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
        public Point(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        public Point(float value)
        {
            _x = _y = _z = value;
        }
        public static Point operator *(Point v, float scale)
        {
            return new Point(v.X * scale, v.Y * scale, v.Z * scale);
        }
        public static Point operator *(float scale, Point v)
        {
            return v * scale;
        }
        public static Point operator /(Point v, float s)
        {
            return new Point(v.X / s, v.Y / s, v.Z / s);
        }
        public static Point operator +(Point v1, Point v2)
        {
            return new Point(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Vector operator -(Point v1, Point v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        public static Point operator +(Vector v1, Point v2)
        {
            return new Point(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Point operator +(Point v1, Vector v2)
        {
            return new Point(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Point operator -(Vector v1, Point v2)
        {
            return new Point(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        public static Point operator -(Point v1, Vector v2)
        {
            return new Point(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        public static explicit operator Vector(Point p)
        {
            return new Vector(p.X, p.Y, p.Z);
        }
        //public static Vector operator +(Point v1, Point v2)
        //{
        //    return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        //}
        //public static Vector operator -(Point v1, Point v2)
        //{
        //    return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        //}
        //public static Vector operator +(Vector v1, Point v2)
        //{
        //    return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        //}
        //public static Vector operator +(Point v1, Vector v2)
        //{
        //    return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        //}
        //public static Vector operator -(Vector v1, Point v2)
        //{
        //    return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        //}
        //public static Vector operator -(Point v1, Vector v2)
        //{
        //    return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        //}
        public static bool operator ==(Point v1, Point v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
        }
        public static bool operator !=(Point v1, Point v2)
        {
            return v1.X != v2.X || v1.Y != v2.Y || v1.Z != v2.Z;
        }
        public static float Distance(Point v1, Point v2)
        {
            Vector p = (v2 - v1);
            return p.Magnitude;
        }
        public static float DistanceSquared(Point v1, Point v2)
        {
            Vector p = (v2 - v1);
            return p.SquaredMagnitude;
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
