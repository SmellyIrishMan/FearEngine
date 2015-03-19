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

        ResourceInformation defaultInformation;

        abstract protected ResourceInformation CreateFreshResourceInformation();

        abstract public string GetFilename();

        abstract protected string GetType();

        public ResourceFile(string location, ResourceInformation defaultInfo)
        {
            defaultInformation = defaultInfo;

            filePath = location + "\\" + GetFilename();
            if (System.IO.File.Exists(filePath))
            {
                UpdateDefaultResourceInformation();
            }
            else
            {
                CreateFile();
            }
        }

        private void CreateFile()
        {
            System.IO.StreamWriter file = new StreamWriter(filePath);
            file.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            file.WriteLine("<" + GetRootElement() + ">");
            file.WriteLine("</" + GetRootElement() + ">");
            file.Close();

            AddDefaultResource();
        }

        private void UpdateDefaultResourceInformation()
        {
            ResourceInformation updatedInfo = GetResourceInformationByName(defaultInformation.GetName());
            if (updatedInfo.GetName().CompareTo(defaultInformation.GetName()) == 0)
            {
                ReplaceCurrentDefault(updatedInfo);
            }
            else
            {
                AddDefaultResource();
            }
        }

        private void ReplaceCurrentDefault(ResourceInformation updatedInfo)
        {
            List<string> lines = GetFileLines();

            RemoveEntryByName(lines, defaultInformation.GetString("Name"));

            List<string> newEntry = CreateFileEntryFromInfo(updatedInfo);
            lines.InsertRange(2, newEntry);
            File.WriteAllLines(filePath, lines.ToArray());
        }

        private void RemoveEntryByName(List<string> lines, string name)
        {
            int startingIndex = lines.IndexOf("      <Name>" + name + "</Name>");
            if (startingIndex > 0)
            {
                startingIndex = startingIndex - 1;

                int endIndex = lines.IndexOf("   </" + GetType() + ">", startingIndex);
                endIndex = endIndex + 1;
                lines.RemoveRange(startingIndex, endIndex - startingIndex);
            }
        }

        private List<string> CreateFileEntryFromInfo(ResourceInformation info)
        {
            List<string> newEntry = new List<string>();
            newEntry.Add("   <" + GetType() + ">");
            foreach (string key in info.GetInformationKeys())
            {
                newEntry.Add("      <" + key + ">" + info.GetString(key) + "</" + key + ">");
            }
            newEntry.Add("   </" + GetType() + ">");
            return newEntry;
        }

        private void AddDefaultResource()
        {
            List<string> lines = GetFileLines();
            List<string> newEntry = CreateFileEntryFromInfo(defaultInformation);

            lines.InsertRange(2, newEntry);
            File.WriteAllLines(filePath, lines.ToArray());
        }

        private List<string> GetFileLines()
        {
            string[] fullResourceFile = File.ReadAllLines(filePath);
            List<string> lines = new List<string>();
            lines.AddRange(fullResourceFile);
            return lines;
        }

        private string GetRootElement()
        {
            return GetType() + XMLRootElementSuffix;
        }

        public ResourceInformation GetResourceInformationByName(string name, bool fallbackToDefault = false)
        {
            XmlTextReader xmlReader = SearchFileForResource(name);

            ResourceInformation info = CreateFreshResourceInformation();
            if (!xmlReader.EOF)
            {
                info = ParseInformationFromXMLBlock(xmlReader);
            }
            else if (fallbackToDefault)
            {
                info = GetResourceInformationByName(defaultInformation.GetName());
            }

            return info;
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
            ResourceInformation populatedInfo = CreateFreshResourceInformation();

            string key = "Name";
            string value = xmlReader.Value;
            populatedInfo.UpdateInformation(key, value);

            while (!ReachedEndOfResourceBlock(xmlReader))
            {
                if (xmlReader.NodeType == XmlNodeType.Element && populatedInfo.GetInformationKeys().Contains(xmlReader.Name))
                {
                    key = xmlReader.Name;
                    xmlReader.Read();
                    value = xmlReader.Value;
                    populatedInfo.UpdateInformation(key, value);
                }

                xmlReader.Read();
            }
            xmlReader.Close();

            return populatedInfo;
        }

        private bool ReachedEndOfResourceBlock(XmlTextReader xmlReader)
        {
            return xmlReader.NodeType == XmlNodeType.EndElement &&
                xmlReader.Name.CompareTo(GetType()) == 0;
        }

        private string GetFilepathForDefault()
        {
            return GetResourceInformationByName(defaultInformation.GetName()).GetFilepath();
        }

        public string GetDefaultResourceName()
        {
            return defaultInformation.GetName();
        }
    }
}
