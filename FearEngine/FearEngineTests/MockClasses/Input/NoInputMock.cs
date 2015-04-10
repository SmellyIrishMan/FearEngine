using FearEngine.Inputs;

namespace FearEngineTests.MockClasses
{
    class NoInputMock : Input
    {
        public SharpDX.Vector2 MouseDelta
        {
            get { return new SharpDX.Vector2(); }
        }

        public void Update(SharpDX.Toolkit.GameTime gameTime)
        {
            
        }

        public bool IsKeyDown(SharpDX.Toolkit.Input.Keys key)
        {
            return false;
        }

        public bool IsMouseButtonDown(MouseButton button)
        {
            return false;
        }
    }
}
