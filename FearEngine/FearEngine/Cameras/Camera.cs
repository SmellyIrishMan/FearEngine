﻿using SharpDX;
using System;

namespace FearEngine.Cameras
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

        public void UpdateView()
        {
            //transform.m_Forward = Vector3.Normalize(transform.m_Forward);
            //transform.m_Up = Vector3.Normalize(Vector3.Cross(transform.m_Forward, transform.m_Right));
            //transform.m_Right = Vector3.Cross(transform.m_Up, transform.m_Forward);

            //// Fill in the view matrix entries.
            //float x = -Vector3.Dot(transform.Position, transform.m_Right);
            //float y = -Vector3.Dot(transform.Position, transform.m_Up);
            //float z = -Vector3.Dot(transform.Position, transform.m_Forward);

            //View.M11 = transform.m_Right.X;
            //View.M21 = transform.m_Right.Y;
            //View.M31 = transform.m_Right.Z;
            //View.M41 = x;

            //View.M12 = transform.m_Up.X;
            //View.M22 = transform.m_Up.Y;
            //View.M32 = transform.m_Up.Z;
            //View.M42 = y;

            //View.M13 = transform.m_Forward.X;
            //View.M23 = transform.m_Forward.Y;
            //View.M33 = transform.m_Forward.Z;
            //View.M43 = z;

            //View.M14 = 0.0f;
            //View.M24 = 0.0f;
            //View.M34 = 0.0f;
            //View.M44 = 1.0f;
        }
    }
}