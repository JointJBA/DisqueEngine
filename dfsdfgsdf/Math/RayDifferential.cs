using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Raytracer.Math
{
    public class RayDifferential : Ray
    {
        public bool HasDifferentials { get; set; }
        public Point RXOrigin { get; set; }
        public Point RYOrigin { get; set; }
        public Vector RXDirection { get; set; }
        public Vector RYDirection { get; set; }
        public void ScaleDifferentials(float s)
        {
            RXOrigin = Origin + (RXOrigin - Origin) * s;
            RYOrigin = Origin + (RYOrigin - Origin) * s;
            RXDirection = Direction + (Vector)(RXDirection - Origin) * s;
            RYDirection = Direction + (Vector)(RYDirection - Origin) * s;
        }
        public RayDifferential()
        {
            HasDifferentials = false;
        }
        public RayDifferential(Point orig, Vector dir, float start, float end = float.MaxValue, float t = 0.0f, int d = 0)
            : base(orig, dir, start, end, t, d)
        {
            HasDifferentials = false;
        }
        public RayDifferential(Point orig, Vector dir, Ray parent, float start, float end = float.MaxValue)
            : base(orig, dir, start, end, parent.Time, parent.Depth + 1)
        {
            HasDifferentials = false;
        }
        public RayDifferential(Ray ray)
            : base(ray)
        {
            HasDifferentials = false;
        }
    }
}
