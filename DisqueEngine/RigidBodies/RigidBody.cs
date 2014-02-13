using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Math;

namespace Disque.RigidBodies
{
    public class RigidBody
    {
        protected float inverseMass, linearDamping, angularDamping;
        protected bool isAwake, canSleep;
        protected Vector3 velocity, acceleration, lastFrameAcceleration, position, rotation, forceAccum, torqueAccum;
        protected Quaternion orientation = Quaternion.Identity;
        protected Matrix4 transformMatrix = Matrix4.Identity;
        protected Matrix3 inverseInertiaTensor = Matrix3.Identity, inverseInertiaTensorWorld = Matrix3.Identity;
        public void CalculateDerivedData()
        {
            orientation.Normalize();
            _calculateTransformMatrix(out transformMatrix, position, orientation);
            _transformInertiaTensor(out inverseInertiaTensorWorld, orientation, inverseInertiaTensor, transformMatrix);
        }
        public void SetInertiaTensor(Matrix3 matrix)
        {
            inverseInertiaTensor.SetInverse(matrix);
        }
        public void AddForce(Vector3 force)
        {
            forceAccum += force;
            isAwake = true;
        }
        public void AddForceAtPoint(Vector3 force, Vector3 point)
        {
            Vector3 pt = point;
            pt -= position;
            forceAccum += force;
            torqueAccum += Vector3.Cross(pt, force);
            isAwake = true;
        }
        public void AddForceAtBodyPoint(Vector3 force, Vector3 point)
        {
            Vector3 pt = GetPointInWorldSpace(point);
            AddForceAtPoint(force, pt);
            isAwake = true;
        }
        public Vector3 GetPointInWorldSpace(Vector3 p)
        {
            return transformMatrix.Transform(p);
        }
        public Vector3 GetPointInLocalSpace(Vector3 p)
        {
            return transformMatrix.Transform(p);
        }
        public void ClearAccumulators()
        {
            forceAccum.Clear();
            torqueAccum.Clear();
        }
        public void Integrate(float duration)
        {
            lastFrameAcceleration = acceleration;
            lastFrameAcceleration += (forceAccum * inverseMass);
            Vector3 angularAcceleration = inverseInertiaTensorWorld.Transform(torqueAccum);
            velocity += (lastFrameAcceleration * duration);
            rotation += (angularAcceleration * duration);
            velocity *= MathHelper.Pow(linearDamping, duration);
            rotation *= MathHelper.Pow(angularDamping, duration);
            position += (velocity * duration);
            orientation.AddScaledVector(rotation, duration);
            CalculateDerivedData();
            ClearAccumulators();
        }
        public bool HasFiniteMass()
        {
            return inverseMass > 0.0f;
        }
        public float GetMass()
        {
            return 1.0f / inverseMass;
        }
        public Matrix4 GetTransform()
        {
            return transformMatrix;
        }
        public Vector3 GetVelocity()
        {
            return velocity;
        }
        static internal void _calculateTransformMatrix(out Matrix4 transformMatrix, Vector3 position, Quaternion orientation)
        {
            transformMatrix = Matrix4.Identity;
            transformMatrix[0] = 1.0f - 2.0f * orientation.Z * orientation.Z -
    2.0f * orientation.W * orientation.W;
            transformMatrix[1] = 2.0f * orientation.Y * orientation.Z -
                2.0f * orientation.X * orientation.W;
            transformMatrix[2] = 2.0f * orientation.Y * orientation.W +
                2.0f * orientation.X * orientation.Z;
            transformMatrix[3] = position.X;
            transformMatrix[4] = 2.0f * orientation.Y * orientation.Z +
                2.0f * orientation.X * orientation.W;
            transformMatrix[5] = 1.0f - 2.0f * orientation.Y * orientation.Y -
                2.0f * orientation.W * orientation.W;
            transformMatrix[6] = 2.0f * orientation.Z * orientation.W -
                2.0f * orientation.X * orientation.Y;
            transformMatrix[7] = position.Y;
            transformMatrix[8] = 2.0f * orientation.Y * orientation.W -
                2.0f * orientation.X * orientation.Z;
            transformMatrix[9] = 2.0f * orientation.Z * orientation.W +
                2.0f * orientation.X * orientation.Y;
            transformMatrix[10] = 1.0f - 2.0f * orientation.Y * orientation.Y -
                2.0f * orientation.Z * orientation.Z;
            transformMatrix[11] = position.Z;
        }
        static internal void _transformInertiaTensor(out Matrix3 iitWorld, Quaternion q, Matrix3 iitBody, Matrix4 rotmat)
        {
            float t4 = rotmat[0] * iitBody[0] +
    rotmat[1] * iitBody[3] +
    rotmat[2] * iitBody[6];
            float t9 = rotmat[0] * iitBody[1] +
                rotmat[1] * iitBody[4] +
                rotmat[2] * iitBody[7];
            float t14 = rotmat[0] * iitBody[2] +
                rotmat[1] * iitBody[5] +
                rotmat[2] * iitBody[8];
            float t28 = rotmat[4] * iitBody[0] +
                rotmat[5] * iitBody[3] +
                rotmat[6] * iitBody[6];
            float t33 = rotmat[4] * iitBody[1] +
                rotmat[5] * iitBody[4] +
                rotmat[6] * iitBody[7];
            float t38 = rotmat[4] * iitBody[2] +
                rotmat[5] * iitBody[5] +
                rotmat[6] * iitBody[8];
            float t52 = rotmat[8] * iitBody[0] +
                rotmat[9] * iitBody[3] +
                rotmat[10] * iitBody[6];
            float t57 = rotmat[8] * iitBody[1] +
                rotmat[9] * iitBody[4] +
                rotmat[10] * iitBody[7];
            float t62 = rotmat[8] * iitBody[2] +
                rotmat[9] * iitBody[5] +
                rotmat[10] * iitBody[8];
            iitWorld = Matrix3.Identity;
            iitWorld[0] = t4 * rotmat[0] +
                t9 * rotmat[1] +
                t14 * rotmat[2];
            iitWorld[1] = t4 * rotmat[4] +
                t9 * rotmat[5] +
                t14 * rotmat[6];
            iitWorld[2] = t4 * rotmat[8] +
                t9 * rotmat[9] +
                t14 * rotmat[10];
            iitWorld[3] = t28 * rotmat[0] +
                t33 * rotmat[1] +
                t38 * rotmat[2];
            iitWorld[4] = t28 * rotmat[4] +
                t33 * rotmat[5] +
                t38 * rotmat[6];
            iitWorld[5] = t28 * rotmat[8] +
                t33 * rotmat[9] +
                t38 * rotmat[10];
            iitWorld[6] = t52 * rotmat[0] +
                t57 * rotmat[1] +
                t62 * rotmat[2];
            iitWorld[7] = t52 * rotmat[4] +
                t57 * rotmat[5] +
                t62 * rotmat[6];
            iitWorld[8] = t52 * rotmat[8] +
                t57 * rotmat[9] +
                t62 * rotmat[10];
        }
    }
}
