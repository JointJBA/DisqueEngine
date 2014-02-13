using System;
using System.Xml.Linq;
using Disque.Math;
using Disque.Raytracer.Cameras;
using Disque.Raytracer.GeometricObjects;
using Disque.Raytracer.GeometricObjects.Primitives;
using Disque.Raytracer.Lights;
using Disque.Raytracer.Materials;
using Disque.Raytracer.Samplers;
using Disque.Raytracer.Tracers;
using Disque.Raytracer.Worlds;
using Disque.Raytracer.Textures;
using Disque.Raytracer.GeometricObjects.CompoundObjects;
using System.Collections.Generic;
using Disque.Raytracer.Mappings;

namespace Disque.Raytracer.Rml
{
    public class RmlReader
    {
        public static readonly Dictionary<string, Image> Images = new Dictionary<string, Image>();
        static readonly Dictionary<string, Texture> textures = new Dictionary<string, Texture>();

        public static World Compile(string file, ref string directory)
        {
            World world = new World();
            XDocument xdoc = XDocument.Parse(file);
            directory = getAttribute("Directory", xdoc.Element("World"));
            world.Tracer = getTracer(getAttribute("Tracer", xdoc.Element("World")), world);
            foreach(XElement xele in xdoc.Element("World").Elements())
            {
                if (xele.Name == "ViewPlane")
                    getViewPlane(world, xele);
                else if (xele.Name == "Camera")
                {
                    foreach (XElement ele in xele.Elements())
                    {
                        world.Camera = getCamera(ele);
                        break;
                    }
                }
                else if (xele.Name == "Textures")
                {
                    getTextures(xele);
                }
                else if (xele.Name == "EnviromentalLight")
                {
                    foreach (XElement ele in xele.Elements())
                    {
                        world.AmbientLight = getLight(ele, world);
                        break;
                    }
                }
                else if (xele.Name == "Lights")
                    getLights(world, xele);
                else if (xele.Name == "Objects")
                    getObjects(world, xele);
            }
            textures.Clear();
            return world;
        }

        static Material getMaterial(XElement xele)
        {
            foreach (XElement ele in xele.Elements())
            {
                string typ = ele.Name.LocalName;
                if (typ == "Matte")
                {
                    return new Matte(float.Parse(getAttribute("ReflectionCoff", ele)), float.Parse(getAttribute("DiffuseCoff", ele)), getVector(getAttribute("Color", ele))) { Shadows = bool.Parse(getAttribute("Shadows", ele)) };
                }
                else if (typ == "Phong")
                {
                    Phong p = new Phong();
                    p.SetSpecularColor(getVector(getAttribute("SpecularColor", ele)));
                    p.SetExp(float.Parse(getAttribute("Exponent", ele)));
                    p.SetCD(getVector(getAttribute("Color", ele)));
                    p.SetAmbientRC(float.Parse(getAttribute("AmbientRelfectionCoff", ele)));
                    p.SetDiffuseRC(float.Parse(getAttribute("DiffuseRelfectionCoff", ele)));
                    p.Shadows = bool.Parse(getAttribute("Shadows", ele));
                    return p;
                }
                else if (typ == "Reflective")
                {
                    Reflective r = new Reflective();
                    r.SetSpecularColor(getVector(getAttribute("SpecularColor", ele)));
                    r.SetSpecularRC(float.Parse(getAttribute("SpecularCoff", ele)));
                    r.SetRColor(getVector(getAttribute("ReflectiveColor", ele)));
                    r.SetReflectiveRC(float.Parse(getAttribute("ReflectiveCoff", ele)));
                    r.SetExp(float.Parse(getAttribute("Exponent", ele)));
                    r.SetCD(getVector(getAttribute("Color", ele)));
                    r.SetAmbientRC(float.Parse(getAttribute("AmbientRelfectionCoff", ele)));
                    r.SetDiffuseRC(float.Parse(getAttribute("DiffuseRelfectionCoff", ele)));
                    r.Shadows = bool.Parse(getAttribute("Shadows", ele));
                    return r;
                }
                else if (typ == "Transparent")
                {
                    Transparent t = new Transparent();
                    t.SetSpecularColor(getVector(getAttribute("SpecularColor", ele)));
                    t.SetSpecularRC(float.Parse(getAttribute("SpecularCoff", ele)));
                    t.SetReflectiveRC(float.Parse(getAttribute("ReflectiveCoff", ele)));
                    t.SetExp(float.Parse(getAttribute("Exponent", ele)));
                    t.SetCD(getVector(getAttribute("Color", ele)));
                    t.SetAmbientRC(float.Parse(getAttribute("AmbientRelfectionCoff", ele)));
                    t.SetDiffuseRC(float.Parse(getAttribute("DiffuseRelfectionCoff", ele)));
                    t.SetIndexOfRefraction(float.Parse(getAttribute("IOR", ele)));
                    t.SetTransmissionCoefficient(float.Parse(getAttribute("TransCoff", ele)));
                    t.Shadows = bool.Parse(getAttribute("Shadows", ele));
                    return t;
                }
                else if (typ == "TextureMatte")
                {
                    return new SV_Matte(float.Parse(getAttribute("ReflectionCoff", ele)), float.Parse(getAttribute("DiffuseCoff", ele)), textures[getAttribute("Texture", ele)]);
                }
                else if (typ == "TexturePhong")
                {
                    SV_Phong p = new SV_Phong();
                    p.SetSpecularColor(textures[getAttribute("SpecularTexture", ele)]);
                    p.SetExp(float.Parse(getAttribute("Exponent", ele)));
                    p.SetCD(textures[getAttribute("Texture", ele)]);
                    p.SetAmbientRC(float.Parse(getAttribute("AmbientRelfectionCoff", ele)));
                    p.SetDiffuseRC(float.Parse(getAttribute("DiffuseRelfectionCoff", ele)));
                    p.Shadows = bool.Parse(getAttribute("Shadows", ele));
                    return p;
                }
                break;
            }
            throw new ElementNotFound("Material");
        }

