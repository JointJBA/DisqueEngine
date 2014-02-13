using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Core;
using Disque.Math;

namespace Disque.RigidBodies.Forces
{
    public class Buoyancy : ForceGenerator
    {
        public float MaxDepth { get; set; }
        public float Volume { get; set; }
        public float WaterHeight { get; set; }
        public float LiquidDensity { get; set; }
        public Vector3 CenterOfBuoyancy { get; set; }
        public Buoyancy(float maxDepth, float volume, float waterHeight, float liquidDensity, Vector3 centerOfBuoyancy)
        {
            MaxDepth = maxDepth;
            Volume = volume;
            WaterHeight = waterHeight;
            LiquidDensity = liquidDensity;
            CenterOfBuoyancy = centerOfBuoyancy;
        }
        public override void UpdateForce(RigidBody body, float duration)
        {
            Vector3 pointInWorld = body.GetPointInWorldSpace(CenterOfBuoyancy);
            float depth = pointInWorld.Y;
            if (depth >= WaterHeight + MaxDepth) return;
            Vector3 force = new Vector3(0, 0, 0);
            if (depth <= WaterHeight - MaxDepth)
            {
                force.Y = LiquidDensity * Volume;
                body.AddForceAtBodyPoint(force, CenterOfBuoyancy);
                return;
            }
            force.Y = LiquidDensity * Volume * ((depth - MaxDepth - WaterHeight) / 2.0f) * MaxDepth;
            body.AddForceAtBodyPoint(force, CenterOfBuoyancy);
        }
    }
}
