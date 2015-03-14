using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FearEngine.Resources.Managment.Loaders
{
    public class ResourceInformation
    {
        Dictionary<string, string> information;

        public ResourceInformation()
        {
            information = new Dictionary<string,string>();

            information["Name"] = "";
            information["Filepath"] = "";
        }

        public void AddInformation(string key, string value)
        {
            if (information.ContainsKey(key))
            {
                information[key] = value;
            }
            else
            {
                information.Add(key, value);
            }
        }

        public string GetName()
        {
            return information["Name"];
        }

        public string GetFilepath()
        {
            return information["Filepath"];
        }

        public string GetString(string key)
        {
            return information[key];
        }

        public int GetInt(string key)
        {
            return Int32.Parse(information[key]);
        }

        public List<string> GetPotentialInformationKeys()
        {
            return information.Keys.ToList();
        }
    }
}
