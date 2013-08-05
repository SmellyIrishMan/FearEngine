using System.Drawing;

namespace FearEngine
{
    public class PresentationProperties
    {
        private const int DEFAULT_WIDTH = 1280;
        private const int DEFAULT_HEIGHT = 720;

        public bool Fullscreen {get;  set; }

        private Size m_WindowSize;
        public Size WindowSize 
        {
            get
            {
                return m_WindowSize;
            }

            private set
            {
                ChangeWindowSize(value.Width, value.Height);
            }
        }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float AspectRatio { get; private set; }

        public void ChangeWindowSize(int w, int h)
        {
            Width = w;
            Height = h;
            AspectRatio = Width / (float)Height;
            m_WindowSize.Width = Width;
            m_WindowSize.Height = Height;
        }

        public PresentationProperties()
        {
            Fullscreen = false;
            ChangeWindowSize(DEFAULT_WIDTH, DEFAULT_HEIGHT);
        }
    }
}