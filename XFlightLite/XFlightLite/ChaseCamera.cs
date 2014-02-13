using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Xna.Cameras;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XFlightLite
{
    public class ChaseCamera : Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Target { get; set; }
        public Vector3 Up { get; set; }
        public ChaseCamera(Vector3 position, Vector3 target, Vector3 up, GraphicsDevice graphic)
            : base(graphic)
        {
            Position = position;
            Target = target;
            Up = up;
        }
        public override void Update()
        {
            View = Matrix.CreateLookAt(Position, Target, Up);
        }
    }
}
