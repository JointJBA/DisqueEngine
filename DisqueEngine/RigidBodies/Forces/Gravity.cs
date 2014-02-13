using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.RigidBodies.Forces
{
    public class Gravity : ForceGenerator
    {
        public Vector3 VectorGravity { get; set; }
        public Gravity(Vector3 gravity)
        {
            VectorGravity = gravity;
        }
        public override void UpdateForce(RigidBody body, float duration)
        {
            if (body.HasFiniteMass()) return;
            body.AddForce(VectorGravity * body.GetMass());
        }
    }
}
