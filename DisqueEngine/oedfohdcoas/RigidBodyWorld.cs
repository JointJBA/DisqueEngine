using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.RigidBodies.Contacts;
using Disque.Math;

namespace Disque.RigidBodies
{
    public class RigidBodyWorld
    {
        List<RigidBody> bodies = new List<RigidBody>();
        List<ForceGenerator> globalgenerators = new List<ForceGenerator>();
        List<ContactGenerator> cgenerators = new List<ContactGenerator>();
        internal List<PotentialContact> pcontacts = new List<PotentialContact>();
        ContactResolver cr = new ContactResolver(4);
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
        public List<ContactGenerator> ContactGenerators
        {
            get
            {
                return cgenerators;
            }
            set
            {
                cgenerators = value;
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
                body.ClearAccumulation();
                body.CalculateDerivedData();
            }
            cr.SetIterations(bodies.Count * 4);
        }
        public void Integrate(float duration)
        {
            foreach (RigidBody body in bodies)
            {
                body.ClearAccumulation();
                body.CalculateDerivedData();
                //constructTree();
                //getPotentialcontacts();
                getcontacts();
                solvecontacts(duration);
                foreach (ForceGenerator force in body.ForceGenerators)
                {
                    force.UpdateForce(body, duration);
                }
                foreach (ForceGenerator force in globalgenerators)
                {
                    force.UpdateForce(body, duration);
                }
                body.Integrate(duration);
            }
        }
        void constructTree()
        {
            hnode = new HNode(null, bodies[0].BoundingSphere, bodies[0]);
            for (int i = 1; i < bodies.Count; i++)
                hnode.Insert(bodies[i], bodies[i].BoundingSphere);
        }
        void getPotentialcontacts()
        {
            hnode.GetPotentialContacts(pcontacts, ((bodies.Count + 1) / 2) * bodies.Count);
        }
        void getcontacts()
        {
            for (int i = 0; i < cgenerators.Count; i++)
            {
               int v = cgenerators[i].AddContact(ref collisiondata, this, 1000);
            }
        }
        void solvecontacts(float duration)
        {
            cr.ResolveContacts(collisiondata.Contacts, duration);
        }
        CollisionData collisiondata = new CollisionData() { Contacts = new List<Contact>() };
        public CollisionData CollisionData
        {
            get
            {
                return collisiondata;
            }
            set
            {
                collisiondata = value;
            }
        }
        HNode hnode;
        public HNode Parent
        {
            get
            {
                return hnode;
            }
        }
    }
}
