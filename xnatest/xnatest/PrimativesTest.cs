using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Disque.Xna.Cameras;
using Disque.Xna;
using Disque.Xna.Primatives;
using Microsoft.Xna.Framework.Input;

namespace xnatest
{
    public class PrimativesTest : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera cam;
        List<IDraw> drawables = new List<IDraw>();
        Sphere sph;
        Sphere sph2;
        public PrimativesTest()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            cam = new TargetCamera(new Vector3(0, 0, 100), Vector3.Zero, GraphicsDevice);
            sph = new Sphere(new Vector3(30, 0, 0), 10, Color.Violet, GraphicsDevice);
            sph2 = new Sphere(new Vector3(-30, 0, 0), 10, Color.LightGreen, GraphicsDevice);
            drawables.Add(sph);
            drawables.Add(sph2);
        }
        Quaternion quat = Quaternion.Identity;
        float angle = 1f;
        protected override void Update(GameTime gameTime)
        {
            Matrix m1 = Matrix.CreateTranslation(-sph2.Position) * Matrix.CreateRotationY(MathHelper.ToRadians(angle)) * Matrix.CreateTranslation(sph2.Position);
            Matrix m2 = Matrix.CreateTranslation(-sph.Position) * Matrix.CreateRotationY(MathHelper.ToRadians(angle)) * Matrix.CreateTranslation(sph.Position);
            sph.Position = Vector3.Transform(sph.Position, m1);
            sph2.Position = Vector3.Transform(sph2.Position, m2);
            cam.Update();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            for (int i = 0; i < drawables.Count; i++)
            {
                if (cam.Frustrum.Contains(drawables[i].BoundingBox) != ContainmentType.Disjoint)
                    drawables[i].Draw(gameTime, cam);
            }
            base.Draw(gameTime);
        }
        Vector3 vaxis = Vector3.Up;
        Vector3 haxis = Vector3.Right;
        void rotatev(float angle)
        {
            quat*= Quaternion.CreateFromAxisAngle(vaxis, MathHelper.ToRadians(angle));
        }
        void rotateh(float angle)
        {
            quat *= Quaternion.CreateFromAxisAngle(haxis, MathHelper.ToRadians(angle));
        }
    }
}
