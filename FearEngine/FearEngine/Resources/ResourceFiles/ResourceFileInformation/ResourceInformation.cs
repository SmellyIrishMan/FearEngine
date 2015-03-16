using FearEngine.Logger;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FearEngine.Resources.Managment.Loaders
{
    public abstract class ResourceInformation
    {
        private Dictionary<string, string> information;

        public ResourceInformation()
        {
            information = new Dictionary<string,string>();

            information["Name"] = "DEFAULT";
            information["Filepath"] = "";
            PopulateDefaultValues();
        }

        abstract protected void PopulateDefaultValues();

        public List<string> GetInformationKeys()
        {
            return information.Keys.ToList();
        }

        protected void AddInformation(string key, string value)
        {
            if (information.ContainsKey(key))
            {
                FearLog.Log("Key already present. No information added.", LogPriority.HIGH);
            }
            else
            {
                information[key] = value;
            }
        }

        public void UpdateInformation(string key, string value)
        {
            if (information.ContainsKey(key))
            {
                information[key] = value;
            }
            else
            {
                FearLog.Log("Key not found in Information layout. No information updated.", LogPriority.HIGH);
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

        internal bool GetBool(string key)
        {
            return Boolean.Parse(information[key]);
        }
    }
}
