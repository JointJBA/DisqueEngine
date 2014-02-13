using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.GeometricObjects;
using Disque.Raytracer.Materials;
using Disque.Raytracer.GeometricObjects.Primitives;
using Disque.Math;
using Disque.Raytracer.Textures;
using Disque.Raytracer.Cameras;
using Disque.Raytracer.Lights;
using Disque.Raytracer.Tracers;
using Disque.Raytracer.Worlds;
using Disque.Raytracer.Samplers;
using Disque.Raytracer.Mappings;
using Disque.Raytracer.GeometricObjects.CompoundObjects;
using Disque.Raytracer.GeometricObjects.Triangles;

namespace Disque.Raytracer.Rml
{
    public static class P
    {
        public static Random Random = new Random();
        public static readonly string Name = "Name";
        public static readonly string Tracer = "Tracer";
        public static readonly string Directory = "Directory";
        public static readonly string Height = "Height";
        public static readonly string Width = "Width";
        public static readonly string Samples = "Samples";
        public static readonly string PixelSize = "PixelSize";
        public static readonly string MaxDepth = "MaxDepth";
        public static readonly string Sampler = "Sampler";
        public static readonly string DiffRefCoeff = "DiffuseCoeff";
        public static readonly string AmbRefCoeff = "AmbientCoeff";
        public static readonly string SpecCoeff = "SpecularCoeff";
        public static readonly string RefCoeff = "ReflectiveCoeff";
        public static readonly string TransCoeff = "TransCoeff";
        public static readonly string Col = "Color";
        public static readonly string SpCol = "SpecularColor";
        public static readonly string SpTxt = "SpecularTexture";
        public static readonly string RefCol = "ReflectiveColor";
        public static readonly string Exp = "Exponent";
        public static readonly string IOR = "IOR";
        public static readonly string Pos = "Position";
        public static readonly string Dir = "Direction";
        public static readonly string Min = "Min";
        public static readonly string Max = "Max";
        public static readonly string Rad = "Radius";
        public static readonly string Radi = "Radiance";
        public static readonly string IRad = "InnerRadius";
        public static readonly string ORad = "OuterRadius";
        public static readonly string Shad = "Shadows";
        public static readonly string Mat = "Material";
        public static readonly string Text = "Texture";
        public static readonly string Bottom = "Bottom";
        public static readonly string Top = "Top";
        public static readonly string Tar = "Target";
        public static readonly string Dist = "Distance";
        public static readonly string Zoom = "Zoom";
        public static readonly string Map = "Mapping";
        public static readonly string Matr = "Matrix";
        public static readonly string Trans = "Transform";
        public static readonly string ObjRef = "ObjectRef";
        public static readonly string Srad = "SweptRadius";
        public static readonly string Trad = "TubeRadius";
        public static readonly string GlosCoeff = "GlossyCoeff";
        public static readonly string GlosCol = "GlossyColor";

        public static class Obj
        {
            public static readonly string Sph = "Sphere";
            public static readonly string ConSph = "ConcaveSphere";
            public static readonly string ConHsph = "ConcaveHemisphere";
            public static readonly string Box = "Box";
            public static readonly string Plane = "Plane";
            public static readonly string Disk = "Disk";
            public static readonly string Annulus = "Annulus";
            public static readonly string Tri = "Triangle";
            public static readonly string STri = "SmoothTriangle";
            public static readonly string Ins = "Instance";
            public static readonly string Rect = "Rectangle";
            public static readonly string OCyl = "OpenCylinder";
            public static readonly string Cyl = "Cylinder";
            public static readonly string OC = "OpenCone";
            public static readonly string CO = "Cone";
            public static readonly string Tor = "Torus";
            public static readonly string COCyl = "ConvexOpenCylinder";
            public static readonly string Mesh = "Mesh";
        }

