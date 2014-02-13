using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.RigidBodies
{
    public class RigidBodyWorld
    {
        List<RigidBody> bodies = new List<RigidBody>();
        List<ForceGenerator> globalgenerators = new List<ForceGenerator>();
        public bool CalculateIterations { get; set; }
        public int MaxContacts{get;set;}
        public List<RigidBody> RigidBodies
        {
            get
            {
                return bodies;
            }
            set
            {
                bodies = value;
            }
        }
        public List<ForceGenerator> GlobalForceGenerators
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
        public RigidBodyWorld(int maxContacts, int iterations = 0)
        {
            CalculateIterations = iterations == 0;
        }
        public void RunPhysics(float duration)
        {
            foreach (RigidBody body in bodies)
            {
                body.Integrate(duration);
            }
        }
        public void StartFrame()
        {
            foreach (RigidBody body in bodies)
            {
                body.ClearAccumulators();
                body.CalculateDerivedData();
            }
        }
        public void Integrate(float duration)
        {
            foreach (RigidBody body in bodies)
            {
                body.ClearAccumulators();
                body.CalculateDerivedData();
                foreach (ForceGenerator force in globalgenerators)
                {
                    force.UpdateForce(body, duration);
                }
                body.Integrate(duration);
            }
        }
    }
}
