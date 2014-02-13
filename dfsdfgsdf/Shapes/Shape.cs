using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Math;

namespace Disque.Raytracer.Shapes
{
    public class Shape
    {
        Transform otw, wto;
        bool RO, tsh;
        int shapeId;
        public Shape(Transform o2w, Transform w2o, bool ro)
        {
            otw = o2w;
            wto = w2o;
            RO = ro;
            tsh = o2w.SwitchHandedness();
            shapeId = NextShapeId++;
        }
        static int NextShapeId = 1;
        public virtual BBox ObjectBound()
        {
            return new BBox();
        }
        public virtual BBox WorldBound()
        {
            return ObjectToWorld[ObjectBound()];
        }
        public virtual bool CanIntersect()
        {
            return true;
        }
        public virtual bool Intersect(Ray r, Pointer<float> tHit, Pointer<float> rayEpsilon, Pointer<DifferentialGeometry> dg)
        {
            return false;
        }
        public virtual bool IntersectP(Ray r)
        {
            return false;
        }
        public virtual void GetShadingGeometry(Transform obj2world, DifferentialGeometry dg, Pointer<DifferentialGeometry> dgShading)
        {
            dgShading[0] = dg;
        }
        public int ShapeId
        {
            get
            {
                return shapeId;
            }
        }
        public virtual float Area
        {
            get
            {
                return 0;
            }
        }
        public virtual float Pdf(Point Pshape)
        {
            return 1.0f / Area;
        }
        public virtual float Pdf(Point p, Vector wi)
        {
            Pointer<DifferentialGeometry> dgLight = new Pointer<DifferentialGeometry>(new DifferentialGeometry());
            Ray ray = new Ray(p, wi, 1e-3f);
            ray.Depth = -1;
            Pointer<float> thit = (Pointer<float>)0, rayEpsilon = (Pointer<float>)0;
            if (!Intersect(ray, thit, rayEpsilon, dgLight)) return 0.0f;
            float pdf = (Point.DistanceSquared(p, ray[thit])) / (Vector.AbsDot((Vector)(~dgLight).nn, -1.0f * wi) * Area);
            if (float.IsInfinity(pdf)) pdf = 0.0f;
            return pdf;
        }
        public virtual Point Sample(float u1, float u2, Pointer<Normal> n)
        {
            return new Point();
        }
        public virtual Point Sample(Point p, float u1, float u2, Pointer<Normal> n)
        {
            return Sample(u1, u2, n);
        }
        public virtual void Refine(List<Shape> shapes)
        {
           
        }
        public Transform ObjectToWorld
        {
            get
            {
                return otw;
            }
        }
        public Transform WorldToObject
        {
            get
            {
                return wto;
            }
        }
        public bool ReverseOrientation
        {
            get
            {
                return RO;
            }
        }
        public bool TransformSwapsHandedness
        {
            get
            {
                return tsh;
            }
        }
    }
}
