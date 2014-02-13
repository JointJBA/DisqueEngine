using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Xna.Cameras;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace xnatest
{
    public class FlightCamera : Camera
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public FlightCamera(Vector3 position, Quaternion rotation, GraphicsDevice graphics)
            : base(graphics)
        {
            Position = position;
            Rotation = rotation;
        }
        public override void Update()
        {
            View = Matrix.CreateTranslation(Position) * Matrix.CreateFromQuaternion(Rotation);
        }
    }
}