        static void getTextures(XElement xele)
        {
            foreach (XElement ele in xele.Elements())
            {
                textures.Add(getAttribute("Name", ele), getTexture(ele));
            }
        }

        static Texture getTexture(XElement ele)
        {
            string typ = ele.Name.LocalName;
            if (typ == "ConstantColor")
            {
                return new ConstantColor(getVector(getAttribute("Color", ele)));
            }
            else if (typ == "ImageTexture")
            {
                ImageTexture im = new ImageTexture();
                im.SetImage(Images[getAttribute("ImageRef", ele)]);
                im.SetMapping(getMapping(getAttribute("Mapping", ele), ele));
                return im;
            }
            throw new Exception("No recognized element.");

        }

        static Mapping getMapping(string m, XElement ele)
        {
            if (m == "Hemispherical")
                return new HemisphericalMap();
            else if (m == "Spherical")
                return new SphericalMap();
            else if (m == "Rectangular")
                return new RectangularMap();
            throw new Exception();
        }

        static void getObjects(World world, XElement ele)
        {
            foreach (XElement xele in ele.Elements())
            {
                bool instance = false;
                GeometricObject go = getPObject(xele, ref instance);
                if (!instance)
                    world.Objects.Add(go);
            }
        }

        static GeometricObject getPObject(XElement ele, ref bool instance)
        {
            string typ = ele.Name.LocalName;
            if (ele.Attribute("Instance") != null)
                instance = bool.Parse(getAttribute("Instance", ele));
            else
                instance = false;
            if (typ == "Sphere")
            {
                Sphere sph = new Sphere(getVector(getAttribute("Position", ele)), float.Parse(getAttribute("Radius", ele)), getAttribute("Name", ele));
                sph.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                sph.SetMaterial(getMaterial(ele));
                return sph;
            }
            else if (typ == "Box")
            {
                Box b = new Box(getVector(getAttribute("Min", ele)), getVector(getAttribute("Max", ele)), getAttribute("Name", ele));
                b.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                b.SetMaterial(getMaterial(ele));
                return b;
            }
            else if (typ == "Plane")
            {
                Plane p = new Plane(getVector(getAttribute("Position", ele)), getVector(getAttribute("Direction", ele)), getAttribute("Name", ele));
                p.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                p.SetMaterial(getMaterial(ele));
                return p;
            }
            else if (typ == "Disk")
            {
                Disk d = new Disk(getVector(getAttribute("Center", ele)), getVector(getAttribute("Direction", ele)), float.Parse(getAttribute("Radius", ele)), getAttribute("Name", ele));
                d.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                d.SetMaterial(getMaterial(ele));
                return d;
            }
            else if (typ == "Annulus")
            {
                Annulus d = new Annulus(getVector(getAttribute("Center", ele)), getVector(getAttribute("Direction", ele)), float.Parse(getAttribute("InnerRadius", ele)), float.Parse(getAttribute("OuterRadius", ele)), getAttribute("Name", ele));
                d.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                d.SetMaterial(getMaterial(ele));
                return d;
            }
            else if (typ == "Triangle")
            {
                Triangle t = new Triangle(getVector(getAttribute("P1", ele)), getVector(getAttribute("P2", ele)), getVector(getAttribute("P3", ele)), getAttribute("Name", ele));
                t.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                t.SetMaterial(getMaterial(ele));
                return t;
            }
            else if (typ == "SmoothTriangle")
            {
                SmoothTriangle t = new SmoothTriangle(getVector(getAttribute("P1", ele)), getVector(getAttribute("P2", ele)), getVector(getAttribute("P3", ele)), getAttribute("Name", ele));
                t.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                t.SetMaterial(getMaterial(ele));
                return t;
            }
            else if (typ == "Rectangle")
            {
                Rectangle r = new Rectangle(getVector(getAttribute("P", ele)), getVector(getAttribute("A", ele)), getVector(getAttribute("B", ele)), getAttribute("Name", ele));
                r.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                r.SetMaterial(getMaterial(ele));
                return r;
            }
            else if (typ == "OpenCylinder")
            {
                OpenCylinder oc = new OpenCylinder(float.Parse(getAttribute("Bottom", ele)), float.Parse(getAttribute("Top", ele)), float.Parse(getAttribute("Radius", ele)), getAttribute("Name", ele));
                oc.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                oc.SetMaterial(getMaterial(ele));
                return oc;
            }
            else if (typ == "OpenCone")
            {
                OpenCone oc = new OpenCone(float.Parse(getAttribute("Radius", ele)), float.Parse(getAttribute("Height", ele)), getAttribute("Name", ele));
                oc.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                oc.SetMaterial(getMaterial(ele));
                return oc;
            }
            else if (typ == "ConvexOpenCylinder")
            {
                ConvexOpenCylinder oc = new ConvexOpenCylinder(float.Parse(getAttribute("Bottom", ele)), float.Parse(getAttribute("Top", ele)), float.Parse(getAttribute("Radius", ele)), getAttribute("Name", ele));
                oc.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                oc.SetMaterial(getMaterial(ele));
                return oc;
            }
            else if (typ == "Instance")
            {
                Instance ins = new Instance(GeometricObject.GetObject(getAttribute("ObjectRef", ele)), getAttribute("Name", ele));
                ins.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                return ins;
            }
            else
            {
                return getCObject(ele);
            }
        }

