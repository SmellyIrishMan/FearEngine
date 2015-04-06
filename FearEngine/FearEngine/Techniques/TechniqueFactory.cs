using FearEngine.DeviceState;
using FearEngine.Resources.Managment;
using FearEngine.Shadows;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FearEngine.Techniques
{
    public enum ShadowTechnique
    {
        Basic
    }

    public class TechniqueFactory
    {
        GraphicsDevice device;
        FearResourceManager resourceManager;
        DeviceStateFactory devStateFactory;

        public TechniqueFactory(GraphicsDevice dev, FearResourceManager resMan, DeviceStateFactory devStateFac)
        {
            device = dev;
            resourceManager = resMan;
            devStateFactory = devStateFac;
        }

        public BasicShadowTechnique CreateShadowTechnique(ShadowTechnique tech)
        {
            return new BasicShadowTechnique(device, resourceManager.GetMaterial("DepthWrite"), null);
        }
    }
}
