using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Yolk.Input;

namespace Yolk.Graphics
{
    public sealed class DisplayScreen
    {
        private readonly GameWindow window;

        private bool isFullscreen;
        private bool isBorderless;

        public DisplayScreen(Game game, int width, int height)
        {
            Graphics = new GraphicsDeviceManager(game)
            {
                PreferredBackBufferWidth = width,
                PreferredBackBufferHeight = height
            };
            window = game.Window;
        }

        public DisplayScreen(Game game)
            : this(game,
                   GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 3,
                   GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 3)
        {
        }

        public GraphicsDeviceManager Graphics { get; }

        public int Width
        {
            get => Graphics.PreferredBackBufferWidth;
            set => Graphics.PreferredBackBufferWidth = value;
        }

        public int Height
        {
            get => Graphics.PreferredBackBufferHeight;
            set => Graphics.PreferredBackBufferHeight = value;
        }

        public bool IsFullscreen => isFullscreen;
        public bool IsBorderless => isBorderless;

        public void Update()
        {
            if (KeyListener.IsKeyClicked(Keys.F11))
            {
                if (KeyListener.IsKeyPressed(Keys.LeftControl))
                {
                    ToggleBorderless();
                }
                else
                {
                    ToggleFullscreen();
                }
            }
        }

        public void ToggleFullscreen()
        {
            bool wasFullscreen = isFullscreen;
            if (isBorderless)
            {
                isBorderless = false;
            }
            else
            {
                isFullscreen = !isFullscreen;
            }
            ApplyFullscreenChanges(wasFullscreen);
        }

        public void ToggleBorderless()
        {
            bool wasFullscreen = IsFullscreen;
            isBorderless = !isBorderless;
            isFullscreen = isBorderless;
            ApplyFullscreenChanges(wasFullscreen);
        }

        private void ApplyFullscreenChanges(bool wasFullscreen)
        {
            if (isFullscreen)
            {
                if (wasFullscreen)
                {
                    ApplyHardwareMode();
                }
                else
                {
                    ActivateFullscreen();
                }
            }
            else
            {
                DeactivateFullscreen();
            }
        }

        private void ApplyHardwareMode()
        {
            Graphics.HardwareModeSwitch = !isBorderless;
            Graphics.ApplyChanges();
        }

        private void ActivateFullscreen()
        {
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            Graphics.HardwareModeSwitch = !isBorderless;
            Graphics.IsFullScreen = true;

            Graphics.ApplyChanges();
        }

        private void DeactivateFullscreen()
        {
            Graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 3;
            Graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 3;
            Graphics.IsFullScreen = false;
            Graphics.ApplyChanges();
        }
    }
}
