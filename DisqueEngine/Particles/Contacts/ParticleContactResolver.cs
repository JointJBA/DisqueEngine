using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Math;

namespace Disque.Particles.Contacts
{
    public class ParticleContactResolver
    {
        public int Iterations { get; set; }
        public int IterationsUsed { get; set; }
        public ParticleContactResolver(int iterations)
        {
            Iterations = iterations;
        }
        public void ResolveContacts(List<ParticleContact> contacts, float duration)
        {
            int i;

            IterationsUsed = 0;
            while (IterationsUsed < Iterations)
            {
                // Find the contact with the largest closing velocity;
                float max = float.MaxValue;
                int maxIndex = contacts.Count;
                for (i = 0; i < contacts.Count; i++)
                {
                    float sepVel = contacts[i].CalculateSeparatingVelocity();
                    if (sepVel < max && (sepVel < 0 || contacts[i].Penetration > 0))
                    {
                        max = sepVel;
                        maxIndex = i;
                    }
                }

                // Do we have anything worth resolving?
                if (maxIndex == contacts.Count) break;

                // Resolve this contact
                contacts[maxIndex].Resolve(duration);

                // Update the interpenetrations for all particles
                Vector3[] move = contacts[maxIndex].ParticleMovement;
                for (i = 0; i < contacts.Count; i++)
                {
                    if (contacts[i].Particles[0] == contacts[maxIndex].Particles[0])
                    {
                        contacts[i].Penetration -= Vector3.Dot(move[0], contacts[i].ContactNormal);
                    }
                    else if (contacts[i].Particles[0] == contacts[maxIndex].Particles[1])
                    {
                        contacts[i].Penetration -= Vector3.Dot(move[1], contacts[i].ContactNormal);
                    }
                    if (contacts[i].Particles[1] != null)
                    {
                        if (contacts[i].Particles[1] == contacts[maxIndex].Particles[0])
                        {
                            contacts[i].Penetration += Vector3.Dot(move[0], contacts[i].ContactNormal);
                        }
                        else if (contacts[i].Particles[1] == contacts[maxIndex].Particles[1])
                        {
                            contacts[i].Penetration += Vector3.Dot(move[1], contacts[i].ContactNormal);
                        }
                    }
                }

                IterationsUsed++;
            }
        }
    }
}
