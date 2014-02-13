using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Math;

namespace Disque.Particles.Contacts
{
    public class ParticleContact
    {
        public ParticleContact()
        {
            Particles = new Particle[2];
            ParticleMovement = new Vector3[2];
        }
        public Particle[] Particles { get; set; }
        public Vector3[] ParticleMovement { get; set; }
        public float Restitution { get; set; }
        public Vector3 ContactNormal { get; set; }
        public float Penetration { get; set; }
        public void Resolve(float duration)
        {
            resolveinterpenetration(duration);
            resolveVelocity(duration);
        }
        public float CalculateSeparatingVelocity()
        {
            Vector3 relativeVelocity = Particles[0].Velocity;
            if (Particles[1] != null) relativeVelocity -= Particles[1].Velocity;
            return Vector3.Dot(relativeVelocity, ContactNormal);
        }
        void resolveVelocity(float duration)
        {
            float separatingVelocity = CalculateSeparatingVelocity();
            if (separatingVelocity > 0)
            {
                return;
            }
            float newSepVelocity = -separatingVelocity * Restitution;
            Vector3 accCausedVelocity = Particles[0].Acceleration;
            if (Particles[1] != null) accCausedVelocity -= Particles[1].Acceleration;
            float accCausedSepVelocity = Vector3.Dot(accCausedVelocity, ContactNormal) * duration;
            if (accCausedSepVelocity < 0)
            {
                newSepVelocity += Restitution * accCausedSepVelocity;
                if (newSepVelocity < 0) newSepVelocity = 0;
            }
            float deltaVelocity = newSepVelocity - separatingVelocity;
            float totalInverseMass = Particles[0].InverseMass;
            if (Particles[1]!= null) totalInverseMass += Particles[1].InverseMass;
            if (totalInverseMass <= 0) return;
            float impulse = deltaVelocity / totalInverseMass;
            Vector3 impulsePerIMass = ContactNormal * impulse;
            Particles[0].Velocity = (Particles[0].Velocity + impulsePerIMass * Particles[0].InverseMass);
            //float min = 0.0001f;
            //Particles[0].Velocity = Particles[0].Velocity.Magnitude > min ? Particles[0].Velocity : Vector3.Zero;
            if (Particles[1] != null)
            {
                Particles[1].Velocity = (Particles[1].Velocity + impulsePerIMass * -Particles[1].InverseMass);
                //Particles[1].Velocity = Particles[1].Velocity.Magnitude > min ? Particles[1].Velocity : Vector3.Zero;
            }
        }
        void resolveinterpenetration(float duaration)
        {
            if (Penetration <= 0) return;
            float totalInverseMass = Particles[0].InverseMass;
            if (Particles[1] != null) totalInverseMass += Particles[1].InverseMass;
            if (totalInverseMass <= 0) return;
            Vector3 movePerIMass = ContactNormal * (Penetration / totalInverseMass);
            ParticleMovement[0] = new Vector3();
            ParticleMovement[0] = movePerIMass * Particles[0].InverseMass;
            if (Particles[1] != null)
            {
                ParticleMovement[1] = new Vector3();
                ParticleMovement[1] = movePerIMass * -Particles[1].InverseMass;
            }
            else
            {
                ParticleMovement[1] = new Vector3();
            }
            Particles[0].Position = Particles[0].Position + ParticleMovement[0];
            if (Particles[1] != null)
            {
                Particles[1].Position = (Particles[1].Position + ParticleMovement[1]);
            }
        }
    }
}
