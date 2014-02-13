using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.Particles.Forces
{
    public class Buoyancy : ParticleForceGenerator
    {
        public float MaxDepth { get; set; }
        public float Volume { get; set; }
        public float WaterHeight { get; set; }
        public float LiquidDensity { get; set; }
        public Buoyancy(float maxDepth, float volume, float waterHeight, float liquidDensity = 1000.0f)
        {
            MaxDepth = maxDepth;
            Volume = volume;
            WaterHeight = waterHeight;
            LiquidDensity = liquidDensity;
        }
        public override void UpdateForce(Particle particle, float duaration)
        {
            float depth = particle.Position.Y;
            if (depth >= WaterHeight + MaxDepth) return;
            Vector3 force = new Vector3();
            if (depth <= WaterHeight - MaxDepth)
            {
                force.Y = LiquidDensity * Volume;
                particle.AddForce(force);
                return;
            }
            // Otherwise we are partly submerged.
            force.Y = LiquidDensity * Volume * (depth - MaxDepth - WaterHeight) / 2 * MaxDepth;
            particle.AddForce(force);
        }
    }
}
