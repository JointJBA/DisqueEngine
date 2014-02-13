using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.RigidBodies.Forces
{
    public class Aero : ForceGenerator
    {
        public Matrix3 Tensor { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 WindSpeed { get; set; }
        public Aero(Matrix3 tensor, Vector3 position, Vector3 windspeed)
        {
            Tensor = tensor;
            Position = position;
            WindSpeed = windspeed;
        }
        public override void UpdateForce(RigidBody body, float duration)
        {
            updateforcefromtensor(body, duration, Tensor);
        }
        internal void updateforcefromtensor(RigidBody body, float duration, Matrix3 tensor)
        {
            Vector3 velocity = body.Velocity;
            velocity += WindSpeed;
            Vector3 bodyVel = body.GetTransform().TransformInverseDirection(velocity);
            Vector3 bodyForce = tensor.Transform(bodyVel);
            Vector3 force = body.GetTransform().TransformDirection(bodyForce);
            body.AddForceAtBodyPoint(force, Position);
        }
    }
}
