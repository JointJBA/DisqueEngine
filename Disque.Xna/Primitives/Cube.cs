using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Xna;
using Disque.Xna.Cameras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Disque.Xna.Primatives
{
    public class Cube : IPrimitive
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
            Vector3 topLeftFront = new Vector3(-1.0f, 1.0f, -1.0f);
            Vector3 topLeftBack = new Vector3(-1.0f, 1.0f, 1.0f);
            Vector3 topRightFront = new Vector3(1.0f, 1.0f, -1.0f);
            Vector3 topRightBack = new Vector3(1.0f, 1.0f, 1.0f);
            Vector3 btmLeftFront = new Vector3(-1.0f, -1.0f, -1.0f);
            Vector3 btmLeftBack = new Vector3(-1.0f, -1.0f, 1.0f);
            Vector3 btmRightFront = new Vector3(1.0f, -1.0f, -1.0f);
            Vector3 btmRightBack = new Vector3(1.0f, -1.0f, 1.0f);
            Vector3 normalFront = new Vector3(0.0f, 0.0f, 1.0f);
            Vector3 normalBack = new Vector3(0.0f, 0.0f, -1.0f);
            Vector3 normalTop = new Vector3(0.0f, 1.0f, 0.0f);
            Vector3 normalBottom = new Vector3(0.0f, -1.0f, 0.0f);
            Vector3 normalLeft = new Vector3(-1.0f, 0.0f, 0.0f);
            Vector3 normalRight = new Vector3(1.0f, 0.0f, 0.0f);
            cube[0] = new VertexPositionColor(topLeftFront, color);
            cube[1] = new VertexPositionColor(btmLeftFront, color);
            cube[2] = new VertexPositionColor(topRightFront, color);
            cube[3] = new VertexPositionColor(btmLeftFront, color);
            cube[4] = new VertexPositionColor(btmRightFront, color);
            cube[5] = new VertexPositionColor(topRightFront, color);
            cube[6] = new VertexPositionColor(topLeftBack, color);
            cube[7] = new VertexPositionColor(topRightBack, color);
            cube[8] = new VertexPositionColor(btmLeftBack, color);
            cube[9] = new VertexPositionColor(btmLeftBack, color);
            cube[10] = new VertexPositionColor(topRightBack, color);
            cube[11] = new VertexPositionColor(btmRightBack, color);
            cube[12] = new VertexPositionColor(topLeftFront, color);
            cube[13] = new VertexPositionColor(topRightBack, color);
            cube[14] = new VertexPositionColor(topLeftBack, color);
            cube[15] = new VertexPositionColor(topLeftFront, color);
            cube[16] = new VertexPositionColor(topRightFront, color);
            cube[17] = new VertexPositionColor(topRightBack, color);

            // Add the vertices for the BOTTOM face. 
            cube[18] = new VertexPositionColor(btmLeftFront, color);
            cube[19] = new VertexPositionColor(btmLeftBack, color);
            cube[20] = new VertexPositionColor(btmRightBack, color);
            cube[21] = new VertexPositionColor(btmLeftFront, color);
            cube[22] = new VertexPositionColor(btmRightBack, color);
            cube[23] = new VertexPositionColor(btmRightFront, color);

            // Add the vertices for the LEFT face.
            cube[24] = new VertexPositionColor(topLeftFront, color);
            cube[25] = new VertexPositionColor(btmLeftBack, color);
            cube[26] = new VertexPositionColor(btmLeftFront, color);
            cube[27] = new VertexPositionColor(topLeftBack, color);
            cube[28] = new VertexPositionColor(btmLeftBack, color);
            cube[29] = new VertexPositionColor(topLeftFront, color);

            // Add the vertices for the RIGHT face. 
            cube[30] = new VertexPositionColor(topRightFront, color);
            cube[31] = new VertexPositionColor(btmRightFront, color);
            cube[32] = new VertexPositionColor(btmRightBack, color);
            cube[33] = new VertexPositionColor(topRightBack, color);
            cube[34] = new VertexPositionColor(topRightFront, color);
            cube[35] = new VertexPositionColor(btmRightBack, color);

            //setupface(0, new VertexPositionColor(new Vector3(-1, -1, -1), color), new VertexPositionColor(new Vector3(-1, -1, -1), color), new VertexPositionColor(new Vector3(1, -1, 1), color), new VertexPositionColor(new Vector3(1, -1, -1), color));
            //setupface(6, new VertexPositionColor(new Vector3(-1, 1, -1), color), new VertexPositionColor(new Vector3(-1, 1, -1), color), new VertexPositionColor(new Vector3(1, 1, 1), color), new VertexPositionColor(new Vector3(1, -1, 1), color));
            //setupface(12, new VertexPositionColor(new Vector3(-1, -1, -1), color), new VertexPositionColor(new Vector3(-1, 1, -1), color), new VertexPositionColor(new Vector3(1, 1, -1), color), new VertexPositionColor(new Vector3(1, -1, -1), color));
            //setupface(18, new VertexPositionColor(new Vector3(-1, -1, 1), color), new VertexPositionColor(new Vector3(-1, 1, 1), color), new VertexPositionColor(new Vector3(1, 1, 1), color), new VertexPositionColor(new Vector3(1, -1, 1), color));
            //setupface(24, new VertexPositionColor(new Vector3(-1, -1, -1), color), new VertexPositionColor(new Vector3(-1, -1, 1), color), new VertexPositionColor(new Vector3(-1, 1, 1), color), new VertexPositionColor(new Vector3(-1, 1, -1), color));
            //setupface(30, new VertexPositionColor(new Vector3(1, -1, -1), color), new VertexPositionColor(new Vector3(1, -1, 1), color), new VertexPositionColor(new Vector3(1, 1, 1), color), new VertexPositionColor(new Vector3(1, 1, -1), color));
        }
        public Cube(Vector3 position, float width, float height, float depth, Color color, GraphicsDevice graphics)
        {
            this.height = height * 0.5f;
            this.width = width * 0.5f;
            this.depth = depth * 0.5f;
            this.color = color;
            Position = position;
            Graphics = graphics;
            cube = new VertexPositionColor[36];
            effect = new BasicEffect(graphics);
            effect.VertexColorEnabled = true;
            setvertices();
        }
        void setupface(int index, VertexPositionColor v1, VertexPositionColor v2, VertexPositionColor v3, VertexPositionColor v4)
        {
            cube[index] = v1;
            cube[index + 1] = v2;
            cube[index + 2] = v3;
            cube[index + 3] = v3;
            cube[index + 4] = v4;
            cube[index + 5] = v1;
        }
        public void Draw(GameTime gametime, Camera cam)
        {
            effect.Projection = cam.Projection;
            effect.View = cam.View;
            effect.World = Transformation * (Matrix.CreateScale(scale));
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, cube, 0, 12);
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
