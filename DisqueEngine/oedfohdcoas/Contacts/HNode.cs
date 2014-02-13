using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.RigidBodies.Contacts
{
    public class HNode
    {
        HNode[] nodes = new HNode[2];
        public HNode[] Children
        {
            get
            {
                return nodes;
            }
            set
            {
                nodes = value;
            }
        }
        public BoundingSphere Volume
        {
            get;
            set;
        }
        public RigidBody Body { get; set; }
        public HNode Parent { get; set; }
        public bool IsLeaf
        {
            get
            {
                return Body != null;
            }
        }
        public int GetPotentialContacts(List<PotentialContact> contacts, int limit)
        {
            if (IsLeaf || limit == 0) return 0;
            return Children[0].getPotentialcontactswith(Children[1], contacts, limit);
        }
        public void Insert(RigidBody body, BoundingSphere volume)
        {
            if (IsLeaf)
            {
                Children[0] = new HNode(this, Volume, Body);
                Children[1] = new HNode(this, volume, body);
                Body = null;
                recalculateboundingVolume();
            }
            else
            {
                if (Children[0].Volume.GetGrowth(volume) < Children[1].Volume.GetGrowth(volume))
                    Children[0].Insert(body, volume);
                else
                    Children[1].Insert(body, volume);
            }
        }
        public HNode(HNode parent, BoundingSphere volume, RigidBody body = null)
        {
            Parent = parent;
            Volume = volume;
            Body = body;
            Children = new HNode[2];
        }
        bool overlaps(HNode other)
        {
            return Volume.Overlaps(other.Volume);
        }
        int getPotentialcontactswith(HNode other, List<PotentialContact> contacts, int limit, int current = 0)
        {
            if (!overlaps(other) || limit == 0) return 0;
            if (IsLeaf && other.IsLeaf)
            {
                contacts.Add(new PotentialContact());
                contacts[current].Bodies = new RigidBody[2];
                contacts[current].Bodies[0] = Body;
                contacts[current].Bodies[1] = other.Body;
                return 1;
            }
            if (other.IsLeaf || (!IsLeaf && Volume.GetSize() >= other.Volume.GetSize()))
            {
                int count = Children[0].getPotentialcontactswith(other, contacts, limit, current);
                if (limit > count)
                {
                    return count + Children[1].getPotentialcontactswith(other, contacts, limit - count, current + count);
                }
                else
                {
                    return count;
                }
            }
            else
            {
                int count = getPotentialcontactswith(other.Children[0], contacts, limit, current);
                if (limit > count)
                {
                    return count + getPotentialcontactswith(other.Children[1], contacts, limit - count, current + count);
                }
                else
                {
                    return count;
                }
            }
        }
        void recalculateboundingVolume(bool recurse = true)
        {
            if (IsLeaf) return;
            Volume = new BoundingSphere(Children[0].Volume, Children[1].Volume);
            if (Parent != null) Parent.recalculateboundingVolume(true);
        }
    }
}
