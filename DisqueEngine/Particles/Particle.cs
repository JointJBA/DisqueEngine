using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.Particles
{
    public class Particle : IParticle
    {
        public Vector3 Position
        {
            get;
            set;
        }
        public Vector3 Acceleration
        {
            get;
            set;
        }
        public Vector3 Velocity
        {
            get;
            set;
        }
        public float Drag { get; set; }
        public void Integrate(float duration)
        {
            Position += (Velocity * duration);
            Vector3 resultingacc = Acceleration;
            resultingacc += (ForceAccumulation * InverseMass);
            Velocity += (resultingacc * duration);
            Velocity *= MathHelper.Pow(Drag, duration);
            ClearAccumulation();
        }
        public float Mass
        {
            get;
            set;
        }
        public Vector3 ForceAccumulation
        {
            get;
            set;
        }
        public void AddForce(Vector3 force)
        {
            ForceAccumulation += force;
        }
        public void ClearAccumulation()
        {
            ForceAccumulation = new Vector3();
        }
        public float InverseMass
        {
            get { return 1.0f / Mass; }
        }
        List<ParticleForceGenerator> generatrs = new List<ParticleForceGenerator>();
        public List<ParticleForceGenerator> Generators
        {
            get { return generatrs; }
            set { generatrs = value; }
        }
    }
}
