using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Disque.Xna.Cameras;

namespace Disque.Xna.Primatives
{
    public class Pyramid : IPrimitive
    {
        float height;
        float width;
        float depth;
        BasicEffect effect;
        GraphicsDevice Graphics;
        VertexPositionColor[] cube;
        Color color;
        Matrix transf = Matrix.Identity;
        Vector3 scale;
        public Matrix Transformation { get { return transf; } set { transf = value; } }
        public Vector3 Position { get; set; }
        void setvertices()
        {
            scale = new Vector3(width, height, depth);
            Vector3 top = new Vector3(0f, 1, 0f);
            Vector3 corner1 = new Vector3(-1, -1, -1);
            Vector3 corner2 = new Vector3(1, -1, -1);
            Vector3 corner3 = new Vector3(1, -1, 1);
            Vector3 corner4 = new Vector3(-1, -1, 1);
            cube[0] = new VertexPositionColor(top, color);
            cube[1] = new VertexPositionColor(corner1, color);
            cube[2] = new VertexPositionColor(corner2, color);
            cube[3] = new VertexPositionColor(top, color);
            cube[4] = new VertexPositionColor(corner2, color);
            cube[5] = new VertexPositionColor(corner3, color);
            cube[6] = new VertexPositionColor(top, color);
            cube[7] = new VertexPositionColor(corner3, color);
            cube[8] = new VertexPositionColor(corner4, color);
            cube[9] = new VertexPositionColor(top, color);
            cube[10] = new VertexPositionColor(corner4, color);
            cube[11] = new VertexPositionColor(corner1, color);
            cube[12] = new VertexPositionColor(corner1, color);
            cube[13] = new VertexPositionColor(corner4, color);
            cube[14] = new VertexPositionColor(corner3, color);
            cube[15] = new VertexPositionColor(corner3, color);
            cube[16] = new VertexPositionColor(corner2, color);
            cube[17] = new VertexPositionColor(corner1, color);
        }
        public Pyramid(Vector3 position, float width, float height, float depth, Color color, GraphicsDevice graphics)
        {
            this.height = height * 0.5f;
            this.width = width * 0.5f;
            this.depth = depth * 0.5f;
            this.color = color;
            Position = position;
            Graphics = graphics;
            cube = new VertexPositionColor[18];
            effect = new BasicEffect(graphics);
            effect.VertexColorEnabled = true;
            setvertices();
        }
        public void Draw(GameTime gametime, Camera cam)
        {
            effect.Projection = cam.Projection;
            effect.View = cam.View;
            effect.World = Matrix.CreateScale(scale) * Matrix.CreateTranslation(Position) * Transformation;
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, cube, 0, 6);
            }
        }
        public void Update(GameTime gametime)
        {
        }
        public BoundingBox BoundingBox
        {
            get 
            {
                return new BoundingBox(new Vector3(-width, -height, -depth), new Vector3(width, height, depth));
            }
        }
    }
}
