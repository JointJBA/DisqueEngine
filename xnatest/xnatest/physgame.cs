using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Disque.Particles;
using Disque.Particles.Contacts;
using Disque.Particles.Forces;
using Disque.Particles.Springs;
using Disque.Xna;
using Disque.Xna.Cameras;
using Disque.Xna.Particles;
using Disque.Xna.Primatives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Disque.Xna.UI.Input;
using Disque.RigidBodies;
using Disque.RigidBodies.Contacts;

namespace xnatest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class physgame : Microsoft.Xna.Framework.Game
    {
        BasicEffect groundeffect;
        VertexPositionNormalTexture[] ground;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        FreeCamera cam;
        List<Box> bodies = new List<Box>();
        List<IDraw> drawables = new List<IDraw>();
        public physgame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            cam = new FreeCamera(new Vector3(10, 10, 60), 0, 0, GraphicsDevice);
            setupworld();
            groundeffect = new BasicEffect(GraphicsDevice);
            groundeffect.TextureEnabled = true;
            groundeffect.EnableDefaultLighting();
            groundeffect.Texture = Content.Load<Texture2D>("Checker");
            ground = new VertexPositionNormalTexture[6];
            setupground();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            cam.Update();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            float duration = (float)gameTime.ElapsedGameTime.TotalMilliseconds * 0.001f;
            updateobjects(duration);
            for (int i = 0; i < bodies.Count; i++)
            {
                bodies[i].Update(gameTime);
            }
            generateContacts();
            try
            {
                resolver.ResolveContacts(cdata.Contacts, duration);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);
            }
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            for (int i = 0; i < bodies.Count; i++)
            {
                bodies[i].Draw(gameTime, cam);
            }
            groundeffect.View = cam.View;
            groundeffect.World = Matrix.CreateScale(30);
            groundeffect.Projection = cam.Projection;
            foreach(EffectPass pass in groundeffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, ground, 0, 2);
            }
            base.Draw(gameTime);
        }
        protected override void EndRun()
        {
        }
        CollisionData cdata = new CollisionData();
        ContactResolver resolver = new ContactResolver(256 * 8);
        Random ran = new Random();
        void setupworld()
        {
            for (int i = 0; i < 1; i++)
            {
                Box box = new Box(GraphicsDevice, new Vector3(15, 1.5f, 15));
                bodies.Add(box);
            }
        }
        void generateContacts()
        {
            CollisionPlane plane = new CollisionPlane(new Vector3(0, 1, 0).ToVector3(), 0);
            cdata.Reset(256);
            cdata.Friction = 0.9f;
            cdata.Restitution = 0.1f;
            cdata.Tolerance = 0.1f;
            for (int i = 0; i < bodies.Count; i++)
            {
                if (!cdata.HasMoreContacts) return;
                CollisionDetector.BoxAndHalfSpace(bodies[i], plane, ref cdata);
            }
        }
        void updateobjects(float duration)
        {
            for (int i = 0; i < bodies.Count; i++)
            {
                bodies[i].Body.Integrate(duration);
                bodies[i].CalculateInternals();
            }
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
    }
    class Box : CollisionBox, IDraw
    {
        Cube cube;
        Random r = new Random();
        public Box(GraphicsDevice graphic, Vector3 startupPos)
        {
            cube = new Cube(startupPos, 3, 3, 3, randomColor(), graphic);
            Body.Position = startupPos.ToVector3();
            HalfSize = new Vector3(1.5f, 1.5f, 1.5f).ToVector3();
            Body.Mass = 1.5f * 1.5f * 1.5f * 8.0f;
            Disque.Math.Matrix3 tensor = Disque.Math.Matrix3.Identity;
            tensor.SetBlockInertiaTensor(HalfSize, Body.Mass);
            Body.SetInertiaTensor(tensor);
            Body.LinearDrag = 0.95f;
            Body.AngularDrag = 0.8f;
            Body.ClearAccumulation();
            Body.Acceleration = Disque.Math.Vector3.SmallGravity;
            Body.SetCanSleep(false);
            Body.SetAwake();
            Body.CalculateDerivedData();
            CalculateInternals();
        }
        public void Draw(GameTime gt, Camera cam)
        {
            cube.Draw(gt, cam);
        }
        Color randomColor()
        {
            return new Color(new Vector3((float)r.NextDouble(), (float)r.NextDouble(), (float)r.NextDouble()));
        }
        public void Update(GameTime gametime)
        {
            cube.Transformation = Body.TransformMatrix.ToMatrix();
        }
        public BoundingBox BoundingBox
        {
            get 
            {
                return cube.BoundingBox;
            }
        }
    }
}
