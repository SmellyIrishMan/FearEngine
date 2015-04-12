using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.GameObjects;
using FearEngineTests.MockClasses.InputMocks;
using SharpDX;
using System.Collections.Generic;
using FearEngine.Inputs;
using FearEngine.Timer;
using FearEngineTests.MockClasses.TimerMocks;
using FearEngine.GameObjects.Updateables;

namespace FearEngineTests.GameObjectComponents
{
    [TestClass]
    public class GameObjectComponentTests
    {
        [TestMethod]
        public void TestCameraControllerComponent()
        {
            //Given
            Input fixedDirectionMouseWithButtonHeldDown = new FixedInputMock(
                new Vector2(0.14f, 0.05f),
                new List<MouseButton>(new MouseButton[] { MouseButton.RightMouseButton }));
            GameTimer constantTimer = new ConstantTimer(new TimeSpan(0, 0, 0, 0, 99));

            BaseGameObject testObject = new BaseGameObject("CameraComponetTest");
            CameraControllerComponent component = new CameraControllerComponent(fixedDirectionMouseWithButtonHeldDown);

            //When
            for (int update = 0; update < 5; update++)
            {
                component.Update(testObject, constantTimer);
            }

            //Then
            Quaternion comparisonRotation = new Quaternion(0.01768886f, -0.9228921f, 0.3822745f, 0.04270469f);
            Assert.IsTrue(MathUtil.WithinEpsilon(testObject.Transform.Rotation.Length(), comparisonRotation.Length(), 0.0001f));
        }
    }
}
