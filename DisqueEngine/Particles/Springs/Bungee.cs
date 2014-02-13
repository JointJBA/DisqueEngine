using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.Particles.Springs
{
    public class Bungee : ParticleForceGenerator
    {
        public Particle Other { get; set; }
        public float SpringConstant { get; set; }
        public float RestLength { get; set; }
        public Bungee(Particle other, float sc, float rl)
        {
            Other = other;
            SpringConstant = sc;
            RestLength = rl;
        }
        public override void UpdateForce(Particle particle, float duaration)
        {
            Vector3 force = particle.Position;
            force -= Other.Position;
            float magnitude = force.Magnitude;
            if (magnitude <= RestLength) return;
            magnitude = SpringConstant * (RestLength - magnitude);
            force.Normalize();
            force *= -magnitude;
            particle.AddForce(force);
        }
    }
}
