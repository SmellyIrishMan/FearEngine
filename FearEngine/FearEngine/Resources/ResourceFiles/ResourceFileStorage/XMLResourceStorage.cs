using FearEngine.Logger;
using FearEngine.Resources.Managment;
using FearEngine.Resources.Managment.Loaders;
using FearEngine.Resources.ResourceFiles.ResourceFileInformation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace FearEngine.Resources.ResourceFiles
{
    public class XMLResourceStorage : ResourceStorage
    {
        private string location;
        private string filename;
        private string filePath;

        private ResourceType type;
        private ResourceInformationFactory infoFactory;

        private ResourceInformation defaultInfo;

        public XMLResourceStorage(string loc, string name, ResourceType t)
        {
            location = loc;
            filename = name;
            filePath = location + "\\" + filename;

            type = t;
            infoFactory = new ResourceInformationFactory();

            defaultInfo = infoFactory.CreateResourceInformation(type);

            if (!System.IO.File.Exists(filePath))
            {
                CreateFile();
            }

            defaultInfo = GetInformationByName(defaultInfo.Name);
            StoreInformation(defaultInfo);
        }

        private void CreateFile()
        {
            System.IO.StreamWriter file = new StreamWriter(filePath);
            file.WriteLine("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            file.WriteLine("<" + GetRootElement() + ">");
            file.WriteLine("</" + GetRootElement() + ">");
            file.Close();
        }

        public ResourceInformation GetInformationByName(string name)
        {
            ResourceInformation info = defaultInfo;

            XmlTextReader xmlReader = SearchFileForResource(name);
            if (!xmlReader.EOF)
            {
                PopulateInformationFromXMLBlock(info, xmlReader);
            }

            return info;
        }

        public void StoreInformation(ResourceInformation information)
        {
            List<string> lines = GetFileLines();

            RemoveEntryByName(lines, information.Name);

            List<string> newEntry = CreateFileEntryFromInfo(information);
            lines.InsertRange(2, newEntry);

            try
            {
                File.WriteAllLines(filePath, lines.ToArray());
            }
            catch (Exception e)
            {
                FearLog.Log(e.Message, LogPriority.EXCEPTION);
            }
        }

        private void RemoveEntryByName(List<string> lines, string name)
        {
            int startingIndex = lines.IndexOf("      <Name>" + name + "</Name>");
            if (startingIndex > 0)
            {
                startingIndex = startingIndex - 1;

                int endIndex = lines.IndexOf("   </" + GetTypeString() + ">", startingIndex);
                endIndex = endIndex + 1;
                lines.RemoveRange(startingIndex, endIndex - startingIndex);
            }
        }

        private List<string> CreateFileEntryFromInfo(ResourceInformation info)
        {
            List<string> newEntry = new List<string>();
            newEntry.Add("   <" + GetTypeString() + ">");
            foreach (string key in info.InformationKeys)
            {
                newEntry.Add("      <" + key + ">" + info.GetString(key) + "</" + key + ">");
            }
            newEntry.Add("   </" + GetTypeString() + ">");
            return newEntry;
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
            return type.ToString() + "List";
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

        private void PopulateInformationFromXMLBlock(ResourceInformation parsedInfo, XmlTextReader xmlReader)
        {
            string key = "Name";
            string value = xmlReader.Value;
            parsedInfo.UpdateInformation(key, value);

            while (!ReachedEndOfResourceBlock(xmlReader))
            {
                if (xmlReader.NodeType == XmlNodeType.Element && parsedInfo.InformationKeys.Contains(xmlReader.Name))
                {
                    key = xmlReader.Name;
                    xmlReader.Read();
                    value = xmlReader.Value;
                    parsedInfo.UpdateInformation(key, value);
                }

                xmlReader.Read();
            }
            xmlReader.Close();
        }

        private bool ReachedEndOfResourceBlock(XmlTextReader xmlReader)
        {
            return xmlReader.NodeType == XmlNodeType.EndElement &&
                xmlReader.Name.CompareTo(GetTypeString()) == 0;
        }

        private string GetTypeString()
        {
            return type.ToString();
        }
    }
}
