using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.RigidBodies.Contacts
{
    public class ContactGenerator
    {
        public virtual int AddContact(ref CollisionData collisionData, RigidBodyWorld world, int limit)
        {
            return 0;
        }
    }
}
