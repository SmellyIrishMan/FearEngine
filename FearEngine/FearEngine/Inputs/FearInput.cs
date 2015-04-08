using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Input;
using System.Collections.Generic;

namespace FearEngine.Inputs
{
    public class FearInput : Input
    {
        public static int GlobalInputID = 0;

        private MouseManager mouse;
        private MouseState mouseState;

        private Vector2 currentPosition = Vector2.Zero;
        private Vector2 previousPosition = Vector2.Zero;

        private Vector2 mouseMovedDelta;
        public Vector2 MouseDelta { get { return mouseMovedDelta; } }

        private KeyboardManager keyboard;
        private List<Keys> pressedKeys;

        private int InputID;

        public FearInput(MouseManager m, KeyboardManager keyb)
        {
            InputID = GlobalInputID;
            GlobalInputID++;

            mouse = m;

            keyboard = keyb;
            pressedKeys = new List<Keys>();
        }

        public void Update(GameTime gameTime)
        {
            FearEngine.Logger.FearLog.Log("Right mouse button state for input; " + mouseState.RightButton);
            UpdateMouseState();

            UpdateKeyboardState();
        }

        private void UpdateMouseState()
        {
            mouseState = mouse.GetState();
            currentPosition = new Vector2(mouseState.X, mouseState.Y);

            CalculateDelta();
        }

        private void CalculateDelta()
        {
            mouseMovedDelta = currentPosition - previousPosition;
            previousPosition = currentPosition;
        }

        private void UpdateKeyboardState()
        {
            KeyboardState keyState = keyboard.GetState();
            keyState.GetDownKeys(pressedKeys);
        }

        public bool IsKeyDown(Keys key)
        {
            return pressedKeys.Contains(key);
        }

        public bool IsMouseButtonDown(MouseButton button)
        {
            switch(button)
            {
                case MouseButton.LeftMouseButton:
                    return mouseState.LeftButton.Down;
                case MouseButton.RightMouseButton:
                    return mouseState.RightButton.Down;
            }
            return false;
        }
    }
}