        public static class Mate
        {
            public static readonly string Matt = "Matte";
            public static readonly string Pho = "Phong";
            public static readonly string Refl = "Reflective";
            public static readonly string Trans = "Transparent";
            public static readonly string GlosRef = "GlossyReflective";
            public static readonly string Emis = "Emissive";
        }

        public static class Cam
        {
            public static readonly string Pin = "Pinhole";
        }

        public static class Lights
        {
            public static readonly string Amb = "Ambient";
            public static readonly string AmbOcc = "AmbientOccluder";
            public static readonly string PL = "PointLight";
            public static readonly string Direc = "Directional";
            public static readonly string AL = "AreaLight";
        }
    }

    public static class Extensions
    {
        public static GeometricObject CreateObjectFromElement(this RElement ele, Dictionary<string, Texture> txts, ref bool instance)
        {
            string n = ele.Name;
            string name = getName(ele);
            bool shad = getShadow(ele);
            instance = !getInstance(ele);
            Material m = getMaterial(ele, txts);
            if (n == P.Obj.Sph)
            {
                Sphere sph = new Sphere(ele.Attributes[P.Pos].ToVector3(), ele.Attributes[P.Rad].ToFloat(), name);
                sph.SetShadows(shad);
                sph.SetMaterial(m);
                return sph;
            }
            else if (n == P.Obj.Box)
            {
                Box box = new Box(ele.Attributes[P.Min].ToVector3(), ele.Attributes[P.Max].ToVector3(), name);
                box.SetShadows(shad);
                box.SetMaterial(m);
                return box;
            }
            else if (n == P.Obj.Plane)
            {
                Plane p = new Plane(ele.Attributes[P.Pos].ToVector3(), ele.Attributes[P.Dir].ToVector3(), name);
                p.SetShadows(shad);
                p.SetMaterial(m);
                return p;
            }
            else if (n == P.Obj.Disk)
            {
                Disk d = new Disk(ele.Attributes[P.Pos].ToVector3(), ele.Attributes[P.Dir].ToVector3(), ele.Attributes[P.Rad].ToFloat(), name);
                d.SetShadows(shad);
                d.SetMaterial(m);
                return d;
            }
            else if (n == P.Obj.Annulus)
            {
                Annulus d = new Annulus(ele.Attributes[P.Pos].ToVector3(), ele.Attributes[P.Dir].ToVector3(), float.Parse(ele.Attributes[P.IRad].Value), float.Parse(ele.Attributes[P.ORad].Value), name);
                d.SetShadows(shad);
                d.SetMaterial(m);
                return d;
            }
            else if (n == P.Obj.Tri)
            {
                Triangle t = new Triangle(ele.Attributes["P1"].ToVector3(), ele.Attributes["P2"].ToVector3(), ele.Attributes["P3"].ToVector3(), name);
                t.SetShadows(shad);
                t.SetMaterial(m);
                return t;
            }
            else if (n == P.Obj.STri)
            {
                SmoothTriangle t = new SmoothTriangle(ele.Attributes["P1"].ToVector3(), ele.Attributes["P2"].ToVector3(), ele.Attributes["P3"].ToVector3(), name);
                t.SetShadows(shad);
                t.SetMaterial(m);
                return t;
            }
            else if (n == P.Obj.Rect)
            {
                Rectangle r = new Rectangle(ele.Attributes["P"].ToVector3(), GetVector(ele.Attributes["A"]), GetVector(ele.Attributes["B"]), name);
                r.SetShadows(shad);
                r.SetMaterial(m);
                return r;
            }
            else if (n == P.Obj.OCyl)
            {
                OpenCylinder oc = new OpenCylinder(float.Parse(ele.Attributes[P.Bottom].Value), float.Parse(ele.Attributes[P.Top].Value), float.Parse(ele.Attributes[P.Rad].Value), name);
                oc.SetShadows(shad);
                oc.SetMaterial(m);
                return oc;
            }
            else if (n == P.Obj.Cyl)
            {
                SolidCylinder oc = new SolidCylinder(float.Parse(ele.Attributes[P.Bottom].Value), float.Parse(ele.Attributes[P.Top].Value), float.Parse(ele.Attributes[P.Rad].Value), name);
                oc.SetShadows(shad);
                oc.SetMaterial(m);
                return oc;
            }
            else if (n == P.Obj.OC)
            {
                OpenCone oc = new OpenCone(float.Parse(ele.Attributes[P.Rad].Value), float.Parse(ele.Attributes[P.Height].Value), name);
                oc.SetShadows(shad);
                oc.SetMaterial(m);
                return oc;
            }
            else if (n == P.Obj.COCyl)
            {
                ConvexOpenCylinder oc = new ConvexOpenCylinder(float.Parse(ele.Attributes[P.Bottom].Value), float.Parse(ele.Attributes[P.Top].Value), float.Parse(ele.Attributes[P.Rad].Value), name);
                oc.SetShadows(shad);
                oc.SetMaterial(m);
                return oc;
            }
            else if (n == P.Obj.Ins)
            {
                Instance ins = new Instance(GeometricObject.GetObject(ele.Attributes["ObjectRef"].Value), name);
                ins.SetShadows(shad);
                transform(ins, ele);
                if (m != null)
                    ins.SetMaterial(m);
                return ins;
            }
            else if (n == P.Obj.Tor)
            {
                Torus tor = new Torus(ele.Attributes[P.Srad].ToFloat(), ele.Attributes[P.Trad].ToFloat(), name);
                tor.SetShadows(shad);
                if (m != null)
                    tor.SetMaterial(m);
                return tor;
            }
            else if (n == P.Obj.ConSph)
            {
                ConcaveSphere conc = new ConcaveSphere(ele.Attributes[P.Pos].ToVector3(), ele.Attributes[P.Rad].ToFloat(), name);
                conc.SetShadows(shad);
                if (m != null)
                    conc.SetMaterial(m);
                return conc;
            }
            else if (n == P.Obj.ConHsph)
            {
                ConcaveHemisphere conc = new ConcaveHemisphere(ele.Attributes[P.Pos].ToVector3(), ele.Attributes[P.Rad].ToFloat(), name);
                conc.SetShadows(shad);
                if (m != null)
                    conc.SetMaterial(m);
                return conc;
            }
            else if (n == P.Obj.Mesh)
            {
                Mesh mesh = new Mesh(name);
                mesh.SetShadows(shad);
                getVertices(mesh, ele);
                getNormals(mesh, ele);
                getFaces(mesh, ele);
                if (m != null)
                    mesh.SetMaterial(m);
                return mesh;
            }
            else
            {
                return getCObject(ele);
            }
        }

