using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Math;
using Disque.Raytracer.Textures;

namespace Disque.Raytracer.Shapes
{
    public class TriangleMesh : Shape
    {
        public TriangleMesh(Transform o2w, Transform w2o, bool ro, int nt,
                            int nv, int[] vi, Point[] P, Normal[] N,
                             Vector[] S, float[] uv, Texture<float> atex)
            : base(o2w, w2o, ro)
        {
            alphaTexture = atex;
            ntris = nt;
            nverts = nv;
            MathHelper.Copy<int>(ref vertexIndex, ref vi);
            if (uv != null)
            {
                MathHelper.Copy<float>(ref uvs, ref uv);
            }
            else
                uvs = null;
            if (N != null)
            {
                MathHelper.Copy<Normal>(ref n, ref N);
            }
            else n = null;
            if (S != null)
            {
                MathHelper.Copy<Vector>(ref s, ref S);
            }
            else s = null;
            for (int i = 0; i < nverts; ++i)
                p[i] = (ObjectToWorld)[P[i]];
        }
        public override BBox ObjectBound()
        {
            BBox objectBounds = new BBox();
            for (int i = 0; i < nverts; i++)
                objectBounds = BBox.Union(objectBounds, (WorldToObject)[p[i]]);
            return objectBounds;
        }
        public override BBox WorldBound()
        {
            BBox worldBounds = new BBox();
            for (int i = 0; i < nverts; i++)
                worldBounds = BBox.Union(worldBounds, p[i]);
            return worldBounds;
        }
        public override bool CanIntersect()
        {
            return false;
        }
        public override void Refine(List<Shape> shapes)
        {
            for (int i = 0; i < ntris; ++i)
                shapes.Add(new Triangle(ObjectToWorld, WorldToObject, ReverseOrientation, this, i));
        }
        internal int ntris, nverts;
        internal int[] vertexIndex;
        internal Point[] p; 
        internal Normal[] n;
        internal Vector[] s;
        internal float[] uvs;
        internal Texture<float> alphaTexture;
    }
}
