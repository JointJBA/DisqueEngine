using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Disque.Math;
using Disque.Raytracer;
using Disque.Raytracer.Acceleration;
using Disque.Raytracer.Cameras;
using Disque.Raytracer.GeometricObjects;
using Disque.Raytracer.GeometricObjects.CompoundObjects;
using Disque.Raytracer.GeometricObjects.Primitives;
using Disque.Raytracer.Lights;
using Disque.Raytracer.Mappings;
using Disque.Raytracer.Materials;
using Disque.Raytracer.Samplers;
using Disque.Raytracer.Textures;
using Disque.Raytracer.Tracers;
using Disque.Raytracer.Worlds;
using XRectangle = Disque.Raytracer.GeometricObjects.Primitives.Rectangle;
using System.Drawing;
using Disque.Raytracer.GeometricObjects.Meshes;
using Disque.MovieMaker.Animation;
using System.Collections.Generic;

namespace DisqueRenderToy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        World world = new World();
        bool rendercompleted = false;
        Instance ins;
        public MainWindow()
        {
            InitializeComponent();
            build13();
        }
        string rendertime = "";
        void render()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            world.RenderScene();
            watch.Stop();
            TimeSpan ts = TimeSpan.FromMilliseconds(watch.ElapsedMilliseconds);
            rendertime = (ts.Hours < 10 ? "0" + ts.Hours : ts.Hours.ToString()) + ":" + (ts.Minutes < 10 ? "0" + ts.Minutes : ts.Minutes.ToString()) + ":" + (ts.Seconds < 10 ? "0" + ts.Seconds : ts.Seconds.ToString()) + "." + (ts.Milliseconds < 10 ? "0" + ts.Milliseconds : ts.Milliseconds.ToString());
        }
        void build1()
        {
            world.ViewPlane = new ViewPlane();
            world.ViewPlane.S = 0.5f;
            world.ViewPlane.HRes = world.ViewPlane.VRes = 400;
            world.ViewPlane.SetSamples(36);
            Ambient ambient_ptr = new Ambient();
            ambient_ptr.RadianceScale = (1.0f);
            world.AmbientLight = (ambient_ptr);
            world.Background = new Vector3();			// default color - this can be left out
            world.Tracer = new RayCast(world);
            Pinhole pinhole_ptr = new Pinhole();
            pinhole_ptr.Position = new Vector3(0, 500, 500);
            pinhole_ptr.Target = new Vector3(0, 25, 0);
            pinhole_ptr.Distance = (600.0f);
            world.Camera = (pinhole_ptr);
            PointLight light_ptr1 = new PointLight();
            light_ptr1.Shadows = true;
            light_ptr1.Position = new Vector3(0, 100, 0);
            light_ptr1.Color = new Vector3(1, 1, 1);
            light_ptr1.RadianceScale = (3.0f);
            world.Lights.Add(light_ptr1);
            Vector3 yellow = new Vector3(1, 1, 0);										// yellow
            Vector3 brown = new Vector3(0.71f, 0.40f, 0.16f);								// brown
            Vector3 darkGreen = new Vector3(0.0f, 0.41f, 0.41f);							// darkGreen
            Vector3 orange = new Vector3(1f, 0.75f, 0f);									// orange
            Vector3 green = new Vector3(0f, 0.6f, 0.3f);									// green
            Vector3 lightGreen = new Vector3(0.65f, 1, 0.30f);								// light green
            Vector3 darkYellow = new Vector3(0.61f, 0.61f, 0);								// dark yellow
            Vector3 lightPurple = new Vector3(0.65f, 0.3f, 1);								// light purple
            Vector3 darkPurple = new Vector3(0.5f, 0, 1);									// dark purple
            Vector3 grey = new Vector3(0.25f);											// grey	
            float ka = 0.25f;
            float kd = 0.75f;
            Matte matte_ptr2 = new Matte(ka, kd, brown);
            Sphere sphere_ptr2 = new Sphere(new Vector3(45, -7, -60), 20);
            sphere_ptr2.SetMaterial(matte_ptr2);
            world.Objects.Add(sphere_ptr2);
            //Matte matte_ptr3 = new Matte(ka, kd, darkGreen);
            //Sphere sphere_ptr3 = new Sphere(new Vector3(40, 43, -100), 17);
            //sphere_ptr3.SetMaterial(matte_ptr3);								// dark green
            //world.Objects.Add(sphere_ptr3);
            //Matte matte_ptr4 = new Matte(ka, kd, orange);
            //Sphere sphere_ptr4 = new Sphere(new Vector3(-20, 28, -15), 20);
            //sphere_ptr4.SetMaterial(matte_ptr4);							// orange
            //world.Objects.Add(sphere_ptr4);
            //Matte matte_ptr5 = new Matte(ka, kd, green);
            //Sphere sphere_ptr5 = new Sphere(new Vector3(-25, -7, -35), 27);
            //sphere_ptr5.SetMaterial(matte_ptr5);								// green
            //world.Objects.Add(sphere_ptr5);
            //Matte matte_ptr6 = new Matte(ka, kd, lightGreen);
            //Sphere sphere_ptr6 = new Sphere(new Vector3(20, -27, -35), 25);
            //sphere_ptr6.SetMaterial((matte_ptr6);								// light green
            //world.Objects.Add(sphere_ptr6);
            //Matte matte_ptr7 = new Matte(ka, kd, green);
            //Sphere sphere_ptr7 = new Sphere(new Vector3(35, 18, -35), 22);
            //sphere_ptr7.SetMaterial((matte_ptr7);   							// green
            //world.Objects.Add(sphere_ptr7);
            //Matte matte_ptr8 = new Matte(ka, kd, brown);
            //Sphere sphere_ptr8 = new Sphere(new Vector3(-57, -17, -50), 15);
            //sphere_ptr8.SetMaterial((matte_ptr8);								// brown
            //world.Objects.Add(sphere_ptr8);
            //Matte matte_ptr9 = new Matte(ka, kd, lightGreen);
            //Sphere sphere_ptr9 = new Sphere(new Vector3(-47, 16, -80), 23);
            //sphere_ptr9.SetMaterial((matte_ptr9);								// light green
            //world.Objects.Add(sphere_ptr9);
            //Matte matte_ptr10 = new Matte(ka, kd, darkGreen);
            //Sphere sphere_ptr10 = new Sphere(new Vector3(-15, -32, -60), 22);
            //sphere_ptr10.SetMaterial((matte_ptr10);     						// dark green
            //world.Objects.Add(sphere_ptr10);
            //Matte matte_ptr11 = new Matte(ka, kd, darkYellow);
            //Sphere sphere_ptr11 = new Sphere(new Vector3(-35, -37, -80), 22);
            //sphere_ptr11.SetMaterial((matte_ptr11);							// dark yellow
            //world.Objects.Add(sphere_ptr11);
            //Matte matte_ptr12 = new Matte(ka, kd, darkYellow);
            //Sphere sphere_ptr12 = new Sphere(new Vector3(10, 43, -80), 22);
            //sphere_ptr12.SetMaterial((matte_ptr12);							// dark yellow
            //world.Objects.Add(sphere_ptr12);
            //Matte matte_ptr13 = new Matte(ka, kd, darkYellow);
            //Sphere sphere_ptr13 = new Sphere(new Vector3(30, -7, -80), 10);
            //sphere_ptr13.SetMaterial((matte_ptr13);
            //world.Objects.Add(sphere_ptr13);											// dark yellow (hidden)
            //Matte matte_ptr14 = new Matte(ka, kd, darkGreen);
            //Sphere sphere_ptr14 = new Sphere(new Vector3(-40, 48, -110), 18);
            //sphere_ptr14.SetMaterial((matte_ptr14); 							// dark green
            //world.Objects.Add(sphere_ptr14);
            //Matte matte_ptr15 = new Matte(ka, kd, brown);
            //Sphere sphere_ptr15 = new Sphere(new Vector3(-10, 53, -120), 18);
            //sphere_ptr15.SetMaterial((matte_ptr15); 							// brown
            //world.Objects.Add(sphere_ptr15);
            //Matte matte_ptr16 = new Matte(ka, kd, lightPurple);
            //Sphere sphere_ptr16 = new Sphere(new Vector3(-55, -52, -100), 10);
            //sphere_ptr16.SetMaterial((matte_ptr16);							// light purple
            //world.Objects.Add(sphere_ptr16);
            //Matte matte_ptr17 = new Matte(ka, kd, brown);
            //Sphere sphere_ptr17 = new Sphere(new Vector3(5, -52, -100), 15);
            //sphere_ptr17.SetMaterial((matte_ptr17);							// browm
            //world.Objects.Add(sphere_ptr17);
            //Matte matte_ptr18 = new Matte(ka, kd, darkPurple);
            //Sphere sphere_ptr18 = new Sphere(new Vector3(-20, -57, -120), 15);
            //sphere_ptr18.SetMaterial((matte_ptr18);							// dark purple
            //world.Objects.Add(sphere_ptr18);
            //Matte matte_ptr19 = new Matte(ka, kd, darkGreen);
            //Sphere sphere_ptr19 = new Sphere(new Vector3(55, -27, -100), 17);
            //sphere_ptr19.SetMaterial((matte_ptr19);							// dark green
            //world.Objects.Add(sphere_ptr19);

            //Matte matte_ptr20 = new Matte(ka, kd, brown);
            //Sphere sphere_ptr20 = new Sphere(new Vector3(50, -47, -120), 15);
            //sphere_ptr20.SetMaterial((matte_ptr20);							// browm
            //world.Objects.Add(sphere_ptr20);

            //Matte matte_ptr21 = new Matte(ka, kd, lightPurple);
            //Sphere sphere_ptr21 = new Sphere(new Vector3(70, -42, -150), 10);
            //sphere_ptr21.SetMaterial((matte_ptr21);							// light purple
            //world.Objects.Add(sphere_ptr21);

            //Matte matte_ptr22 = new Matte(ka, kd, lightPurple);
            //Sphere sphere_ptr22 = new Sphere(new Vector3(5, 73, -130), 12);
            //sphere_ptr22.SetMaterial((matte_ptr22);							// light purple
            //world.Objects.Add(sphere_ptr22);

            //Matte matte_ptr23 = new Matte(ka, kd, darkPurple);
            //Sphere sphere_ptr23 = new Sphere(new Vector3(66, 21, -130), 13);
            //sphere_ptr23.SetMaterial((matte_ptr23);							// dark purple
            //world.Objects.Add(sphere_ptr23);

            //Matte matte_ptr24 = new Matte(ka, kd, lightPurple);
            //Sphere sphere_ptr24 = new Sphere(new Vector3(72, -12, -140), 12);
            //sphere_ptr24.SetMaterial((matte_ptr24);							// light purple
            //world.Objects.Add(sphere_ptr24);

            //Matte matte_ptr25 = new Matte(ka, kd, lightPurple);
            //Sphere sphere_ptr25 = new Sphere(new Vector3(64, 5, -160), 11);
            //sphere_ptr25.SetMaterial((matte_ptr25);					 		// green
            //world.Objects.Add(sphere_ptr25);

            //Matte matte_ptr26 = new Matte(ka, kd, lightPurple);
            //Sphere sphere_ptr26 = new Sphere(new Vector3(55, 38, -160), 12);
            //sphere_ptr26.SetMaterial((matte_ptr26);							// light purple
            //world.Objects.Add(sphere_ptr26);
            //Matte matte_ptr27 = new Matte(ka, kd, lightPurple);
            //Sphere sphere_ptr27 = new Sphere(new Vector3(-73, -2, -160), 12);
            //sphere_ptr27.SetMaterial((matte_ptr27);							// light purple
            //world.Objects.Add(sphere_ptr27);
            //Matte matte_ptr28 = new Matte(ka, kd, darkPurple);
            //Sphere sphere_ptr28 = new Sphere(new Vector3(30, -62, -140), 15);
            //sphere_ptr28.SetMaterial((matte_ptr28); 							// dark purple
            //world.Objects.Add(sphere_ptr28);
            //Matte matte_ptr29 = new Matte(ka, kd, darkPurple);
            //Sphere sphere_ptr29 = new Sphere(new Vector3(25, 63, -140), 15);
            //sphere_ptr29.SetMaterial((matte_ptr29);							// dark purple
            //world.Objects.Add(sphere_ptr29);
            //Matte matte_ptr30 = new Matte(ka, kd, darkPurple);
            //Sphere sphere_ptr30 = new Sphere(new Vector3(-60, 46, -140), 15);
            //sphere_ptr30.SetMaterial((matte_ptr30); 							// dark purple
            //world.Objects.Add(sphere_ptr30);
            //Matte matte_ptr31 = new Matte(ka, kd, lightPurple);
            //Sphere sphere_ptr31 = new Sphere(new Vector3(-30, 68, -130), 12);
            //sphere_ptr31.SetMaterial((matte_ptr31); 							// light purple
            //world.Objects.Add(sphere_ptr31);
            //Matte matte_ptr32 = new Matte(ka, kd, green);
            //Sphere sphere_ptr32 = new Sphere(new Vector3(58, 56, -180), 11);
            //sphere_ptr32.SetMaterial((matte_ptr32);							//  green
            //world.Objects.Add(sphere_ptr32);
            //Matte matte_ptr33 = new Matte(ka, kd, green);
            //Sphere sphere_ptr33 = new Sphere(new Vector3(-63, -39, -180), 11);
            //sphere_ptr33.SetMaterial((matte_ptr33);							// green 
            //world.Objects.Add(sphere_ptr33);
            //Matte matte_ptr34 = new Matte(ka, kd, lightPurple);
            //Sphere sphere_ptr34 = new Sphere(new Vector3(46, 68, -200), 10);
            //sphere_ptr34.SetMaterial((matte_ptr34);							// light purple
            //world.Objects.Add(sphere_ptr34);
            //Matte matte_ptr35 = new Matte(ka, kd, lightPurple);
            //Sphere sphere_ptr35 = new Sphere(new Vector3(-3, -72, -130), 12);
            //sphere_ptr35.SetMaterial((matte_ptr35);							// light purple
            //world.Objects.Add(sphere_ptr35);
            //Matte matte_ptr36 = new Matte(ka, kd, grey);
            //Disque.Raytracer.GeometricObjects.Primitives.Plane plane_ptr = new Disque.Raytracer.GeometricObjects.Primitives.Plane(new Vector3(0, 0, -150), new Vector3(0, 0, 1));
            //plane_ptr.SetMaterial((matte_ptr36);
            //world.Objects.Add(plane_ptr);
        }
        void build2()
        {
            int ns = 100;
            world.ViewPlane = new ViewPlane();
            world.ViewPlane.HRes = world.ViewPlane.VRes = 400;
            world.ViewPlane.SetSamples(ns);
            world.Tracer = new RayCast(world);
            MultiJittered sampler = new MultiJittered(ns);
            AmbientOccluder occulder = new AmbientOccluder();
            occulder.RadianceScale = 1.0f;
            occulder.Color = Vector3.One;
            occulder.MinAmount = 0.0f;
            occulder.SetSampler(sampler);
            occulder.Shadows = true;
            world.AmbientLight = occulder;
            Directional light_ptr = new Directional();
            light_ptr.Direction = Vector3.Normalize(new Vector3(100, 100, 100));
            light_ptr.RadianceScale = 1.0f;
            light_ptr.Shadows = true;
            light_ptr.Color = new Vector3(1);
            world.Lights.Add(light_ptr);
            Pinhole cam = new Pinhole();
            cam.Position = new Vector3(25, 20, 45);
            cam.Target = new Vector3(0, 1, 0);
            cam.Distance = 5000;
            world.Camera = cam;
            Matte matte = new Matte(0.25f, 0.7f, new Vector3(1, 1, 0));
            matte.Shadows = true;
            Sphere s = new Sphere(new Vector3(0, 1, 0), 1);
            s.Shadows = true;
            s.SetMaterial(matte);
            world.Objects.Add(s);
            Matte matte2 = new Matte(0.75f, 0, new Vector3(1, 1, 1));
            matte2.Shadows = true;
            Plane p = new Plane(Vector3.Zero, new Vector3(0, 1, 0));
            p.Shadows = true;
            p.SetMaterial(matte2);
            world.Objects.Add(p);
        }
        void build3()
        {
            world.ViewPlane = new ViewPlane();
            world.ViewPlane.HRes = world.ViewPlane.VRes = 400;
            world.ViewPlane.S = 0.5f;
            world.ViewPlane.NumSamples = 1;
            world.ViewPlane.SetSamples(1);
            Ambient ambient_ptr = new Ambient();
            ambient_ptr.RadianceScale = (1.0f);
            world.AmbientLight = (ambient_ptr);
            world.Tracer = new RayCast(world);
            Pinhole pinhole_ptr = new Pinhole();
            pinhole_ptr.Position = new Vector3(0, 500, 500);
            pinhole_ptr.Target = new Vector3(0, 25, 0);
            pinhole_ptr.Distance = (600.0f);
            pinhole_ptr.ComputeUVW();
            world.Camera = (pinhole_ptr);
            Directional light_ptr1 = new Directional();
            light_ptr1.Direction = Vector3.Normalize(new Vector3(100, 100, 200));
            light_ptr1.RadianceScale = 3.0f;
            world.Lights.Add(light_ptr1);
            Vector3 yellow = new Vector3(1, 1, 0);										// yellow
            Vector3 brown = new Vector3(0.71f, 0.40f, 0.16f);								// brown
            Vector3 darkGreen = new Vector3(0.0f, 0.41f, 0.41f);							// darkGreen
            Vector3 orange = new Vector3(1f, 0.75f, 0f);									// orange
            Vector3 green = new Vector3(0f, 0.6f, 0.3f);									// green
            Vector3 lightGreen = new Vector3(0.65f, 1, 0.30f);								// light green
            Vector3 darkYellow = new Vector3(0.61f, 0.61f, 0);								// dark yellow
            Vector3 lightPurple = new Vector3(0.65f, 0.3f, 1);								// light purple
            Vector3 darkPurple = new Vector3(0.5f, 0, 1);									// dark purple
            Vector3 grey = new Vector3(0.25f);											// grey	
            float ka = 0.25f;
            float kd = 0.75f;
            Matte matte_ptr1 = new Matte(ka, kd, yellow);
            Sphere sphere_ptr1 = new Sphere(new Vector3(5, 3, 0), 30);
            sphere_ptr1.SetMaterial(matte_ptr1);   							// yellow
            world.Objects.Add(sphere_ptr1);

            Matte matte_ptr2 = new Matte(ka, kd, brown);
            Sphere sphere_ptr2 = new Sphere(new Vector3(45, -7, -60), 20);
            sphere_ptr2.SetMaterial((matte_ptr2));								// brown
            world.Objects.Add(sphere_ptr2);
            Matte matte_ptr3 = new Matte(ka, kd, darkGreen);
            Sphere sphere_ptr3 = new Sphere(new Vector3(40, 43, -100), 17);
            sphere_ptr3.SetMaterial((matte_ptr3));								// dark green
            world.Objects.Add(sphere_ptr3);
            Matte matte_ptr4 = new Matte(ka, kd, orange);
            Sphere sphere_ptr4 = new Sphere(new Vector3(-20, 28, -15), 20);
            sphere_ptr4.SetMaterial((matte_ptr4));							// orange
            world.Objects.Add(sphere_ptr4);
            Matte matte_ptr5 = new Matte(ka, kd, green);
            Sphere sphere_ptr5 = new Sphere(new Vector3(-25, -7, -35), 27);
            sphere_ptr5.SetMaterial((matte_ptr5));								// green
            world.Objects.Add(sphere_ptr5);
            Matte matte_ptr6 = new Matte(ka, kd, lightGreen);
            Sphere sphere_ptr6 = new Sphere(new Vector3(20, -27, -35), 25);
            sphere_ptr6.SetMaterial((matte_ptr6));								// light green
            world.Objects.Add(sphere_ptr6);
            Matte matte_ptr7 = new Matte(ka, kd, green);
            Sphere sphere_ptr7 = new Sphere(new Vector3(35, 18, -35), 22);
            sphere_ptr7.SetMaterial((matte_ptr7));   							// green
            world.Objects.Add(sphere_ptr7);
            Matte matte_ptr8 = new Matte(ka, kd, brown);
            Sphere sphere_ptr8 = new Sphere(new Vector3(-57, -17, -50), 15);
            sphere_ptr8.SetMaterial((matte_ptr8));								// brown
            world.Objects.Add(sphere_ptr8);
            Matte matte_ptr9 = new Matte(ka, kd, lightGreen);
            Sphere sphere_ptr9 = new Sphere(new Vector3(-47, 16, -80), 23);
            sphere_ptr9.SetMaterial((matte_ptr9));								// light green
            world.Objects.Add(sphere_ptr9);
            Matte matte_ptr10 = new Matte(ka, kd, darkGreen);
            Sphere sphere_ptr10 = new Sphere(new Vector3(-15, -32, -60), 22);
            sphere_ptr10.SetMaterial((matte_ptr10));     						// dark green
            world.Objects.Add(sphere_ptr10);
            Matte matte_ptr11 = new Matte(ka, kd, darkYellow);
            Sphere sphere_ptr11 = new Sphere(new Vector3(-35, -37, -80), 22);
            sphere_ptr11.SetMaterial((matte_ptr11));							// dark yellow
            world.Objects.Add(sphere_ptr11);
            Matte matte_ptr12 = new Matte(ka, kd, darkYellow);
            Sphere sphere_ptr12 = new Sphere(new Vector3(10, 43, -80), 22);
            sphere_ptr12.SetMaterial((matte_ptr12));							// dark yellow
            world.Objects.Add(sphere_ptr12);
            Matte matte_ptr13 = new Matte(ka, kd, darkYellow);
            Sphere sphere_ptr13 = new Sphere(new Vector3(30, -7, -80), 10);
            sphere_ptr13.SetMaterial((matte_ptr13));
            world.Objects.Add(sphere_ptr13);											// dark yellow (hidden)
            Matte matte_ptr14 = new Matte(ka, kd, darkGreen);
            Sphere sphere_ptr14 = new Sphere(new Vector3(-40, 48, -110), 18);
            sphere_ptr14.SetMaterial((matte_ptr14)); 							// dark green
            world.Objects.Add(sphere_ptr14);
            Matte matte_ptr15 = new Matte(ka, kd, brown);
            Sphere sphere_ptr15 = new Sphere(new Vector3(-10, 53, -120), 18);
            sphere_ptr15.SetMaterial((matte_ptr15)); 							// brown
            world.Objects.Add(sphere_ptr15);
            Matte matte_ptr16 = new Matte(ka, kd, lightPurple);
            Sphere sphere_ptr16 = new Sphere(new Vector3(-55, -52, -100), 10);
            sphere_ptr16.SetMaterial((matte_ptr16));							// light purple
            world.Objects.Add(sphere_ptr16);
            Matte matte_ptr17 = new Matte(ka, kd, brown);
            Sphere sphere_ptr17 = new Sphere(new Vector3(5, -52, -100), 15);
            sphere_ptr17.SetMaterial((matte_ptr17));							// browm
            world.Objects.Add(sphere_ptr17);
            Matte matte_ptr18 = new Matte(ka, kd, darkPurple);
            Sphere sphere_ptr18 = new Sphere(new Vector3(-20, -57, -120), 15);
            sphere_ptr18.SetMaterial((matte_ptr18));							// dark purple
            world.Objects.Add(sphere_ptr18);
            Matte matte_ptr19 = new Matte(ka, kd, darkGreen);
            Sphere sphere_ptr19 = new Sphere(new Vector3(55, -27, -100), 17);
            sphere_ptr19.SetMaterial((matte_ptr19));							// dark green
            world.Objects.Add(sphere_ptr19);

            Matte matte_ptr20 = new Matte(ka, kd, brown);
            Sphere sphere_ptr20 = new Sphere(new Vector3(50, -47, -120), 15);
            sphere_ptr20.SetMaterial((matte_ptr20));							// browm
            world.Objects.Add(sphere_ptr20);

            Matte matte_ptr21 = new Matte(ka, kd, lightPurple);
            Sphere sphere_ptr21 = new Sphere(new Vector3(70, -42, -150), 10);
            sphere_ptr21.SetMaterial((matte_ptr21));							// light purple
            world.Objects.Add(sphere_ptr21);

            Matte matte_ptr22 = new Matte(ka, kd, lightPurple);
            Sphere sphere_ptr22 = new Sphere(new Vector3(5, 73, -130), 12);
            sphere_ptr22.SetMaterial((matte_ptr22));							// light purple
            world.Objects.Add(sphere_ptr22);

            Matte matte_ptr23 = new Matte(ka, kd, darkPurple);
            Sphere sphere_ptr23 = new Sphere(new Vector3(66, 21, -130), 13);
            sphere_ptr23.SetMaterial((matte_ptr23));							// dark purple
            world.Objects.Add(sphere_ptr23);

            Matte matte_ptr24 = new Matte(ka, kd, lightPurple);
            Sphere sphere_ptr24 = new Sphere(new Vector3(72, -12, -140), 12);
            sphere_ptr24.SetMaterial((matte_ptr24));							// light purple
            world.Objects.Add(sphere_ptr24);

            Matte matte_ptr25 = new Matte(ka, kd, lightPurple);
            Sphere sphere_ptr25 = new Sphere(new Vector3(64, 5, -160), 11);
            sphere_ptr25.SetMaterial((matte_ptr25));					 		// green
            world.Objects.Add(sphere_ptr25);

            Matte matte_ptr26 = new Matte(ka, kd, lightPurple);
            Sphere sphere_ptr26 = new Sphere(new Vector3(55, 38, -160), 12);
            sphere_ptr26.SetMaterial((matte_ptr26));							// light purple
            world.Objects.Add(sphere_ptr26);
            Matte matte_ptr27 = new Matte(ka, kd, lightPurple);
            Sphere sphere_ptr27 = new Sphere(new Vector3(-73, -2, -160), 12);
            sphere_ptr27.SetMaterial((matte_ptr27));							// light purple
            world.Objects.Add(sphere_ptr27);
            Matte matte_ptr28 = new Matte(ka, kd, darkPurple);
            Sphere sphere_ptr28 = new Sphere(new Vector3(30, -62, -140), 15);
            sphere_ptr28.SetMaterial((matte_ptr28)); 							// dark purple
            world.Objects.Add(sphere_ptr28);
            Matte matte_ptr29 = new Matte(ka, kd, darkPurple);
            Sphere sphere_ptr29 = new Sphere(new Vector3(25, 63, -140), 15);
            sphere_ptr29.SetMaterial((matte_ptr29));							// dark purple
            world.Objects.Add(sphere_ptr29);
            Matte matte_ptr30 = new Matte(ka, kd, darkPurple);
            Sphere sphere_ptr30 = new Sphere(new Vector3(-60, 46, -140), 15);
            sphere_ptr30.SetMaterial((matte_ptr30)); 							// dark purple
            world.Objects.Add(sphere_ptr30);
            Matte matte_ptr31 = new Matte(ka, kd, lightPurple);
            Sphere sphere_ptr31 = new Sphere(new Vector3(-30, 68, -130), 12);
            sphere_ptr31.SetMaterial((matte_ptr31)); 							// light purple
            world.Objects.Add(sphere_ptr31);
            Matte matte_ptr32 = new Matte(ka, kd, green);
            Sphere sphere_ptr32 = new Sphere(new Vector3(58, 56, -180), 11);
            sphere_ptr32.SetMaterial((matte_ptr32));							//  green
            world.Objects.Add(sphere_ptr32);
            Matte matte_ptr33 = new Matte(ka, kd, green);
            Sphere sphere_ptr33 = new Sphere(new Vector3(-63, -39, -180), 11);
            sphere_ptr33.SetMaterial((matte_ptr33));							// green 
            world.Objects.Add(sphere_ptr33);
            Matte matte_ptr34 = new Matte(ka, kd, lightPurple);
            Sphere sphere_ptr34 = new Sphere(new Vector3(46, 68, -200), 10);
            sphere_ptr34.SetMaterial((matte_ptr34));							// light purple
            world.Objects.Add(sphere_ptr34);
            Matte matte_ptr35 = new Matte(ka, kd, lightPurple);
            Sphere sphere_ptr35 = new Sphere(new Vector3(-3, -72, -130), 12);
            sphere_ptr35.SetMaterial((matte_ptr35));							// light purple
            world.Objects.Add(sphere_ptr35);
            Matte matte_ptr36 = new Matte(ka, kd, grey);
            Disque.Raytracer.GeometricObjects.Primitives.Plane plane_ptr = new Plane(new Vector3(0, 0, -150), new Vector3(0, 0, 1));
            plane_ptr.SetMaterial((matte_ptr36));
            world.Objects.Add(plane_ptr);
        }
        void build4()
        {
            int ns = 256;
            world.ViewPlane.HRes = world.ViewPlane.VRes = 400;
            world.ViewPlane.SetSamples(ns);
            world.Tracer = new RayCast(world);
            AmbientOccluder occ = new AmbientOccluder();
            occ.RadianceScale = 1.0f;
            occ.MinAmount = 0f;
            occ.Shadows = true;
            occ.Color = Vector3.One;
            occ.SetSampler(new MultiJittered(ns));
            world.AmbientLight = occ;
            Pinhole cam = new Pinhole();
            cam.Position = new Vector3(25, 20, 45);
            cam.Target = new Vector3(0, 1, 0);
            cam.Distance = 5000;
            world.Camera = cam;
            Matte matte = new Matte(0.25f, 0f, new Vector3(0.5f, 0, 0.5f));
            matte.Shadows = true;
            Sphere s = new Sphere(new Vector3(0, 1, 0), 1);
            s.Shadows = true;
            s.SetMaterial(matte);
            world.Objects.Add(s);
            Matte matte2 = new Matte(0.75f, 0f, new Vector3(1, 1, 1));
            matte2.Shadows = true;
            Plane p = new Plane(Vector3.Zero, new Vector3(0, 1, 0));
            p.Shadows = true;
            p.SetMaterial(matte2);
            world.Objects.Add(p);
        }
        void build5()
        {
            int ns = 149;
            world.ViewPlane.HRes = 400;
            world.ViewPlane.VRes = 400;
            world.ViewPlane.SetSamples(ns);
            world.Tracer = new AreaLighting(world);
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
            cam.Distance = 5000;
            cam.Zoom = 1;
            world.Camera = cam;
            Sphere ss = new Sphere(new Vector3(2f, 0.5f, 1f), 0.5f);
            ss.Shadows = true;
            ss.SetMaterial(new Matte(0.25f, 0.5f, new Vector3(1f, 1f, 1f)) { Shadows = true });
            world.Objects.Add(ss);
            Matte matte = new Matte(0.25f, 0.5f, new Vector3(0.5f, 0, 0.5f));
            matte.Shadows = true;
            Box s = new Box(new Vector3(0, 0, 1), new Vector3(1, 1, 2));
            s.Shadows = true;
            s.SetMaterial(matte);
            world.Objects.Add(s);
            Matte m2 = new Matte(0.25f, 0, new Vector3(1, 1, 0));
            m2.Shadows = true;
            Box b2 = new Box(new Vector3(1, 1, 0), new Vector3(2, 2, 1));
            b2.SetMaterial(m2);
            b2.Shadows = true;
            //world.Objects.Add(b2);
            Matte matte2 = new Matte(0.75f, 0.5f, new Vector3(1, 1, 1));
            matte2.Shadows = true;
            Plane p = new Plane(Vector3.Zero, new Vector3(0, 1, 0));
            p.Shadows = true;
            p.SetMaterial(matte2);
            world.Objects.Add(p);
            Matte m3 = new Matte(1, 1, Vector3.One);
            m3.Shadows = true;
            Disk d = new Disk(new Vector3(0f, 1.6f, 1), new Vector3(1, 1, 1), 0.5f);
            d.SetMaterial(m3);
            d.Shadows = true;
            world.Objects.Add(d);
            Matte m5 = new Matte(0.75f, 0.1f, new Vector3(0, 0.5f, 1));
            m5.Shadows = true;
            Annulus an = new Annulus(new Vector3(1, 1.2f, 0), Vector3.Up, 0.4f, 0.2f);
            an.SetMaterial(m5);
            an.Shadows = true;
            world.Objects.Add(an);
            Matte m6 = new Matte(0.75f, 0.1f, new Vector3(1, 0, 0.4f));
            m6.Shadows = true;
            SolidCylinder sc = new SolidCylinder(0, 1, 0.5f);
            sc.Shadows = true;
            sc.SetMaterial(m6);
            world.Objects.Add(sc);
            PointLight pl = new PointLight();
            pl.Color = Vector3.One;
            pl.Position = new Vector3(0, 4, 0);
            pl.RadianceScale = 1.0f;
            pl.Shadows = true;
            world.Lights.Add(pl);
        }
        void build6()
        {
            try
            {
                int ns = 121;
                world.ViewPlane.HRes = 400;
                world.ViewPlane.VRes = 400;
                world.ViewPlane.SetSamples(ns);
                world.Tracer = new AreaLighting(world);
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
                cam.Distance = 5000;
                cam.Zoom = 1;
                world.Camera = cam;
                Sphere ss = new Sphere(new Vector3(2f, 0.5f, 1f), 0.5f);
                ss.Shadows = true;
                ss.SetMaterial(new Matte(0.25f, 0.5f, new Vector3(1f, 1f, 1f)) { Shadows = true });
                world.Objects.Add(ss);
                Matte matte = new Matte(0.25f, 0.5f, new Vector3(0.5f, 0, 0.5f));
                matte.Shadows = true;
                Box s = new Box(new Vector3(0, 0, 1), new Vector3(1, 1, 2));
                s.Shadows = true;
                s.SetMaterial(matte);
                world.Objects.Add(s);
                Matte m2 = new Matte(0.25f, 0, new Vector3(1, 1, 0));
                m2.Shadows = true;
                Box b2 = new Box(new Vector3(1, 1, 0), new Vector3(2, 2, 1));
                b2.SetMaterial(m2);
                b2.Shadows = true;
                //world.Objects.Add(b2);
                Matte matte2 = new Matte(0.75f, 0.5f, new Vector3(1, 1, 1));
                matte2.Shadows = true;
                Plane p = new Plane(Vector3.Zero, new Vector3(0, 1, 0));
                p.Shadows = true;
                p.SetMaterial(matte2);
                world.Objects.Add(p);
                Matte m3 = new Matte(0.25f, 0.5f, new Vector3(0.2f, 0.7f, 0.3f));
                m3.Shadows = true;
                Disk d = new Disk(new Vector3(0f, 1.6f, 1), new Vector3(1), 0.5f);
                d.SetMaterial(m3);
                d.Shadows = true;
                world.Objects.Add(d);
                Matte m5 = new Matte(0.75f, 0.1f, new Vector3(0, 0.5f, 1));
                m5.Shadows = true;
                Annulus an = new Annulus(new Vector3(1, 1.2f, 0), Vector3.Up, 0.4f, 0.2f);
                an.SetMaterial(m5);
                an.Shadows = true;
                world.Objects.Add(an);
                Matte m6 = new Matte(0.75f, 0.1f, new Vector3(1, 0, 0.4f));
                m6.Shadows = true;
                Instance sc = new Instance(new SolidCylinder(0, 1, 0.5f));
                sc.Shadows = true;
                sc.SetMaterial(m6);
                world.Objects.Add(sc);
                Matte mcone = new Matte(0.45f, 1, new Vector3(0.1f, 0, 0.4f));
                Instance cone = new Instance(new SolidCone(0.5f, 1));
                cone.Shadows = true;
                cone.SetMaterial(mcone);
                cone.Translate(new Vector3(0, 1.5f, -0.5f));
                world.Objects.Add(cone);
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
                throw new Exception("yup");
            }
        }
        void build7()
        {
            int ns = 121;
            world.ViewPlane.HRes = 400;
            world.ViewPlane.VRes = 400;
            world.ViewPlane.SetSamples(ns);
            world.Tracer = new AreaLighting(world);
            AmbientOccluder occ = new AmbientOccluder();
            occ.RadianceScale = 1.0f;
            occ.MinAmount = 0f;
            occ.Shadows = true;
            occ.Color = Vector3.One;
            occ.SetSampler(new MultiJittered(ns));
            world.AmbientLight = occ;
            Pinhole cam = new Pinhole();
            cam.Position = new Vector3(0, 50, 0);
            cam.Target = new Vector3(0, 0, 0);
            cam.Zoom = 2;
            cam.Distance = 500;
            world.Camera = cam;
            var grid = new SimpleBVHTree(GroupingMethod.DistanceM, 40);
            for (int i = 0; i < 1000; i++)
            {
                float rad = MathHelper.RandomFloat(0, 0.25f);
                Sphere ss = new Sphere(new Vector3(MathHelper.RandomFloat(-10, 10), rad, MathHelper.RandomFloat(-10, 10)), rad);
                ss.Shadows = true;
                ss.SetMaterial(new Matte(0.25f, 0.5f, new Vector3(MathHelper.RandomFloat(0, 1), MathHelper.RandomFloat(0, 1), MathHelper.RandomFloat(0, 1))) { Shadows = true });
                grid.AddObject(ss);
            }
            grid.PrepareObjects();
            world.Objects.Add(grid);
            Matte matte2 = new Matte(0.75f, 0.5f, new Vector3(1, 1, 1));
            matte2.Shadows = true;
            Plane p = new Plane(Vector3.Zero, new Vector3(0, 1, 0));
            p.Shadows = true;
            p.SetMaterial(matte2);
            world.Objects.Add(p);
            PointLight pl = new PointLight();
            pl.Color = Vector3.One;
            pl.Position = new Vector3(0, 4, 0);
            pl.RadianceScale = 1.0f;
            pl.Shadows = true;
            world.Lights.Add(pl);
        }
        void build8()
        {
            int ns = 256;
            world.ViewPlane.HRes = 400;
            world.ViewPlane.VRes = 400;
            world.ViewPlane.MaxDepth = 1;
            world.ViewPlane.SetSamples(ns);
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
            cam.Distance = 5000;
            cam.Zoom = 1;
            world.Camera = cam;
            Reflective matte2 = new Reflective();
            matte2.SetAmbientRC(0.001f);
            matte2.SetDiffuseRC(0.5f);
            matte2.SetRColor(new Vector3(1));
            matte2.SetSpecularRC(0);
            matte2.SetExp(100);
            matte2.SetReflectiveRC(0.5f);
            matte2.SetCD(new Vector3(1));
            Sphere ss = new Sphere(new Vector3(2f, 0.5f, 1f), 0.5f);
            ss.Shadows = true;
            ss.SetMaterial(matte2);
            world.Objects.Add(ss);
            Matte matte = new Matte(0.25f, 0.5f, new Vector3(0.5f, 0, 0.5f));
            matte.Shadows = true;
            Box s = new Box(new Vector3(0, 0, 1), new Vector3(1, 1, 2));
            s.Shadows = true;
            s.SetMaterial(matte);
            world.Objects.Add(s);
            Matte m2 = new Matte(0.25f, 0, new Vector3(1, 1, 0));
            m2.Shadows = true;
            Box b2 = new Box(new Vector3(1, 1, 0), new Vector3(2, 2, 1));
            b2.SetMaterial(m2);
            b2.Shadows = true;
            //world.Objects.Add(b2);
            Plane p = new Plane(Vector3.Zero, new Vector3(0, 1, 0));
            p.Shadows = true;
            p.SetMaterial(new Matte(0.25f, 0.5f, new Vector3(1f, 1f, 1f)) { Shadows = true });
            world.Objects.Add(p);
            Matte m3 = new Matte(1, 1, Vector3.One);
            m3.Shadows = true;
            Disk d = new Disk(new Vector3(0f, 1.6f, 1), new Vector3(1, 1, 1), 0.5f);
            d.SetMaterial(m3);
            d.Shadows = true;
            world.Objects.Add(d);
            Matte m5 = new Matte(0.75f, 0.1f, new Vector3(0, 0.5f, 1));
            m5.Shadows = true;
            Annulus an = new Annulus(new Vector3(1, 1.2f, 0), Vector3.Up, 0.4f, 0.2f);
            an.SetMaterial(m5);
            an.Shadows = true;
            world.Objects.Add(an);
            Matte m6 = new Matte(0.75f, 0.1f, new Vector3(1, 0, 0.4f));
            m6.Shadows = true;
            SolidCylinder sc = new SolidCylinder(0, 1, 0.5f);
            sc.Shadows = true;
            sc.SetMaterial(m6);
            world.Objects.Add(sc);
            PointLight pl = new PointLight();
            pl.Color = Vector3.One;
            pl.Position = new Vector3(0, 4, 0);
            pl.RadianceScale = 1.0f;
            pl.Shadows = true;
            world.Lights.Add(pl);
        }
        void build9()
        {
            int ns = 256;
            world.ViewPlane.HRes = 400;
            world.ViewPlane.VRes = 400;
            world.ViewPlane.MaxDepth = 20;
            world.ViewPlane.SetSampler(new Hammersley(ns));
            world.Tracer = new Whitted(world);
            AmbientOccluder occ = new AmbientOccluder();
            occ.RadianceScale = 1.0f;
            occ.MinAmount = 0f;
            occ.Shadows = true;
            occ.Color = Vector3.One;
            occ.SetSampler(new Jittered(ns));
            world.AmbientLight = occ;
            Pinhole cam = new Pinhole();
            cam.Position = new Vector3(10, 10, 10);
            cam.Target = new Vector3(0, 0, 0);
            cam.Distance = 5000;
            cam.Zoom = 1f;
            world.Camera = cam;
            Matte floor_material = new Matte(0.75f, 0.25f, Vector3.One);
            Plane floor = new Plane(Vector3.Zero, Vector3.Up);
            floor.SetMaterial(floor_material);
            world.Objects.Add(floor);
            Reflective matte2 = new Reflective();
            matte2.SetAmbientRC(0.25f);
            matte2.SetDiffuseRC(0.5f);
            matte2.SetRColor(new Vector3(1));
            matte2.SetSpecularRC(0.15f);
            matte2.SetExp(100);
            matte2.SetReflectiveRC(0.75f);
            matte2.SetCD(new Vector3(1));
            Box wall1 = new Box(Vector3.Zero, new Vector3(0.5f, 0.5f, 0.1f));
            Box wall2 = new Box(Vector3.Zero, new Vector3(0.1f, 0.5f, 0.5f));
            wall2.SetMaterial(matte2);
            wall1.SetMaterial(matte2);
            float exp = 2000;
            Transparent sphere_material = new Transparent();
            sphere_material.SetSpecularRC(0.5f);
            sphere_material.SetExp(exp);
            sphere_material.SetIndexOfRefraction(1.5f);
            sphere_material.SetTransmissionCoefficient(0.1f);
            sphere_material.SetTransmissionCoefficient(0.9f);
            Sphere s = new Sphere(new Vector3(0.3f, 0.1f, 0.3f), 0.1f);
            s.SetMaterial(sphere_material);
            world.Objects.Add(s);
            world.Objects.Add(wall1);
            world.Objects.Add(wall2);
            PointLight pl = new PointLight();
            pl.RadianceScale = 1.0f;
            pl.Color = Vector3.One;
            pl.Position = new Vector3(0, 8, 0);
            world.Lights.Add(pl);
        }
        void build10()
        {
            int ns = 49;
            world.ViewPlane.HRes = 800;
            world.ViewPlane.VRes = 700;
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
            cam.Distance = 5000;
            cam.Zoom = 1.5f;
            world.Camera = cam;
            Sphere ss = new Sphere(new Vector3(2f, 0.5f, 1f), 0.5f);
            ss.Shadows = true;
            ss.SetMaterial(new Matte(0.25f, 0.5f, new Vector3(1f, 1f, 1f)) { Shadows = true });
            world.Objects.Add(ss);
            Matte matte = new Matte(0.25f, 0.5f, new Vector3(0.5f, 0, 0.5f));
            matte.Shadows = true;
            Box s = new Box(new Vector3(0, 0, 1), new Vector3(1, 1, 2));
            s.Shadows = true;
            s.SetMaterial(matte);
            world.Objects.Add(s);
            Matte m2 = new Matte(0.25f, 0, new Vector3(1, 1, 0));
            m2.Shadows = true;
            Box b2 = new Box(new Vector3(1, 1, 0), new Vector3(2, 2, 1));
            b2.SetMaterial(m2);
            b2.Shadows = true;
            //world.Objects.Add(b2);
            Matte matte2 = new Matte(0.75f, 0.5f, new Vector3(1, 1, 1));
            matte2.Shadows = true;
            Plane p = new Plane(Vector3.Zero, new Vector3(0, 1, 0));
            p.Shadows = true;
            p.SetMaterial(matte2);
            world.Objects.Add(p);
            Matte m5 = new Matte(0.75f, 0.1f, new Vector3(0, 0.5f, 1));
            m5.Shadows = true;
            Annulus an = new Annulus(new Vector3(1, 1.2f, 0), Vector3.Up, 0.4f, 0.2f);
            an.SetMaterial(m5);
            an.Shadows = true;
            world.Objects.Add(an);
            Matte m6 = new Matte(0.75f, 0.1f, new Vector3(1, 0, 0.4f));
            m6.Shadows = true;
            SolidCylinder sc = new SolidCylinder(0, 1, 0.5f);
            sc.Shadows = true;
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
            Sphere sph = new Sphere(new Vector3(2f, 0.5f, 0f), 0.5f);
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
            Sphere sphere = new Sphere(new Vector3(0, 2, 0), 0.4f);
            sphere.SetMaterial(smatte2);
            world.Objects.Add(sphere);
        }
        void build11()
        {
            int ns = 100;
            world.ViewPlane.HRes = 600;
            world.ViewPlane.VRes = 600;
            world.ViewPlane.SetSampler(new Regular(ns));
            world.ViewPlane.MaxDepth = 10;
            world.Tracer = new Whitted(world);
            AmbientOccluder occ = new AmbientOccluder();
            occ.RadianceScale = 1.0f;
            occ.MinAmount = 0f;
            occ.Shadows = true;
            occ.Color = Vector3.One;
            occ.SetSampler(new Regular(ns));
            world.AmbientLight = occ;
            Pinhole cam = new Pinhole();
            cam.Position = new Vector3(-6, 6, 45);
            cam.Target = new Vector3(0, 0, 0);
            cam.Distance = 5000;
            cam.Zoom = 1.7f;
            world.Camera = cam;
            Matte plane_material = new Matte(0.9f, 0.9f, Vector3.One);
            XRectangle pl = new XRectangle(new Vector3(-2.5f, 0, -2.5f), new Vector3(0, 0, 5), new Vector3(5, 0, 0), Vector3.Up);
            //Plane pl = new Plane(new Vector3(0), Vector3.Up);
            pl.SetMaterial(plane_material);
            world.Objects.Add(pl);
            PointLight light = new PointLight();
            light.Color = Vector3.One;
            light.Position = new Vector3(-3, 3, 0);
            light.RadianceScale = 2.0f;
            float refl = 0.18f;
            float spec = 0.8f;
            float amb = 0.5f;
            float diff = 1f;
            float exp = 100;
            Group tree = new Group();
            world.Lights.Add(light);
            {
                ImageTexture im = new ImageTexture();
                Disque.Raytracer.Textures.Image imm = Extensions.CreateFromBitmap(@"C:\Users\Belal\Downloads\Earth_Diffuse_2.jpg");
                im.SetImage(imm);
                im.SetMapping(new SphericalMap());
                SV_Phong sphere_m = new SV_Phong();
                sphere_m.SetCD(im);
                sphere_m.SetSpecularColor(im);
                sphere_m.SetExp(40);
                sphere_m.SetSpecularRC(0.2f);
                sphere_m.SetDiffuseRC(0.6f);
                sphere_m.SetAmbientRC(0.25f);
                Sphere sphere = new Sphere(new Vector3(0, 0f, 0), 1f);
                Instance ins = new Instance(sphere);
                ins.SetMaterial(sphere_m);
                ins.RotateY(MathHelper.ToRadians(90));
                ins.Scale(new Vector3(0.4f));
                ins.Translate(new Vector3(0, 0.4f, 0.5f));
                world.Objects.Add(ins);
            }
            {
                Reflective smatte2 = new Reflective();
                smatte2.SetAmbientRC(amb);
                smatte2.SetDiffuseRC(diff);
                smatte2.SetCD(new Vector3(1, 0.77f, 0.77f));
                smatte2.SetRColor(new Vector3(1, 0.77f, 0.77f));
                smatte2.SetSpecularRC(spec);
                smatte2.SetExp(exp);
                smatte2.SetReflectiveRC(refl);
                Sphere s = new Sphere(new Vector3(-0.8f, 0.45f, 0.1f), 0.45f);
                s.SetMaterial(smatte2);
                tree.AddObject(s);
                //world.Objects.Add(s);
            }
            {
                Reflective smatte2 = new Reflective();
                smatte2.SetAmbientRC(amb);
                smatte2.SetDiffuseRC(diff);
                smatte2.SetCD(new Vector3(0.92f, 0.74f, 1));
                smatte2.SetRColor(new Vector3(0.92f, 0.74f, 1));
                smatte2.SetSpecularRC(spec);
                smatte2.SetExp(exp);
                smatte2.SetReflectiveRC(refl);
                Sphere s = new Sphere(new Vector3(0.7f, 0.4f, 1.5f), 0.4f);
                s.SetMaterial(smatte2);
                tree.AddObject(s);
                //world.Objects.Add(s);
            }
            {
                Reflective smatte2 = new Reflective();
                smatte2.SetAmbientRC(amb);
                smatte2.SetDiffuseRC(diff);
                smatte2.SetCD(new Vector3(0.62f, 0f, 0.7f));
                smatte2.SetRColor(new Vector3(0.62f, 0f, 0.7f));
                smatte2.SetSpecularRC(spec);
                smatte2.SetExp(exp);
                smatte2.SetReflectiveRC(refl);
                Sphere s = new Sphere(new Vector3(-0.7f, 0.2f, 1.5f), 0.2f);
                s.SetMaterial(smatte2);
                tree.AddObject(s);
                //world.Objects.Add(s);
            }
            world.Objects.Add(tree);
        }
        void build12()
        {
            int ns = 256;
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
            cam.Position = new Vector3(10, 20, 40);
            cam.Target = new Vector3(0, 0, 0);
            cam.Distance = 200;
            cam.Zoom = 3f;
            world.Camera = cam;
            Matte matte2 = new Matte(0.75f, 0.5f, new Vector3(1, 1, 1));
            matte2.Shadows = true;
            Plane p = new Plane(Vector3.Zero, new Vector3(0, 1, 0));
            p.Shadows = true;
            p.SetMaterial(matte2);
            world.Objects.Add(p);
            Transparent smatte2 = new Transparent();
            smatte2.SetIndexOfRefraction(1.37896f);
            smatte2.SetReflectiveRC(0.1f);
            smatte2.SetExp(2000);
            smatte2.SetTransmissionCoefficient(0.9f);
            smatte2.SetSpecularColor(new Vector3(1));
            smatte2.SetSpecularRC(0.5f);
            Sphere s = new Sphere(new Vector3(0, 7, 0), 5);
            s.SetMaterial(smatte2);
            world.Objects.Add(s);
            Matte stage_m = new Matte(0.5f, 0.5f, new Vector3(1, 0, 0));
            PointLight light = new PointLight();
            light.Color = Vector3.One;
            light.Position = new Vector3(12, 12, 12);
            light.RadianceScale = 1.0f;
            world.Lights.Add(light);
            Matte wall_m = new Matte(0.75f, 0.75f, new Vector3(1));
            Box b = getbox(new Vector3(-20, 5, 0), new Vector3(2, 50, 50));
            b.SetMaterial(wall_m);
            Box b2 = getbox(new Vector3(0, 5, -20), new Vector3(50, 50, 2));
            b2.SetMaterial(wall_m);
            world.Objects.Add(b2);
            world.Objects.Add(b);
            Box stage = getbox(new Vector3(0, 1, 0), new Vector3(5, 2, 5));
            stage.SetMaterial(stage_m);
            world.Objects.Add(stage);
        }
        void build13()
        {
            int ns = 9;
            world.ViewPlane.HRes = 400;
            world.ViewPlane.VRes = 400;
            world.ViewPlane.SetSampler(new MultiJittered(ns));
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
            cam.Position = new Vector3(0, 20, -60);
            cam.Target = new Vector3(0, 7.5f, 0);
            cam.Distance = 200;
            cam.Zoom = 3.5f;
            world.Camera = cam;
            float refl = 0.8f;
            float spec = 0.2f;
            float amb = 0.2f;
            float diff = 0.2f;
            float exp = 100;
            Plane p = new Plane(Vector3.Zero, new Vector3(0, 1, 0));
            p.Shadows = true;
            p.SetMaterial(new Matte(0.75f, 0.75f, new Vector3(0.56, 0.45, 0.32)));
            world.Objects.Add(p);
            ImageTexture im = new ImageTexture();
            Disque.Raytracer.Textures.Image imm = Extensions.CreateFromBitmap(@"C:\Users\Belal\Downloads\Earth_Diffuse_2.jpg");
            im.SetImage(imm);
            im.SetMapping(new SphericalMap());
            SV_Phong sphere_m = new SV_Phong();
            sphere_m.SetCD(im);
            sphere_m.SetSpecularColor(new ConstantColor(Vector3.One));
            sphere_m.SetExp(1);
            sphere_m.SetSpecularRC(1f);
            sphere_m.SetDiffuseRC(1f);
            sphere_m.SetAmbientRC(1f);
            Sphere sphere = new Sphere(new Vector3(0, 0f, 0), 1f);
            ins = new Instance(sphere);
            ins.SetMaterial(sphere_m);
            ins.RotateY(45);
            ins.Scale(new Vector3(10));
            ins.Translate(new Vector3(0, 13, 0));
            Reflective smatte2 = new Reflective();
            smatte2.SetAmbientRC(amb);
            smatte2.SetDiffuseRC(diff);
            smatte2.SetCD(new Vector3(1, 0, 0));
            smatte2.SetRColor(new Vector3(1));
            smatte2.SetSpecularRC(spec);
            smatte2.SetExp(exp);
            smatte2.SetReflectiveRC(refl);
            SolidCylinder sc = new SolidCylinder(0, 3, 13);
            sc.SetMaterial(new Matte(0.5f, 0.5f, new Vector3(0.5f, 0.5f, 0)));
            world.Objects.Add(sc);
            world.Objects.Add(ins);
            PointLight light = new PointLight();
            light.Color = Vector3.One;
            light.Position = new Vector3(0, 40, -20);
            light.RadianceScale = 2f;
            world.Lights.Add(light);
        }
        void build14()
        {
            int ns = 1;
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
            cam.Position = new Vector3(0, 30, 20);
            cam.Target = new Vector3(0, 0, 0);
            cam.Distance = 200;
            cam.Zoom = 2f;
            world.Camera = cam;
            PointLight light = new PointLight();
            light.Color = Vector3.One;
            light.Position = new Vector3(10, 10, 10);
            light.RadianceScale = 1.0f;
            world.Lights.Add(light);
            Matte matte = new Matte(0.25f, 0f, new Vector3(0.5f, 0, 0.5f));
            matte.Shadows = true;
            Sphere s = new Sphere(new Vector3(0, 5, 0), 5);
            s.Shadows = true;
            s.SetMaterial(matte);
            world.Objects.Add(s);
            try
            {
                Bitmap bm = new Bitmap(@"E:\Visual Studio Projects\Flight\Flight\FlightContent\sterrain.bmp");
                HeightMap map = new HeightMap(Extensions.GetHeightMap(bm), bm.Width, bm.Height, 1, 5);
                Matte matte2 = new Matte(0.75f, 0.5f, new Vector3(1, 1, 1));
                matte2.Shadows = true;
                map.SetMaterial(matte2);
                world.Objects.Add(map);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);
            }
        }
        void build15()
        {
            int ns = 1;
            world.ViewPlane.HRes = 400;
            world.ViewPlane.VRes = 400;
            world.ViewPlane.SetSampler(new MultiJittered(ns));
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
            cam.Position = new Vector3(0, 20, -60);
            cam.Target = new Vector3(0, 7.5f, 0);
            cam.Distance = 200;
            cam.Zoom = 3.5f;
            world.Camera = cam;
            ins = new Instance(new Sphere(new Vector3(0, 7.5f, 0), 7.5f));
            ins.SetMaterial(new Matte(0.7f, 0.7f, new Vector3(0.56, 0.32, 0.2)));
            world.Objects.Add(ins);
            Plane p = new Plane(Vector3.Zero, new Vector3(0, 1, 0));
            p.Shadows = true;
            p.SetMaterial(new Matte(0.75f, 0.75f, new Vector3(0.56, 0.45, 0.32)));
            world.Objects.Add(p);
        }
        Box getbox(Vector3 c, Vector3 s)
        {
            return new Box(c - s, c + s);
        }
        private void movie_Click(object sender, RoutedEventArgs e)
        {
            if (movieName.Text == "")
            {
                MessageBox.Show("Enter the movie's name.");
                return;
            }
            movieName.IsEnabled = false;
            int frames = 1;
            Recorder rec = new Recorder(world, frames);
            rec.Start(false);
            int nf = 0;
            string filen = movieName.Text;
            List<string> files = new List<string>();
            float angle = 9f;
            rec.NextFrame += (sr, ed) =>
            {
                ins.RotateY(MathHelper.ToRadians(angle));
                nf = ed.FinishedFrameNumber;
                string file = ScreenImage.RenderImageFromScreen(ed.FinishedFrame, world.ViewPlane.VRes, world.ViewPlane.HRes, filen, nf);
                files.Add(file);
                rec.Frames.Remove(ed.FinishedFrame);
            };
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Start();
            dt.Tick += (hfg, gh) =>
                {
                    num.Text = nf.ToString();
                    if (frames == nf)
                    {
                        foreach (string file in files)
                        {
                            ListBoxItem item = new ListBoxItem();
                            item.Height = item.Width = 90;
                            item.Content = new System.Windows.Controls.Image() { Source = new System.Windows.Media.Imaging.BitmapImage(new Uri(file, UriKind.RelativeOrAbsolute)) };
                            item.Selected += (sef, ffg) =>
                            {
                                cImage.Source = ((System.Windows.Controls.Image)((ListBoxItem)sef).Content).Source;
                            };
                            imagebox.Items.Add(item);
                        }
                        dt.Stop();
                    }
                };
        }

    }
}
