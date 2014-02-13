using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Raytracer.Rml;
using Disque.Math;
using Disque.Raytracer.Worlds;
using Disque;
using Disque.Raytracer.Math;
using System.IO;
using Disque.Raytracer.Lights;
using Disque.Raytracer.Samplers;
using Disque.Raytracer.Tracers;
using Disque.Raytracer.Cameras;
using Disque.Raytracer.GeometricObjects.Primitives;
using Disque.Raytracer.Materials;
using Disque.Raytracer.GeometricObjects.CompoundObjects;
using Disque.Raytracer.GeometricObjects.Triangles;

namespace TesterConsole
{
    class Program
    {
        static World world2;
        static void Main(string[] args)
        {
            #region a
            {
                new Parser().Parse(@"world
{
  object : sphere : 'name'
  {
    position = (0);
    radius = (0.5);
  }
  freeobject : sphere : 'name'
  {
  }
  object : mesh : 'duck'
  {
    vertices
    {
      
    }
    material : reflective
    {
    }
  }
  
}");
            }
            #endregion
            #region b
            RmlParser rml = new RmlParser(File.ReadAllText(@"C:\Users\Belal\Downloads\bunny.txt"));
            #endregion
            World world = rml.Compile();
            //Console.WriteLine(((PointLight)world.Lights[0]).RadianceScale);
            //world.RenderScene();
            //OBJReader obj = new OBJReader(File.ReadAllText(@"C:\Users\Belal\Downloads\bunny.obj.txt"));
            //File.WriteAllText(@"C:\Users\Belal\Downloads\bunny.txt", obj.ToRml());
            //build1(1);
            //world2.RenderScene();
            //float[] sol = new float[4];
            //int i = MathHelper.SolveQuartic(new float[] { 3, 6, -123, -126, 1080 }, sol);
            Vector3 v = Vector3.Zero;
            v.X = 4;
            v.Y = 5;
            v.Z = 1;
            Console.WriteLine(v);
            v = new Vector3(4);
            Console.WriteLine(v);
            Console.ReadKey();
        }

        static void build1(int ns)
        {
            world2 = new World();
            world2.ViewPlane.HRes = 400;
            world2.ViewPlane.VRes = 400;
            world2.ViewPlane.MaxDepth = 5;
            world2.ViewPlane.SetSamples(ns);
            world2.Tracer = new Whitted(world2);
            Ambient occ = new Ambient();
            occ.RadianceScale = 0f;
            occ.Shadows = true;
            occ.Color = Vector3.One;
            world2.AmbientLight = occ;
            Pinhole cam = new Pinhole();
            cam.Position = new Vector3(0, 60, 120);
            cam.Distance = 100;
            cam.Zoom = 1400;
            world2.Camera = cam;
            PointLight pl = new PointLight();
            pl.Color = Vector3.One;
            pl.Position = new Vector3(0, 1000, 0);
            pl.RadianceScale = 1.0f;
            pl.Shadows = true;
            world2.Lights.Add(pl);
            OBJReader obj = new OBJReader(File.ReadAllText(@"C:\Users\Belal\Downloads\bunny.obj.txt"));
            Mesh mesh = obj.GetMesh();
            mesh.SetShadows(false);
            mesh.SetMaterial(Material.Glass);
            world2.Objects.Add(mesh);
            BBox bb = mesh.GetBoundingBox();
            cam.Target = (bb.Min + bb.Max) * 0.5f;
            Matte matte2 = new Matte(0.5f, 0.5f, new Vector3(1, 1, 1));
            matte2.Shadows = true;
            Plane p = new Plane(new Vector3(0, -20, 0), new Vector3(0, 1, 0), "p1");
            p.SetShadows(true);
            p.SetMaterial(matte2);
            world2.Objects.Add(p);
        }

        void build10()
        {
            World world = new World();
            int ns = 49;
            world.ViewPlane.HRes = 400;
            world.ViewPlane.VRes = 400;
            world.ViewPlane.SetSampler(new Regular(ns));
            world.ViewPlane.MaxDepth = 10;
            world.Tracer = new Whitted(world);
            AmbientOccluder occ = new AmbientOccluder();
            occ.RadianceScale = 1.0f;
            occ.MinAmount = 0f;
            occ.Shadows = true;
            occ.Color = Vector3.One;
            occ.SetSampler(new MultiJittered(ns));
            world.AmbientLight = occ;
            Pinhole cam = new Pinhole();
            cam.Position = new Vector3(40, 30, 20);
            cam.Target = new Vector3(0, 0, 0);
            cam.Distance = 5500;
            cam.Zoom = 1.5f;
            world.Camera = cam;
            PointLight pl = new PointLight();
            pl.Color = Vector3.One;
            pl.Position = new Vector3(4, 4, 0);
            pl.RadianceScale = 1.0f;
            pl.Shadows = true;
            world.Lights.Add(pl);
        }

    }
}
