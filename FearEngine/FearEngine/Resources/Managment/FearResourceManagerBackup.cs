//using FearEngine.Logger;
//using FearEngine.Resources.Managment.Loaders;
//using FearEngine.Resources.Managment.Loaders.Collada;
//using FearEngine.Resources.Meshes;
//using SharpDX.Toolkit.Graphics;
//using System.Collections.Generic;
//using System.Drawing;
//using System.IO;
//using System.Xml;

//namespace FearEngine.Resources.Managment
//{
//    public class FearResourceManagerBackup
//    {
//        GraphicsDevice device;

//        private Dictionary<string, Bitmap> LoadedImages;

//        private MaterialLoader materialLoader;
//        private Dictionary<string, Material> LoadedMaterials;

//        private ColladaMeshLoader meshLoader;
//        private Dictionary<string, Mesh> LoadedMeshes;

//        private const string DEFAULT_MATERIAL_NAME = "DEFAULT_MATERIAL";
//        private const string DEFAULT_MESH_NAME = "DEFAULT_MESH";

//        public string RootResourcePath { get; private set; }

//        public FearResourceManagerBackup(GraphicsDevice dev)
//        {
//            device = dev;

//            //Setup the storage for the different resources
//            LoadedImages = new Dictionary<string, Bitmap>();

//            //materialLoader = new MaterialLoader();
//            LoadedMaterials = new Dictionary<string, Material>();

//            meshLoader = new ColladaMeshLoader();
//            LoadedMeshes = new Dictionary<string, Mesh>();

//            //Find our resource directory, or create one.
//            DirectoryInfo resourceDir = new System.IO.DirectoryInfo(System.Environment.CurrentDirectory);
//            resourceDir = resourceDir.Parent.Parent;
//            RootResourcePath = System.IO.Path.Combine(resourceDir.FullName, "Resources");

//            if (!System.IO.Directory.Exists(RootResourcePath))
//            {
//                FearLog.Log("No Resource Directory Found, Creating One", LogPriority.EXCEPTION);
//                System.IO.Directory.CreateDirectory(RootResourcePath);

//                CreateResourceDirectory();
//            }

//            FearLog.Log("Current Resource Directory; " + RootResourcePath);
//        }

//        public void Initialize(GraphicsDevice dev)
//        {
//            device = dev;
//        }

//        public void CreateResourceDirectory()
//        {
//            CreateSubResourceFolder("Textures");
//            CreateSubResourceFolder("Meshes");
//            CreateSubResourceFolder("Materials");
//        }

//        private void CreateSubResourceFolder(string name)
//        {
//            string subResourcePath = System.IO.Path.Combine(RootResourcePath, name);
//            System.IO.Directory.CreateDirectory(subResourcePath);
//            string xmlFilePath = subResourcePath + "\\" + name + ".xml";
//            PopulateXMLFile(xmlFilePath, name);
//        }

//        private void PopulateXMLFile(string xmlFilePath, string type)
//        {
//            Dictionary<string, string[]> defaultResource = PrepareDefaultResources();

//            System.IO.StreamWriter file = new StreamWriter(xmlFilePath);
//            file.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
//            file.WriteLine("<" + type + ">");
//            foreach (string s in defaultResource[type])
//            {
//                file.WriteLine(s);
//            }
//            file.WriteLine("</" + type + ">");
//            file.Close();
//        }

