using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FearEngine.Input
{
    public enum MouseButton
    {
        LeftMouseButton,
        RightMouseButton
    };

    class InputManager
    {
        private static InputManager instance;

        private InputManager() {}
        public static InputManager Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new InputManager();
                }
                return instance;
            }
        }

        private MouseManager m_Mouse;
        private MouseState m_MouseState;

        private Vector2 m_CurrentPosition = Vector2.Zero;
        private Vector2 m_PreviousMousePosition = Vector2.Zero;
        private Vector2 m_MouseMovedDelta;

        private KeyboardManager m_Keyboard;
        private List<Keys> m_PressedKeys;

        public void Initialise(MouseManager mouse, KeyboardManager keyboard)
        {
            m_Mouse = mouse;

            m_Keyboard = keyboard;
            m_PressedKeys = new List<Keys>();
        }

        public void Update(GameTime gameTime)
        {
            UpdateMouseState();

            UpdateKeyboardState();
        }

        private void UpdateMouseState()
        {
            m_MouseState = m_Mouse.GetState();
            m_CurrentPosition = new Vector2(m_MouseState.X, m_MouseState.Y);

            CalculateDelta();
        }

        private void CalculateDelta()
        {
            m_MouseMovedDelta = m_CurrentPosition - m_PreviousMousePosition;
            m_PreviousMousePosition = m_CurrentPosition;
        }

        private void UpdateKeyboardState()
        {
            KeyboardState keyState = m_Keyboard.GetState();
            keyState.GetDownKeys(m_PressedKeys);
        }

        public bool IsKeyDown(Keys key)
        {
            return m_PressedKeys.Contains(key);
        }

        public bool IsMouseButtonDown(MouseButton button)
        {
            switch(button)
            {
                case MouseButton.LeftMouseButton:
                    return m_MouseState.LeftButton.Down;
                case MouseButton.RightMouseButton:
                    return m_MouseState.RightButton.Down;
            }
            return false;
        }

        public Vector2 GetMouseDelta()
        {
            return m_MouseMovedDelta;
        }
    }
}
