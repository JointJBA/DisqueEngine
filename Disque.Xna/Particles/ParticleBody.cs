using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Disque.Xna.Cameras;
using Disque.Particles;
using Microsoft.Xna.Framework.Graphics;
using Disque.Xna.Primatives;

namespace Disque.Xna.Particles
{
    public class ParticleBody : IDraw
    {
        public float Density { get; set; }
        public Color Color { get; set; }
        public Vector3 Position
        { 
            get 
            {
                return particle.Position.ToVector3();
            }
            set
            {
                particle.Position = value.ToVector3();
            }
        }
        public Vector3 Acceleration
        {
            get
            {
                return particle.Acceleration.ToVector3();
            }
            set
            {
                particle.Acceleration = value.ToVector3();
            }
        }
        public Vector3 Velocity 
        {
            get
            {
                return particle.Velocity.ToVector3();
            }
            set
            {
                particle.Velocity = value.ToVector3();
            }
        }
        public float Drag 
        {
            get
            {
                return particle.Drag;
            }
            set
            {
                particle.Drag = value;
            }
        }
        Particle particle = new Particle();
        public Particle Particle
        {
            get
            {
                return particle;
            }
        }
        Sphere sphere;
        public ParticleBody(Vector3 position, Vector3 acceleration, Vector3 velocity, float drag, float density, Color color, GraphicsDevice graphic, float radius = 0.5f)
        {
            Density = density;
            Color = color;
            Position = position;
            Acceleration = acceleration;
            Velocity = velocity;
            Drag = drag;
            particle.Mass = MathHelper.PI4div3 * 0.5f * Density;
            sphere = new Sphere(position, radius, color, graphic);
        }
        public void Update(GameTime gametime)
        {
            sphere.Position = Position;
        }
        public void Draw(GameTime gametime, Camera camera)
        {
            sphere.Draw(gametime, camera);
        }
        public BoundingBox BoundingBox
        {
            get { return sphere.BoundingBox; }
        }
    }
}
