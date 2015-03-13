using FearEngine.Logger;
using FearEngine.Resources.Managment.Loaders;
using FearEngine.Resources.Managment.Loaders.Collada;
using FearEngine.Resources.Meshes;
using SharpDX.Toolkit.Graphics;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

namespace FearEngine.Resources.Managment
{
    //public abstract class ResourceDirectory
    //{
    //    public ResourceDirectory(string rootResourcePath)
    //    {
    //        if (System.IO.Directory.Exists(rootResourcePath))
    //        {
    //            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(rootResourcePath, GetDirectoryName()));
    //        }
    //    }

    //    virtual protected string GetDirectoryName();

    //    public Bitmap GetImage(string name)
    //    {
    //        if (!LoadedImages.ContainsKey(name))
    //        {
    //            LoadedImages[name] = LoadImage(name);
    //        }

    //        return LoadedImages[name];
    //    }

    //    private Bitmap LoadImage(string name)
    //    {
    //        XmlTextReader xmlReader = new XmlTextReader(RootResourcePath + "\\Textures\\Textures.xml");
    //        while (xmlReader.Read())
    //        {
    //            FearLog.Log(xmlReader.Name, LogPriority.LOW);
    //            if (xmlReader.Name.CompareTo("Name") == 0)
    //            {
    //                xmlReader.Read();
    //                if (xmlReader.Value.CompareTo(name) == 0)
    //                {
    //                    FearLog.Log("Loading image " + xmlReader.Value, LogPriority.HIGH);
    //                    xmlReader.ReadToFollowing("Filepath");
    //                    xmlReader.Read();
    //                    return new Bitmap(xmlReader.Value);
    //                }
    //            }
    //        }
    //        FearLog.Log("Failed to load image " + name, LogPriority.EXCEPTION);
    //        return new Bitmap(128, 128);
    //    }

    //    public Material GetMaterial(string name)
    //    {
    //        if (!LoadedMaterials.ContainsKey(name))
    //        {
    //            LoadedMaterials[name] = materialLoader.Load(RootResourcePath + "\\Materials\\Materials.xml", name, device);
    //            if (LoadedMaterials[name] == null)
    //            {
    //                FearLog.Log("Failed to load material " + name, LogPriority.EXCEPTION);
    //                return LoadedMaterials[DEFAULT_MATERIAL_NAME];
    //            }
    //        }

    //        return LoadedMaterials[name];
    //    }

    //    public MeshRenderable GetMesh(string name)
    //    {
    //        if (!LoadedMeshes.ContainsKey(name))
    //        {
    //            LoadedMeshes[name] = new MeshRenderable(device, meshLoader.Load(GetFilenameFromXML(RootResourcePath + "\\Meshes\\Meshes.xml", name)));
    //            if (LoadedMeshes[name] == null)
    //            {
    //                FearLog.Log("Failed to load mesh " + name, LogPriority.EXCEPTION);
    //                return LoadedMeshes[DEFAULT_MATERIAL_NAME];
    //            }
    //        }

    //        return LoadedMeshes[name];
    //    }

    //    public void Shutdown()
    //    {
    //        foreach (Material mat in LoadedMaterials.Values)
    //        {
    //            mat.RenderEffect.Dispose();
    //        }
    //    }
    //}
}
