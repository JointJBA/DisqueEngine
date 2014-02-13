using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Disque.Raytracer.GeometricObjects;
using Disque.Raytracer.Materials;
using Disque.Math;
using Disque.Raytracer.GeometricObjects.CompoundObjects;
using Disque.Raytracer.Lights;
using Disque.Raytracer.GeometricObjects.Primitives;
using Disque.Raytracer.Tracers;
using Disque.Raytracer.Samplers;
using Disque.Raytracer.Cameras;
using Disque.Raytracer.Worlds;
using System.IO;
using System.Windows.Threading;
using System.Threading;
using Disque.Raytracer.Textures;
using Disque.Raytracer.Math;
using Disque.Raytracer.GeometricObjects.Triangles;
using Disque.Raytracer.Rml;
using Disque;
using Disque.Raytracer.BRDFs;

namespace RenderToy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        World world = new World();
        DispatcherTimer dt = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                build6(16);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            dt.Interval = TimeSpan.FromMilliseconds(1);
            dt.Start();
            dt.Tick += new EventHandler(dt_Tick);
            new Thread(world.RenderScene).Start();
        }

        void dt_Tick(object sender, EventArgs e)
        {
            progress.Value = 100.0 * ((double)world.Screen.Pixels.Count / (double)(world.ViewPlane.HRes * world.ViewPlane.VRes));
            progresstext.Text = world.Screen.Pixels.Count + " / " + (world.ViewPlane.HRes * world.ViewPlane.VRes);
            if (progress.Value == 100)
            {
                dt.Stop();
                RenderImage(world);
            }
        }

        void build1(int ns)
        {
            world.ViewPlane.HRes = 400;
            world.ViewPlane.VRes = 400;
            world.ViewPlane.MaxDepth = 5;
            world.ViewPlane.SetSamples(ns);
            world.Tracer = new Whitted(world);
            Ambient occ = new Ambient();
            occ.RadianceScale = 0f;
            occ.Shadows = true;
            occ.Color = Vector3.One;
            world.AmbientLight = occ;
            Pinhole cam = new Pinhole();
            cam.Position = new Vector3(0, 60, 120);
            cam.Distance = 100;
            cam.Zoom = 1400;
            world.Camera = cam;
            PointLight pl = new PointLight();
            pl.Color = Vector3.One;
            pl.Position = new Vector3(0, 1000, 0);
            pl.RadianceScale = 1.0f;
            pl.Shadows = true;
            world.Lights.Add(pl);
            OBJReader obj = new OBJReader(File.ReadAllText(@"C:\Users\Belal\Downloads\bunny.obj.txt"));
            Mesh mesh = obj.GetMesh();
            mesh.SetShadows(false);
            mesh.SetMaterial(Material.Glass);
            world.Objects.Add(mesh);
            BBox bb = mesh.GetBoundingBox();
            cam.Target = (bb.Min + bb.Max) * 0.5f;
            Matte matte2 = new Matte(0.5f, 0.5f, new Vector3(1, 1, 1));
            matte2.Shadows = true;
            Plane p = new Plane(new Vector3(0, -20, 0), new Vector3(0, 1, 0), "p1");
            p.SetShadows(true);
            p.SetMaterial(matte2);
            world.Objects.Add(p);
        }

        void build2()
        {
            world = new RmlParser(@"<World Tracer='Whitted'>
  <World.ViewPlane Width='400' Height='400' Samples='1' PixelSize='1' MaxDepth='5' Sampler='Regular'/>
  <World.AmbientLight>
    <Ambient Color='1,1,1' Radiance='0' Shadows='True'/>
  </World.AmbientLight>
  <World.Camera>
    <Pinhole Position='0,60,120' Target='-0.01680081,0.09349851,-0.001482265' Zoom='1400' Distance='100' ExposureTime='1' RollAngle='0'/>
  </World.Camera>
  <Lights>
    <PointLight Color='1,1,1' Position='0,1000,0' Shadows='True' Radiance='1'/>
  </Lights>
  <Objects>
    <Plane Position='0,-20,0' Direction='0,1,0' Shadows='True'>
      <Plane.Material>
        <Matte AmbientCoeff='0.5' DiffuseCoeff='0.5' Color='1' Shadows='True'/>
      </Plane.Material>
    </Plane>
  </Objects>
</World>").Compile();
        }

        void build6(int ns)
        {
            try
            {
                world.ViewPlane.HRes = 400;
                world.ViewPlane.VRes = 400;
                world.ViewPlane.MaxDepth = 5;
                world.ViewPlane.SetSamples(ns);
                world.Tracer = new Whitted(world);
                Ambient occ = new Ambient();
                occ.RadianceScale = 1.0f;
                //occ.MinAmount = 0f;
                occ.Shadows = true;
                occ.Color = Vector3.One;
                //occ.SetSampler(new MultiJittered(ns));
                world.AmbientLight = occ;
                Pinhole cam = new Pinhole();
                cam.Position = new Vector3(40, 30, 20);
                cam.Target = new Vector3(0, 0, 0);
                cam.Distance = 5000;
                cam.Zoom = 1;
                world.Camera = cam;
                Plastic plas = new Plastic();
                Vector3 color = new Vector3(1);
                float ls = 0.8f;
                plas.ambient = new Lambertian() { Color = color, ReflectionCoefficient = ls };
                plas.diffuse = new Lambertian() { Color = color, ReflectionCoefficient = ls };
                plas.specular = new GlossySpecular() { SpecularColor = color, ReflectionCoefficient = ls, Exponent = 500f };
                Sphere sp = new Sphere(new Vector3(0, 0.5f, 0), 0.5f, "sp");
                sp.SetMaterial(plas);
                world.Objects.Add(sp);
                Matte matte2 = new Matte(0.75f, 0.5f, new Vector3(1, 1, 1));
                matte2.Shadows = true;
                Plane p = new Plane(Vector3.Zero, new Vector3(0, 1, 0), "p1");
                p.SetShadows(true);
                p.SetMaterial(matte2);
                world.Objects.Add(p);
                PointLight pl = new PointLight();
                pl.Color = Vector3.One;
                pl.Position = new Vector3(0, 4, 0);
                pl.RadianceScale = 1.0f;
                pl.Shadows = true;
                world.Lights.Add(pl);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);
                MessageBox.Show(e.TargetSite.ToString());
            }
        }

        void build10()
        {
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
            Sphere ss = new Sphere(new Vector3(2f, 0.5f, 1f), 0.5f, "MainPlane");
            ss.SetShadows(true);
            ss.SetMaterial(new Matte(0.25f, 0.5f, new Vector3(1f, 1f, 1f)) { Shadows = true });
            world.Objects.Add(ss);
            Matte matte = new Matte(0.25f, 0.5f, new Vector3(0.5f, 0, 0.5f));
            matte.Shadows = true;
            Box s = new Box(new Vector3(0, 0, 1), new Vector3(1, 1, 2), "Box1");
            s.SetShadows(true);
            s.SetMaterial(matte);
            world.Objects.Add(s);
            Matte m2 = new Matte(0.25f, 0, new Vector3(1, 1, 0));
            m2.Shadows = true;
            Box b2 = new Box(new Vector3(1, 1, 0), new Vector3(2, 2, 1), "Box2");
            b2.SetMaterial(m2);
            b2.SetShadows(true);
            //world.Objects.Add(b2);
            Matte matte2 = new Matte(0.75f, 0.5f, new Vector3(1, 1, 1));
            matte2.Shadows = true;
            Plane p = new Plane(Vector3.Zero, new Vector3(0, 1, 0), "Plane2");
            p.SetShadows(true);
            p.SetMaterial(matte2);
            world.Objects.Add(p);
            Matte m5 = new Matte(0.75f, 0.1f, new Vector3(0, 0.5f, 1));
            m5.Shadows = true;
            Annulus an = new Annulus(new Vector3(1, 1.2f, 0), Vector3.Up, 0.4f, 0.2f, "Annulus");
            an.SetMaterial(m5);
            an.SetShadows(true);
            world.Objects.Add(an);
            Matte m6 = new Matte(0.75f, 0.1f, new Vector3(1, 0, 0.4f));
            m6.Shadows = true;
            SolidCylinder sc = new SolidCylinder(0, 1, 0.5f, "Cs");
            sc.SetShadows(true);
            sc.SetMaterial(m6);
            world.Objects.Add(sc);
            PointLight pl = new PointLight();
            pl.Color = Vector3.One;
            pl.Position = new Vector3(4, 4, 0);
            pl.RadianceScale = 1.0f;
            pl.Shadows = true;
            world.Lights.Add(pl);
            float exp = 2000;
            Transparent sphere_material = new Transparent();
            sphere_material.SetSpecularRC(0.5f);
            sphere_material.SetExp(exp);
            sphere_material.SetIndexOfRefraction(1.5f);
            sphere_material.SetReflectiveRC(0.1f);
            sphere_material.SetTransmissionCoefficient(0.9f);
            Sphere sph = new Sphere(new Vector3(2f, 0.5f, 0f), 0.5f, "s1");
            sph.SetMaterial(sphere_material);
            world.Objects.Add(sph);
            Reflective smatte2 = new Reflective();
            smatte2.SetAmbientRC(0.25f);
            smatte2.SetDiffuseRC(0.5f);
            smatte2.SetRColor(new Vector3(1));
            smatte2.SetSpecularRC(0.15f);
            smatte2.SetExp(100);
            smatte2.SetReflectiveRC(0.75f);
            smatte2.SetCD(new Vector3(1));
            Sphere sphere = new Sphere(new Vector3(0, 2, 0), 0.4f, "s2");
            sphere.SetMaterial(smatte2);
            world.Objects.Add(sphere);
        }

        Box getbox(Vector3 c, Vector3 s, string name)
        {
            return new Box(c - s, c + s, name);
        }

        public void RenderImage(World screen)
        {
            System.Drawing.Bitmap bm = new System.Drawing.Bitmap(screen.ViewPlane.HRes, screen.ViewPlane.VRes);
            for (int i = 0; i < screen.Screen.Pixels.Count; i++)
            {
                Pixel p = screen.Screen.Pixels[i];
                Vector3 r = max_to_one(new Vector3(p.Color.R, p.Color.G, p.Color.B));
                bm.SetPixel((int)p.X, (int)p.Y, System.Drawing.Color.FromArgb((int)(r.X * 255.0f), (int)(r.Y * 255.0f), (int)(r.Z * 255.0f)));
            }
            string name = DateTime.Now.Millisecond.ToString() + ".png";
            while (File.Exists(name))
            {
                name = DateTime.Now.Millisecond.ToString() + ".png";
            }
            string file = @"C:\Users\Belal\Pictures\3D\" + name;
            bm.Save(file, System.Drawing.Imaging.ImageFormat.Png);
        }

        static Vector3 max_to_one(Vector3 c)
        {
            float max_value = MathHelper.Max(c.X, MathHelper.Max(c.Y, c.Z));

            if (max_value > 1.0)
                return (c / max_value);
            else
                return (c);
        }
    }
}
