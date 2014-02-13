using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Raytracer.Math
{
    public class Ray
    {
        public Point Origin { get; set; }
        public Vector Direction { get; set; }
        public float MaxT { get; set; }
        public float MinT { get; set; }
        public float Time { get; set; }
        public int Depth { get; set; }
        public Ray()
        {
            MinT = 0.0f;
            MaxT = float.MaxValue;
            Time = 0;
            Depth = 0;
        }
        public Ray(Point orig, Vector dir, float start, float end = float.MaxValue, float t = 0, int d = 0)
        {
            Origin = orig;
            Direction = dir;
            MinT = start;
            MaxT = end;
            Time = t;
            Depth = d;
        }
        public Ray(Point orig, Vector dir, Ray parent, float start, float end = float.MaxValue)
        {
            Origin = orig;
            Direction = dir;
            MinT = start;
            MaxT = end;
            Time = parent.Time;
            Depth = parent.Depth + 1;
        }
        public Ray(Ray ray)
        {
            Origin = ray.Origin;
            Direction = ray.Direction;
            MaxT = ray.MaxT;
            MinT = ray.MinT;
            Time = ray.Time;
            Depth = ray.Depth;
        }
        public Point this[float t]
        {
            get
            {
                return Origin + Direction * t;
            }
        }
    }
}
