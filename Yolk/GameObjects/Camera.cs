using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using Yolk.Graphics;
using Yolk.Input;

namespace Yolk.GameObjects
{
    public sealed class Camera : GameObject
    {
        private readonly Scene scene;

        private float aspectRatio;
        private float hfov;
        private float vfov;

        private Location3D location;

        private float minimumZ;
        private float maximumZ;

        private float zoomSpeed;
        private float minimumZoomRate;
        private float maximumZoomRate;

        public Camera(Scene scene, float x, float y)
        {
            this.scene = scene;

            aspectRatio = (float) scene.Width / scene.Height;
            vfov = MathHelper.PiOver2;
            hfov = CalculateHFOV();

            minimumZ = 1f;
            maximumZ = GetZFromHeight(scene.Height);
            location = new Location3D(x, y, maximumZ);

            zoomSpeed = 0.03f;
            minimumZoomRate = 0f;
            maximumZoomRate = 12f;
            UpdateMatrices();
        }

        public Camera(Scene scene, Location2D location) : this(scene, location.X, location.Y)
        {
        }

        public Camera(Scene scene) : this(scene, 0, 0)
        {
        }

        public float AspectRatio
        {
            get => aspectRatio;
            set
            {
                if (aspectRatio > 0)
                {
                    aspectRatio = value;
                }
                hfov = CalculateHFOV();
            }
        }

        public float FOV
        {
            get => vfov;
            set
            {
                vfov = MathF.PI / 180f * value;
                hfov = CalculateHFOV();
            }
        }

        public float MaximumZ
        {
            get => maximumZ;
            set
            {
                if (value >= minimumZ && value > 0)
                {
                    maximumZ = value;
                }
            }
        }

        public float MinimumZ
        {
            get => minimumZ;
            set
            {
                if (value <= MaximumZ && value > 0)
                {
                    minimumZ = value;
                }
            }
        }

        public float X
        {
            get => location.X;
            set => location.X = value;
        }

        public float Y
        {
            get => location.Y;
            set => location.Y = value;
        }

        public float Z
        {
            get => location.Z;
            set => location.Z = MathHelper.Clamp(value, MinimumZ, MaximumZ);
        }

        public float MaximumZoomRate
        {
            get => MaximumZoomRate;
            set
            {
                if (value >= minimumZoomRate && value > 0)
                {
                    maximumZoomRate = value;
                }
            }
        }

        public float MinimumZoomRate
        {
            get => minimumZoomRate;
            set
            {
                if (value <= MaximumZoomRate && value > 0)
                {
                    minimumZoomRate = value;
                }
            }
        }

        public float ZoomSpeed
        {
            get => zoomSpeed;
            set
            {
                if (value >= 0)
                {
                    zoomSpeed = value;
                }
            }
        }

        public Scene Scene => scene;

        public Matrix Projection { get; private set; }

        public Matrix View { get; private set; }

        public Matrix World { get; private set; }

        private float CalculateHFOV()
        {
            return 2 * MathF.Atan(MathF.Tan(0.5f * vfov) * aspectRatio);
        }

        public void GetExtents(out float width, out float height)
        {
            height = Z * MathF.Tan(0.5f * FOV) * 2f;
            width = height * AspectRatio;
        }

        public void GetExtents(out float left, out float right, out float top, out float bottom)
        {
            GetExtents(out float width, out float height);
            left = X - (width * 0.5f);
            right = left + width;
            top = Y - (height * 0.5f);
            bottom = top + height;
        }

        public float GetZFromWidth(float width)
        {
            return 0.5f * width / MathF.Tan(0.5f * hfov);
        }

        public float GetZFromHeight(float height)
        {
            return 0.5f * height / MathF.Tan(0.5f * vfov);
        }

        public void ResetZ()
        {
            Z = MaximumZ;
        }

        public override void Update(GameTime gameTime)
        {
            if (KeyListener.IsKeyPressed(Keys.OemPlus))
            {
                float zoomRate = ZoomSpeed * MathF.Pow(2, 0.01f*Z);
                      zoomRate = MathF.Round(zoomRate * gameTime.ElapsedGameTime.Milliseconds);

                Z -= MathHelper.Clamp(zoomRate, minimumZoomRate, maximumZoomRate);
            }

            if (KeyListener.IsKeyPressed(Keys.OemMinus))
            {
                float zoomRate = ZoomSpeed * MathF.Pow(2, 0.01f*Z);
                      zoomRate = MathF.Round(zoomRate * gameTime.ElapsedGameTime.Milliseconds);

                Z += MathHelper.Clamp(zoomRate, minimumZoomRate, maximumZoomRate);
            }
        }

        public void UpdateMatrices()
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(FOV, AspectRatio, 1, MaximumZ);
            View = Matrix.CreateLookAt(new Vector3(0, 0, Z), Vector3.Zero, Vector3.Up);
            World = Matrix.CreateWorld(new Vector3(-X, -Y, 0), Vector3.Forward, Vector3.Up);
        }

        public void EnableRenderTargeting() => scene.EnableRenderTargeting();

        public void DisableRenderTargeting() => scene.DisableRenderTargeting();

        public void Display(bool isTextureFilteringEnabled) => scene.Display(isTextureFilteringEnabled);
    }
}
