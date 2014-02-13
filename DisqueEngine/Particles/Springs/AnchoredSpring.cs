using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.Particles.Springs
{
    public class AnchoredSpring : ParticleForceGenerator
    {
        public Vector3 Anchor { get; set; }
        public float SpringConstant { get; set; }
        public float RestLength { get; set; }
        public AnchoredSpring(Vector3 anchor, float sc, float rl)
        {
            Anchor = anchor;
            SpringConstant = sc;
            RestLength = rl;
        }
        public override void UpdateForce(Particle particle, float duaration)
        {
            Vector3 force = particle.Position;
            force -= Anchor;
            float magnitude = force.Magnitude;
            magnitude = MathHelper.Abs(magnitude - RestLength);
            magnitude *= SpringConstant;
            force.Normalize();
            force *= -magnitude;
            particle.AddForce(force);
        }
    }
}
