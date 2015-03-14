using FearEngine.Logger;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Managment.Loaders
{
    public class MaterialLoader : ResourceLoader
    {
        GraphicsDevice device;

        public MaterialLoader(GraphicsDevice dev)
        {
            device = dev;
        }

        public Resource Load(ResourceInformation info)
        {
            EffectCompiler compiler = new EffectCompiler();
            var effectResult = compiler.CompileFromFile(info.GetFilepath());
            if (effectResult.HasErrors)
            {
                FearLog.Log("ERROR Compiling effect; " + info.GetFilepath(), LogPriority.EXCEPTION);
            }

            Effect effect = new Effect(device, effectResult.EffectData);
            effect.CurrentTechnique = effect.Techniques[info.GetString("Technique")];
            
            Material mat = new Material(info.GetName(), effect);

            return mat;
        }
    }
}
