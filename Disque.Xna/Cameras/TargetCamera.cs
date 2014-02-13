using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Disque.Xna.Cameras
{
    public class TargetCamera : Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }
        public TargetCamera(Vector3 position, Vector3 target, GraphicsDevice graphicsdevice)
            : base(graphicsdevice)
        {
            Target = target;
            Position = position;
            Update();
        }
        public override void Update()
        {
            Vector3 forward = Target - Position;
            Vector3 side = Vector3.Cross(forward, Vector3.Up);
            Vector3 up = Vector3.Cross(forward, side);
            this.View = Matrix.CreateLookAt(Position, Target, up);
        }
    }
}
