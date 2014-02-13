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
using Disque.Particles.Contacts;
using Disque.Particles.Forces;
using Disque.Xna.Particles;
using Disque.Particles.Links;

namespace xnatest
{
    public class MassAggregate : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ParticleWorld pworld = new ParticleWorld();
        FreeCamera cam;
        List<IDraw> drawables = new List<IDraw>();
        Thread thr;
        BasicEffect groundeffect;
        VertexPositionNormalTexture[] ground;
        public MassAggregate()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            cam = new FreeCamera(new Vector3(10, 10, 60), 0, 0, GraphicsDevice);
            groundeffect = new BasicEffect(GraphicsDevice);
            groundeffect.TextureEnabled = true;
            groundeffect.EnableDefaultLighting();
            groundeffect.Texture = Content.Load<Texture2D>("Checker");
            ground = new VertexPositionNormalTexture[6];
            setupworld();
            setupground();
            setupcube();
            addparticles();
            base.Initialize();
        }
        protected override void Update(GameTime gameTime)
        {
            for (int i = 0; i < drawables.Count; i++)
            {
                drawables[i].Update(gameTime);
            }
            pworld.Integrate(gameTime.ElapsedGameTime.Milliseconds * 0.001f);
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
        void setupworld()
        {
            pworld.GlobalForceGenerators.Add(new Gravity(new Disque.Math.Vector3(0, -9.81f, 0)));
            pworld.GlobalForceGenerators.Add(new Drag(0.52f));
            pworld.ContactGenerators.Add(new GroundContactGenerator(0.0f));
        }
        void setupcube()
        {
            ParticleBody pb1 = new ParticleBody(new Vector3(5, 20, 5), Vector3.Zero, Vector3.Zero, 1.0f, 40, Color.Green, GraphicsDevice, 0.1f);
            ParticleBody pb2 = new ParticleBody(new Vector3(6, 20, 5), Vector3.Zero, Vector3.Zero, 1.0f, 40, Color.Red, GraphicsDevice, 0.1f);
            ParticleBody pb3 = new ParticleBody(new Vector3(5, 20, 4), Vector3.Zero, Vector3.Zero, 1.0f, 40, Color.Green, GraphicsDevice, 0.1f);
            ParticleBody pb4 = new ParticleBody(new Vector3(6, 20, 4), Vector3.Zero, Vector3.Zero, 1.0f, 40, Color.Red, GraphicsDevice, 0.1f);
            ParticleBody pb5 = new ParticleBody(new Vector3(5, 21, 5), Vector3.Zero, Vector3.Zero, 1.0f, 40, Color.Green, GraphicsDevice, 0.1f);
            ParticleBody pb6 = new ParticleBody(new Vector3(6, 21, 5), Vector3.Zero, Vector3.Zero, 1.0f, 40, Color.Red, GraphicsDevice, 0.1f);
            ParticleBody pb7 = new ParticleBody(new Vector3(5, 21, 4), Vector3.Zero, Vector3.Zero, 1.0f, 40, Color.Green, GraphicsDevice, 0.1f);
            ParticleBody pb8 = new ParticleBody(new Vector3(6, 21, 4), Vector3.Zero, Vector3.Zero, 1.0f, 40, Color.Red, GraphicsDevice, 0.1f);
            setuprod(pb1, pb2, 1);
            setuprod(pb3, pb4, 1);
            setuprod(pb5, pb6, 1);
            setuprod(pb7, pb8, 1);
            setuprod(pb1, pb3, 1);
            setuprod(pb2, pb4, 1);
            setuprod(pb5, pb7, 1);
            setuprod(pb6, pb8, 1);
            setuprod(pb1, pb5, 1);
            setuprod(pb2, pb6, 1);
            setuprod(pb3, pb7, 1);
            setuprod(pb4, pb8, 1);
            addparticle(pb1, pb2, pb3, pb4, pb5, pb6, pb7, pb8);
        }
        void setuprod(ParticleBody pb, ParticleBody pb2, float rodlength)
        {
            pworld.ContactGenerators.Add(new Rod(rodlength, new Particle[] { pb.Particle, pb2.Particle }));
        }
        void addparticle(params ParticleBody[] particles)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                pworld.Particles.Add(particles[i].Particle);
                drawables.Add(particles[i]);
            }
        }
        void addparticle(Vector3 pos, Color color, float density)
        {
            ParticleBody pb = new ParticleBody(pos, Vector3.Zero, Vector3.Zero, 1.0f, density, color, GraphicsDevice, 0.1f);
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
    }
}
