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

        abstract protected string GetFilename();

        abstract protected string GetType();

        abstract protected string GetDefaultName();

        public ResourceFile(string location, string defautFilePath)
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
                    AddDefaultMesh(defautFilePath);
                }
            }
            else
            {
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
        }

        private void AddDefaultMesh(string defautFilePath)
        {
            string[] fullResourceFile = File.ReadAllLines(filePath);
            List<string> lines = new List<string>();
            lines.AddRange(fullResourceFile);
            lines.InsertRange(2, CreateNewResourceEntry(defautFilePath));
            File.WriteAllLines(filePath, lines.ToArray());
        }

        private IEnumerable<string> CreateNewResourceEntry(string defautFilePath)
        {
            List<string> newEntry = new List<string>();
            newEntry.Add("\t<" + GetType() + ">");
            newEntry.Add("\t\t<Name>" + GetDefaultName() + "</Name>");
            newEntry.Add("\t\t<Filepath>" + defautFilePath + "</Filepath>");
            newEntry.Add("\t<" + GetType() + ">");

            return newEntry;
        }

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
