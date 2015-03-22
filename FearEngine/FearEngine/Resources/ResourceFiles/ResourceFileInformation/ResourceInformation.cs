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

        public List<string> InformationKeys { get { return information.Keys.ToList(); } }
        public string Name                  { get { return information["Name"]; } }
        public string Filepath              { get { return information["Filepath"]; } }

        abstract protected void PopulateDefaultValues();
        abstract public ResourceType Type { get; }

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
