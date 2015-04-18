using FearEngine.Cameras;
using FearEngine.Lighting;

namespace FearEngine.Scenes
{
    public interface SceneFactory
    {
        BasicScene CreateSceneWithSingleLight(Camera cam, Light light);
    }
}
