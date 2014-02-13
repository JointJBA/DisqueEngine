using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Particles.Forces;
using Disque.Core;
using GParticles = System.Collections.Generic.List<Disque.Particles.Particle>;
using Disque.Particles.Contacts;
using System.Diagnostics;
using System.Threading;

namespace Disque.Particles
{
    public class ParticleWorld
    {
        GParticles particles = new GParticles();
        ParticleContactResolver contactresolver = new ParticleContactResolver(200);
        List<IParticleContactGenerator> cgenerators = new List<IParticleContactGenerator>();
        List<ParticleForceGenerator> globalgenerators = new List<ParticleForceGenerator>();
        List<ParticleContact> contacts = new List<ParticleContact>();
        public List<ParticleForceGenerator> GlobalForceGenerators
        {
            get
            {
                return globalgenerators;
            }
            set
            {
                globalgenerators = value;
            }
        }
        public GParticles Particles
        {
            get
            {
                return particles;
            }
            set
            {
                particles = value;
            }
        }
        Stopwatch sw = new Stopwatch();
        object b = new object();
        public void Integrate(float duration)
        {
            contactresolver.Iterations = particles.Count * 4;
            contacts.Clear();
            getgeneratorcontacts();
            solvecontacts(duration);
            foreach (Particle particle in particles)
            {
                particle.ClearAccumulation();
                foreach (ParticleForceGenerator pfg in particle.Generators)
                {
                    pfg.UpdateForce(particle, duration);
                }
                foreach (ParticleForceGenerator pfg in globalgenerators)
                {
                    pfg.UpdateForce(particle, duration);
                }
                particle.Integrate(duration);
            }
        }
        public List<IParticleContactGenerator> ContactGenerators { get { return cgenerators; } set { cgenerators = value; } }
        void solvecontacts(float duration)
        {
            contactresolver.ResolveContacts(contacts, duration);
        }
        void solvecontacts(object duration)
        {
            contactresolver.ResolveContacts(contacts, (float)duration);
        }
        void getgeneratorcontacts()
        {
            for (int i = 0; i < cgenerators.Count; i++)
            {
                cgenerators[i].GetContacts(contacts, this);
            }
        }

    }
}
