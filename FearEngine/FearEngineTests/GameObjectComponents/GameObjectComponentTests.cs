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
        public void CameraControllerComponentTest()
        {
            //Given
            Input fixedInput = new FixedInputMock(
                new Vector2(0.14f, 0.05f),
                new List<MouseButton>(new MouseButton[] { MouseButton.RightMouseButton }));
            GameTimer constantTimer = new ConstantTimer(new TimeSpan(0, 0, 0, 0, 100));

            Transform transform = new Transform();
            CameraControllerComponent camComp = new CameraControllerComponent(transform, fixedInput);

            //When
            for (int update = 0; update < 5; update++)
            {
                camComp.Update( constantTimer );
            }

            //Then
            Quaternion comparisonRotation = new Quaternion(-0.084f, 0.90f, -0.37f, -0.20f);
            comparisonRotation.Normalize();
            float dot = Quaternion.Dot(transform.Rotation, comparisonRotation);

            Assert.IsTrue(MathUtil.WithinEpsilon(dot, 1.0f, 0.0001f));
        }

        [TestMethod]
        public void ContinuousRandomSlerpTest()
        {
            //Given
            GameTimer constantTimer = new ConstantTimer(new TimeSpan(0, 0, 0, 0, 33));

            BaseGameObject testObject = new BaseGameObject("OrbitTestObject");
            ContinuousRandomSlerp component = new ContinuousRandomSlerp(testObject.Transform, 0.25f);

            //When
            for (int update = 0; update < 300; update++)
            {
                component.Update(constantTimer);
            }

            //Then
            Assert.IsTrue(testObject.Transform.Rotation.Length() > 0.0f);
        }
    }
}
