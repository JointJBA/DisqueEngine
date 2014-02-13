using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Particles.Contacts;
using Disque.Core;

namespace Disque.Particles.Links
{
    public class ParticleLink : IParticleContactGenerator
    {
        public Particle[] Particles { get; set; }
        public float CurrentLength
        {
            get
            {
                return (Particles[0].Position - Particles[1].Position).Magnitude;
            }
        }
        public virtual void GetContacts(List<ParticleContact> contacts, ParticleWorld world)
        {
        }
    }
}