        static GeometricObject getCObject(RElement ele)
        {
            
            throw new NotImplementedException();
        }

        public static Material CreateMaterialFromElement(this RElement ele, Dictionary<string, Texture> textures)
        {
            string n = ele.Name;
            bool shad = getShadow(ele);
            Sampler s = null;
            if (ele.Attributes.ContainsKey(P.Sampler))
            {
                s = ele.Attributes[P.Sampler].CreateSamplerFromAttribute(ele.Attributes[P.Samples].ToInt());
            }
            if (n == P.Mate.Matt)
            {
                Matte m = new Matte(ele.Attributes[P.AmbRefCoeff].ToFloat(), ele.Attributes[P.DiffRefCoeff].ToFloat(), ele.Attributes[P.Col].ToVector3());
                m.Shadows = shad;
                if (s != null) m.SetSampler(s);
                return m;
            }
            else if (n == P.Mate.Pho)
            {
                Phong p = new Phong();
                p.SetSpecularColor(ele.Attributes[P.SpCol].ToVector3());
                p.SetExp(ele.Attributes[P.Exp].ToFloat());
                p.SetCD(GetVector(ele.Attributes[P.Col]));
                p.SetAmbientRC(ele.Attributes[P.AmbRefCoeff].ToFloat());
                p.SetDiffuseRC(ele.Attributes[P.DiffRefCoeff].ToFloat());
                p.SetSpecularRC(ele.Attributes[P.SpecCoeff].ToFloat());
                p.Shadows = shad;
                if (s != null) p.SetSampler(s);
                return p;
            }
            else if (n == P.Mate.Refl)
            {
                Reflective r = new Reflective();
                r.SetSpecularColor(ele.Attributes[P.SpCol].ToVector3());
                r.SetSpecularRC(ele.Attributes[P.SpecCoeff].ToFloat());
                r.SetRColor(GetVector(ele.Attributes[P.RefCol]));
                r.SetReflectiveRC(ele.Attributes[P.RefCoeff].ToFloat());
                r.SetExp(ele.Attributes[P.Exp].ToFloat());
                r.SetCD(GetVector(ele.Attributes[P.Col]));
                r.SetAmbientRC(ele.Attributes[P.AmbRefCoeff].ToFloat());
                r.SetDiffuseRC(ele.Attributes[P.DiffRefCoeff].ToFloat());
                r.Shadows = shad;
                if (s != null) r.SetSampler(s);
                return r;
            }
            else if (n == P.Mate.Trans)
            {
                Transparent t = new Transparent();
                t.SetSpecularColor(ele.Attributes[P.SpCol].ToVector3());
                t.SetSpecularRC(ele.Attributes[P.SpecCoeff].ToFloat());
                t.SetReflectiveRC(ele.Attributes[P.RefCoeff].ToFloat());
                t.SetExp(ele.Attributes[P.Exp].ToFloat());
                t.SetCD(GetVector(ele.Attributes[P.Col]));
                t.SetAmbientRC(ele.Attributes[P.AmbRefCoeff].ToFloat());
                t.SetDiffuseRC(ele.Attributes[P.DiffRefCoeff].ToFloat());
                t.SetIndexOfRefraction(ele.Attributes[P.IOR].ToFloat());
                t.SetTransmissionCoefficient(ele.Attributes[P.TransCoeff].ToFloat());
                t.Shadows = shad;
                if (s != null) t.SetSampler(s);
                return t;
            }
            else if (n == P.Mate.GlosRef)
            {
                GlossyReflective p = new GlossyReflective();
                p.SetSpecularColor(ele.Attributes[P.SpCol].ToVector3());
                p.SetSpecularRC(ele.Attributes[P.SpecCoeff].ToFloat());
                p.SetExp(ele.Attributes[P.Exp].ToFloat());
                p.SetCD(ele.Attributes[P.Col].ToVector3());
                p.SetAmbientRC(ele.Attributes[P.AmbRefCoeff].ToFloat());
                p.SetDiffuseRC(ele.Attributes[P.DiffRefCoeff].ToFloat());
                p.SetCR(ele.Attributes[P.GlosCol].ToVector3());
                p.SetKR(ele.Attributes[P.GlosCoeff].ToFloat());
                p.Shadows = shad;
                if (s != null) p.SetSampler(s);
                return p;
            }
            else if (n == P.Mate.Emis)
            {
                Emissive em = new Emissive();
                em.Color = ele.Attributes[P.Col].ToVector3();
                em.Radiance = ele.Attributes[P.Radi].ToFloat();
                em.Shadows = shad;
                if (s != null) em.SetSampler(s);
                return em;
            }
            else if (n == (P.Text + P.Mate.Matt))
            {
                SV_Matte p = new SV_Matte(ele.Attributes[P.AmbRefCoeff].ToFloat(), ele.Attributes[P.DiffRefCoeff].ToFloat(), textures[ele.Attributes[P.Text].Value]) { Shadows = shad };
                if (s != null) p.SetSampler(s);
                return p;
            }
            else if (n == (P.Text + P.Mate.Pho))
            {
                SV_Phong p = new SV_Phong();
                p.SetSpecularColor(textures[ele.Attributes[P.SpTxt].Value]);
                p.SetExp(ele.Attributes[P.Exp].ToFloat());
                p.SetCD(textures[ele.Attributes[P.Text].Value]);
                p.SetAmbientRC(ele.Attributes[P.AmbRefCoeff].ToFloat());
                p.SetDiffuseRC(ele.Attributes[P.DiffRefCoeff].ToFloat());
                p.Shadows = shad;
                if (s != null) p.SetSampler(s);
                return p;
            }
            throw new Exception(); //Create an exception for not found elements;
        }

