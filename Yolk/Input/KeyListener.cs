using Microsoft.Xna.Framework.Input;

namespace Yolk.Input
{
    public static class KeyListener
    {
        private static KeyboardState currentState;
        private static KeyboardState previousState;

        static KeyListener()
        {
            previousState = Keyboard.GetState();
            currentState = PreviousState;
        }

        public static KeyboardState CurrentState => currentState;
        public static KeyboardState PreviousState => previousState;

        public static bool IsKeyClicked(Keys key)
        {
            return CurrentState.IsKeyDown(key)
                && PreviousState.IsKeyUp(key);
        }

        public static bool IsKeyPressed(Keys key)
        {
            return CurrentState.IsKeyDown(key);
        }

        public static void Update()
        {
            previousState = CurrentState;
            currentState = Keyboard.GetState();
        }
    }
}
