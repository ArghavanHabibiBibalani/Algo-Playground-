using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scrtpts.BFS.Nodes
{
    [CreateAssetMenu(fileName = "GraphData", menuName = "Scriptable Objects/GraphData")]
    public class GraphData : ScriptableObject
    {
        public List<NodeData> Nodes;
        public List<EdgeData> Edges = new();
    }
}
