using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Worlds;
using System.IO;
using System.Xml.Linq;
using Disque.Raytracer.Tracers;
using Disque.Raytracer.GeometricObjects;
using Disque.Raytracer.Textures;

namespace Disque.Raytracer.Rml
{
    public class RmlParser
    {
        public readonly Dictionary<string, Image> Images = new Dictionary<string, Image>();

        readonly Dictionary<string, Texture> textures = new Dictionary<string, Texture>();

        string file;

        Random random = new Random();

        public RmlParser(string data)
        {
            Parse(data);
        }

        public RmlParser()
        {
        }

        public void Parse(string data)
        {
            textures.Clear();
            file = FunctionParser.ParseFunctions(data);
            root = getRoot();
        }

        public static RmlParser Load(string uri)
        {
            return new RmlParser(File.ReadAllText(uri));
        }

        public World Compile()
        {
            World world = new World();
            getWorldAttributes(world);
            getTextures();
            getObjects(world);
            getLights(world);
            return world;
        }

        RElement root;

        RElement getRoot()
        {
            XDocument xdoc = XDocument.Parse(file);
            return getElement(xdoc.Root);
        }

        RElement getElement(XElement ele)
        {
            RElement ret = new RElement(ele.Name.LocalName);
            ret.Value = ele.Value;
            if (ele.HasElements)
            {
                ret.HasElements = true;
                foreach (XElement xele in ele.Elements())
                    ret.Elements.Add(getElement(xele));
            }
            if (ele.HasAttributes)
            {
                ret.HasAttributes = true;
                foreach (XAttribute att in ele.Attributes())
                    ret.Attributes.Add(att.Name.LocalName, getAttribute(ret, att));
            }
            return ret;
        }

        RAttribute getAttribute(RElement p, XAttribute ele)
        {
            return new RAttribute() { Parent = p, Name = ele.Name.LocalName, Value = ele.Value };
        }

        void getWorldAttributes(World w)
        {
            if (root.HasAttribute("Background"))
                w.Background = Extensions.GetVector(root.Attributes["Background"]);
            w.Tracer = root.Attributes["Tracer"].CreateTracerFromAttribute(w);
            w.ViewPlane = root.GetElement("World.ViewPlane").CreateViewPlaneFromElement();
            w.Camera = root.GetElement("World.Camera").Elements[0].CreateCameraFromElement();
            getEnvLight(w);
        }

        void getObjects(World world)
        {
            foreach (RElement ele in root.GetElement("Objects").Elements)
            {
                bool add = true;
                GeometricObject go = ele.CreateObjectFromElement(textures, ref add);
                if (add)
                    world.Objects.Add(go);
                else
                    world.FreeObjects.Add(go);
            }
        }

        void getLights(World world)
        {
            if (root.HasElement("Lights"))
                foreach (RElement ele in root.GetElement("Lights").Elements)
                    world.Lights.Add(ele.CreateLightFromElement(world.ViewPlane.NumSamples));
        }

        void getTextures()
        {
            if (root.HasElement("Textures"))
            {
                foreach (RElement ele in root.GetElement("Textures").Elements)
                {
                    textures.Add(ele.Attributes["Name"].Value, ele.CreateTextureFromElement(Images));
                }
            }
        }

        void getEnvLight(World w)
        {
            w.AmbientLight = root.GetElement("World.AmbientLight").Elements[0].CreateLightFromElement(w.ViewPlane.NumSamples);
        }
    }
}
