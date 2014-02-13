using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Disque.Xna.Cameras
{
    public class FreeCamera : Camera
    {
        public float Yaw { get; set; }
        public float Pitch { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Target { get; private set; }
        private Vector3 translation;
        public FreeCamera(Vector3 Position, float Yaw, float Pitch, GraphicsDevice graphics)
            : base(graphics)
        {
            this.Position = Position;
            this.Yaw = Yaw;
            this.Pitch = Pitch;
            translation = Vector3.Zero;
            Update();
        }
        public void Rotate(float yawchange, float pitchchange)
        {
            this.Yaw += yawchange;
            this.Pitch += pitchchange;
        }
        public void Translate(Vector3 translatechange)
        {
            translation += translatechange;
        }
        public override void Update()
        {
            Matrix rotation = Matrix.CreateFromYawPitchRoll(Yaw, Pitch, 0);
            translation = Vector3.Transform(translation, rotation);
            Position += translation;
            translation = Vector3.Zero;
            Vector3 forward = Vector3.Transform(Vector3.Forward, rotation);
            Target = Position + forward;
            Vector3 up = Vector3.Transform(Vector3.Up, rotation);
            View = Matrix.CreateLookAt(Position, Target, up);
            base.Update();
        }
    }
}
