using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Math;

namespace Disque.RigidBodies.Contacts
{
    public class Contact
    {
        public RigidBody[] Bodies = new RigidBody[2];
        public float Friction;
        public float Restitution;
        public float Penetration;
        public Vector3 ContactPoint;
        public Vector3 ContactNormal;
        public void SetData(RigidBody one, RigidBody two, float friction, float restitution)
        {
            Bodies = new RigidBody[2];
            Bodies[0] = one;
            Bodies[1] = two;
            Friction = friction;
            Restitution = restitution;
        }
        internal Matrix3 ContactToWorld = Matrix3.Identity;
        internal Vector3 ContactVelocity;
        internal float DesiredDeltaVelocity { get; set; }
        internal Vector3[] RelativeContactPosition = new Vector3[2];
        internal void calculateinternals(float duration)
        {
            if (Bodies[0] == null) swapbodies();
            calculateContactBasis();
            RelativeContactPosition[0] = ContactPoint - Bodies[0].Position;
            if (Bodies[1] != null)
            {
                RelativeContactPosition[1] = ContactPoint - Bodies[1].Position;
            }
            ContactVelocity = calculateLocalVelocity(0, duration);
            if (Bodies[1] != null)
            {
                ContactVelocity -= calculateLocalVelocity(1, duration);
            }
            calculateDesiredDeltaVelocity(duration);
        }
        internal void swapbodies()
        {
            ContactNormal *= -1;
            RigidBody temp = Bodies[0];
            Bodies[0] = Bodies[1];
            Bodies[1] = temp;
        }
        internal void matchAwakeState()
        {
            if (Bodies[1] == null) return;

            bool body0awake = Bodies[0].IsAwake;
            bool body1awake = Bodies[1].IsAwake;

            // Wake up only the sleeping one
            if (body0awake ^ body1awake)
            {
                if (body0awake) Bodies[1].SetAwake();
                else Bodies[0].SetAwake();
            }
        }
        internal static float velocityLimit = 0.25f;
        internal void calculateDesiredDeltaVelocity(float duration)
        {
            float velocityFromAcc = 0;
            if (Bodies[0].IsAwake)
                velocityFromAcc = Vector3.Dot(Bodies[0].LastFrameAcceleration, (duration * ContactNormal));
            if (Bodies[1] != null && Bodies[1].IsAwake)
                velocityFromAcc -= Vector3.Dot(Bodies[1].LastFrameAcceleration, (duration * ContactNormal));
            float thisRestitution = Restitution;
            if (MathHelper.Abs(ContactVelocity.X) < velocityLimit)
                thisRestitution = 0.0f;
            DesiredDeltaVelocity = -ContactVelocity.X - thisRestitution * (ContactVelocity.X - velocityFromAcc);
        }
        internal void calculateContactBasis()
        {
            Vector3[] contactTangent = new Vector3[2];
            if (MathHelper.Abs(ContactNormal.X) > MathHelper.Abs(ContactNormal.Y))
            {
                float s = 1.0f / MathHelper.Sqrt(ContactNormal.Z * ContactNormal.Z +
                    ContactNormal.X * ContactNormal.X);
                contactTangent[0].X = ContactNormal.Z * s;
                contactTangent[0].Y = 0;
                contactTangent[0].Z = -ContactNormal.X * s;
                contactTangent[1].X = ContactNormal.Y * contactTangent[0].X;
                contactTangent[1].Y = ContactNormal.Z * contactTangent[0].X -
                    ContactNormal.X * contactTangent[0].Z;
                contactTangent[1].Z = -ContactNormal.Y * contactTangent[0].X;
            }
            else
            {
                float s = 1.0f / MathHelper.Sqrt(ContactNormal.Z * ContactNormal.Z + ContactNormal.Y * ContactNormal.Y);
                contactTangent[0].X = 0;
                contactTangent[0].Y = -ContactNormal.Z * s;
                contactTangent[0].Z = ContactNormal.Y * s;
                contactTangent[1].X = ContactNormal.Y * contactTangent[0].Z -
                    ContactNormal.Z * contactTangent[0].Y;
                contactTangent[1].Y = -ContactNormal.X * contactTangent[0].Z;
                contactTangent[1].Z = ContactNormal.X * contactTangent[0].Y;
            }
            ContactToWorld.SetComponents(
                ContactNormal,
                contactTangent[0],
                contactTangent[1]);
        }
        internal Vector3 calculateLocalVelocity(int bodyIndex, float duration)
        {
            RigidBody thisBody = Bodies[bodyIndex];
            Vector3 velocity = Vector3.Cross(thisBody.Rotation, RelativeContactPosition[bodyIndex]);
            velocity += thisBody.Velocity;
            Vector3 contactVelocity = ContactToWorld.TransformTranspose(velocity);
            Vector3 accVelocity = thisBody.LastFrameAcceleration * duration;
            accVelocity = ContactToWorld.TransformTranspose(accVelocity);
            accVelocity.X = 0;
            contactVelocity += accVelocity;
            return contactVelocity;
        }
        //void applyImpulse(Vector3 impulse, RigidBody body, Vector3 velocityChange, Vector3 rotationChange)
        //{
        //}
        internal void applyVelocityChange(Vector3[] velocityChange, Vector3[] rotationChange)
        {
            Matrix3[] inverseInertiaTensor = new Matrix3[2];
            inverseInertiaTensor[0] = Bodies[0].InverseInertiaTensorWorld;
            if (Bodies[1] != null)
                inverseInertiaTensor[1] = Bodies[1].InverseInertiaTensorWorld;
            Vector3 impulseContact;
            if (Friction == 0.0f)
            {
                impulseContact = calculateFrictionlessImpulse(inverseInertiaTensor);
            }
            else
            {
                impulseContact = calculateFrictionImpulse(inverseInertiaTensor);
            }
            Vector3 impulse = ContactToWorld.Transform(impulseContact);
            Vector3 impulsiveTorque = Vector3.Cross(RelativeContactPosition[0], impulse);
            rotationChange[0] = inverseInertiaTensor[0].Transform(impulsiveTorque);
            velocityChange[0] = new Vector3();
            velocityChange[0] += (impulse * Bodies[0].InverseMass);
            Bodies[0].AddVelocity(velocityChange[0]);
            Bodies[0].AddRotation(rotationChange[0]);
            if (Bodies[1] != null)
            {
                Vector3 impulsiveTorque2 = Vector3.Cross(impulse, RelativeContactPosition[1]);
                rotationChange[1] = inverseInertiaTensor[1].Transform(impulsiveTorque2);
                velocityChange[1] = new Vector3();
                velocityChange[1] += (impulse * -Bodies[1].InverseMass);
                Bodies[1].AddVelocity(velocityChange[1]);
                Bodies[1].AddRotation(rotationChange[1]);
            }
        }
        internal void applyPositionChange(Vector3[] linearChange, Vector3[] angularChange, float duration)
        {
            const float angularLimit = 0.2f;
            float[] angularMove = new float[2];
            float[] linearMove = new float[2];
            float totalInertia = 0;
            float[] linearInertia = new float[2];
            float[] angularInertia = new float[2];
            for (uint i = 0; i < 2; i++) if (Bodies[i] != null)
                {
                    Matrix3 inverseInertiaTensor = Matrix3.Identity;
                    inverseInertiaTensor = Bodies[i].InverseInertiaTensorWorld;
                    Vector3 angularInertiaWorld = Vector3.Cross(RelativeContactPosition[i], ContactNormal);
                    angularInertiaWorld = inverseInertiaTensor.Transform(angularInertiaWorld);
                    angularInertiaWorld = Vector3.Cross(angularInertiaWorld, RelativeContactPosition[i]);
                    angularInertia[i] = Vector3.Dot(angularInertiaWorld, ContactNormal);
                    linearInertia[i] = Bodies[i].InverseMass;
                    totalInertia += linearInertia[i] + angularInertia[i];
                }
            for (uint i = 0; i < 2; i++) if (Bodies[i] != null)
                {
                    float sign = (i == 0) ? 1 : -1;
                    angularMove[i] = sign * Penetration * (angularInertia[i] / totalInertia);
                    linearMove[i] = sign * Penetration * (linearInertia[i] / totalInertia);
                    Vector3 projection = RelativeContactPosition[i];
                    projection += ContactNormal * Vector3.Dot(-1 * RelativeContactPosition[i], ContactNormal);
                    float maxMagnitude = angularLimit * projection.Magnitude;
                    if (angularMove[i] < -maxMagnitude)
                    {
                        float totalMove = angularMove[i] + linearMove[i];
                        angularMove[i] = -maxMagnitude;
                        linearMove[i] = totalMove - angularMove[i];
                    }
                    else if (angularMove[i] > maxMagnitude)
                    {
                        float totalMove = angularMove[i] + linearMove[i];
                        angularMove[i] = maxMagnitude;
                        linearMove[i] = totalMove - angularMove[i];
                    }
                    if (angularMove[i] == 0)
                    {
                        angularChange[i] = new Vector3();
                    }
                    else
                    {
                        Vector3 targetAngularDirection = RelativeContactPosition[i] * (ContactNormal);
                        Matrix3 inverseInertiaTensor = Matrix3.Identity;
                        inverseInertiaTensor = Bodies[i].InverseInertiaTensorWorld;
                        angularChange[i] = inverseInertiaTensor.Transform(targetAngularDirection) * (angularMove[i] / angularInertia[i]);
                    }
                    linearChange[i] = ContactNormal * linearMove[i];
                    Vector3 pos = new Vector3();
                    pos = Bodies[i].Position;
                    pos += (ContactNormal * linearMove[i]);
                    Bodies[i].Position = (pos);
                    Quaternion q = Bodies[i].Orientation;
                    q.AddScaledVector(angularChange[i], ((float)1.0));
                    Bodies[i].Orientation = (q);
                    if (!Bodies[i].IsAwake) Bodies[i].CalculateDerivedData();
                }
        }
        internal Vector3 calculateFrictionlessImpulse(Matrix3[] inverseInertiaTensor)
        {
            Vector3 impulseContact = new Vector3();
            Vector3 deltaVelWorld = Vector3.Cross(RelativeContactPosition[0], ContactNormal);
            deltaVelWorld = inverseInertiaTensor[0].Transform(deltaVelWorld);
            deltaVelWorld = Vector3.Cross(deltaVelWorld, RelativeContactPosition[0]);
            float deltaVelocity = Vector3.Dot(deltaVelWorld, ContactNormal);
            deltaVelocity += Bodies[0].InverseMass;
            if (Bodies[1] != null)
            {
                Vector3 vdeltaVelWorld = Vector3.Cross(RelativeContactPosition[1], ContactNormal);
                vdeltaVelWorld = inverseInertiaTensor[1].Transform(vdeltaVelWorld);
                vdeltaVelWorld = Vector3.Cross(vdeltaVelWorld, RelativeContactPosition[1]);
                deltaVelocity += Vector3.Dot(vdeltaVelWorld, ContactNormal);
                deltaVelocity += Bodies[1].InverseMass;
            }
            impulseContact.X = DesiredDeltaVelocity / deltaVelocity;
            impulseContact.Y = 0;
            impulseContact.Z = 0;
            return impulseContact;
        }
        internal Vector3 calculateFrictionImpulse(Matrix3[] inverseInertiaTensor)
        {
            Vector3 impulseContact = new Vector3();
            float inverseMass = Bodies[0].InverseMass;
            Matrix3 impulseToTorque = Matrix3.Identity;
            impulseToTorque.SetSkewSymmetric(RelativeContactPosition[0]);
            Matrix3 deltaVelWorld = impulseToTorque;
            deltaVelWorld *= inverseInertiaTensor[0];
            deltaVelWorld *= impulseToTorque;
            deltaVelWorld *= -1;
            if (Bodies[1] != null)
            {
                impulseToTorque.SetSkewSymmetric(RelativeContactPosition[1]);
                Matrix3 deltaVelWorld2 = impulseToTorque;
                deltaVelWorld2 *= inverseInertiaTensor[1];
                deltaVelWorld2 *= impulseToTorque;
                deltaVelWorld2 *= -1;
                deltaVelWorld += deltaVelWorld2;
                inverseMass += Bodies[1].InverseMass;
            }
            Matrix3 deltaVelocity = ContactToWorld.Transpose();
            deltaVelocity *= deltaVelWorld;
            deltaVelocity *= ContactToWorld;
            deltaVelocity[0] += inverseMass;
            deltaVelocity[4] += inverseMass;
            deltaVelocity[8] += inverseMass;
            Matrix3 impulseMatrix = deltaVelocity.Inverse();
            Vector3 velKill = new Vector3(DesiredDeltaVelocity, -ContactVelocity.Y, -ContactVelocity.Z);
            impulseContact = impulseMatrix.Transform(velKill);
            float planarImpulse = MathHelper.Sqrt(impulseContact.Y * impulseContact.Y +
                impulseContact.Z * impulseContact.Z
                );
            if (planarImpulse > impulseContact.X * Friction)
            {
                impulseContact.Y /= planarImpulse;
                impulseContact.Z /= planarImpulse;

                impulseContact.X = deltaVelocity[0] +
                    deltaVelocity[1] * Friction * impulseContact.Y +
                    deltaVelocity[2] * Friction * impulseContact.Z;
                impulseContact.X = DesiredDeltaVelocity / impulseContact.X;
                impulseContact.Y *= Friction * impulseContact.X;
                impulseContact.Z *= Friction * impulseContact.X;
            }
            return impulseContact;
        }
    }
}
