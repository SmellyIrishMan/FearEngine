﻿using FearEngine.Logger;
using FearEngine.Resources.Managment.Loaders;
using FearEngine.Resources.Managment.Loaders.Collada;
using FearEngine.Resources.Meshes;
using SharpDX.Toolkit.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Xml;

namespace FearEngine.Resources.Managment
{
    public class FearResourceManager
    {
        private ResourceDirectory resourceDir;
        private Dictionary<string, Texture> loadedTextures;
        private Dictionary<string, Material> loadedMaterials;
        private Dictionary<string, RenderableMesh> loadedMeshes;

        private Dictionary<ResourceType, ResourceLoader> loaders;

        public FearResourceManager(ResourceDirectory directory, ResourceLoader materialLoader, ResourceLoader meshLoader, ResourceLoader textureLoader)
        {
            resourceDir = directory;

            loaders = new Dictionary<ResourceType, ResourceLoader>();
            loaders[ResourceType.Material] = materialLoader;
            loaders[ResourceType.Mesh] = meshLoader;
            loaders[ResourceType.Texture] = textureLoader;
        
            loadedMaterials = new Dictionary<string, Material>();
            loadedMeshes = new Dictionary<string, RenderableMesh>();
            loadedTextures = new Dictionary<string, Texture>();
        }

        public Material GetMaterial(string name)
        {
            if (!loadedMaterials.ContainsKey(name))
            {
                Resource newRes = LoadResource(name, ResourceType.Material, resourceDir.GetMaterialInformation(name));
                loadedMaterials[name] = (Material)newRes;
            }

            return loadedMaterials[name];
        }

        public RenderableMesh GetMesh(string name)
        {
            if (!loadedMeshes.ContainsKey(name))
            {
                Resource newRes = LoadResource(name, ResourceType.Mesh, resourceDir.GetMeshInformation(name));
                loadedMeshes[name] = (RenderableMesh)newRes;
            }

            return loadedMeshes[name];
        }

        public Texture GetTexture(string name)
        {
            if (!loadedTextures.ContainsKey(name))
            {
                Resource newRes = LoadResource(name, ResourceType.Texture, resourceDir.GetTextureInformation(name));
                loadedTextures[name] = (Texture)newRes;
            }

            return loadedTextures[name];
        }

        private Resource LoadResource(string name, ResourceType type, ResourceInformation info)
        {
            Resource newResource = loaders[type].Load(info);
            if (newResource.IsLoaded())
            {
                return newResource;
            }

            throw new UnableToLoadResourceException();
        }

        public int GetNumberOfLoadedResources(ResourceType type)
        {
            switch(type)
            {
                case ResourceType.Material:
                    return loadedMaterials.Count;
                case ResourceType.Mesh:
                    return loadedMaterials.Count;
                case ResourceType.Texture:
                    return loadedMaterials.Count;
                default:
                    return 0;
            }
        }

        public void Shutdown()
        {
            foreach (Material mat in loadedMaterials.Values)
            {
                mat.Dispose();
            }
            foreach (RenderableMesh mesh in loadedMeshes.Values)
            {
                mesh.Dispose();
            }
            foreach (Texture tex in loadedTextures.Values)
            {
                tex.Dispose();
            }
        }
    }

    public class UnableToLoadResourceException : Exception
    {
        public UnableToLoadResourceException()
        {
        }

        public UnableToLoadResourceException(string message)
            : base(message)
        {
        }

        public UnableToLoadResourceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