        public static Camera CreateCameraFromElement(this RElement ele)
        {
            string n = ele.Name;
            if (n == P.Cam.Pin)
            {
                Pinhole p = new Pinhole();
                p.Position = ele.Attributes[P.Pos].ToVector3();
                p.Target = ele.Attributes[P.Tar].ToVector3();
                p.Distance = ele.Attributes[P.Dist].ToFloat();
                p.Zoom = ele.Attributes[P.Zoom].ToFloat();
                p.ExposureTime = 1;
                p.SetRollAngle(ele.Attributes["RollAngle"].ToFloat());
                return p;
            }
            throw new Exception(); //create notfound excep
        }

        public static Light CreateLightFromElement(this RElement ele, int ns)
        {
            bool shad = getShadow(ele);
            float rad = ele.Attributes[P.Radi].ToFloat();
            string n = ele.Name;
            if (n == P.Lights.Amb)
            {
                Ambient amb = new Ambient();
                amb.RadianceScale = rad;
                amb.Color = ele.Attributes[P.Col].ToVector3();
                amb.Shadows = shad;
                return amb;
            }
            else if (n == P.Lights.AmbOcc)
            {
                AmbientOccluder amb = new AmbientOccluder();
                amb.SetSampler(ele.Attributes[P.Sampler].CreateSamplerFromAttribute(ns));
                amb.RadianceScale = rad;
                amb.Color = ele.Attributes[P.Col].ToVector3();
                amb.Shadows = shad;
                return amb;
            }
            else if (n == P.Lights.PL)
            {
                PointLight pl = new PointLight();
                pl.Position = ele.Attributes[P.Pos].ToVector3();
                pl.RadianceScale = rad;
                pl.Color = ele.Attributes[P.Col].ToVector3();
                pl.Shadows = shad;
                return pl;
            }
            else if (n == P.Lights.Direc)
            {
                Directional d = new Directional();
                d.Direction = ele.Attributes[P.Dir].ToVector3();
                d.RadianceScale = rad;
                d.Color = ele.Attributes[P.Col].ToVector3();
                d.Shadows = shad;
                return d;
            }
            else if (n == P.Lights.AL)
            {
                AreaLight al = new AreaLight();
                al.SetObject(GeometricObject.GetObject(ele.Attributes[P.Name].Value));
                al.Shadows = shad;
                return al;
            }
            throw new Exception();
        }