//        private Dictionary<string, string[]> PrepareDefaultResources()
//        { 
//            Dictionary<string, string[]> defaultResource = new Dictionary<string, string[]>();
//            defaultResource.Add("Textures",
//                new string[] {  "\t<Texture>",
//                                    "\t\t<Name>TEX_Default</Name>",
//                                    "\t\t<Filepath>C:\\Someplace</Filepath>",
//                                "\t</Texture>",
//                });
//            defaultResource.Add("Meshes",
//                new string[] {  "\t<Mesh>",
//                                    "\t\t<Name>"+DEFAULT_MESH_NAME+"</Name>",
//                                    "\t\t<Filepath>C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Models\\Box.DAE</Filepath>",
//                                "\t</Mesh>",
//                });
//            defaultResource.Add("Materials",
//                new string[] {  "\t<Material>",
//                                    "\t\t<Name>"+DEFAULT_MATERIAL_NAME+"</Name>",
//                                    "\t\t<Filepath>C:\\Users\\Andy\\Documents\\Coding\\Visual Studio 2012\\Projects\\FearEngine\\Resources\\Shaders\\Basic.fx</Filepath>",
//                                    "\t\t<Technique>BasicPositionColorTech</Technique>",
//                                "\t</Material>",
//                });

//            return defaultResource;
//        }

//        public Bitmap GetImage(string name)
//        {
//            if (!LoadedImages.ContainsKey(name))
//            {
//                LoadedImages[name] = LoadImage(name);
//            }

//            return LoadedImages[name];
//        }

//        private Bitmap LoadImage(string name)
//        {
//            XmlTextReader xmlReader = new XmlTextReader(RootResourcePath + "\\Textures\\Textures.xml");
//            while (xmlReader.Read())
//            {
//                FearLog.Log(xmlReader.Name, LogPriority.LOW);
//                if (xmlReader.Name.CompareTo("Name") == 0)
//                {
//                    xmlReader.Read();
//                    if (xmlReader.Value.CompareTo(name) == 0)
//                    {
//                        FearLog.Log("Loading image " + xmlReader.Value, LogPriority.HIGH);
//                        xmlReader.ReadToFollowing("Filepath");
//                        xmlReader.Read();
//                        return new Bitmap(xmlReader.Value);
//                    }
//                }
//            }
//            FearLog.Log("Failed to load image " + name, LogPriority.EXCEPTION);
//            return new Bitmap(128, 128);
//        }

//        public Material GetMaterial(string name)
//        {
//            if (!LoadedMaterials.ContainsKey(name))
//            {
//                LoadedMaterials[name] = materialLoader.Load(RootResourcePath + "\\Materials\\Materials.xml", name, device);
//                if (LoadedMaterials[name] == null)
//                {
//                    FearLog.Log("Failed to load material " + name, LogPriority.EXCEPTION);
//                    return LoadedMaterials[DEFAULT_MATERIAL_NAME];
//                }
//            }

//            return LoadedMaterials[name];
//        }

//        public Mesh GetMesh(string name)
//        {
//            if (!LoadedMeshes.ContainsKey(name))
//            {
//                LoadedMeshes[name] = new Mesh(device, meshLoader.Load(GetFilenameFromXML(RootResourcePath + "\\Meshes\\Meshes.xml", name)));
//                if (LoadedMeshes[name] == null)
//                {
//                    FearLog.Log("Failed to load mesh " + name, LogPriority.EXCEPTION);
//                    return LoadedMeshes[DEFAULT_MATERIAL_NAME];
//                }
//            }

//            return LoadedMeshes[name];
//        }

//        private string GetFilenameFromXML(string xmlFile, string name)
//        {
//            string filepath = "";
//            XmlTextReader xmlReader = new XmlTextReader(xmlFile);
//            while (xmlReader.Read())
//            {
//                FearLog.Log(xmlReader.Name, LogPriority.LOW);
//                if (xmlReader.Name.CompareTo("Name") == 0)
//                {
//                    xmlReader.Read();
//                    if (xmlReader.Value.CompareTo(name) == 0)
//                    {
//                        FearLog.Log("Loading mesh " + xmlReader.Value, LogPriority.HIGH);

//                        xmlReader.ReadToFollowing("Filepath");
//                        xmlReader.Read();
//                        filepath = xmlReader.Value;
//                    }
//                }
//            }

//            return filepath;
//        }

//        public void Shutdown()
//        {
//            foreach (Material mat in LoadedMaterials.Values)
//            {
//                mat.RenderEffect.Dispose();
//            }
//        }
//    }
//}
