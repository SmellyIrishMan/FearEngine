using FearEngine.Logger;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FearEngine.Resources.Managment.Loaders
{
    public class MaterialLoader
    {
        public Material Load(string xmlFile, string name, GraphicsDevice device)
        {
            XmlTextReader xmlReader = new XmlTextReader(xmlFile);
            while (xmlReader.Read())
            {
                FearLog.Log(xmlReader.Name, LogPriority.LOW);
                if (xmlReader.Name.CompareTo("Name") == 0)
                {
                    xmlReader.Read();
                    if (xmlReader.Value.CompareTo(name) == 0)
                    {
                        FearLog.Log("Loading material " + xmlReader.Value, LogPriority.HIGH);

                        xmlReader.ReadToFollowing("Filepath");
                        xmlReader.Read();
                        string shaderFilepath = xmlReader.Value;

                        xmlReader.ReadToFollowing("Technique");
                        xmlReader.Read();
                        string shaderTech = xmlReader.Value;

                        EffectCompiler compiler = new EffectCompiler();
                        var effectResult = compiler.CompileFromFile(shaderFilepath);
                        if (effectResult.HasErrors)
                        {
                            FearLog.Log("ERROR Compiling effect; " + shaderFilepath, LogPriority.EXCEPTION);
                        }

                        Material mat = new Material();
                        mat.Name = name;
                        mat.RenderEffect = new Effect(device, effectResult.EffectData);
                        mat.RenderEffect.CurrentTechnique = mat.RenderEffect.Techniques[shaderTech];

                        return mat;
                    }
                }
            }
            FearLog.Log("Failed to load material " + name, LogPriority.EXCEPTION);
            return null;
        }
    }
}
