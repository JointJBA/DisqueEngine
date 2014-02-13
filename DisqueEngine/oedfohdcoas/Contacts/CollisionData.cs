using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Disque.RigidBodies.Contacts
{
    public struct CollisionData
    {
        public List<Contact> Contacts;
        public int ContactsLeft;
        public float Friction;
        public float Restitution;
        public float Tolerance;
        public int ContactCount;
        public bool HasMoreContacts
        {
            get
            {
                return ContactsLeft > 0.0f;
            }
        }
        public void Reset(int maxContacts)
        {
            ContactsLeft = maxContacts;
            Contacts = new List<Contact>(maxContacts);
            ContactCount = 0;
        }
        public void AddContacts(int count)
        {
            ContactsLeft -= count;
            ContactCount += count;
        }
    }
}
