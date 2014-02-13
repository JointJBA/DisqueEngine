using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Particles.Contacts;
using Disque.Math;

namespace Disque.Particles.Links
{
    public class Rod : ParticleLink
    {
        public float RodLength { get; set; }
        public Rod(float rodlength, Particle[] particles)
        {
            RodLength = rodlength;
            Particles = particles;
        }
        public override void  GetContacts(List<ParticleContact> contacts, ParticleWorld world)
        {
            ParticleContact contact = new ParticleContact();
            float currentLen = CurrentLength;
            if (currentLen == RodLength)
            {
                return;
            }
            contact.Particles[0] = Particles[0];
            contact.Particles[1] = Particles[1];
            Vector3 normal = Particles[1].Position - Particles[0].Position;
            normal.Normalize();
            if (currentLen > RodLength)
            {
                contact.ContactNormal = normal;
                contact.Penetration = currentLen - RodLength;
            }
            else
            {
                contact.ContactNormal = normal * -1;
                contact.Penetration = RodLength - currentLen;
            }
            contact.Restitution = 0;
            contacts.Add(contact);
        }
    }
}
