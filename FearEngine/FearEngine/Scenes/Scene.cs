using FearEngine.Timer;

namespace FearEngine.Scenes
{
    public interface Scene
    {
        void AddSceneObject(SceneObject sceneObj);
        void Render(GameTimer gameTime);
        void EnableShadows();
    }
}
