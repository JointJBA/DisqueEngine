using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Disque.Xna.Cameras;
using Microsoft.Xna.Framework.Graphics;

namespace Disque.Xna.Terrain
{
    public class HeightTerrain : IDraw
    {
        VertexPositionNormalTexture[] vertices;
        GraphicsDevice graphics;
        BasicEffect effect;
        VertexBuffer vbuffer;
        IndexBuffer ibuffer;
        float[,] heights;
        int width, length, nvertices, nindices;
        float height, cellSize;
        short[] indices;
        float rep;
        public HeightTerrain(float[,] heights,int Width, int Length, float CellSize, float Height, float repition, GraphicsDevice graphic, Texture2D terrainTexture)
        {
            graphics = graphic;
            effect = new BasicEffect(graphic);
            effect.Texture = terrainTexture;
            effect.TextureEnabled = true;
            this.heights = heights;
            width = Width;
            cellSize = CellSize;
            height = Height;
            length = Length;
            nvertices = width * length;
            nindices = (width - 1) * (length - 1) * 6;
            vbuffer = new VertexBuffer(graphics, typeof(VertexPositionNormalTexture), nvertices, BufferUsage.WriteOnly);
            ibuffer = new IndexBuffer(graphics, IndexElementSize.SixteenBits, nindices, BufferUsage.WriteOnly);
            rep = repition;
            getheights();
            createvertices();
            createindices();
            genNormals();
            vbuffer.SetData<VertexPositionNormalTexture>(vertices);
            ibuffer.SetData<short>(indices);
        }
        public void Update(GameTime gametime)
        {
        }
        public void Draw(GameTime gametime, Camera camera)
        {
            effect.View = camera.View;
            effect.World = Matrix.Identity;
            effect.Projection = camera.Projection;
            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, vertices, 0, nvertices, indices, 0, indices.Length / 3);
            }
        }
        void getheights()
        {
            for (int x = 0; x < width; x++)
                for (int y = 0; y < length; y++)
                {
                    float amt = heights[x, y];
                    amt /= 255;
                    amt *= height;
                    heights[x, y] = amt;
                }
        }
        public BoundingBox BoundingBox
        {
            get
            {
                return new BoundingBox();
            }
        }
        void createindices()
        {
            indices = new short[nindices];
            int i = 0;
            for (int x = 0; x < width - 1; x++)
                for (int z = 0; z < length - 1; z++)
                {
                    // Find the indices of the corners
                    short upperLeft = (short)(z * width + x);
                    short upperRight = (short)(upperLeft + 1);
                    short lowerLeft = (short)(upperLeft + width);
                    short lowerRight = (short)(lowerLeft + 1);
                    // Specify upper triangle
                    indices[i++] = upperLeft;
                    indices[i++] = upperRight;
                    indices[i++] = lowerLeft;
                    // Specify lower triangle
                    indices[i++] = lowerLeft;
                    indices[i++] = upperRight;
                    indices[i++] = lowerRight;
                }
        }
        void genNormals()
        {
            for (int i = 0; i < nindices; i += 3)
            {
                Vector3 v1 = vertices[indices[i]].Position;
                Vector3 v2 = vertices[indices[i + 1]].Position;
                Vector3 v3 = vertices[indices[i + 2]].Position;
                Vector3 normal = Vector3.Cross(v1 - v2, v1 - v3);
                normal.Normalize();
                vertices[indices[i]].Normal += normal;
                vertices[indices[i + 1]].Normal += normal;
                vertices[indices[i + 2]].Normal += normal;
            }
            for (int i = 0; i < nvertices; i++)
                vertices[i].Normal.Normalize();
        }
        void createvertices()
        {
            vertices = new VertexPositionNormalTexture[nvertices];
            Vector3 offsetToCenter = -new Vector3((width / 2.0f) * cellSize, 0, (length / 2.0f) * cellSize);
            for (int x = 0; x < width; x++)
                for (int z = 0; z < length; z++)
                {
                    Vector3 position = new Vector3(x * cellSize, heights[x, z], z * cellSize) + offsetToCenter;
                    Vector2 uv = new Vector2(x / ((float)width) * rep, z / ((float)length) * rep);
                    vertices[x + z * width] = new VertexPositionNormalTexture(position, Vector3.Zero, uv);
                }
        }
    }
}
