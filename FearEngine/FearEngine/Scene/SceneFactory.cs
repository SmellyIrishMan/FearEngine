using FearEngine.Cameras;
using FearEngine.DeviceState;
using FearEngine.Lighting;
using FearEngine.Resources.Meshes;
using FearEngine.Techniques;

namespace FearEngine.Scene
{
    public class SceneFactory
    {
        MeshRenderer renderer;
        TechniqueFactory techFactory;
        LightFactory lightFactory;

        public SceneFactory(MeshRenderer rend,
            TechniqueFactory techFac,
            LightFactory lightFac)
        {
            renderer = rend;

            techFactory = techFac;
            lightFactory = lightFac;
        }

        public Scene CreateScene(Camera camera)
        {
            return new Scene(renderer, camera, lightFactory, techFactory);
        }

    }
}
