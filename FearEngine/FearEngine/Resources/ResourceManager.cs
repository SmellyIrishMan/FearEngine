using FearEngine.Logger;
using SharpDX.Toolkit.Graphics;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

namespace FearEngine.Resources
{
    public static class ResourceManager
    {
        private static string RootResourcePath{get; set;}
        private static Dictionary<string, Bitmap> LoadedImages;
        private static Dictionary<string, Material> LoadedMaterials;

        public static void Initialise()
        {
            //Setup the storage for the different resources
            LoadedImages = new Dictionary<string, Bitmap>();
            LoadedMaterials = new Dictionary<string, Material>();

            //Find our resource directory, or create one.
            DirectoryInfo resourceDir = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory);
            resourceDir = resourceDir.Parent.Parent.Parent;
            RootResourcePath = System.IO.Path.Combine(resourceDir.FullName, "Resources");

            if (!System.IO.Directory.Exists(RootResourcePath))
            {
                FearLog.Log("No Resource Directory Found, Creating One", LogPriority.EXCEPTION);
                System.IO.Directory.CreateDirectory(RootResourcePath);

                CreateResourceDirectory();
            }

            FearLog.Log("Current Resource Directory; " + RootResourcePath);
        }

        public static void CreateResourceDirectory()
        {
            CreateSubResourceFolder("Textures");
            CreateSubResourceFolder("Shaders");
            CreateSubResourceFolder("Scenes");
            CreateSubResourceFolder("Models");
            CreateSubResourceFolder("Materials");
        }

        private static void CreateSubResourceFolder(string name)
        {
            string subResourcePath = System.IO.Path.Combine(RootResourcePath, name);
            System.IO.Directory.CreateDirectory(subResourcePath);
            string xmlFilePath = subResourcePath + "\\" + name + ".xml";
            PopulateXMLFile(xmlFilePath, name);
        }

        private static void PopulateXMLFile(string xmlFilePath, string type)
        {
            Dictionary<string, string[]> defaultResource = PrepareDefaultResources();

            System.IO.StreamWriter file = new StreamWriter(xmlFilePath);
            file.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            file.WriteLine("<" + type + ">");
            foreach (string s in defaultResource[type])
            {
                file.WriteLine(s);
            }
            file.WriteLine("</" + type + ">");
            file.Close();
        }

        private static Dictionary<string, string[]> PrepareDefaultResources()
        { 
            Dictionary<string, string[]> defaultResource = new Dictionary<string, string[]>();
            defaultResource.Add("Textures",
                new string[] {  "\t<Texture>",
                                    "\t\t<Name>TEX_Default</Name>",
                                    "\t\t<Filepath>C:\\Someplace</Filepath>",
                                "\t</Texture>",
                });
            defaultResource.Add("Shaders",
                new string[] {  "\t<Shader>",
                                    "\t\t<Name>SHADER_Default</Name>",
                                    "\t\t<Filepath>C:\\Someplace</Filepath>",
                                "\t</Shader>",
                });
            defaultResource.Add("Scenes",
                new string[] {  "\t<Scene>",
                                    "\t\t<Name>SCENE_Default</Name>",
                                    "\t\t<Filepath>C:\\Someplace</Filepath>",
                                "\t</Scene>",
                });
            defaultResource.Add("Models",
                new string[] {  "\t<Model>",
                                    "\t\t<Name>MODEL_Default</Name>",
                                    "\t\t<Filepath>C:\\Someplace</Filepath>",
                                "\t</Model>",
                });
            defaultResource.Add("Materials",
                new string[] {  "\t<Material>",
                                    "\t\t<Name>MAT_Default</Name>",
                                    "\t\t<Filepath>C:\\Someplace</Filepath>",
                                "\t</Material>",
                });

            return defaultResource;
        }

        public static Bitmap GetImage(string name)
        {
            if (!LoadedImages.ContainsKey(name))
            {
                LoadedImages[name] = LoadImage(name);
            }

            return LoadedImages[name];
        }

        private static Bitmap LoadImage(string name)
        {
            XmlTextReader xmlReader = new XmlTextReader(RootResourcePath + "\\Textures\\Textures.xml");
            while (xmlReader.Read())
            {
                FearLog.Log(xmlReader.Name, LogPriority.LOW);
                if (xmlReader.Name.CompareTo("Name") == 0)
                {
                    xmlReader.Read();
                    if (xmlReader.Value.CompareTo(name) == 0)
                    {
                        FearLog.Log("Loading image " + xmlReader.Value, LogPriority.HIGH);
                        xmlReader.ReadToFollowing("Filepath");
                        xmlReader.Read();
                        return new Bitmap(xmlReader.Value);
                    }
                }
            }
            FearLog.Log("Failed to load image " + name, LogPriority.EXCEPTION);
            return new Bitmap(128, 128);
        }

        public static Material GetMaterial(string name)
        {
            if (!LoadedMaterials.ContainsKey(name))
            {
                LoadedMaterials[name] = LoadMaterial(name);
            }

            return LoadedMaterials[name];
        }

        private static Material LoadMaterial(string name)
        {
            XmlTextReader xmlReader = new XmlTextReader(RootResourcePath + "\\Materials\\Materials.xml");
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
                        mat.RenderEffect = new Effect(FearEngineApp.GetDevice(), effectResult.EffectData);
                        mat.RenderEffect.CurrentTechnique = mat.RenderEffect.Techniques[shaderTech];

                        return mat;
                    }
                }
            }
            FearLog.Log("Failed to load material " + name, LogPriority.EXCEPTION);
            return null;
        }

        public static void Shutdown()
        {
            foreach (Material mat in LoadedMaterials.Values)
            {
                mat.RenderEffect.Dispose();
            }
        }
    }
}
