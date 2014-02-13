using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Particles.Contacts;
using Disque.Math;

namespace Disque.Particles.Links
{
    public class RodConstraint : ParticleConstraint
    {
        public float Length { get; set; }
        public override void GetContacts(List<ParticleContact> contacts, ParticleWorld world)
        {
            float currentLen = CurrentLength;
            if (currentLen == Length)
            {
                return;
            }
            ParticleContact contact = new ParticleContact();
            contact.Particles[0] = Particle;
            contact.Particles[1] = null;
            Vector3 normal = Anchor - Particle.Position;
            normal.Normalize();
            if (currentLen > Length)
            {
                contact.ContactNormal = normal;
                contact.Penetration = currentLen - Length;
            }
            else
            {
                contact.ContactNormal = normal * -1;
                contact.Penetration = Length - currentLen;
            }
            contact.Restitution = 0;
            contacts.Add(contact);
            return;
        }
    }
}
