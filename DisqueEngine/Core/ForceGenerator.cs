using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.RigidBodies;

namespace Disque.Core
{
    public class ForceGenerator
    {
        public virtual void UpdateForce(RigidBody body, float duration) { }
    }
}
