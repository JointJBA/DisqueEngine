using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.Particles.Springs
{
    public class FakeSpring : ParticleForceGenerator
    {
        public Vector3 Anchor { get; set; }
        public float SpringConstant { get; set; }
        public float Damping { get; set; }
        public FakeSpring(Vector3 anchor, float springConstant, float damping)
        {
            Anchor = anchor;
            SpringConstant = springConstant;
            Damping = damping;
        }
        public override void UpdateForce(Particle particle, float duration)
        {
            if (!(particle.Mass > 0.0f)) return;
            Vector3 position = particle.Position;
            position -= Anchor;
            float gamma = 0.5f * MathHelper.Sqrt((4 * SpringConstant - Damping * Damping));
            if (gamma == 0.0f) return;
            Vector3 c = position * (Damping / (2.0f * gamma)) +
            particle.Velocity * (1.0f / gamma);
            Vector3 target = position * MathHelper.Cos(gamma * duration) +
            c * MathHelper.Sin(gamma * duration);
            target *= MathHelper.Exp(-0.5f * duration * Damping);
            Vector3 accel = (target - position) * (1.0f / duration * duration) -
            particle.Velocity * duration;
            particle.AddForce(accel * particle.Mass);
        }
    }
}
