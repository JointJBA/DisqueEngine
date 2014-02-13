using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Math;

namespace Disque.RigidBodies.Contacts
{
    public class CollisionPrimitive
    {
        public RigidBody Body = new RigidBody();
        public Matrix4 Offset = Matrix4.Identity;
        public Matrix4 Transform
        {
            get
            {
                return transform;
            }
        }
        public void CalculateInternals() 
        {
            transform = Body.GetTransform() * Offset;
        }
        public Vector3 GetAxis(int index)
        {
            return transform.GetAxisVector(index);
        }
        Matrix4 transform = Matrix4.Identity;
    }
}
