﻿namespace FearEngine.Resources.Managment
{
    public class MeshResourceFile : ResourceFile
    {
        public MeshResourceFile(string location, string defautFilePath) : base(location, defautFilePath)
        {

        }

        override protected string GetType()
        {
            return "Mesh";
        }

        override protected string GetFilename()
        {
            return "Meshes.xml";
        }

        override protected string GetDefaultName()
        {
            return "DEFAULT_MESH";
        }
    }
}
