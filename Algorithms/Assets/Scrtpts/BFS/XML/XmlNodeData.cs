using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Assets.Scrtpts.BFS.XML
{
    public class XmlNodeData
    {
        [XmlAttribute("value")]
        public string Value;

        [XmlElement("Connection")]
        public List<string> Connections = new List<string>();
    }
}
