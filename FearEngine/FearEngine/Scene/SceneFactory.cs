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
        DeviceStateFactory devStateFactory;
        TechniqueFactory techFactory;
        LightFactory lightFactory;

        public SceneFactory(MeshRenderer rend,
            DeviceStateFactory devStateFac,
            TechniqueFactory techFac,
            LightFactory lightFac)
        {
            renderer = rend;

            devStateFactory = devStateFac;
            techFactory = techFac;
            lightFactory = lightFac;
        }

        public Scene CreateScene(Camera camera)
        {
            return new Scene(renderer, camera, lightFactory, techFactory);
        }

    }
}
