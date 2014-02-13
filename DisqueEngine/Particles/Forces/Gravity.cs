using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.Particles.Forces
{
    public class Gravity : ParticleForceGenerator
    {
        public Vector3 VectorGravity { get; set; }
        public Gravity(Vector3 gravity)
        {
            VectorGravity = gravity;
        }
        public override void UpdateForce(Particle particle, float duration)
        {
            particle.ForceAccumulation += VectorGravity * particle.Mass;
        }
    }
}
