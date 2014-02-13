using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.RigidBodies.Contacts;
using Disque.Math;

namespace Disque.RigidBodies.Joints
{
    public class Joint : ContactGenerator
    {
        public RigidBody[] Bodies { get; set; }
        public Vector3[] Position { get; set; }
        public float Error { get; set; }
        public void Set(RigidBody a, Vector3 pos_a, RigidBody b, Vector3 pos_b, float error)
        {
            Bodies = new RigidBody[2] { a, b };
            Position = new Vector3[2] { pos_a, pos_b };
            Error = error;
        }
        public override int AddContact(ref CollisionData collisionData, RigidBodyWorld world, int limits)
        {
            Vector3 a_pos_world = Bodies[0].GetPointInWorldSpace(Position[0]);
            Vector3 b_pos_world = Bodies[1].GetPointInWorldSpace(Position[1]);
            Vector3 a_to_b = b_pos_world - a_pos_world;
            Vector3 normal = a_to_b;
            normal.Normalize();
            float length = a_to_b.Magnitude;
            if (MathHelper.Abs(length) > Error)
            {
                Contact contact = new Contact();
                contact.Bodies[0] = Bodies[0];
                contact.Bodies[1] = Bodies[1];
                contact.ContactNormal = normal;
                contact.ContactPoint = (a_pos_world + b_pos_world) * 0.5f;
                contact.Penetration = length - Error;
                contact.Friction = 1.0f;
                contact.Restitution = 0;
                collisionData.Contacts.Add(contact);
                return 1;
            }
            return 0;
        }
    }
}
