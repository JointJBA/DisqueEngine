using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Disque.Xna.Cameras;
using Microsoft.Xna.Framework.Graphics;

namespace Disque.Xna.Primatives
{
    public class Sphere : IPrimitive
    {
        VertexPositionColor[] vertices;
        VertexBuffer vbuffer;
        short[] indices;
        IndexBuffer ibuffer;
        int resolution = 45;
        int nvertices, nindices;
        BasicEffect effect;
        GraphicsDevice graphicd;
        Matrix transfomation = Matrix.Identity;
        public Sphere(Vector3 position, float radius, Color color, GraphicsDevice graphic)
        {
            Position = position;
            Radius = radius;
            Color = color;
            graphicd = graphic;
            effect = new BasicEffect(graphicd);
            effect.VertexColorEnabled = true;
            nvertices = resolution * resolution;
            nindices = resolution * resolution * 6;
            vbuffer = new VertexBuffer(graphicd, typeof(VertexPositionColor), nvertices, BufferUsage.WriteOnly);
            ibuffer = new IndexBuffer(graphicd, IndexElementSize.SixteenBits, nindices, BufferUsage.WriteOnly);
            createspherevertices();
            createindices();
            vbuffer.SetData<VertexPositionColor>(vertices);
            ibuffer.SetData<short>(indices);
        }
        public Vector3 Position
        {
            get;
            set;
        }
        public Matrix Transformation
        {
            get
            {
                return transfomation;
            }
            set
            {
                transfomation = value;
            }
        }
        public Color Color { get; set; }
        public float Radius
        {
            get;
            set;
        }
        public void Update(GameTime gametime)
        {
        }
        public void Draw(GameTime gametime, Camera camera)
        {
            effect.World = Matrix.CreateTranslation(Position) * transfomation;
            effect.View = camera.View;
            effect.Projection = camera.Projection;
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicd.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertices, 0, nvertices, indices, 0, indices.Length / 3);
            }
        }
        void createspherevertices()
        {
            vertices = new VertexPositionColor[nvertices];
            Vector3 center = new Vector3(0, 0, 0);
            Vector3 rad = new Vector3(1 * Radius, 0, 0);
            for (int x = 0; x < resolution; x++)
            {
                float difx = 360.0f / resolution;
                for (int y = 0; y < resolution; y++)
                {
                    float dify = 360.0f / resolution;
                    Matrix zrot = Matrix.CreateRotationZ(MathHelper.ToRadians(y * dify));
                    Matrix yrot = Matrix.CreateRotationY(MathHelper.ToRadians(x * difx));
                    Vector3 point = Vector3.Transform(Vector3.Transform(rad, zrot), yrot);
                    vertices[x + y * resolution] = new VertexPositionColor(point,Color);
                }
            }
        }
        void createindices()
        {
            indices = new short[nindices];
            int i = 0;
            for (int x = 0; x < resolution; x++)
            {
                for (int y = 0; y < resolution; y++)
                {
                    int s1 = x == (resolution - 1) ? 0 : x + 1;
                    int s2 = y == (resolution - 1) ? 0 : y + 1;
                    short upperLeft = (short)(x * resolution + y);
                    short upperRight = (short)(s1 * resolution + y);
                    short lowerLeft = (short)(x * resolution + s2);
                    short lowerRight = (short)(s1 * resolution + s2);
                    indices[i++] = upperLeft;
                    indices[i++] = upperRight;
                    indices[i++] = lowerLeft;
                    indices[i++] = lowerLeft;
                    indices[i++] = upperRight;
                    indices[i++] = lowerRight;
                }
            }
        }
        public BoundingBox BoundingBox
        {
            get
            {
                float diameter = Radius * 2.0f;
                return new BoundingBox(new Vector3(-diameter, -diameter, -diameter), new Vector3(diameter, diameter, diameter));
            }
        }
    }
}