        public static Tracer CreateTracerFromAttribute(this RAttribute att, World world)
        {
            string tr = att.Value;
            if (tr == "Whitted")
                return new Whitted(world);
            else if (tr == "RayCast")
                return new RayCast(world);
            else if (tr == "Global")
                return new GlobalTrace(world);
            else if (tr == "Area")
                return new AreaLighting(world);
            else if (tr == "Path")
                return new PathTrace(world);
            throw new Exception();
        }

        public static Sampler CreateSamplerFromAttribute(this RAttribute att, int ns)
        {
            string sampler = att.Value;
            if (sampler == "Regular")
                return new Regular(ns);
            else if (sampler == "Jittered")
                return new Jittered(ns);
            else if (sampler == "MultiJittered")
                return new MultiJittered(ns);
            else if (sampler == "NRooks")
                return new NRooks(ns);
            else if (sampler == "PureRandom")
                return new PureRandom(ns);
            throw new Exception();
        }

        public static Mapping CreateMappingFromAttribute(this RAttribute att)
        {
            string m = att.Value;
            if (m == "Hemispherical")
                return new HemisphericalMap();
            else if (m == "Spherical")
                return new SphericalMap();
            else if (m == "Rectangular")
                return new RectangularMap();
            throw new Exception();
        }

        public static Texture CreateTextureFromElement(this RElement ele, Dictionary<string, Image> images)
        {
            string typ = ele.Name;
            if (typ == "ConstantColor")
            {
                return new ConstantColor(ele.Attributes[P.Col].ToVector3());
            }
            else if (typ == "ImageTexture")
            {
                ImageTexture im = new ImageTexture();
                im.SetImage(images[ele.Attributes["ImageRef"].Value]);
                im.SetMapping(ele.Attributes[P.Map].CreateMappingFromAttribute());
                return im;
            }
            throw new Exception();
        }