        static GeometricObject getCObject(XElement ele)
        {
            string typ = ele.Name.LocalName;
            if (typ == "Group")
            {
                bool va = false;
                Group group = new Group(getAttribute("Name", ele));
                foreach (XElement xele in ele.Elements())
                    group.AddObject(getPObject(ele, ref va));
                group.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                return group;
            }
            else if (typ == "SolidCone")
            {
                SolidCone oc = new SolidCone(float.Parse(getAttribute("Radius", ele)), float.Parse(getAttribute("Height", ele)), getAttribute("Name", ele));
                oc.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                oc.SetMaterial(getMaterial(ele));
                return oc;
            }
            else if (typ == "SolidCylinder")
            {
                SolidCylinder oc = new SolidCylinder(float.Parse(getAttribute("Bottom", ele)), float.Parse(getAttribute("Top", ele)), float.Parse(getAttribute("Radius", ele)), getAttribute("Name", ele));
                oc.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                oc.SetMaterial(getMaterial(ele));
                return oc;
            }
            else if (typ == "ThickAnnulus")
            {
                ThickAnnulus oc = new ThickAnnulus(float.Parse(getAttribute("Bottom", ele)), float.Parse(getAttribute("Top", ele)), float.Parse(getAttribute("InnerRadius", ele)), float.Parse(getAttribute("OuterRadius", ele)), getAttribute("Name", ele));
                oc.SetShadows(bool.Parse(getAttribute("Shadows", ele)));
                oc.SetMaterial(getMaterial(ele));
                return oc;
            }
            throw new Exception("No recognized elements");
        }

