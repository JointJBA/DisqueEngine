using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Disque.Particles;
using Microsoft.Xna.Framework.Graphics;
using Disque.Xna.Cameras;
using Disque.Xna;
using System.Diagnostics;
using System.Threading;
using Disque.RigidBodies;
using Disque.RigidBodies.Forces;
using Disque.Xna.Primatives;
using D = Disque.Math;
using Disque.Xna.UI.Input;

namespace xnatest
{
    public class watertest : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        FreeCamera cam;
        List<IDraw> drawables = new List<IDraw>();
        BasicEffect groundeffect;
        VertexPositionNormalTexture[] ground;
        RigidBodyWorld rbw = new RigidBodyWorld(3874, 32);
        public watertest()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            cam = new FreeCamera(new Vector3(10, 10, 60), 0, 0, GraphicsDevice);
            groundeffect = new BasicEffect(GraphicsDevice);
            groundeffect.TextureEnabled = true;
            groundeffect.EnableDefaultLighting();
            groundeffect.Texture = Content.Load<Texture2D>("Checker");
            ground = new VertexPositionNormalTexture[6];
            setupground();
            setupworld();
            addboat();
            base.Initialize();
        }
        protected override void Update(GameTime gameTime)
        {
            KeyboardManager.Update();
            KeyboardManager.KeysDown += new KeyboardEventHandler(KeyboardManager_KeysDown);
            body.ClearAccumulation();
            rbw.Integrate(gameTime.ElapsedGameTime.Milliseconds * 0.001f);
            //cube.Position = body.Position.ToVector3();
            cube.Transformation = body.GetTransform().ToMatrix();
            for (int i = 0; i < drawables.Count; i++)
            {
                drawables[i].Update(gameTime);
            }
            base.Update(gameTime);
        }
        void KeyboardManager_KeysDown(object sender, KeyboardEventArgs e)
        {
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            for (int i = 0; i < drawables.Count; i++)
            {
                drawables[i].Draw(gameTime, cam);
            }
            groundeffect.View = cam.View;
            groundeffect.World = Matrix.CreateScale(30);
            groundeffect.Projection = cam.Projection;
            foreach (EffectPass pass in groundeffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, ground, 0, 2);
            }
            base.Draw(gameTime);
        }
        void setupground()
        {
            ground[0] = new VertexPositionNormalTexture(new Vector3(0, 0, 0), Vector3.Up, new Vector2(0, 0));
            ground[1] = new VertexPositionNormalTexture(new Vector3(1, 0, 0), Vector3.Up, new Vector2(1, 0));
            ground[2] = new VertexPositionNormalTexture(new Vector3(1, 0, 1), Vector3.Up, new Vector2(1, 1));
            ground[3] = new VertexPositionNormalTexture(new Vector3(1, 0, 1), Vector3.Up, new Vector2(1, 1));
            ground[4] = new VertexPositionNormalTexture(new Vector3(0, 0, 1), Vector3.Up, new Vector2(0, 1));
            ground[5] = new VertexPositionNormalTexture(new Vector3(0, 0, 0), Vector3.Up, new Vector2(0, 0));
        }
        Cube cube;
        RigidBody body;
        Buoyancy buoyancy;
        D.Vector3 windspeed = new D.Vector3(0, 0, 0);
        Aero sail;
        void setupworld()
        {
            rbw.GlobalForceGenerators.Add(new Gravity(new Disque.Math.Vector3(0, -9.81f, 0)));
        }
        void addboat()
        {
            buoyancy = new Buoyancy(1.0f, 3.0f, 1.6f, 1000, new Disque.Math.Vector3(0, 0.5f, 0));
            sail = new Aero(new D.Matrix3(new float[]{ 0,0,0, 0,0,0, 0,0,-1.0f}), new D.Vector3(2.0f, 0, 0), windspeed);
            cube = new Cube(new Vector3(0, 30, 0), 3, 3, 3, Color.Blue, GraphicsDevice);
            drawables.Add(cube);
            body = new RigidBody();
            body.SetOrientation(new Disque.Math.Quaternion(1, 0, 0, 0));
            body.LinearDrag = 0.8f;
            body.AngularDrag = 0.8f;
            body.Position = new Disque.Math.Vector3(0, 1.6f, 0);
            body.Mass = 200;
            D.Matrix3 it = new D.Matrix3();
            it.SetBlockInertiaTensor(new D.Vector3(1.5f, 1.5f, 1.5f), 100.0f);
            body.BoundingSphere = new D.BoundingSphere(body.Position, 1.5f);
            body.SetInertiaTensor(it);
            body.CalculateDerivedData();
            body.SetAwake();
            body.SetCanSleep(true);
            body.ForceGenerators.Add(buoyancy);
            body.ForceGenerators.Add(sail);
            rbw.RigidBodies.Add(body);
        }
    }
}
