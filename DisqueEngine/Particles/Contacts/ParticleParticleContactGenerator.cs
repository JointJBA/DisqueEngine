using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.Particles.Contacts
{
    public class ParticleParticleContactGenerator : IParticleContactGenerator
    {
        public void GetContacts(List<ParticleContact> contacts, ParticleWorld world)
        {
            for (int i = 0; i < world.Particles.Count; i++)
            {
                for (int j = i + 1; j < world.Particles.Count; j++)
                {
                    ParticleContact pc;
                    if (getcollision(out pc, world.Particles[i], world.Particles[j]))
                    {
                        contacts.Add(pc);
                    }
                }
            }
        }
        bool getcollision(out ParticleContact pc, Particle a, Particle b)
        {
            pc = null;
            float maxrad = 1.0f;
            float dist = Vector3.Distance(a.Position, b.Position);
            if (dist <= maxrad)
            {
                pc = new ParticleContact();
                pc.Particles[0] = a;
                pc.Particles[1] = b;
                pc.Restitution = 0.8f;
                pc.Penetration = maxrad - dist;
                pc.ContactNormal = Vector3.Normalize(a.Position - b.Position);
                return true;
            }
            return false;
        }
    }
}
