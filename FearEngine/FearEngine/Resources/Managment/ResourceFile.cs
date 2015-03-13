using FearEngine.Logger;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace FearEngine.Resources.Managment
{
    public abstract class ResourceFile
    {
        string XMLRootElementSuffix = "List";

        string filePath;

        string filePathElement = "Filepath";

        public ResourceFile(string location, string defautFilePath)
        {
            filePath = location + "\\" + GetFilename();
            System.IO.StreamWriter file = new StreamWriter(filePath);
            file.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            file.WriteLine("<" + GetRootElement() + ">");
            file.WriteLine("\t<" + GetType() + ">");
            file.WriteLine("\t\t<Name>" + GetDefaultName() + "</Name>");
            file.WriteLine("\t\t<Filepath>" + defautFilePath + "</Filepath>");
            file.WriteLine("\t</" + GetType() + ">");
            file.WriteLine("</" + GetRootElement() + ">");
            file.Close();
        }

        abstract protected string GetFilename();

        abstract protected string GetType();

        abstract protected string GetDefaultName();

        private string GetRootElement()
        {
            return GetType() + XMLRootElementSuffix;
        }

        public string GetFilepathByResourceName(string name, bool fallbackToDefault = false)
        {
            string filepath = SearchFileForName(name, filePathElement);

            if (filepath.Length == 0 && fallbackToDefault)
            {
                filepath = GetFilepathForDefault();
            }

            return filepath;
        }

        private string GetFilepathForDefault()
        {
            return SearchFileForName(GetDefaultName(), filePathElement);
        }

        private string SearchFileForName(string resourceName, string resourceElement)
        {
            string value = "";
            XmlTextReader xmlReader = new XmlTextReader(filePath);
            while (xmlReader.Read())
            {
                FearLog.Log(xmlReader.Name, LogPriority.LOW);
                if (xmlReader.Name.CompareTo("Name") == 0)
                {
                    xmlReader.Read();
                    if (xmlReader.Value.CompareTo(resourceName) == 0)
                    {
                        xmlReader.ReadToFollowing(resourceElement);
                        xmlReader.Read();
                        value = xmlReader.Value;
                        xmlReader.Close();
                        return value;
                    }
                }
            }
            xmlReader.Close();
            return value;
        }
    }
}
