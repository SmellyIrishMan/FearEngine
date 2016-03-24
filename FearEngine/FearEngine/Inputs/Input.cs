using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Input;

namespace FearEngine.Inputs
{
    public enum MouseButton
    {
        LeftMouseButton,
        RightMouseButton
    };

    public interface Input
    {
        Vector2 MouseDelta { get; }

        void Update(GameTime gameTime);

        bool IsKeyDown(Keys key);
        bool IsMouseButtonDown(MouseButton button);
    }
}
