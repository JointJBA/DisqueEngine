using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Math;

namespace Disque.RigidBodies.Contacts
{
    public class CollisionBox : CollisionPrimitive
    {
        public Vector3 HalfSize { get; set; }
    }
}
