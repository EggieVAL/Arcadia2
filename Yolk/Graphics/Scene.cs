using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Yolk.Graphics
{
    public sealed class Scene : IDisposable
    {
        private readonly Game game;
        private RenderTarget2D renderTarget;

        private bool disposed;

        public Scene(Game game, int width, int height)
        {
            this.game = game;
            renderTarget = new RenderTarget2D(game.GraphicsDevice, width, height);
            disposed = false;
        }

        public Scene(Game game)
            : this(game,
                   GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                   GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height)
        {
        }

        public RenderTarget2D RenderTarget
        {
            get => renderTarget;
            set
            {
                if (value is not null)
                {
                    renderTarget = value;
                }
            }
        }

        private GraphicsDevice Graphics => renderTarget.GraphicsDevice;

        public int Width
        {
            get => renderTarget.Width;
            set
            {
                GraphicsDevice graphics = Graphics;
                int height = Height;
                renderTarget.Dispose();

                renderTarget = new RenderTarget2D(graphics, value, height);
            }
        }

        public int Height
        {
            get => renderTarget.Height;
            set
            {
                GraphicsDevice graphics = Graphics;
                int width = Width;
                renderTarget.Dispose();

                renderTarget = new RenderTarget2D(graphics, width, value);
            }
        }

        public void EnableRenderTargeting()
        {
            game.GraphicsDevice.SetRenderTarget(renderTarget);
        }

        public void DisableRenderTargeting()
        {
            game.GraphicsDevice.SetRenderTarget(null);
        }

        public void Display(bool isTextureFilteringEnabled)
        {
            Rectangle target = GetRenderTargetDestination();
            SpriteManager.Begin(null, isTextureFilteringEnabled);
            SpriteManager.Draw(renderTarget, target, Color.White);
            SpriteManager.End();
        }

        public Rectangle GetRenderTargetDestination()
        {
            Rectangle displayScreen = game.GraphicsDevice.PresentationParameters.Bounds;

            float appAspectRatio = (float) displayScreen.Width / displayScreen.Height;
            float sceneAspectRatio = (float) renderTarget.Width / renderTarget.Height;

            float x = 0f;
            float y = 0f;
            float width = displayScreen.Width;
            float height = displayScreen.Height;

            if (appAspectRatio > sceneAspectRatio)
            {
                width = displayScreen.Height * sceneAspectRatio;
                x = (displayScreen.Width - width) * 0.5f;
            }
            else if (appAspectRatio < sceneAspectRatio)
            {
                height = width / sceneAspectRatio;
                y = (displayScreen.Height - height) * 0.5f;
            }
            return new Rectangle((int) x, (int) y, (int) width, (int) height);
        }

        public void Dispose()
        {
            if (!disposed)
            {
                renderTarget?.Dispose();
                disposed = true;
            }
        }
    }
}
