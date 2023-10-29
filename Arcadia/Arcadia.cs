using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Yolk.GameObjects;
using Yolk.Graphics;
using Yolk.Input;

namespace Arcadia
{
    public class Arcadia : Game
    {
        private DisplayScreen displayScreen;
        private Camera camera;

        public Arcadia()
        {
            displayScreen = new DisplayScreen(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            camera = new Camera(new Scene(this));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteManager.CreateInstance(this);
        }

        protected override void Update(GameTime gameTime)
        {
            KeyListener.Update();
            MouseListener.Update();

            displayScreen.Update();
            camera.Update(gameTime);

            // Click escape to close game
            if (KeyListener.IsKeyClicked(Keys.Escape))
            {
                Exit();
            }

            // Change resolution to 1080p
            if (MouseListener.IsLeftButtonClicked())
            {
                camera.Scene.Width = 1920;
                camera.Scene.Height = 1080;
            }

            // Change resolution to 720p
            if (MouseListener.IsRightButtonClicked())
            {
                camera.Scene.Width = 1280;
                camera.Scene.Height = 720;
            }

            // Change resolution with a non-standard 16:9 aspect ratio
            if (MouseListener.IsMiddleButtonClicked())
            {
                camera.Scene.Width = 920;
                camera.Scene.Height = 480;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            camera.EnableRenderTargeting();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteManager.Begin(camera, false);
            SpriteManager.Draw(Content.Load<Texture2D>("test/player_frame"), new Vector2(0, 0), Color.White);
            SpriteManager.End();

            camera.DisableRenderTargeting();
            camera.Display(true);
            base.Draw(gameTime);
        }
        public static void Main()
        {
            using Arcadia game = new Arcadia();
            game.Run();
        }
    }
}