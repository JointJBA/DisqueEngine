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

namespace xnatest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        BasicEffect groundeffect;
        VertexPositionNormalTexture[] ground;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ParticleWorld pworld = new ParticleWorld();
        FreeCamera cam;
        List<IDraw> drawables = new List<IDraw>();
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        Thread thr;
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            cam = new FreeCamera(new Vector3(10, 10, 60), 0, 0, GraphicsDevice);
            setupworld();
            addparticles();
            thr = new Thread(new ThreadStart(applyphysics));
            thr.Name = "Physics";
            thr.Start();
            //addparticle(new Vector3(0, 30, 0), Color.Red, 10);
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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            cam.Update();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            for (int i = 0; i < drawables.Count; i++)
            {
                drawables[i].Update(gameTime);
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
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
            foreach(EffectPass pass in groundeffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, ground, 0, 2);
            }
            base.Draw(gameTime);
        }
        protected override void EndRun()
        {
            thr.Abort();
        }
        void setupworld()
        {
            pworld.GlobalForceGenerators.Add(new Gravity(new Disque.Math.Vector3(0,-9.81f,0)));
            pworld.GlobalForceGenerators.Add(new Drag(0.52f));
            pworld.ContactGenerators.Add(new ParticleWorldContactGenerator(0, 0.5f));
        }
        void addparticle(Vector3 pos, Color color, float density)
        {
            ParticleBody pb = new ParticleBody(pos, Vector3.Zero, Vector3.Zero, 1.0f, density, color, GraphicsDevice);
            drawables.Add(pb);
            pworld.Particles.Add(pb.Particle);
        }
        void addparticles()
        {
            Random r = new Random();
            for (int i = 0; i < 200; i++)
            {
                int x = r.Next(0, 30);
                int z = r.Next(0, 30);
                int y = r.Next(0, 30);
                int density = r.Next(20, 40);
                int cr = r.Next(0, 255);
                int cg = r.Next(0, 255);
                int cb = r.Next(0, 255);
                addparticle(new Vector3(x, y, z), new Color(cr, cg, cb), density);
            }
        }
        void applyphysics()
        {
            Stopwatch sw = new Stopwatch();
            float duration = 0.001f;
            for (; ; )
            {
                sw.Start();
                pworld.Integrate(duration);
                sw.Stop();
                duration = sw.ElapsedMilliseconds * 0.001f;
                sw.Reset();
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
}