        public static ViewPlane CreateViewPlaneFromElement(this RElement ele)
        {
            ViewPlane vp = new ViewPlane();
            vp.HRes = ele.Attributes[P.Width].ToInt();
            vp.VRes = ele.Attributes[P.Height].ToInt();
            vp.NumSamples = ele.Attributes[P.Samples].ToInt();
            vp.SetSampler(ele.Attributes[P.Sampler].CreateSamplerFromAttribute(vp.NumSamples));
            vp.MaxDepth = ele.Attributes[P.MaxDepth].ToInt();
            return vp;
        }

        public static Vector3 GetVector(string att)
        {
            string v = att;
            Vector3 vector = Vector3.Zero;
            try
            {
                if (v.Contains(','))
                {
                    string[] nums = v.Split(new string[] { ", ", ",", " , " }, StringSplitOptions.RemoveEmptyEntries);
                    vector.X = float.Parse(nums[0]);
                    vector.Y = float.Parse(nums[1]);
                    vector.Z = float.Parse(nums[2]);
                }
                else
                {
                    vector = new Vector3(float.Parse(v));
                }
            }
            catch (Exception)
            {
                throw new Exception("Incorrect vector format at function");
            }
            return vector;
        }

        public static Vector3 GetVector(RAttribute att)
        {
            string v = att.Value;
            Vector3 vector = Vector3.Zero;
            try
            {
                if (v.Contains(','))
                {
                    string[] nums = v.Split(new string[] { ", ", ",", " , " }, StringSplitOptions.RemoveEmptyEntries);
                    vector.X = float.Parse(nums[0]);
                    vector.Y = float.Parse(nums[1]);
                    vector.Z = float.Parse(nums[2]);
                }
                else
                {
                    vector = new Vector3(float.Parse(v));
                }
            }
            catch (Exception)
            {
                throw new Exception("Incorrect vector format at " + att.Name + " at " + att.Parent.Name);
            }
            return vector;
        }

        static string getName(RElement ele)
        {
            return ele.HasAttribute("Name") ? ele.Attributes[P.Name].Value : getUniqueName();
        }

        static bool getShadow(RElement ele)
        {
            if (ele.HasAttribute(P.Shad))
                return ele.Attributes[P.Shad].ToBoolean();
            return true;
        }

        static bool getInstance(RElement ele)
        {
            if (ele.HasAttribute(P.Obj.Ins))
                return bool.Parse(ele.Attributes[P.Obj.Ins].Value);
            return false;
        }

