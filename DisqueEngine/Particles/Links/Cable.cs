using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Particles.Contacts;
using Disque.Math;

namespace Disque.Particles.Links
{
    public class Cable : ParticleLink
    {
        public float MaxLength { get; set; }
        public float Restitution { get; set; }
        public Cable(float maxlength, float restitution)
        {
            MaxLength = maxlength;
            Restitution = restitution;
        }
        public override void GetContacts(List<ParticleContact> contacts, ParticleWorld world)
        {
            float length = CurrentLength;
            if (length < MaxLength)
            {
                return;
            }
            ParticleContact contact = new ParticleContact();
            contact.Particles[0] = Particles[0];
            contact.Particles[1] = Particles[1];
            Vector3 normal = Particles[1].Position - Particles[0].Position;
            normal.Normalize();
            contact.ContactNormal = normal;
            contact.Penetration = length - MaxLength;
            contact.Restitution = Restitution;
            contacts.Add(contact);
        }
    }
}
