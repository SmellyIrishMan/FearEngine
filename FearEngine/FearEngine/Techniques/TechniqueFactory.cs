using FearEngine.DeviceState;
using FearEngine.DeviceState.SamplerStates;
using FearEngine.Resources.Managment;
using FearEngine.Shadows;
using Ninject;

namespace FearEngine.Techniques
{
    public enum ShadowTechnique
    {
        Basic
    }

    public class TechniqueFactory
    {
        SharpDX.Toolkit.Graphics.GraphicsDevice device;
        FearResourceManager resourceManager;

        public TechniqueFactory(SharpDX.Toolkit.Graphics.GraphicsDevice dev, FearResourceManager resMan)
        {
            device = dev;
            resourceManager = resMan;
        }

        public BasicShadowTechnique CreateShadowTechnique(ShadowTechnique tech)
        {
            RasteriserState rasState = FearGameFactory.dependencyKernel.Get<RasteriserState>("ShadowBiasedDepth");
            SamplerState samplerState = FearGameFactory.dependencyKernel.Get<SamplerState>("ShadowMapComparison");
            return new BasicShadowTechnique(device, 
                resourceManager.GetMaterial("DepthWrite"),
                rasState,
                samplerState);
        }
    }
}