        static Material getMaterial(RElement ele, Dictionary<string, Texture> texts)
        {
            if (ele.HasAttribute(P.Mat))
            {
                throw new Exception(); //Create exception for non-matching values or spelling mistakes.
            }
            else
            {
                if (ele.HasElement(ele.Name + "." + P.Mat))
                {
                    if (ele.GetElement(ele.Name + "." + P.Mat).Elements.Count > 0)
                    {
                        return ele.GetElement(ele.Name + "." + P.Mat).Elements[0].CreateMaterialFromElement(texts);
                    }
                    else
                    {
                        throw new Exception(); //Create exception for elements not found.
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        static string getUniqueName()
        {
            string name = "";
            do
            {
                name = "";
                for (int i = 0; i < 12; i++)
                {
                    name += ((char)P.Random.Next(66, 120));
                }
            }
            while (GeometricObject.Contains(name));
            return name;
        }

        public static float ToFloat(this RAttribute att)
        {
            try
            {
                float r = float.Parse(att.Value);
                return r;
            }
            catch (Exception)
            {
                throw new Exception("Incorrect float format at " + att.Name + " at " + att.Parent.Name);
            }
        }

        public static int ToInt(this RAttribute att)
        {
            return int.Parse(att.Value);
        }

        public static bool ToBoolean(this RAttribute att)
        {
            return bool.Parse(att.Value);
        }

        public static Vector3 ToVector3(this RAttribute att)
        {
            return GetVector(att);
        }

        public static Matrix4 ToMatrix4(this RAttribute att)
        {
            string v = att.Value;
            if (v == "Id")
                return Matrix4.Identity;
            else
            {
                string[] s = v.Split(new string[] { ", ", ",", " , " }, StringSplitOptions.RemoveEmptyEntries);
                if (s.Length < 12 || (s.Length > 12 && s.Length < 16))
                    throw new Exception(); //Make exception
                Matrix4 res = Matrix4.Identity;
                for (int r = 0; r < (s.Length == 12 ? 3 : 4); r++)
                    for (int c = 0; c < 4; c++)
                    {
                        res[r, c] = float.Parse(s[c + r * (s.Length == 12 ? 3 : 4)]);
                    }
                return res;
            }
        }

        public static Vector3 ToVector3(this string att)
        {
            return GetVector(att);
        }

        static void transform(Instance ins, RElement ele)
        {
            if (ele.HasElement("Instance.Transforms"))
                foreach (RElement el in ele.GetElement("Instance.Transforms").Elements)
                    if (el.Name == P.Trans)
                    {
                        ins.Transform(el.Attributes[P.Matr].ToMatrix4());
                    }
        }

        static void getVertices(Mesh mesh, RElement ele)
        {
            foreach (RElement xe in ele.GetElement(P.Obj.Mesh + ".Vertices").Elements)
            {
                mesh.Vertices.Add(xe.Attributes[P.Pos].ToVector3());
            }
        }

        static void getFaces(Mesh mesh, RElement ele)
        {
            bool hasNormals = ele.HasElement("Mesh.Normals");
            int i = -1;
            foreach (RElement xe in ele.GetElement(P.Obj.Mesh + ".Faces").Elements)
            {
                i++;
                string[] w = xe.Attributes["Indices"].Value.Replace(" ", "").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (hasNormals)
                {
                    SmoothMeshTriangle fmt = new SmoothMeshTriangle(int.Parse(w[0]), int.Parse(w[1]), int.Parse(w[2]), mesh, i + "" + "smsh" + "Mesh" + mesh.Name);
                    mesh.AddObject(fmt);
                }
                else
                {
                    FlatMeshTriangle fmt = new FlatMeshTriangle(int.Parse(w[0]), int.Parse(w[1]), int.Parse(w[2]), mesh, i + "" + "fmsh" + "Mesh" + mesh.Name);
                    fmt.ComputeNormal(false);
                    mesh.AddObject(fmt);
                }
            }
        }

        static void getNormals(Mesh mesh, RElement ele)
        {
            bool hasNormals = ele.HasElement("Mesh.Normals");
            if (hasNormals)
            {
                foreach (RElement xe in ele.GetElement(P.Obj.Mesh + ".Normals").Elements)
                {
                    mesh.Normals.Add(xe.Attributes["Vector"].ToVector3());
                }
            }
        }

    }
}
