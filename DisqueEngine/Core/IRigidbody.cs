using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Math;

namespace Disque.Core
{
    public interface IRigidbody : IMovable, IMass
    {
        Matrix3 InverseInertiaTensor { get; set; }
        Matrix3 InverseInertiaTensorWorld { get; set; }
        Matrix4 TransformMatrix { get; set; }
        Quaternion Orientation { get; set; }
        Vector3 Rotation { get; set; }
        Vector3 TorqueAccumulation { get; set; }
        Vector3 LastFrameAcceleration { get; set; }
        float Motion { get; set; }
        bool CanSleep { get; set; }
        bool IsAwake { get; set; }
        void CalculateDerivedData();
        void Integrate(float duration);
        void AddRotation(Vector3 deltarotation);
        void AddTorque(Vector3 torque);
        void AddForceAtPoint(Vector3 force, Vector3 point);
        void AddForceAtBodyPoint(Vector3 force, Vector3 point);
        Vector3 GetPointInLocalSpace(Vector3 point);
        Vector3 GetPointInWorldSpace(Vector3 point);
        Vector3 GetDirectionInLocalSpace(Vector3 point);
        Vector3 GetDirectionInWorldSpace(Vector3 point);
    }
}
