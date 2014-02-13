using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.Particles.Forces
{
    public class Drag : ParticleForceGenerator
    {
        public float K1 { get; set; }
        public float K2 { get { return MathHelper.Sqrt(K1); } }
        public Drag(float k1)
        {
            K1 = k1;
        }
        public override void UpdateForce(Particle particle, float duaration)
        {
            Vector3 force = particle.Velocity;
            float dragcoef = force.Magnitude;
            dragcoef = K1 * dragcoef * K2 * dragcoef * dragcoef;
            force.Normalize();
            force *= -dragcoef;
            particle.AddForce(force);
        }
    }
}
