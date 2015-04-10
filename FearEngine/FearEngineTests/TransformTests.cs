using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FearEngine.GameObjects;
using FearEngine;
using FearEngineTests.MockClasses;
using SharpDX;

namespace FearEngineTests
{
    [TestClass]
    public class TransformTests
    {
        float vector3DiffEpsilon = 0.00002f;
        [TestMethod]
        public void DirectionalVectorAreCorrectAfter90DegreeRotationInY()
        {
            //Given
            Transform transform = new Transform();
            
            //When
            transform.SetRotation(Quaternion.RotationMatrix(Matrix.RotationAxis(new Vector3(0, 1, 0), MathUtil.PiOverTwo)));

            //Then
            float diff = (transform.Forward - Vector3.Right).Length();
            Assert.IsTrue(diff < vector3DiffEpsilon);

            diff = (transform.Right - Vector3.BackwardLH).Length();
            Assert.IsTrue(diff < vector3DiffEpsilon);

            diff = (transform.Up - Vector3.Up).Length();
            Assert.IsTrue(diff < vector3DiffEpsilon);
        }

        [TestMethod]
        public void DirectionalVectorAreCorrectAfter90DegreeRotationInX()
        {
            //Given
            Transform transform = new Transform();

            //When
            transform.SetRotation(Quaternion.RotationMatrix(Matrix.RotationAxis(new Vector3(1, 0, 0), MathUtil.PiOverTwo)));

            //Then
            float diff = (transform.Forward - Vector3.Down).Length();
            Assert.IsTrue(diff < vector3DiffEpsilon);

            diff = (transform.Right - Vector3.Right).Length();
            Assert.IsTrue(diff < vector3DiffEpsilon);

            diff = (transform.Up - Vector3.ForwardLH).Length();
            Assert.IsTrue(diff < vector3DiffEpsilon);
        }

        [TestMethod]
        public void DirectionalVectorAreCorrectAfter90DegreeRotationInZ()
        {
            //Given
            Transform transform = new Transform();

            //When
            transform.SetRotation(Quaternion.RotationMatrix(Matrix.RotationAxis(new Vector3(0, 0, 1), MathUtil.PiOverTwo)));

            //Then
            float diff = (transform.Forward - Vector3.ForwardLH).Length();
            Assert.IsTrue(diff < vector3DiffEpsilon);

            diff = (transform.Right - Vector3.Up).Length();
            Assert.IsTrue(diff < vector3DiffEpsilon);

            diff = (transform.Up - Vector3.Left).Length();
            Assert.IsTrue(diff < vector3DiffEpsilon);
        }

        [TestMethod]
        public void ForwardVectorLooksAtPointAfterRotation()
        {
            //Given
            Transform transform = new Transform();
            Vector3 transformPos = new Vector3(1, 3, -5);
            transform.MoveTo(transformPos);

            Vector3 targetPoint = Vector3.Zero;
            Vector3 vectorFromTransformToTargetPoint = targetPoint - transformPos;
            vectorFromTransformToTargetPoint.Normalize();
             
            //When
            Quaternion lookAt = Quaternion.LookAtLH(transformPos, targetPoint, Vector3.Up);
            lookAt.Conjugate();
            transform.SetRotation(lookAt);

            //Then
            float diff = (transform.Forward - vectorFromTransformToTargetPoint).Length();
            Assert.IsTrue(diff < vector3DiffEpsilon);
        }
    }
}
