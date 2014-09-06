using SharpDX.Windows;
using System.Windows.Forms;
using SharpDX;
using System.Drawing;

public static class InputManager
{
    static RenderForm Form{get; set;}
    public static KeyEventHandler KeyUp;
    public static KeyEventHandler KeyDown;
    public static KeyPressEventHandler KeyPressed;

    public delegate void MouseMoveHandler(Vector2 delta);
    public static MouseMoveHandler MouseMoved;
    public static MouseMoveHandler MouseStopped;
    public static MouseEventHandler MouseDown;
    public static MouseEventHandler MouseUp;

    private static Vector2 previousMousePosition = Vector2.Zero;
    public static Vector2 MouseMovedDelta { get; private set; }

    public static void Initialise(RenderForm form)
    {
        //Cursor.
        Form = form;
        Form.KeyUp += OnKeyUp;
        Form.KeyDown += OnKeyDown;
        Form.KeyPress += OnKeyPressed;

        Form.MouseDown += OnMouseDown;
        Form.MouseUp += OnMouseUp;

        //previousMousePosition = SharpDX.Toolkit.Input.MouseManager;
    }

    private static void OnMouseUp(object sender, MouseEventArgs e)
    {
        if (MouseUp != null)
        {
            MouseUp(sender, e);
        }
    }

    private static void OnMouseDown(object sender, MouseEventArgs e)
    {
        if (MouseDown != null)
        {
            MouseDown(sender, e);
        }
    }

    public static void Update()
    {
        //Point p = Cursor.Position;
        Vector2 currentMousePosition = new Vector2(0, 0);
        MouseMovedDelta = currentMousePosition - previousMousePosition;
        if (MouseMovedDelta.Length() != 0.0f)
        {
            if (MouseMoved != null)
            {
                MouseMoved(MouseMovedDelta);
            }
        }
        else
        {
            if (MouseStopped != null)
            {
                MouseStopped(Vector2.Zero);
            }
        }
        previousMousePosition = currentMousePosition;
    }

    private static void OnKeyUp(object sender, KeyEventArgs e)
    {
        if (KeyUp != null)
        {
            KeyUp(sender, e);
        }
    }

    private static void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (KeyDown != null)
        {
            KeyDown(sender, e);
        }
    }

    private static void OnKeyPressed(object sender, KeyPressEventArgs e)
    {
        if (KeyPressed != null)
        {
            KeyPressed(sender, e);
        }
    }
}