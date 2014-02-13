using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;
using Disque.Particles.Contacts;

namespace Disque.Particles.Links
{
    public class ParticleConstraint : IParticleContactGenerator
    {
        public Particle Particle
        {
            get;
            set;
        }
        public Vector3 Anchor
        {
            get;
            set;
        }
        public float CurrentLength
        {
            get
            {
                Vector3 relativePos = Particle.Position - Anchor;
                return relativePos.Magnitude;
            }
        }
        public virtual void GetContacts(List<ParticleContact> contacts, ParticleWorld world)
        {
        }
    }
}
