using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Disque.Wpf;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Disque.Xna.Cameras;
using Disque.Xna.Primatives;
using Microsoft.Xna.Framework.Input;

namespace DisqueRenderToy
{
    public class GameScreen : XnaControlGame
    {
        SpriteBatch spriteBatch;
        VertexPositionColor[] grid;
        BasicEffect grid_effect;
        int fps = 0;
        ArcBallCamera currentcam;
        public GameScreen(IntPtr handler)
            : base(handler, "Content")
        {
        }
        public bool Active = false;
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            grid_effect = new BasicEffect(GraphicsDevice);
            grid_effect.VertexColorEnabled = true;
            currentcam = new ArcBallCamera(Vector3.Zero, 0, 1, 0, 360, 50, 0, 200, GraphicsDevice);
            loadGrid();
            base.LoadContent();
        }
        protected override void Update(GameTime gameTime)
        {
            fps = 1000 / gameTime.ElapsedGameTime.Milliseconds;
            if (Active)
                inputUsers();
            currentcam.Update();
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DimGray);
            drawGrid();
            base.Draw(gameTime);
        }
        void loadGrid()
        {
            grid = new VertexPositionColor[44];
            for (int i = 0; i < 22; i += 2)
            {
                grid[i] = new VertexPositionColor(new Vector3(-20, 0, i * 2) + new Vector3(0, 0, -20), Color.White);
                grid[i + 1] = new VertexPositionColor(new Vector3(20, 0, i * 2) + new Vector3(0, 0, -20), Color.White);
            }
            for (int i = 22; i < 44; i += 2)
            {
                grid[i] = new VertexPositionColor(new Vector3(i * 2, 0, -20) + new Vector3(-64, 0, 0), Color.White);
                grid[i + 1] = new VertexPositionColor(new Vector3(i * 2, 0, 20) + new Vector3(-64, 0, 0), Color.White);
            }
            grid[10].Color = Color.Green;
            grid[11].Color = Color.Green;
            grid[33].Color = Color.Red;
            grid[32].Color = Color.Red;
        }
        void drawGrid()
        {
            grid_effect.World = Matrix.Identity;
            grid_effect.View = currentcam.View;
            grid_effect.Projection = currentcam.Projection;
            foreach (EffectPass pass in grid_effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, grid, 0, grid.Length / 2);
            }
        }
        void handlecamera()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                currentcam.RotationX += MathHelper.ToRadians(1);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                currentcam.RotationX -= MathHelper.ToRadians(1);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                currentcam.RotationY += MathHelper.ToRadians(1);
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                currentcam.RotationY -= MathHelper.ToRadians(1);
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
                currentcam.Distance--;
            if (Keyboard.GetState().IsKeyDown(Keys.X))
                currentcam.Distance++;
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                currentcam.Distance = 50;
                currentcam.RotationY = 1;
                currentcam.RotationX = 0;
            }
        }
        void inputUsers()
        {
            handlecamera();
        }
        public int FPS
        {
            get
            {
                return fps;
            }
        }
        public void AdjustCamera(float width, float height)
        {
            currentcam.GenerateCustomPerspectiveProjection(width, height);
        }
        //Matrix quat = Matrix.CreateRotationX(0.1f);
        //void rotatev(float angle)
        //{
        //    quat *= Matrix.CreateFromAxisAngle(Vector3.Up, MathHelper.ToRadians(angle));
        //}
        //void rotateh(float angle)
        //{
        //    quat *= Matrix.CreateFromAxisAngle(Vector3.Right, MathHelper.ToRadians(angle));
        //}
        //void handleWatcherCamera()
        //{
        //    Vector2 watcher_camera_firstpos = new Vector2();
        //    Vector2 watcher_camera_secondpos = new Vector2();
        //    MouseState ms = Mouse.GetState();
        //    if (ms.RightButton == ButtonState.Pressed)
        //    {
        //        watcher_camera_secondpos.X = ms.X;
        //        watcher_camera_secondpos.Y = ms.Y;
        //    }
        //    else
        //    {
        //        watcher_camera_firstpos.X = ms.X;
        //        watcher_camera_firstpos.Y = ms.Y;
        //    }
        //}
    }
}
