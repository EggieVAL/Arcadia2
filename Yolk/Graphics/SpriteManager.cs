using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using Yolk.GameObjects;

namespace Yolk.Graphics
{
    public sealed class SpriteManager : IDisposable
    {
        private static SpriteManager instance;

        private readonly Game game;

        private readonly SpriteBatch spriteBatch;
        private readonly BasicEffect basicEffect;

        private bool disposed;

        private SpriteManager(Game game)
        {
            this.game = game;
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            basicEffect = new BasicEffect(game.GraphicsDevice)
            {
                FogEnabled = false,
                LightingEnabled = false,
                TextureEnabled = true,
                VertexColorEnabled = false,
                Projection = Matrix.Identity,
                View = Matrix.Identity,
                World = Matrix.Identity
            };
            disposed = false;
        }

        public static SpriteManager Instance => instance;

        public static void CreateInstance(Game game)
        {
            instance ??= new SpriteManager(game);
        }

        public static void Begin(Camera camera, bool isTextureFilteringEnabled)
        {
            SamplerState samplerState = (isTextureFilteringEnabled)
                ? SamplerState.LinearClamp
                : SamplerState.PointClamp;

            if (camera is null)
            {
                Viewport viewport = Instance.game.GraphicsDevice.Viewport;
                Instance.basicEffect.Projection = Matrix.CreateOrthographicOffCenter(
                                                        0, viewport.Width,
                                                        0, viewport.Height,
                                                        0f, 1f
                                                   );

                Instance.basicEffect.View = Matrix.Identity;
                Instance.basicEffect.World = Matrix.Identity;
            }
            else
            {
                camera.UpdateMatrices();
                Instance.basicEffect.Projection = camera.Projection;
                Instance.basicEffect.View = camera.View;
                Instance.basicEffect.World = camera.World;
            }

            Instance.spriteBatch.Begin(
                blendState: BlendState.AlphaBlend,
                samplerState: samplerState,
                rasterizerState: RasterizerState.CullNone,
                effect: Instance.basicEffect
            );
        }

        public static void Begin() => Instance.spriteBatch.Begin();

        public static void End() => Instance.spriteBatch.End();

        public static void Draw(Texture2D texture,
                                Rectangle destinationRectangle,
                                Color color)
        {
            Instance.spriteBatch.Draw(
                texture,
                destinationRectangle,
                color
            );
        }

        public static void Draw(Texture2D texture,
                                Rectangle destinationRectangle,
                                Rectangle? sourceRectangle,
                                Color color)
        {
            Instance.spriteBatch.Draw(
                texture,
                destinationRectangle,
                sourceRectangle,
                color
            );
        }

        public static void Draw(Texture2D texture,
                                Rectangle destinationRectangle,
                                Rectangle? sourceRectangle,
                                Color color,
                                float rotation,
                                Vector2 origin,
                                SpriteEffects effects,
                                float layerDepth)
        {
            Instance.spriteBatch.Draw(
                texture,
                destinationRectangle,
                sourceRectangle,
                color,
                rotation,
                origin,
                effects,
                layerDepth
            );
        }

        public static void Draw(Texture2D texture,
                                Vector2 position,
                                Color color)
        {
            Instance.spriteBatch.Draw(
                texture,
                position,
                color
            );
        }

        public static void Draw(Texture2D texture,
                                Vector2 position,
                                Rectangle? sourceRectangle,
                                Color color)
        {
            Instance.spriteBatch.Draw(
                texture,
                position,
                sourceRectangle,
                color
            );
        }

        public static void Draw(Texture2D texture,
                                Vector2 position,
                                Rectangle? sourceRectangle,
                                Color color,
                                float rotation,
                                Vector2 origin,
                                Vector2 scale,
                                SpriteEffects effects,
                                float layerDepth)
        {
            Instance.spriteBatch.Draw(
                texture,
                position,
                sourceRectangle,
                color,
                rotation,
                origin,
                scale,
                effects,
                layerDepth
            );
        }

        public static void Draw(Texture2D texture,
                                Vector2 position,
                                Rectangle? sourceRectangle,
                                Color color,
                                float rotation,
                                Vector2 origin,
                                float scale,
                                SpriteEffects effects,
                                float layerDepth)
        {
            Instance.spriteBatch.Draw(
                texture,
                position,
                sourceRectangle,
                color,
                rotation,
                origin,
                scale,
                effects,
                layerDepth
            );
        }

        public void Dispose()
        {
            if (!disposed)
            {
                spriteBatch?.Dispose();
                basicEffect?.Dispose();
                disposed = true;
            }
        }
    }
}
