using SharpDX.Windows;
using System.Windows.Forms;
using SharpDX;

public static class InputManager
{
    static RenderForm Form{get; set;}
    public static KeyEventHandler KeyUp;
    public static KeyEventHandler KeyDown;
    public static KeyPressEventHandler KeyPressed;

    public delegate void MouseMoveHandler(object sender, MouseEventArgs e, Vector2 delta);
    public static MouseMoveHandler MouseMoved;

    private static Vector2 previousMousePosition = Vector2.Zero;
    public static Vector2 MouseMovedDelta { get; private set; }

    public static void Initialise(RenderForm form)
    {
        Form = form;
        Form.KeyUp += OnKeyUp;
        Form.KeyDown += OnKeyDown;
        Form.KeyPress += OnKeyPressed;
        Form.MouseMove += OnMouseMoved;
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

    private static void OnMouseMoved(object sender, MouseEventArgs e)
    {
        Vector2 currentMousePosition = new Vector2(e.Location.X, e.Location.Y);
        MouseMovedDelta = currentMousePosition - previousMousePosition;
        previousMousePosition = currentMousePosition;

        if (MouseMoved != null)
        {
            MouseMoved(sender, e, MouseMovedDelta);
        }
    }
}