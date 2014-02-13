using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.RigidBodies.Springs
{
    public class Spring : ForceGenerator
    {
        public Vector3 ConnectionPoint { get; set; }
        public Vector3 OtherConnectionPoint { get; set; }
        public RigidBody Other { get; set; }
        public float SpringConstant { get; set; }
        public float RestLength { get; set; }
        public Spring(Vector3 localconnection, Vector3 otherlocalconnection, RigidBody other, float springconstant, float restlength)
        {
            ConnectionPoint = localconnection;
            OtherConnectionPoint = otherlocalconnection;
            Other = other;
            SpringConstant = springconstant;
            RestLength = restlength;
        }
        public override void UpdateForce(RigidBody body, float duration)
        {
            Vector3 lws = body.GetPointInWorldSpace(ConnectionPoint);
            Vector3 ows = body.GetPointInWorldSpace(OtherConnectionPoint);
            Vector3 force = lws - ows;
            float magnitude = force.Magnitude;
            magnitude = MathHelper.Abs(magnitude - RestLength);
            magnitude *= SpringConstant;
            force.Normalize();
            force *= -magnitude;
            body.AddForceAtPoint(force, lws);
        }
    }
}
