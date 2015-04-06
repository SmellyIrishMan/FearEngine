using FearEngine.Logger;
using FearEngine.Resources.Materials;
using SharpDX.Direct3D11;
using SharpDX.Toolkit.Graphics;

namespace FearEngine.Resources.Managment.Loaders
{
    public class MaterialLoader : ResourceLoader
    {
        GraphicsDevice device;

        bool enableDebug = true;

        public MaterialLoader(GraphicsDevice dev)
        {
            device = dev;
        }

        public Resource Load(ResourceInformation info)
        {
            EffectCompiler compiler = new EffectCompiler();

            EffectCompilerFlags flags = EffectCompilerFlags.None;
            if(enableDebug)
            {
                flags = EffectCompilerFlags.Debug | EffectCompilerFlags.OptimizationLevel0 | EffectCompilerFlags.SkipOptimization;
            }

            var effectResult = compiler.CompileFromFile(info.Filepath, flags);
            if (effectResult.HasErrors)
            {
                FearLog.Log("ERROR Compiling effect; " + info.Filepath, LogPriority.EXCEPTION);
                foreach (SharpDX.Toolkit.Diagnostics.LogMessage message in effectResult.Logger.Messages)
                {
                    FearLog.Log("\t" + message.Text, LogPriority.EXCEPTION);
                }

                return new FearMaterial();
            }
            else
            {
                Effect effect = new Effect(device, effectResult.EffectData);
                effect.CurrentTechnique = effect.Techniques[info.GetString("Technique")];

                FearMaterial mat = new FearMaterial(info.Name, effect);
                return mat;
            }
        }
    }
}
