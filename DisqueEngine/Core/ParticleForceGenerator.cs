using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Particles;

namespace Disque.Core
{
    public abstract class ParticleForceGenerator
    {
        public abstract void UpdateForce(Particle particle, float duration);
    }
}
