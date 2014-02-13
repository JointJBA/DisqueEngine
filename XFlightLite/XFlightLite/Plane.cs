using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Xna;
using Microsoft.Xna.Framework;
using Disque.Xna.Cameras;
using Microsoft.Xna.Framework.Graphics;
using Disque.Xna.Primatives;

namespace XFlightLite
{
    public class Plane : IDraw
    {
        public Vector3 Position { get; set; }
        Cube wings;
        Cube body;
        Cube tail;
        Cube c;
        GraphicsDevice graphic;
        public Plane(GraphicsDevice graphics)
        {
            graphic = graphics;
            setupPlane();
        }
        public void Update(GameTime gametime)
        {
            Matrix global =  Matrix.CreateScale(20, 2.5f, 20) * Matrix.CreateTranslation(Position) * transfor;
            wings.Transformation = global;
            body.Transformation = global;
            tail.Transformation = global;
            c.Transformation = global;
        }
        public void Draw(GameTime gametime, Camera camera)
        {
            wings.Draw(gametime, camera);
            body.Draw(gametime, camera);
            tail.Draw(gametime, camera);
            c.Draw(gametime, camera);
        }
        public BoundingBox BoundingBox
        {
            get 
            {
                return BoundingBox.CreateMerged(wings.BoundingBox, BoundingBox.CreateMerged(body.BoundingBox, tail.BoundingBox));
            }
        }
        void setupPlane()
        {
            wings = new Cube(new Vector3(0f, 0f, 0.2f), 1, 0.2f, 0.125f, Color.Brown, graphic);
            body = new Cube(new Vector3(0, 0, 0), 0.125f, 1, 1, Color.Blue, graphic);
            tail = new Cube(new Vector3(0, 0.5f, -0.46f), 0.015f, 1.5f, 0.1f, Color.Green, graphic);
            c = new Cube(new Vector3(0, -0.4f, -0.45f), 0.3f, 0.1f, 0.1f, Color.Yellow, graphic);
        }
        Matrix transfor = Matrix.Identity;
        public Matrix Transformation { get { return transfor; } set { transfor = value; } }
    }
}
