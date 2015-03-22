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
            var effectResult = compiler.CompileFromFile(info.Filepath);
            if (effectResult.HasErrors)
            {
                FearLog.Log("ERROR Compiling effect; " + info.Filepath, LogPriority.EXCEPTION);
                foreach (SharpDX.Toolkit.Diagnostics.LogMessage message in effectResult.Logger.Messages)
                {
                    FearLog.Log("\t" + message.Text, LogPriority.EXCEPTION);
                }

                return new Material();
            }
            else
            {
                Effect effect = new Effect(device, effectResult.EffectData);
                effect.CurrentTechnique = effect.Techniques[info.GetString("Technique")];

                Material mat = new Material(info.Name, effect);
                return mat;
            }
        }
    }
}
