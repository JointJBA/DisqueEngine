using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Particles.Contacts;
using Disque.Particles;

namespace Disque.Core
{
    public interface IParticleContactGenerator
    {
        void GetContacts(List<ParticleContact> contacts, ParticleWorld world);
    }
}
