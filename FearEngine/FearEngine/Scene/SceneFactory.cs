using FearEngine.Cameras;
using FearEngine.DeviceState;
using FearEngine.Lighting;
using FearEngine.Resources.Meshes;
using FearEngine.Shadows;
using Ninject;

namespace FearEngine.Scene
{
    public class SceneFactory
    {
        MeshRenderer renderer;

        public SceneFactory(MeshRenderer rend)
        {
            renderer = rend;
        }

        public Scene CreateScene(Camera camera)
        {
            return new Scene(renderer, camera, FearGameFactory.dependencyKernel.Get<Light>(), FearGameFactory.dependencyKernel.Get<ShadowTechnique>());
        }
    }
}
