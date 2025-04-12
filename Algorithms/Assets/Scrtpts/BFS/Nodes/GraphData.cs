using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrtpts.BFS.Nodes
{
    [CreateAssetMenu(fileName = "GraphData", menuName = "Scriptable Objects/GraphData")]
    public class GraphData : ScriptableObject
    {
        public List<NodeData> Nodes;
    }
}
