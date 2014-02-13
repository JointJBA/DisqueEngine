using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.RigidBodies.Contacts
{
    public class BodiesContactGenerator : ContactGenerator
    {
        public override int AddContact(ref CollisionData collisionData, RigidBodyWorld world, int limit)
        {
            int constacts = 0;
            for (int i = 0; i < world.pcontacts.Count; i++)
            {
                CollisionPrimitive cp = world.pcontacts[i].Bodies[0].CollisionPrimative;
                CollisionPrimitive cp2 = world.pcontacts[i].Bodies[1].CollisionPrimative;
                cp.CalculateInternals();
                cp2.CalculateInternals();
                if (cp is CollisionSphere && cp2 is CollisionSphere)
                    constacts += CollisionDetector.SphereAndSphere((CollisionSphere)cp, (CollisionSphere)cp2, ref collisionData);
                else if (cp is CollisionSphere && cp2 is CollisionBox)
                    constacts += CollisionDetector.BoxAndSphere((CollisionBox)cp2, (CollisionSphere)cp, ref collisionData);
                else if (cp2 is CollisionSphere && cp is CollisionBox)
                    constacts += CollisionDetector.BoxAndSphere((CollisionBox)cp, (CollisionSphere)cp2, ref collisionData);
            }
            return constacts;
        }
    }
}
