using FearEngine.Cameras;
namespace FearEngine.Scenes
{
    public interface SceneFactory
    {
        BasicScene CreateScene(Camera cam);
    }
}
