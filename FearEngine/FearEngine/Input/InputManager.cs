using SharpDX.Toolkit.Input;
using SharpDX;
using System.Drawing;
using System.Collections.Generic;

public static class InputManager
{
    public delegate void MouseMoveHandler(Vector2 delta);
    public static MouseMoveHandler MouseMoved;
    public static MouseMoveHandler MouseStopped;

    private static bool validPreviousPosition = false;
    private static Vector2 previousMousePosition = Vector2.Zero;
    public static Vector2 MouseMovedDelta { get; private set; }

    private static MouseManager m_Mouse;

    private static KeyboardManager m_Keyboard;
    private static List<Keys> m_PressedKeys;

    public static void Initialise(MouseManager mouse, KeyboardManager keyboard)
    {
        m_Mouse = mouse;
        
        m_Keyboard = keyboard;
        m_PressedKeys = new List<Keys>();
    }

    //private static void OnMouseUp(object sender, MouseEventArgs e)
    //{
    //    if (MouseUp != null)
    //    {
    //        MouseUp(sender, e);
    //    }
    //}

    //private static void OnMouseDown(object sender, MouseEventArgs e)
    //{
    //    if (MouseDown != null)
    //    {
    //        MouseDown(sender, e);
    //    }
    //}

    public static void Update()
    {
        MouseState mouseState = m_Mouse.GetState();
        Vector2 currentMousePosition = new Vector2(mouseState.X, mouseState.Y);
        if (validPreviousPosition)
        {
            MouseMovedDelta = currentMousePosition - previousMousePosition;
            if (MouseMovedDelta.Length() != 0.0f)
            {
                if (MouseMoved != null){ MouseMoved(MouseMovedDelta); }
            }
            else
            {
                if (MouseStopped != null){ MouseStopped(Vector2.Zero); }
            }
        }
        previousMousePosition = currentMousePosition;
        validPreviousPosition = true;

        KeyboardState keyState = m_Keyboard.GetState();
        
        keyState.GetDownKeys(m_PressedKeys);
        if (m_PressedKeys.Count > 0)
        {

        }
    }

    //private static void OnKeyUp(object sender, KeyEventArgs e)
    //{
    //    if (KeyUp != null)
    //    {
    //        KeyUp(sender, e);
    //    }
    //}

    //private static void OnKeyDown(object sender, KeyEventArgs e)
    //{
    //    if (KeyDown != null)
    //    {
    //        KeyDown(sender, e);
    //    }
    //}

    //private static void OnKeyPressed(object sender, KeyPressEventArgs e)
    //{
    //    if (KeyPressed != null)
    //    {
    //        KeyPressed(sender, e);
    //    }
    //}
}