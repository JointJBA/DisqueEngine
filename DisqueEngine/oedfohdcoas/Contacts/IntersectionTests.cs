using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Math;

namespace Disque.RigidBodies.Contacts
{
    public static class IntersectionTests
    {
        public static bool SphereAndHalfSpace(CollisionSphere sphere, CollisionPlane plane)
        {
            float ballDistance = Vector3.Dot(plane.Normal, sphere.GetAxis(3)) - sphere.Radius;
            return ballDistance <= plane.D;
        }
        public static bool SphereAndSphere(CollisionSphere one, CollisionSphere two)
        {
            Vector3 midline = one.GetAxis(3) - two.GetAxis(3);
            return midline.SquaredMagnitude < (one.Radius + two.Radius) * (one.Radius + two.Radius);
        }
        public static bool BoxAndBox(CollisionBox one, CollisionBox two)
        {
            Vector3 toCenter = two.GetAxis(3) - one.GetAxis(3);

            return (
                // Check on box one's axes first
                overlapOnAxis(one, two, one.GetAxis(0), toCenter) &&
                overlapOnAxis(one, two,one.GetAxis(1), toCenter) &&
                overlapOnAxis(one, two,one.GetAxis(2), toCenter) &&

                // And on two's
                overlapOnAxis(one, two,two.GetAxis(0), toCenter) &&
                overlapOnAxis(one, two,two.GetAxis(1), toCenter) &&
                overlapOnAxis(one, two,two.GetAxis(2), toCenter) &&

                // Now on the cross products
                overlapOnAxis(one, two,Vector3.Cross(one.GetAxis(0), two.GetAxis(0)), toCenter) &&
                overlapOnAxis(one, two,Vector3.Cross(one.GetAxis(0), two.GetAxis(1)), toCenter) &&
                overlapOnAxis(one, two,Vector3.Cross(one.GetAxis(0), two.GetAxis(2)), toCenter) &&
                overlapOnAxis(one, two,Vector3.Cross(one.GetAxis(1), two.GetAxis(0)), toCenter) &&
                overlapOnAxis(one, two,Vector3.Cross(one.GetAxis(1), two.GetAxis(1)), toCenter) &&
                overlapOnAxis(one, two,Vector3.Cross(one.GetAxis(1), two.GetAxis(2)), toCenter) &&
                overlapOnAxis(one, two,Vector3.Cross(one.GetAxis(2), two.GetAxis(0)), toCenter) &&
                overlapOnAxis(one, two,Vector3.Cross(one.GetAxis(2), two.GetAxis(1)), toCenter) &&
                overlapOnAxis(one, two,Vector3.Cross(one.GetAxis(2), two.GetAxis(2)), toCenter)
            );
        }
        public static bool BoxAndHalfSpace(CollisionBox box, CollisionPlane plane)
        {
            float projectedRadius = transformToAxis(box, plane.Normal);
            float boxDistance = Vector3.Dot(plane.Normal, box.GetAxis(3)) - projectedRadius;
            return boxDistance <= plane.D;
        }
        internal static float transformToAxis(CollisionBox box, Vector3 axis)
        {
            return
                box.HalfSize.X * MathHelper.Abs(Vector3.Dot(axis, box.GetAxis(0))) +
                box.HalfSize.Y * MathHelper.Abs(Vector3.Dot(axis, box.GetAxis(1))) +
                box.HalfSize.Z * MathHelper.Abs(Vector3.Dot(axis, box.GetAxis(2)));
        }
        static bool overlapOnAxis(CollisionBox one, CollisionBox two, Vector3 axis, Vector3 toCentre)
        {
            // Project the half-size of one onto axis
            float oneProject = transformToAxis(one, axis);
            float twoProject = transformToAxis(two, axis);

            // Project this onto the axis
            float distance = MathHelper.Abs(Vector3.Dot(toCentre, axis));

            // Check for overlap
            return (distance < oneProject + twoProject);
        }
    }
}