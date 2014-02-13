using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Disque.Xna.Cameras
{
    public class ArcBallCamera : Camera
    {
        public float RotationX { get; set; }
        public float RotationY { get; set; }
        public float MinRotationY { get; set; }
        public float MaxRotationY { get; set; }
        public float Distance { get; set; }
        public float MinDistance { get; set; }
        public float MaxDistance { get; set; }
        public Vector3 Position { get; private set; }
        public Vector3 Target { get; set; }
        public ArcBallCamera(Vector3 Target, float RotationX,
        float RotationY, float MinRotationY, float MaxRotationY,
        float Distance, float MinDistance, float MaxDistance,
        GraphicsDevice graphicsDevice)
            : base(graphicsDevice)
        {
            this.Target = Target;
            this.MinRotationY = MinRotationY;
            this.MaxRotationY = MaxRotationY;
            this.RotationY = MathHelper.Clamp(RotationY, MinRotationY,
            MaxRotationY);
            this.RotationX = RotationX;
            this.MinDistance = MinDistance;
            this.MaxDistance = MaxDistance;
            this.Distance = MathHelper.Clamp(Distance, MinDistance,
            MaxDistance);
        }
        public void Move(float DistanceChange)
        {
            this.Distance += DistanceChange;
            this.Distance = MathHelper.Clamp(Distance, MinDistance,
            MaxDistance);
        }
        public void Rotate(float RotationXChange, float RotationYChange)
        {
            this.RotationX += RotationXChange;
            this.RotationY += -RotationYChange;
            this.RotationY = MathHelper.Clamp(RotationY, MinRotationY,
            MaxRotationY);
        }
        public void Translate(Vector3 PositionChange)
        {
            this.Position += PositionChange;
        }
        public override void Update()
        {
            Matrix rotation = Matrix.CreateFromYawPitchRoll(RotationX, -
            RotationY, 0);
            Vector3 translation = new Vector3(0, 0, Distance);
            translation = Vector3.Transform(translation, rotation);
            Position = Target + translation;
            Vector3 up = Vector3.Transform(Vector3.Up, rotation);
            View = Matrix.CreateLookAt(Position, Target, up);
        }
    }
}