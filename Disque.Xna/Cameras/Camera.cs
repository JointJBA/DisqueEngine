using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Disque.Xna.Cameras
{
    public class Camera
    {
        Matrix view;
        Matrix proj;
        public Matrix View
        {
            get
            {
                return view;
            }
            set
            {
                view = value;
                updatefrust();
            }
        }
        public Matrix Projection
        {
            get
            {
                return proj;
            }
            set
            {
                proj = value;
                updatefrust();
            }
        }
        public BoundingFrustum Frustrum { get; private set; }
        protected GraphicsDevice GraphicsDevice { get; set; }
        public Camera(GraphicsDevice graphic)
        {
            this.GraphicsDevice = graphic;
            generateperspectiveproj();
        }
        void generateperspectiveproj()
        {
            PresentationParameters pp = GraphicsDevice.PresentationParameters;
            float aspectratio = ((float)pp.BackBufferWidth / (float)pp.BackBufferHeight);
            this.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), aspectratio, 0.1f, 1000000.0f);
        }
        public void GenerateCustomPerspectiveProjection(float width, float height)
        {
            float aspectratio = (width/ height);
            this.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), aspectratio, 0.1f, 1000000.0f);
        }
        public virtual void Update()
        {
            updatefrust();
        }
        public bool BoundingVolumeIsInView(BoundingSphere sphere)
        {
            return (Frustrum.Contains(sphere) != ContainmentType.Disjoint);
        }
        public bool BoundingVolumeIsInView(BoundingBox box)
        {
            return (Frustrum.Contains(box) != ContainmentType.Disjoint);
        }
        void updatefrust()
        {
            Matrix viewproj = View * Projection;
            Frustrum = new BoundingFrustum(viewproj);
        }
    }
}
