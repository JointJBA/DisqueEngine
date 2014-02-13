using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.Particles.Contacts
{
    public class GroundContactGenerator : IParticleContactGenerator
    {
        public float Height { get; set; }
        public GroundContactGenerator(float height)
        {
            Height = height;
        }
        public void GetContacts(List<ParticleContact> contacts, ParticleWorld world)
        {
            for (int i = 0; i < world.Particles.Count; i++)
            {
                float y = world.Particles[i].Position.Y;
                if (y < Height)
                {
                    ParticleContact c = new ParticleContact();
                    c.ContactNormal = Vector3.Up;
                    c.Particles[0] = world.Particles[i];
                    c.Penetration = -(y - Height);
                    c.Restitution = 0.8f;
                    contacts.Add(c);
                }
            }
        }
        //bool intersectfloor(Plane plane, Particle p, out float pene)
        //{
        //    Vector3 Normal = plane.Normal;
        //    float Distance = plane.Distance;
        //    float f2 = (p.Position.X * Normal.X) + (p.Position.Y * Normal.Y) + (p.Position.Z * Normal.Z);
        //    float f1 = f2 - Distance;
        //    if (f1 > 0.5f)
        //    {
        //        pene = -1;
        //        return false;
        //    }
        //    if (f1 < -0.5f)
        //    {
        //        pene = -1;
        //        return false;
        //    }
        //    pene = 0.5f - f1;
        //    return true;
        //}
    }
}
