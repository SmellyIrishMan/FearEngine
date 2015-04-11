using FearEngine.Inputs;
using SharpDX;
using System.Collections.Generic;

namespace FearEngineTests.MockClasses.InputMocks
{
    public class FixedInputMock : Input
    {
        Vector2 fixedDirection;
        List<MouseButton> heldButtons;
        List<SharpDX.Toolkit.Input.Keys> heldKeys;

        public FixedInputMock(Vector2 fixedDir)
        {
            fixedDirection = fixedDir;
            heldButtons = new List<MouseButton>();
            heldKeys = new List<SharpDX.Toolkit.Input.Keys>();
        }

        public FixedInputMock(Vector2 fixedDir, List<MouseButton> heldButts)
            : this(fixedDir)
        {
            heldButtons = heldButts;
        }

        public FixedInputMock(Vector2 fixedDir, List<MouseButton> heldButts, List<SharpDX.Toolkit.Input.Keys> heldKs)
            : this(fixedDir, heldButts)
        {
            heldKeys = heldKs;
        }

        public SharpDX.Vector2 MouseDelta
        {
            get { return fixedDirection; }
        }

        public void Update(SharpDX.Toolkit.GameTime gameTime)
        {
        }

        public bool IsKeyDown(SharpDX.Toolkit.Input.Keys key)
        {
            return heldKeys.Contains(key);
        }

        public bool IsMouseButtonDown(MouseButton button)
        {
            return heldButtons.Contains(button);
        }
    }
}
