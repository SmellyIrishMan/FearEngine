using FearEngine.Logger;
using FearEngine.Resources.Managment.Loaders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace FearEngine.Resources.Managment
{
    public abstract class ResourceFile
    {
        string XMLRootElementSuffix = "List";

        string filePath;

        const string defaultResourceName = "DEFAULT";

        abstract public string GetFilename();

        abstract protected string GetType();

        public ResourceFile(string location)
        {
            filePath = location + "\\" + GetFilename();
            if (System.IO.File.Exists(filePath))
            {
                if (GetFilepathForDefault().Length > 0)
                {
                    return;
                }
                else
                {
                    AddDefaultMesh();
                }
            }
            else
            {
                System.IO.StreamWriter file = new StreamWriter(filePath);
                file.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
                file.WriteLine("<" + GetRootElement() + ">");
                file.WriteLine("\t<" + GetType() + ">");
                file.WriteLine("\t\t<Name>" + defaultResourceName + "</Name>");
                file.WriteLine("\t\t<Filepath></Filepath>");
                file.WriteLine("\t</" + GetType() + ">");
                file.WriteLine("</" + GetRootElement() + ">");
                file.Close();
            }
        }

        private void AddDefaultMesh()
        {
            string[] fullResourceFile = File.ReadAllLines(filePath);
            List<string> lines = new List<string>();
            lines.AddRange(fullResourceFile);
            lines.InsertRange(2, CreateDefaultResourceEntry());
            try
            {
                File.WriteAllLines(filePath, lines.ToArray());
            }
            catch (Exception e)
            { }
        }

        private IEnumerable<string> CreateDefaultResourceEntry()
        {
            List<string> newEntry = new List<string>();
            newEntry.Add("\t<" + GetType() + ">");
            newEntry.Add("\t\t<Name>" + defaultResourceName + "</Name>");
            newEntry.Add("\t\t<Filepath></Filepath>");
            newEntry.Add("\t</" + GetType() + ">");

            return newEntry;
        }

        private string GetRootElement()
        {
            return GetType() + XMLRootElementSuffix;
        }

        public ResourceInformation GetResouceInformationByName(string name, bool fallbackToDefault = false)
        {
            XmlTextReader xmlReader = SearchFileForResource(name);

            ResourceInformation populatedInformation = new ResourceInformation();
            if (!xmlReader.EOF)
            {
                populatedInformation = ParseInformationFromXMLBlock(xmlReader);
            }
            else if (fallbackToDefault)
            {
                populatedInformation = GetResouceInformationByName(defaultResourceName);
            }

            return populatedInformation;
        }

        private XmlTextReader SearchFileForResource(string resourceNameToFind)
        {
            XmlTextReader xmlReader = new XmlTextReader(filePath);
            while (xmlReader.Read())
            {
                if (xmlReader.Name.CompareTo("Name") == 0)
                {
                    xmlReader.Read();
                    if (xmlReader.Value.CompareTo(resourceNameToFind) == 0)
                    {
                        return xmlReader;
                    }
                }
            }
            return xmlReader;
        }

        private ResourceInformation ParseInformationFromXMLBlock(XmlTextReader xmlReader)
        {
            ResourceInformation populatedInformation = new ResourceInformation();

            string key = "Name";
            string value = xmlReader.Value;
            populatedInformation.AddInformation(key, value);

            while (!ReachedEndOfResourceBlock(xmlReader))
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    key = xmlReader.Name;
                    xmlReader.Read();
                    value = xmlReader.Value;
                    populatedInformation.AddInformation(key, value);
                }

                xmlReader.Read();
            }
            xmlReader.Close();

            return populatedInformation;
        }

        private bool ReachedEndOfResourceBlock(XmlTextReader xmlReader)
        {
            return xmlReader.NodeType == XmlNodeType.EndElement &&
                xmlReader.Name.CompareTo(GetType()) == 0;
        }

        private string GetFilepathForDefault()
        {
            return GetResouceInformationByName(defaultResourceName).GetFilepath();
        }

        public string GetDefaultResourceName()
        { 
            return defaultResourceName;
        }
    }
}
