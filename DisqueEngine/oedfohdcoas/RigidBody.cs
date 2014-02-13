using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;
using Disque.RigidBodies.Contacts;

namespace Disque.RigidBodies
{
    public class RigidBody
    {
        public Vector3 Position;
        public Vector3 Acceleration;
        public Vector3 Velocity;
        public Vector3 ForceAccumulation;
        public Matrix3 InverseInertiaTensor = Matrix3.Identity;
        public Matrix3 InverseInertiaTensorWorld = Matrix3.Identity;
        public Matrix4 TransformMatrix = Matrix4.Identity;
        public Quaternion Orientation = Quaternion.Identity;
        public Vector3 Rotation = new Vector3();
        public Vector3 TorqueAccumulation = new Vector3();
        public Vector3 LastFrameAcceleration = new Vector3();
        public float Mass = 1;
        public float InverseMass
        {
            get { return 1.0f / Mass; }
        }
        public float LinearDrag = 1;
        public float AngularDrag = 1;
        public float Motion ;
        public bool CanSleep;
        public bool IsAwake;
        public Vector3 GetPointInLocalSpace(Vector3 point)
        {
            return TransformMatrix.TransformInverse(point);
        }
        public Vector3 GetPointInWorldSpace(Vector3 point)
        {
            return TransformMatrix.Transform(point);
        }
        public Vector3 GetDirectionInLocalSpace(Vector3 point)
        {
            return TransformMatrix.TransformInverseDirection(point);
        }
        public Vector3 GetDirectionInWorldSpace(Vector3 point)
        {
            return TransformMatrix.TransformDirection(point);
        }
        public void CalculateDerivedData()
        {
            Orientation.Normalize();
            TransformMatrix = _calculateTransformMatrix(Position, Orientation);
            InverseInertiaTensorWorld = _transformInertiaTensor(Orientation, InverseInertiaTensor, TransformMatrix);
        }
        public void Integrate(float duration)
        {
            if (!IsAwake) return;
            LastFrameAcceleration = Acceleration;
            LastFrameAcceleration += (ForceAccumulation * InverseMass);
            Vector3 angularAcceleration = InverseInertiaTensorWorld.Transform(TorqueAccumulation);
            Velocity += (LastFrameAcceleration * duration);
            Rotation += (angularAcceleration * duration);
            Velocity *= MathHelper.Pow(LinearDrag, duration);
            Rotation *= MathHelper.Pow(AngularDrag, duration);
            Position += (Velocity * duration);
            Orientation.AddScaledVector(Rotation, duration);
            CalculateDerivedData();
            ClearAccumulation();
            if (CanSleep)
            {
                float currentMotion = Velocity.Dot(Velocity) + Rotation.Dot(Rotation);
                float bias = MathHelper.Pow(0.5f, duration);
                Motion = bias * Motion + (1.0f - bias) * currentMotion;
                if (Motion < MathHelper.SleepEpilson) IsAwake = (false);
                else if (Motion > 10 * MathHelper.SleepEpilson) Motion = 10.0f * MathHelper.SleepEpilson;
            }
        }
        public void AddRotation(Vector3 deltarotation)
        {
            Rotation += deltarotation;
        }
        public void AddVelocity(Vector3 velocity)
        {
            Velocity += velocity;
        }
        public void AddTorque(Vector3 torque)
        {
            TorqueAccumulation += torque;
            IsAwake = true;
        }
        public void AddForceAtPoint(Vector3 force, Vector3 point)
        {
            Vector3 pt = point;
            pt -= Position;
            ForceAccumulation += force;
            TorqueAccumulation += Vector3.Cross(pt, force);
            IsAwake = true;
        }
        public void AddForceAtBodyPoint(Vector3 force, Vector3 point)
        {
            Vector3 pt = GetPointInWorldSpace(point);
            AddForceAtPoint(force, pt);
            IsAwake = true;
        }
        public void SetOrientation(Quaternion quat)
        {
            Orientation = quat;
            Orientation.Normalize();
        }
        public void GetOrientation(ref Matrix4 matrix)
        {
            matrix[0] = TransformMatrix[0];
            matrix[1] = TransformMatrix[1];
            matrix[2] = TransformMatrix[2];
            matrix[3] = TransformMatrix[4];
            matrix[4] = TransformMatrix[5];
            matrix[5] = TransformMatrix[6];
            matrix[6] = TransformMatrix[8];
            matrix[7] = TransformMatrix[9];
            matrix[8] = TransformMatrix[10];
        }
        public void SetAwake(bool awake = true)
        {
            if (awake)
            {
                IsAwake = true;
                Motion = MathHelper.SleepEpilson * 2.0f;
            }
            else
            {
                IsAwake = false;
                Velocity = new Vector3();
                Rotation = new Vector3();
            }
        }
        public void SetCanSleep(bool canSleep)
        {
            CanSleep = canSleep;
            if (!canSleep && !IsAwake) SetAwake();
        }
        public Quaternion GetOrientation()
        {
            return Orientation;
        }
        public Matrix4 GetTransform()
        {
            return TransformMatrix;
        }
        public void ClearAccumulation()
        {
            ForceAccumulation.Clear();
            TorqueAccumulation.Clear();
        }
        public void AddForce(Vector3 force)
        {
            ForceAccumulation += force;
            IsAwake = true;
        }
        public void SetInertiaTensor(Matrix3 inertiaTensor)
        {
            InverseInertiaTensor.SetInverse(inertiaTensor);
            _checkInverseInertiaTensor(InverseInertiaTensor);
        }
        void _checkInverseInertiaTensor(Matrix3 InverseInertiaTensor)
        {
        }
        static Matrix3 _transformInertiaTensor(Quaternion q, Matrix3 iitBody, Matrix4 rotmat)
        {
            float t4 = rotmat[0] * iitBody[0] + rotmat[1] * iitBody[3] + rotmat[2] * iitBody[6];
            float t9 = rotmat[0] * iitBody[1] + rotmat[1] * iitBody[4] + rotmat[2] * iitBody[7];
            float t14 = rotmat[0] * iitBody[2] + rotmat[1] * iitBody[5] + rotmat[2] * iitBody[8];
            float t28 = rotmat[4] * iitBody[0] + rotmat[5] * iitBody[3] + rotmat[6] * iitBody[6];
            float t33 = rotmat[4] * iitBody[1] + rotmat[5] * iitBody[4] + rotmat[6] * iitBody[7];
            float t38 = rotmat[4] * iitBody[2] + rotmat[5] * iitBody[5] + rotmat[6] * iitBody[8];
            float t52 = rotmat[8] * iitBody[0] + rotmat[9] * iitBody[3] + rotmat[10] * iitBody[6];
            float t57 = rotmat[8] * iitBody[1] + rotmat[9] * iitBody[4] + rotmat[10] * iitBody[7];
            float t62 = rotmat[8] * iitBody[2] + rotmat[9] * iitBody[5] + rotmat[10] * iitBody[8];
            Matrix3 iitWorld = Matrix3.Identity;
            iitWorld[0] = t4 * rotmat[0] + t9 * rotmat[1] + t14 * rotmat[2];
            iitWorld[1] = t4 * rotmat[4] + t9 * rotmat[5] + t14 * rotmat[6];
            iitWorld[2] = t4 * rotmat[8] + t9 * rotmat[9] + t14 * rotmat[10];
            iitWorld[3] = t28 * rotmat[0] + t33 * rotmat[1] + t38 * rotmat[2];
            iitWorld[4] = t28 * rotmat[4] + t33 * rotmat[5] + t38 * rotmat[6];
            iitWorld[5] = t28 * rotmat[8] + t33 * rotmat[9] + t38 * rotmat[10];
            iitWorld[6] = t52 * rotmat[0] + t57 * rotmat[1] + t62 * rotmat[2];
            iitWorld[7] = t52 * rotmat[4] + t57 * rotmat[5] + t62 * rotmat[6];
            iitWorld[8] = t52 * rotmat[8] + t57 * rotmat[9] + t62 * rotmat[10];
            return iitWorld;
        }
        static Matrix4 _calculateTransformMatrix(Vector3 position, Quaternion orientation)
        {
            Matrix4 transformMatrix = Matrix4.Identity;
            transformMatrix[0] = 1.0f - 2.0f * orientation.Z * orientation.Z - 2.0f * orientation.W * orientation.W;
            transformMatrix[1] = 2.0f * orientation.Y * orientation.Z - 2.0f * orientation.X * orientation.W;
            transformMatrix[2] = 2.0f * orientation.Y * orientation.W + 2.0f * orientation.X * orientation.Z;
            transformMatrix[3] = position.X;
            transformMatrix[4] = 2.0f * orientation.Y * orientation.Z + 2.0f * orientation.X * orientation.W;
            transformMatrix[5] = 1.0f - 2.0f * orientation.Y * orientation.Y - 2.0f * orientation.W * orientation.W;
            transformMatrix[6] = 2.0f * orientation.Z * orientation.W - 2.0f * orientation.X * orientation.Y;
            transformMatrix[7] = position.Y;
            transformMatrix[8] = 2.0f * orientation.Y * orientation.W - 2.0f * orientation.X * orientation.Z;
            transformMatrix[9] = 2.0f * orientation.Z * orientation.W + 2.0f * orientation.X * orientation.Y;
            transformMatrix[10] = 1.0f - 2.0f * orientation.Y * orientation.Y - 2.0f * orientation.Z * orientation.Z;
            transformMatrix[11] = position.Z;
            return transformMatrix;
        }
        List<ForceGenerator> forces = new List<ForceGenerator>();
        public List<ForceGenerator> ForceGenerators { get { return forces; } set { forces = value; } }
        public CollisionPrimitive CollisionPrimative { get; set; }
        BoundingSphere bs;
        public BoundingSphere BoundingSphere
        {
            get
            {
                bs.Center = Position;
                return bs;
            }
            set
            {
                bs = value;
            }
        }
        public float FrictionCoff = 1;
        public float Restitution = 1;
    }
}
