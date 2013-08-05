using SharpDX;
using System;

namespace FearEngine
{
    class Camera
    {
        Transform transform;

        Matrix View { get; set; }
        Matrix Projection { get; set; }

        Camera()
        {
            View = Matrix.LookAtLH(new Vector3(0, 0, -5), new Vector3(0, 0, 0), Vector3.UnitY);
            Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4.0f, FearEngineApp.PresentationProps.AspectRatio, 0.1f, 100.0f);
        }
    }
}
