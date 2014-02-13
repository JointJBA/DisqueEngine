using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Particles.Contacts;
using Disque.Math;

namespace Disque.Particles.Links
{
    public class CableConstraint : ParticleConstraint
    {
        public float MaxLength { get; set; }
        public float Restitution { get; set; }
        public override void GetContacts(List<ParticleContact> contacts, ParticleWorld world)
        {
            float length = CurrentLength;
            if (length < MaxLength)
            {
                return;
            }
            ParticleContact contact = new ParticleContact();
            contact.Particles[0] = Particle;
            contact.Particles[1] = null;
            Vector3 normal = Anchor - Particle.Position;
            normal.Normalize();
            contact.ContactNormal = normal;
            contact.Penetration = length - MaxLength;
            contact.Restitution = Restitution;
            contacts.Add(contact);
            return;
        }
    }
}
