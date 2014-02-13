using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.Core
{
    public interface IParticle : IParticleMass, IMovable
    {
        List<ParticleForceGenerator> Generators { get; set; }
        void Integrate(float duration);
    }
}
