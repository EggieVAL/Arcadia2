using Microsoft.Xna.Framework.Input;

namespace Yolk.Input
{
    public static class MouseListener
    {
        private static MouseState previousState;
        private static MouseState currentState;

        static MouseListener()
        {
            previousState = Mouse.GetState();
            currentState = PreviousState;
        }

        public static MouseState CurrentState => currentState;
        public static MouseState PreviousState => previousState;

        public static int PositionX => CurrentState.X;
        public static int PositionY => CurrentState.Y;

        public static bool IsLeftButtonClicked()
        {
            return CurrentState.LeftButton == ButtonState.Pressed
                && PreviousState.LeftButton == ButtonState.Released;
        }

        public static bool IsLeftButtonPressed()
        {
            return CurrentState.LeftButton == ButtonState.Pressed;
        }

        public static bool IsMiddleButtonClicked()
        {
            return CurrentState.MiddleButton == ButtonState.Pressed
                && PreviousState.MiddleButton == ButtonState.Released;
        }

        public static bool IsMiddleButtonPressed()
        {
            return CurrentState.MiddleButton == ButtonState.Pressed;
        }

        public static bool IsRightButtonClicked()
        {
            return CurrentState.RightButton == ButtonState.Pressed
                && PreviousState.RightButton == ButtonState.Released;
        }

        public static bool IsRightButtonDown()
        {
            return CurrentState.RightButton == ButtonState.Pressed;
        }

        public static bool IsScrollingDown()
        {
            return CurrentState.ScrollWheelValue < PreviousState.ScrollWheelValue;
        }

        public static bool IsScrollingUp()
        {
            return CurrentState.ScrollWheelValue > PreviousState.ScrollWheelValue;
        }

        public static void Update()
        {
            previousState = CurrentState;
            currentState = Mouse.GetState();
        }
    }
}
