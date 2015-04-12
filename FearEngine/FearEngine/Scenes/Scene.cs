using FearEngine.Cameras;
using FearEngine.GameObjects;
using FearEngine.Lighting;
using FearEngine.Resources.Materials;
using FearEngine.Resources.Meshes;
using FearEngine.Shadows;
using SharpDX;
using SharpDX.Toolkit;
using System.Collections.Generic;

namespace FearEngine.Scenes
{
    public interface Scene
    {
        void AddSceneObject(SceneObject sceneObj);
        void Render(GameTime gameTime);
        void EnableShadows();
    }
}