        static void getLights(World world, XElement ele)
        {
            foreach (XElement xele in ele.Elements())
            {
                world.Lights.Add(getLight(xele, world));
            }
        }

        static Light getLight(XElement ele, World world)
        {
            Light l = new Ambient();
            string typ = ele.Name.LocalName;
            if (typ == "Ambient")
            {
                Ambient amb = new Ambient();
                amb.Color = getVector(getAttribute("Color", ele));
                amb.RadianceScale = float.Parse(getAttribute("Radiance", ele));
                l = amb;
            }
            else if (typ == "AmbientOcculder")
            {
                AmbientOccluder amb = new AmbientOccluder();
                amb.Color = getVector(getAttribute("Color", ele));
                amb.RadianceScale = float.Parse(getAttribute("Radiance", ele));
                amb.SetSampler(getSampler(getAttribute("Sampler", ele), world.ViewPlane.NumSamples));
                l = amb;
            }
            else if (typ == "PointLight")
            {
                PointLight p = new PointLight();
                p.Color = getVector(getAttribute("Color", ele));
                p.RadianceScale = float.Parse(getAttribute("Radiance", ele));
                p.Position = getVector(getAttribute("Position", ele));
                l = p;
            }
            else if (typ == "Directional")
            {
                Directional d = new Directional();
                d.Color = getVector(getAttribute("Color", ele));
                d.RadianceScale = float.Parse(getAttribute("Radiance", ele));
                d.Direction = getVector(getAttribute("Direction", ele));
                l = d;
            }
            l.Shadows = bool.Parse(getAttribute("Shadows", ele));
            return l;
        }

        static void getViewPlane(World world, XElement ele)
        {
            world.ViewPlane.VRes = int.Parse(getAttribute("Height", ele));
            world.ViewPlane.HRes = int.Parse(getAttribute("Width", ele));
            world.ViewPlane.NumSamples = int.Parse(getAttribute("Samples", ele));
            world.ViewPlane.S = float.Parse(getAttribute("PixelSize", ele));
            world.ViewPlane.MaxDepth = int.Parse(getAttribute("MaxDepth", ele));
            world.ViewPlane.SetSampler(getSampler(getAttribute("Sampler", ele), world.ViewPlane.NumSamples));
        }

        static Tracer getTracer(string tr, World world)
        {
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
            throw new NotCorrectValue("World", "Tracer");
        }

        static Camera getCamera(XElement ele)
        {
            string typ = ele.Name.LocalName;
            if (typ == "Pinhole")
            {
                Pinhole hole = new Pinhole();
                hole.Position = getVector(getAttribute("Position", ele));
                hole.Target = getVector(getAttribute("Target", ele));
                hole.Distance = float.Parse(getAttribute("Distance", ele));
                hole.Zoom = float.Parse(getAttribute("Zoom", ele));
                hole.ExposureTime = float.Parse(getAttribute("ExposureTime", ele));
                hole.SetRollAngle(float.Parse(getAttribute("RollAngle", ele)));
                return hole;
            }
            throw new ElementNotFound("Camera");
        }

        static Vector3 getVector(string vectors)
        {
            Vector3 vector = Vector3.Zero;
            try
            {
                string[] nums = vectors.Split(new char[1] { ',' });
                vector.X = float.Parse(nums[0]);
                vector.Y = float.Parse(nums[1]);
                vector.Z = float.Parse(nums[2]);
            }
            catch (Exception)
            {
                throw new IncorrectVectorFormat();
            }
            return vector;
        }

        static Sampler getSampler(string sampler, int ns)
        {
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
            throw new NotCorrectValue("ViewPlane", "Sampler");
        }

        static string getAttribute(string att, XElement ele)
        {
            string rvalue;
            try
            {
                rvalue = ele.Attribute(att).Value;
            }
            catch (Exception)
            {
                throw new PropertyNotFound(ele.Name.NamespaceName, att);
            }
            return rvalue;
        }

        public static void Clear()
        {
            Images.Clear();
        }
    }
}
