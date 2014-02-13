using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Disque.Xna.Cameras;
using Disque.Xna.Terrain;

namespace XFlightLite
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class FlightGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Plane p;
        ChaseCamera cam;
        Vector3 mover = new Vector3(0, 0, -1);
        public FlightGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        Vector3 pos = new Vector3(0, 5, 10);
        Vector3 tar = new Vector3(0, 5, -1);
        Vector3 u = new Vector3(0, 1, 0);
        HeightTerrain hterrain;
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            p = new Plane(GraphicsDevice);
            p.Position = new Vector3(10, 10, 10);
            cam = new ChaseCamera(p.Position, p.Position - new Vector3(0, 0, 1), Vector3.Up, GraphicsDevice);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture2D text = Content.Load<Texture2D>("terrain");
            int h,w;
            float[,] he = getheights(text, out w, out h);
            hterrain = new HeightTerrain(he, w, h, 2, 25, 6, GraphicsDevice, Content.Load<Texture2D>("grass9"));
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {
            //movecam();
            cam.Update();
            cam.Position = p.Position + (u * 2.0f);
            cam.Target = cam.Position + mover;
            cam.Up = u;
            u = Vector3.Transform(Vector3.Up, quat);
            mover = Vector3.Transform(Vector3.Forward, quat);
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            p.Update(gameTime);
            p.Transformation = Matrix.CreateTranslation(-p.Position) * Matrix.CreateRotationY(MathHelper.ToRadians(180)) * Matrix.CreateFromQuaternion(quat) * Matrix.CreateTranslation(p.Position);
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                rotateh(1);
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                rotateh(-1);
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                rotatev(1);
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                rotatev(-1);
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                cam.Position += new Vector3(0, 0.5f, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                cam.Position -= new Vector3(0, 0.5f, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                cam.Position -= new Vector3(0.5f, 0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                cam.Position += new Vector3(0.5f, 0, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
                cam.Position += new Vector3(0, 0, 0.5f);
            if (Keyboard.GetState().IsKeyDown(Keys.X))
                cam.Position -= new Vector3(0, 0, 0.5f);
            p.Position += mover * 0.2f;
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            p.Draw(gameTime, cam);
            hterrain.Draw(gameTime, cam);
            base.Draw(gameTime);
        }
        Quaternion quat = Quaternion.Identity;
        Vector3 vaxis = Vector3.Forward;
        Vector3 haxis = Vector3.Right;
        void rotatev(float angle)
        {
            quat *= Quaternion.CreateFromAxisAngle(vaxis, MathHelper.ToRadians(angle));
        }
        void rotateh(float angle)
        {
            quat *= Quaternion.CreateFromAxisAngle(haxis, MathHelper.ToRadians(angle));
        }
        //void movecam()
        //{
        //    Matrix m = Matrix.CreateTranslation(p.Position) * Matrix.CreateFromQuaternion(quat) * Matrix.CreateTranslation(-p.Position);
        //    mover = Vector3.Transform(Vector3.Forward, m);
        //    u = Vector3.Transform(Vector3.Up, m);
        //    pos = p.Position + (u * 2.5f);
        //    tar = pos - Vector3.Transform(new Vector3(0, 0, 1), m);
        //    Console.WriteLine(pos);
        //    Console.WriteLine(tar + "," + pos + "," + u);
        //    cam.Position = pos;
        //    cam.Up = u;
        //    cam.Target = tar;
        //}
        float[,] getheights(Texture2D text, out int width, out int length)
        {
            width = text.Width;
            length = text.Height;
            Texture2D texture = text;
            Color[] color = new Color[width * length];
            texture.GetData<Color>(color);
            float[,] heights = new float[width, length];
            for (int x = 0; x < width; x++)
                for (int z = 0; z < length; z++)
                {
                    heights[x, z] = color[x + z * width].R;
                }
            text.Dispose();
            return heights;
        }
    }
}
