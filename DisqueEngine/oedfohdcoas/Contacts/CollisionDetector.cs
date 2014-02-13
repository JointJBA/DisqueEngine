using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Math;

namespace Disque.RigidBodies.Contacts
{
    public class CollisionDetector
    {
        public static int SphereAndHalfSpace(CollisionSphere sphere, CollisionPlane plane, ref CollisionData data)
        {
            if (data.ContactsLeft <= 0) return 0;
            Vector3 position = sphere.GetAxis(3);
            float ballDistance = Vector3.Dot(plane.Normal, position) - sphere.Radius - plane.D;
            if (ballDistance >= 0) return 0;
            Contact contact = new Contact();
            contact.ContactNormal = plane.Normal;
            contact.Penetration = -ballDistance;
            contact.ContactPoint = position - plane.Normal * (ballDistance + sphere.Radius);
            contact.SetData(sphere.Body, null, data.Friction, data.Restitution);
            data.Contacts.Add(contact);
            data.AddContacts(1);
            return 1;
        }
        public static int SphereAndTruePlane(CollisionSphere sphere, CollisionPlane plane, ref CollisionData data)
        {
            if (data.ContactsLeft <= 0) return 0;
            Vector3 position = sphere.GetAxis(3);
            float centreDistance = Vector3.Dot(plane.Normal, position) - plane.D;
            if (centreDistance * centreDistance > sphere.Radius * sphere.Radius)
            {
                return 0;
            }
            Vector3 normal = plane.Normal;
            float penetration = -centreDistance;
            if (centreDistance < 0)
            {
                normal *= -1;
                penetration = -penetration;
            }
            penetration += sphere.Radius;
            Contact contact = new Contact();
            contact.ContactNormal = normal;
            contact.Penetration = penetration;
            contact.ContactPoint = position - plane.Normal * centreDistance;
            contact.SetData(sphere.Body, null, data.Friction, data.Restitution);
            data.Contacts.Add(contact);
            data.AddContacts(1);
            return 1;
        }
        public static int SphereAndSphere(CollisionSphere one, CollisionSphere two, ref CollisionData data)
        {
            if (data.ContactsLeft <= 0) return 0;
            Vector3 positionOne = one.GetAxis(3);
            Vector3 positionTwo = two.GetAxis(3);
            Vector3 midline = positionOne - positionTwo;
            float size = midline.Magnitude;
            if (size <= 0.0f || size >= one.Radius + two.Radius)
            {
                return 0;
            }
            Vector3 normal = midline * ((1.0f) / size);
            Contact contact = new Contact();
            contact.ContactNormal = normal;
            contact.ContactPoint = positionOne + midline * (float)0.5;
            contact.Penetration = (one.Radius + two.Radius - size);
            contact.SetData(one.Body, two.Body, data.Friction, data.Restitution);
            data.Contacts.Add(new Contact());
            data.AddContacts(1);
            return 1;
        }
        static float[,] mults = new float[8, 3] { { 1, 1, 1 }, { -1, 1, 1 }, { 1, -1, 1 }, { -1, -1, 1 }, { 1, 1, -1 }, { -1, 1, -1 }, { 1, -1, -1 }, { -1, -1, -1 } };
        public static int BoxAndHalfSpace(CollisionBox box, CollisionPlane plane, ref CollisionData data)
        {
            if (data.ContactsLeft <= 0) return 0;
            if (!IntersectionTests.BoxAndHalfSpace(box, plane))
            {
                return 0;
            }
            int contactsUsed = 0;
            for (int i = 0; i < 8; i++)
            {
                Vector3 vertexPos = new Vector3(mults[i, 0], mults[i, 1], mults[i, 2]);
                vertexPos *= (box.HalfSize);
                vertexPos = box.Transform.Transform(vertexPos);
                float vertexDistance = Vector3.Dot(vertexPos, plane.Normal);
                if (vertexDistance <= plane.D)
                {
                    Contact contact = new Contact();
                    contact.ContactPoint = plane.Normal;
                    contact.ContactPoint *= (vertexDistance - plane.D);
                    contact.ContactPoint = vertexPos;
                    contact.ContactNormal = plane.Normal;
                    contact.Penetration = plane.D - vertexDistance;
                    contact.SetData(box.Body, null, data.Friction, data.Restitution);
                    data.Contacts.Add(contact);
                    if (contactsUsed == data.ContactsLeft) return contactsUsed;
                }
            }
            data.AddContacts(contactsUsed);
            return contactsUsed;
        }
        public static int BoxAndBox(CollisionBox one, CollisionBox two, ref CollisionData data)
        {
            Vector3 toCenter = two.GetAxis(3) - one.GetAxis(3);
            float pen = float.MaxValue;
            int best = 0xffffff;
            CHECK_OVERLAP(one.GetAxis(0), 0, one, two, toCenter, ref pen, ref best);
            CHECK_OVERLAP(one.GetAxis(1), 1, one, two, toCenter, ref pen, ref best);
            CHECK_OVERLAP(one.GetAxis(2), 2, one, two, toCenter, ref pen, ref best);
            CHECK_OVERLAP(one.GetAxis(0), 3, one, two, toCenter, ref pen, ref best);
            CHECK_OVERLAP(one.GetAxis(1), 4, one, two, toCenter, ref pen, ref best);
            CHECK_OVERLAP(one.GetAxis(2), 5, one, two, toCenter, ref pen, ref best);
            int bestSingleAxis = best;
            CHECK_OVERLAP(Vector3.Cross(one.GetAxis(0), two.GetAxis(0)), 6, one, two, toCenter, ref pen, ref best);
            CHECK_OVERLAP(Vector3.Cross(one.GetAxis(0), two.GetAxis(1)), 7, one, two, toCenter, ref pen, ref best);
            CHECK_OVERLAP(Vector3.Cross(one.GetAxis(0), two.GetAxis(2)), 8, one, two, toCenter, ref pen, ref best);
            CHECK_OVERLAP(Vector3.Cross(one.GetAxis(1), two.GetAxis(0)), 9, one, two, toCenter, ref pen, ref best);
            CHECK_OVERLAP(Vector3.Cross(one.GetAxis(1), two.GetAxis(1)), 10, one, two, toCenter, ref pen, ref best);
            CHECK_OVERLAP(Vector3.Cross(one.GetAxis(1), two.GetAxis(2)), 11, one, two, toCenter, ref pen, ref best);
            CHECK_OVERLAP(Vector3.Cross(one.GetAxis(2), two.GetAxis(0)), 12, one, two, toCenter, ref pen, ref best);
            CHECK_OVERLAP(Vector3.Cross(one.GetAxis(2), two.GetAxis(1)), 13, one, two, toCenter, ref pen, ref best);
            CHECK_OVERLAP(Vector3.Cross(one.GetAxis(2), two.GetAxis(2)), 14, one, two, toCenter, ref pen, ref best);
            if (best == 0xffffff) throw new Exception("same, same");
            if (best < 3)
            {
                fillPointFaceBoxBox(one, two, toCenter, data, best, pen);
                data.AddContacts(1);
            }
            else if (best < 6)
            {
                fillPointFaceBoxBox(two, one, toCenter * -1.0f, data, best - 3, pen);
                data.AddContacts(1);
                return 1;
            }
            else
            {
                best -= 6;
                int oneAxisIndex = best / 3;
                int twoAxisIndex = best % 3;
                Vector3 oneAxis = one.GetAxis(oneAxisIndex);
                Vector3 twoAxis = two.GetAxis(twoAxisIndex);
                Vector3 axis = Vector3.Cross(oneAxis, twoAxis);
                axis.Normalize();
                if (Vector3.Dot(axis, toCenter) > 0) axis = axis * -1.0f;
                Vector3 ptOnOneEdge = one.HalfSize;
                Vector3 ptOnTwoEdge = two.HalfSize;
                for (int i = 0; i < 3; i++)
                {
                    if (i == oneAxisIndex) ptOnOneEdge[i] = 0;
                    else if (Vector3.Dot(one.GetAxis(i), axis) > 0) ptOnOneEdge[i] = -ptOnOneEdge[i];
                    if (i == twoAxisIndex) ptOnTwoEdge[i] = 0;
                    else if (Vector3.Dot(two.GetAxis(i), axis) < 0) ptOnTwoEdge[i] = -ptOnTwoEdge[i];
                }
                ptOnOneEdge = Vector3.Transform(ptOnOneEdge, one.Transform);
                ptOnTwoEdge = Vector3.Transform(ptOnTwoEdge, two.Transform);
                Vector3 vertex = contactPoint(
                    ptOnOneEdge, oneAxis, one.HalfSize[oneAxisIndex],
                    ptOnTwoEdge, twoAxis, two.HalfSize[twoAxisIndex],
                    bestSingleAxis > 2
                    );
                Contact contact = new Contact();
                contact.Penetration = pen;
                contact.ContactNormal = axis;
                contact.ContactPoint = vertex;
                contact.SetData(one.Body, two.Body,
                    data.Friction, data.Restitution);
                data.Contacts.Add(contact);
                data.AddContacts(1);
                return 1;
            }
            return 0;
        }
        public static int BoxAndPoint(CollisionBox box, Vector3 point, ref CollisionData data)
        {
            Vector3 relPt = box.Transform.TransformInverse(point);
            Vector3 normal = new Vector3();
            float min_depth = box.HalfSize.X - MathHelper.Abs(relPt.X);
            if (min_depth < 0) return 0;
            normal = box.GetAxis(0) * ((relPt.X < 0) ? -1 : 1);
            float depth = box.HalfSize.Y - MathHelper.Abs(relPt.Y);
            if (depth < 0) return 0;
            else if (depth < min_depth)
            {
                min_depth = depth;
                normal = box.GetAxis(1) * ((relPt.Y < 0) ? -1 : 1);
            }
            depth = box.HalfSize.Z - MathHelper.Abs(relPt.Z);
            if (depth < 0) return 0;
            else if (depth < min_depth)
            {
                min_depth = depth;
                normal = box.GetAxis(2) * ((relPt.Z < 0) ? -1 : 1);
            }
            Contact contact = new Contact();
            contact.ContactNormal = normal;
            contact.ContactPoint = point;
            contact.Penetration = min_depth;
            contact.SetData(box.Body, null, data.Friction, data.Restitution);
            data.Contacts.Add(contact);
            data.AddContacts(1);
            return 1;
        }
        public static int BoxAndSphere(CollisionBox box, CollisionSphere sphere, ref CollisionData data)
        {
            Vector3 centre = sphere.GetAxis(3);
            Vector3 relCentre = box.Transform.TransformInverse(centre);
            if (MathHelper.Abs(relCentre.X) - sphere.Radius > box.HalfSize.X ||
                MathHelper.Abs(relCentre.Y) - sphere.Radius > box.HalfSize.Y ||
                MathHelper.Abs(relCentre.Z) - sphere.Radius > box.HalfSize.Z)
            {
                return 0;
            }
            Vector3 closestPt = new Vector3();
            float dist;
            dist = relCentre.X;
            if (dist > box.HalfSize.X) dist = box.HalfSize.X;
            if (dist < -box.HalfSize.X) dist = -box.HalfSize.X;
            closestPt.X = dist;
            dist = relCentre.Y;
            if (dist > box.HalfSize.Y) dist = box.HalfSize.Y;
            if (dist < -box.HalfSize.Y) dist = -box.HalfSize.Y;
            closestPt.Y = dist;
            dist = relCentre.Z;
            if (dist > box.HalfSize.Z) dist = box.HalfSize.Z;
            if (dist < -box.HalfSize.Z) dist = -box.HalfSize.Z;
            closestPt.Z = dist;
            dist = (closestPt - relCentre).SquaredMagnitude;
            if (dist > sphere.Radius * sphere.Radius) return 0;
            Vector3 closestPtWorld = box.Transform.Transform(closestPt);
            Contact contact = new Contact();
            contact.ContactNormal = (closestPtWorld - centre);
            contact.ContactNormal.Normalize();
            contact.ContactPoint = closestPtWorld;
            contact.Penetration = sphere.Radius - MathHelper.Sqrt(dist);
            contact.SetData(box.Body, sphere.Body,
                data.Friction, data.Restitution);
            data.Contacts.Add(contact);
            data.AddContacts(1);
            return 1;
        }
        static float penetrationOnAxis(CollisionBox one, CollisionBox two, Vector3 axis, Vector3 toCentre)
        {
            float oneProject = IntersectionTests.transformToAxis(one, axis);
            float twoProject = IntersectionTests.transformToAxis(two, axis);
            float distance = MathHelper.Abs(Vector3.Dot(toCentre, axis));
            return oneProject + twoProject - distance;
        }
        static bool tryAxis(CollisionBox one, CollisionBox two, Vector3 axis, Vector3 toCentre, int index, ref float smallestPenetration, ref int smallestCase)
        {
            if (axis.SquaredMagnitude < 0.0001) return true;
            axis.Normalize();
            float penetration = penetrationOnAxis(one, two, axis, toCentre);
            if (penetration < 0) return false;
            if (penetration < smallestPenetration)
            {
                smallestPenetration = penetration;
                smallestCase = index;
            }
            return true;
        }
        static void fillPointFaceBoxBox(CollisionBox one, CollisionBox two, Vector3 toCentre, CollisionData data, int best, float pen)
        {
            Contact contact = new Contact();
            Vector3 normal = one.GetAxis(best);
            if (Vector3.Dot(one.GetAxis(best), toCentre) > 0)
            {
                normal = normal * -1.0f;
            }
            Vector3 vertex = two.HalfSize;
            if (Vector3.Dot(two.GetAxis(0), normal) < 0) vertex.X = -vertex.X;
            if (Vector3.Dot(two.GetAxis(1), normal) < 0) vertex.Y = -vertex.Y;
            if (Vector3.Dot(two.GetAxis(2), normal) < 0) vertex.Z = -vertex.Z;
            contact.ContactNormal = normal;
            contact.Penetration = pen;
            contact.ContactPoint = Vector3.Transform(vertex, two.Transform);
            contact.SetData(one.Body, two.Body, data.Friction, data.Restitution);
            data.Contacts.Add(contact);
        }
        static Vector3 contactPoint(Vector3 pOne, Vector3 dOne, float oneSize, Vector3 pTwo, Vector3 dTwo, float twoSize, bool useOne)
        {
            Vector3 toSt, cOne, cTwo;
            float dpStaOne, dpStaTwo, dpOneTwo, smOne, smTwo;
            float denom, mua, mub;
            smOne = dOne.SquaredMagnitude;
            smTwo = dTwo.SquaredMagnitude;
            dpOneTwo = Vector3.Dot(dTwo, dOne);
            toSt = pOne - pTwo;
            dpStaOne = Vector3.Dot(dOne, toSt);
            dpStaTwo = Vector3.Dot(dTwo, toSt);
            denom = smOne * smTwo - dpOneTwo * dpOneTwo;
            if (MathHelper.Abs(denom) < 0.0001f)
            {
                return useOne ? pOne : pTwo;
            }
            mua = (dpOneTwo * dpStaTwo - smTwo * dpStaOne) / denom;
            mub = (smOne * dpStaTwo - dpOneTwo * dpStaOne) / denom;
            if (mua > oneSize || mua < -oneSize || mub > twoSize || mub < -twoSize)
            {
                return useOne ? pOne : pTwo;
            }
            else
            {
                cOne = pOne + dOne * mua;
                cTwo = pTwo + dTwo * mub;

                return cOne * 0.5f + cTwo * 0.5f;
            }
        }
        static int CHECK_OVERLAP(Vector3 axis, int index, CollisionBox one, CollisionBox two, Vector3 toCenter, ref float pen, ref int best)
        {
            if (!tryAxis(one, two, axis, toCenter, index, ref pen, ref best))
                return 0;
            return 1;
        }
    }
}
