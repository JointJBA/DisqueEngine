using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.Particles.Springs
{
    public class Spring : ParticleForceGenerator
    {
        public Particle Other { get; set; }
        public float SpringConstant { get; set; }
        public float RestLength { get; set; }
        public Spring(Particle other, float sc, float rl)
        {
            Other = other;
            SpringConstant = sc;
            RestLength = rl;
        }
        public override void UpdateForce(Particle particle, float duaration)
        {
            Vector3 force = particle.Position;
            force -= Other.Position;
            float magnit = force.Magnitude;
            magnit = MathHelper.Abs(magnit - RestLength);
            magnit *= SpringConstant;
            force.Normalize();
            force *= -magnit;
            particle.AddForce(force);
        }
    }
}
