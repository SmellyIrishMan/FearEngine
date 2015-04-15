using FearEngine.Cameras;
using FearEngine.Lighting;

namespace FearEngine.Scenes
{
    public interface SceneFactory
    {
        BasicScene CreateScene(Camera cam, Light light);
    }
}
