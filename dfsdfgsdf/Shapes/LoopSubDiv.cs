using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Math;

namespace Disque.Raytracer.Shapes
{
    public class LoopSubDiv : Shape
    {
        public struct SDVertex
        {
            public SDVertex(Point pt)
            {
                p = pt;
                sface = null;
                child = null;
                r = b = false;
            }
            public int Valence()
            {
                Pointer<SDFace> f = StartFace;
                if (!Boundary)
                {
                    int nf = 1;
                    while ((f = f[0].NextFace(this)) != StartFace)
                        ++nf;
                    return nf;
                }
                else
                {
                    int nf = 1;
                    while ((f = f[0].NextFace(this)) != null)
                        ++nf;
                    f = StartFace;
                    while ((f = f[0].PrevFace(this)) != null)
                        ++nf;
                    return nf + 1;
                }
            }
            public void OneRing(Pointer<Point> P)
            {
                if (!Boundary)
                {
                    Pointer<SDFace> face = StartFace;
                    do
                    {
                        P.Increment()[0] = face[0].NextVert(this)[0].P;
                        face = face[0].NextFace(this);
                    } while (face != StartFace);
                }
                else
                {
                    Pointer<SDFace> face = StartFace, f2;
                    while ((f2 = face[0].NextFace(this)) != null)
                        face = f2;
                    P.Increment()[0] = face[0].NextVert(this)[0].P;
                    do
                    {
                        P.Increment()[0] = face[0].PrevVert(this)[0].P;
                        face = face[0].PrevFace(this);
                    } while (face != null);
                }
            }
            Point p;
            Pointer<SDFace> sface;
            Pointer<SDVertex> child;
            bool r, b;
            public Point P { get { return p; } set { p = value; } }
            public Pointer<SDFace> StartFace { get { return sface; } set { sface = value; } }
            public void SetStartFace(Pointer<SDFace> Startface)
            {
                StartFace = Startface;
            }
            public Pointer<SDVertex> Child { get { return child; } set { child = value; } }
            public bool Regular { get { return r; } set { r = value; } }
            public bool Boundary { get { return b; } set { b = value; } }
            public static bool operator ==(SDVertex v1, SDVertex v2)
            {
                return ((v1.P == v2.P) && (v1.StartFace == v2.StartFace) && (v1.Child == v2.Child) && (v1.Regular == v2.Regular) && (v1.Boundary == v2.Boundary));
            }
            public static bool operator !=(SDVertex v1, SDVertex v2)
            {
                return ((v1.P != v2.P) || (v1.StartFace != v2.StartFace) || (v1.Child != v2.Child) || (v1.Regular != v2.Regular) || (v1.Boundary != v2.Boundary));
            }
        }
        static Pointer<SDVertex> min(Pointer<SDVertex> a, Pointer<SDVertex> b)
        {
            if (a < b)
                return a;
            return b;
        }
        static Pointer<SDVertex> max(Pointer<SDVertex> a, Pointer<SDVertex> b)
        {
            if (a > b)
                return a;
            return b;
        }
        public struct SDEdge
        {
            public SDEdge(Pointer<SDVertex> v0, Pointer<SDVertex> v1)
            {
                v = new Pointer<SDVertex>[2];
                v[0] = min(v0, v1);
                v[1] = max(v0, v1);
                f = new Pointer<SDFace>[2];
                f0edgeNum = -1;
            }
            internal Pointer<SDFace>[] f;
            internal Pointer<SDVertex>[] v;
            internal int f0edgeNum;
            public static bool operator <(SDEdge e1, SDEdge e2)
            {
                if (e1.v[0] == e2.v[0]) return e1.v[1] < e2.v[1];
                return e1.v[0] < e2.v[0];
            }
            public static bool operator >(SDEdge e1, SDEdge e2)
            {
                return !(e1 < e2);
            }
        }
        public struct SDFace
        {
            int vnum(SDVertex vert)
            {
                for (int i = 0; i < 3; ++i)
                {
                    if (v[i][0] == vert)
                        return i;
                }
                throw new Exception("Logic Error in SDFace.vnum(SDVertex)");
            }
            public Pointer<SDFace> NextFace(SDVertex vert)
            {
                return f[vnum(vert)];
            }
            public Pointer<SDFace> PrevFace(SDVertex vert)
            {
                return f[PREV(vnum(vert))];
            }
            public Pointer<SDVertex> NextVert(SDVertex vert)
            {
                return v[NEXT(vnum(vert))];
            }
            public Pointer<SDVertex> PrevVert(SDVertex vert)
            {
                return v[PREV(vnum(vert))];
            }
            public Pointer<SDVertex> OtherVert(Pointer<SDVertex> v0, Pointer<SDVertex> v1)
            {
                for (int i = 0; i < 3; ++i)
                    if (v[i] != v0 && v[i] != v1)
                        return v[i];
                throw new Exception("logic Error in SDFace.OtherVert(SDVertex, SDVertex)");
            }
            internal Pointer<SDVertex>[] v { get; set; }
            internal Pointer<SDFace>[] f { get; set; }
            internal Pointer<SDFace>[] children { get; set; }
        }
        static int NEXT(int i)
        {
            return (((i) + 1) % 3);
        }
        static int PREV(int i)
        {
            return (((i) + 2) % 3);
        }
        public LoopSubDiv(Transform o2w, Transform w2o, bool ro, int nfaces, int nvertices, int[] vi, Point[] P, int nl)
            : base(o2w, w2o, ro)
        {
            nLevels = nl;
            int i;
            Pointer<SDVertex> verts = Pointer<SDVertex>.Allocate(nvertices);
            for (i = 0; i < nvertices; ++i)
            {
                verts[i] = new SDVertex(P[i]);
                vertices.Add((verts + i));
            }
            Pointer<SDFace> fs = Pointer<SDFace>.Allocate(nfaces);
            for (i = 0; i < nfaces; ++i)
            {
                faces.Add((fs + i));
            }
            int c = 0;
            for (i = 0; i < nfaces; ++i)
            {
                Pointer<SDFace> f = faces[i];
                for (int j = 0; j < 3; j++)
                {
                    Pointer<SDVertex> v = vertices[vi[c + j]];
                    f[0].v[j] = v;
                    v[0].SetStartFace(f);
                }
                c += 3;
            }
            List<SDEdge> edges = new List<SDEdge>();
            for (i = 0; i < nfaces; ++i)
            {
                Pointer<SDFace> f = faces[i];
                for (int edgeNum = 0; edgeNum < 3; ++edgeNum)
                {
                    int v0 = edgeNum, v1 = NEXT(edgeNum);
                    SDEdge e = new SDEdge(f[0].v[v0], f[0].v[v1]);
                    if (edges.IndexOf(e) == edges.Count - 1)
                    {
                        e.f[0] = f;
                        e.f0edgeNum = edgeNum;
                        edges.Add(e);
                    }
                    else
                    {
                        e = edges[edges.IndexOf(e)];
                        e.f[0][0].f[e.f0edgeNum] = f;
                        f[0].f[edgeNum] = e.f[0];
                        edges.Remove(e);
                    }
                }
            }
        }
        int nLevels = 0;
        List<Pointer<SDVertex>> vertices = new List<Pointer<SDVertex>>();
        List<Pointer<SDFace>> faces = new List<Pointer<SDFace>>();
    }
}