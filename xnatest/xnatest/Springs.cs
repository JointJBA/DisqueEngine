using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Disque.Xna.Primatives;
using Disque.Xna.Cameras;
using Disque.Particles;
using Disque.Xna.Particles;
using Disque.Particles.Forces;
using Disque.Particles.Contacts;
using Disque.Xna;
using Disque.Particles.Springs;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace xnatest
{
    public class Springs : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ParticleWorld pworld = new ParticleWorld();
        FreeCamera cam;
        List<IDraw> drawables = new List<IDraw>();
        public Springs()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            cam = new FreeCamera(new Vector3(0, 10, 60), 0, 0, GraphicsDevice);
            setupworld();
            addparticles();
        }
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            pworld.Integrate(gameTime.ElapsedGameTime.Milliseconds * 0.001f);
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
            base.Draw(gameTime);
        }
        void setupworld()
        {
            pworld.GlobalForceGenerators.Add(new Gravity(new Disque.Math.Vector3(0, -9.81f, 0)));
            pworld.GlobalForceGenerators.Add(new Drag(0.52f));
            pworld.ContactGenerators.Add(new ParticleWorldContactGenerator(0, 0.5f));
        }
        void addparticles()
        {
            ParticleBody pb = new ParticleBody(new Vector3(0, 15, 0), Vector3.Zero, Vector3.Zero, 1.0f, 10, Color.Azure, GraphicsDevice);
            pb.Particle.Generators.Add(new AnchoredSpring(new Disque.Math.Vector3(0, 30, 0), 50f, 5));
            pworld.Particles.Add(pb.Particle);
            drawables.Add(pb);
        }
    }
}
