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

    private static Vector2 previousMousePosition = Vector2.Zero;
    public static Vector2 MouseMovedDelta { get; private set; }

    public static void Initialise(RenderForm form)
    {
        //Cursor.
        Form = form;
        Form.KeyUp += OnKeyUp;
        Form.KeyDown += OnKeyDown;
        Form.KeyPress += OnKeyPressed;

        Point p = Cursor.Position;
        previousMousePosition = new Vector2(p.X, p.Y);
    }

    public static void Update()
    {
        Point p = Cursor.Position;
        Vector2 currentMousePosition = new Vector2(p.X, p.Y);
        MouseMovedDelta = currentMousePosition - previousMousePosition;
        if (MouseMovedDelta.Length() != 0.0f)
        {
            MouseMoved(MouseMovedDelta);
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