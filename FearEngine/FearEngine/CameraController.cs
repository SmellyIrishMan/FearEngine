using SharpDX.Toolkit;

namespace FearEngine
{
    class CameraController : IUpdateable
    {
        public void Update(GameObject obj, GameTime gameTime)
        {
        }
    }
}

//public void Update()
//{
//    if (EnableFPSControl)
//    {
//        if (IsStrafing)
//        {
//            Strafe(StrafeDir);
//        }
//        if (IsWalking)
//        {
//            Walk(WalkDir);
//        }
//        if (IsRotating)
//        {
//            Pitch(RotationDir.Y);
//            RotateY(RotationDir.X);
//        }
//    }
//}

//private void OnMouseDown(object sender, MouseEventArgs e)
//{
//    if (EnableFPSControl)
//    {
//        IsRotating = e.Button == MouseButtons.Right;
//    }
//}

//private void OnMouseUp(object sender, MouseEventArgs e)
//{
//    if (EnableFPSControl && IsRotating)
//    {
//        IsRotating = !(e.Button == MouseButtons.Right);
//    }
//}

//private void OnMouseMoved(Vector2 delta)
//{
//    if (EnableFPSControl)
//    {
//        RotationDir = delta;
//    }
//}

//private void OnMouseStopped(Vector2 delta)
//{
//    if (EnableFPSControl)
//    {
//        RotationDir = delta;
//    }
//}

//private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
//{
//    if(EnableFPSControl)
//    {
//        switch(e.KeyCode)
//        {
//            case Keys.D:
//                IsStrafing = true;
//                StrafeDir = 1.0f;
//                break;
//            case Keys.A:
//                IsStrafing = true;
//                StrafeDir = -1.0f;
//                break;
//            case Keys.W:
//                IsWalking = true;
//                WalkDir = 1.0f;
//                break;
//            case Keys.S:
//                IsWalking = true;
//                WalkDir = -1.0f;
//                break;
//        }
//    }
//}

//private void OnKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
//{
//    if (EnableFPSControl)
//    {
//        switch (e.KeyCode)
//        {
//            case Keys.D:
//            case Keys.A:
//                IsStrafing = false;
//                break;
//            case Keys.W:
//            case Keys.S:
//                IsWalking = false;
//                break;
//        }
//    }
//}

//public void Strafe(float dir)
//{
//    Position = Position + (Right * dir * STRAFE_SPEED * TimeKeeper.Delta);
//}

//public void Walk(float dir)
//{
//   Position = Position + (Forward * dir * WALK_SPEED * TimeKeeper.Delta);
//}

//public void Pitch(float angle)
//{
//    Matrix rotation = Matrix.RotationAxis(Right, angle * TimeKeeper.Delta * ROTATION_SPEED);
//    Up = Vector3.TransformNormal(Up, rotation);
//    Forward = Vector3.TransformNormal(Forward, rotation);
//}

//public void RotateY(float angle)
//{
//    Matrix rotation = Matrix.RotationY(angle * TimeKeeper.Delta * ROTATION_SPEED);

//    Up = Vector3.TransformNormal(Up, rotation);
//    Right = Vector3.TransformNormal(Right, rotation);
//    Forward = Vector3.TransformNormal(Forward, rotation);
//}
