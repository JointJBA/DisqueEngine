using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Math;

namespace Disque.RigidBodies.Contacts
{
    public class CollisionPlane
    {
        public Vector3 Normal { get; set; }
        public float D { get; set; }
        public CollisionPlane(Vector3 normal, float d)
        {
            Normal = normal;
            D = d;
        }
    }
}
