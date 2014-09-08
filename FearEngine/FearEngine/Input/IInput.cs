using SharpDX;
using SharpDX.Toolkit.Input;

namespace FearEngine.Input
{
    public enum MouseButton
    {
        LeftMouseButton,
        RightMouseButton
    };

    public interface IInput : IUpdateable
    {
        bool IsKeyDown(Keys key);

        bool IsMouseButtonDown(MouseButton button);

        Vector2 GetMouseDelta();
    }
}
