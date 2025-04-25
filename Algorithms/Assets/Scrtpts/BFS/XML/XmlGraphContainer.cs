using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Assets.Scrtpts.BFS.XML
{
    [XmlRoot("Graph")]
    public class XmlGraphContainer
    {
        [XmlElement("Node")]
        public List<XmlNodeData> Nodes = new List<XmlNodeData>();
    }
}
