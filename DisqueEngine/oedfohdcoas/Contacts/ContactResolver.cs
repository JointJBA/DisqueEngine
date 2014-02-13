using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Math;

namespace Disque.RigidBodies.Contacts
{
    public class ContactResolver
    {
        int VelocityIterations;
        int PositionIterations;
        float VelocityEpsilon;
        float PositionEpsilon;
        public int VelocityIterationsUsed;
        public int PositionIterationsUsed;
        internal bool ValidSettings;
        public ContactResolver(int iterations, float velocityEpsilon = 0.01f, float positionEpsilon = 0.01f)
        {
            SetIterations(iterations);
            SetEpsilon(velocityEpsilon, positionEpsilon);
        }
        public ContactResolver(int velocityIterations, int positionIterations, float velocityEpsilon = 0.01f, float positionEpsilon = 0.01f)
        {
            SetIterations(velocityIterations, positionIterations);
            SetEpsilon(velocityEpsilon, positionEpsilon);
        }
        public bool IsValid
        {
            get
            {
                return (VelocityIterations > 0) &&
                       (PositionIterations > 0) &&
                       (PositionEpsilon >= 0.0f) &&
                       (PositionEpsilon >= 0.0f);
            }
        }
        public void SetIterations(int viteration, int piterations)
        {
            VelocityIterations = viteration;
            PositionIterations = piterations;
        }
        public void SetIterations(int iterations)
        {
            SetIterations(iterations, iterations);
        }
        public void SetEpsilon(float vepsilon, float pepsilon)
        {
            VelocityEpsilon = vepsilon;
            PositionEpsilon = pepsilon;
        }
        public void SetEpsilon(float epsilon)
        {
            SetEpsilon(epsilon, epsilon);
        }
        public void ResolveContacts(List<Contact> contacts, float duration)
        {
            if (contacts.Count == 0) return;
            if (!IsValid) return;
            prepareContacts(contacts, duration);
            adjustPositions(contacts, duration);
            adjustVelocities(contacts, duration);
        }
        void prepareContacts(List<Contact> contacts, float duration)
        {
            Contact lastcontact = contacts[contacts.Count - 1];
            for (int i = 0; i < contacts.Count; i++)
            {
                contacts[i].calculateinternals(duration);
            }
        }
        void adjustVelocities(List<Contact> contacts, float duration)
        {
            Vector3[] velocityChange = new Vector3[2];
            Vector3[] rotationChange = new Vector3[2];
            Vector3 deltaVel = new Vector3();
            VelocityIterationsUsed = 0;
            while (VelocityIterationsUsed < VelocityIterations)
            {
                float max = VelocityEpsilon;
                int index = contacts.Count;
                for (int i = 0; i < contacts.Count; i++)
                {
                    if (contacts[i].DesiredDeltaVelocity > max)
                    {
                        max = contacts[i].DesiredDeltaVelocity;
                        index = i;
                    }
                }
                if (index == contacts.Count) break;
                contacts[index].matchAwakeState();
                contacts[index].applyVelocityChange(velocityChange, rotationChange);
                for (int i = 0; i < contacts.Count; i++)
                {
                    for (int b = 0; b < 2; b++) 
                        if (contacts[i].Bodies[b] != null)
                        {
                            for (int d = 0; d < 2; d++)
                            {
                                if (contacts[i].Bodies[b] == contacts[index].Bodies[d])
                                {
                                    deltaVel = velocityChange[d] + rotationChange[d].Cross(contacts[i].RelativeContactPosition[b]);
                                    contacts[i].ContactVelocity += contacts[i].ContactToWorld.TransformTranspose(deltaVel) * (b == 1 ? -1 : 1);
                                    contacts[i].calculateDesiredDeltaVelocity(duration);
                                }
                            }
                        }
                }
                VelocityIterationsUsed++;
            }
        }
        void adjustPositions(List<Contact> contacts, float duration)
        {
            int i, index;
            Vector3[] linearChange = new Vector3[2];
            Vector3[] angularChange = new Vector3[2];
            float max;
            Vector3 deltaPosition;
            PositionIterationsUsed = 0;
            while (PositionIterationsUsed < PositionIterations)
            {
                max = PositionEpsilon;
                index = contacts.Count;
                for (i = 0; i < contacts.Count; i++)
                {
                    if (contacts[i].Penetration > max)
                    {
                        max = contacts[i].Penetration;
                        index = i;
                    }
                }
                if (index == contacts.Count) break;
                contacts[index].matchAwakeState();
                contacts[index].applyPositionChange(
                    linearChange,
                    angularChange,
                    max);
                for (i = 0; i < contacts.Count; i++)
                {
                    for (int b = 0; b < 2; b++) if (contacts[i].Bodies[b] != null)
                        {
                            for (int d = 0; d < 2; d++)
                            {
                                if (contacts[i].Bodies[b] == contacts[index].Bodies[d])
                                {
                                    deltaPosition = linearChange[d] +
                                        angularChange[d].Cross(
                                            contacts[i].RelativeContactPosition[b]);
                                    contacts[i].Penetration +=
                                        deltaPosition.Dot(contacts[i].ContactNormal)
                                        * (b == 1 ? 1 : -1);
                                }
                            }
                        }
                }
                PositionIterationsUsed++;
            }
        }
    }
}
