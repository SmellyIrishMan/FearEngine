using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Input;
using System.Collections.Generic;

namespace FearEngine.Input
{
    class MouseAndKeyboardInputComponent : IInput
    {
        private MouseManager m_Mouse;
        private MouseState m_MouseState;

        private Vector2 m_CurrentPosition = Vector2.Zero;
        private Vector2 m_PreviousMousePosition = Vector2.Zero;
        private Vector2 m_LockedMousePosition = Vector2.Zero;

        private Vector2 m_MouseMovedDelta;
        private float m_MouseMovedErrorMargin = 0.001f;

        private bool m_LockingPosition;

        private KeyboardManager m_Keyboard;

        private List<Keys> m_PressedKeys;

        public MouseAndKeyboardInputComponent(MouseManager mouse, KeyboardManager keyboard)
        {
            m_Mouse = mouse;

            m_Keyboard = keyboard;
            m_PressedKeys = new List<Keys>();
        }

        public void Update(GameObject owner, GameTime gameTime)
        {
            UpdateMouseState();

            UpdateKeyboardState();
        }

        private void UpdateMouseState()
        {
            m_MouseState = m_Mouse.GetState();
            m_CurrentPosition = new Vector2(m_MouseState.X, m_MouseState.Y);
            if (IsMouseButtonDown(MouseButton.RightMouseButton))
            {
                LockPosition();
            }
            else
            {
                UnlockPosition();
            }

            CalculateDelta();

            if (m_LockingPosition)
            {
                m_Mouse.SetPosition(m_LockedMousePosition);
            }
        }

        private void LockPosition()
        {
            if (!m_LockingPosition)
            {
                m_LockingPosition = true;
                m_MouseState = m_Mouse.GetState();
                m_LockedMousePosition = m_CurrentPosition;
            }
        }

        private void UnlockPosition()
        {
            m_LockingPosition = false;
        }

        private void CalculateDelta()
        {
            Vector2 currentMousePosition = new Vector2(m_MouseState.X, m_MouseState.Y);
            if (m_LockingPosition)
            {
                m_MouseMovedDelta = m_CurrentPosition - m_LockedMousePosition;
                //FearEngine.Logger.FearLog.Log("Locked mouse.\nCurrentPosition; " + m_CurrentPosition.ToString() + "\nLockedPosition; " + m_LockedMousePosition.ToString() + "\nDelta; " + m_MouseMovedDelta.ToString() + "\n", Logger.LogPriority.ALWAYS);
            }
            else
            {
                m_MouseMovedDelta = m_CurrentPosition - m_PreviousMousePosition;
                m_PreviousMousePosition = m_CurrentPosition;
            }

            if (m_MouseMovedDelta.Length() <= m_MouseMovedErrorMargin)
            {
                m_MouseMovedDelta = Vector2.Zero;
            }
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