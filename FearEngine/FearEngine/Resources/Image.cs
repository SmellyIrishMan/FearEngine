using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FearEngine.Resources
{
    [Serializable()]
    //TODO This class is not used at all yet but I'm sure it will be in time.
    class Image
    {
        [System.Xml.Serialization.XmlElementAttribute("Name")]
        public string Name { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("Filepath")]
        public string Filepath { get; set; }  
    }
}