using SharpDX.Windows;
using System.Windows.Forms;

public static class InputManager
{
    static RenderForm Form{get; set;}
    public static void Initialise(RenderForm form)
    {
        Form = form;
    }

    public static KeyEventHandler KeyUp;
}