using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Math;

namespace Disque.RigidBodies.Contacts
{
    public struct CollisionPrimitive
    {
        public RigidBody Body;
        public Matrix4 Offset;
        Matrix4 transform;
        public Matrix4 GetTransform()
        {
            return transform;
        }
        public void CalculateInternals()
        {
            transform = Body.GetTransform() * Offset;
        }
        public Vector3 GetAxis(int index)
        {
            return transform.GetAxisVector(index);
        }
    }
}
