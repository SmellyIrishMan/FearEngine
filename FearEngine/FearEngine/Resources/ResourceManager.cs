using FearEngine.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FearEngine.Resources
{
    public static class ResourceManager
    {
        private static string RootResourcePath{get; set;}

        public static void Initialise()
        {
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
            System.IO.File.Create(subResourcePath + "\\" + name + ".xml");
        }

        public static void Shutdown()
        {

        }
    }
}
