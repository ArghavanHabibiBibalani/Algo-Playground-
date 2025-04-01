using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GraphData", menuName = "Scriptable Objects/GraphData")]
public class GraphData : ScriptableObject
{
    public List<NodeData> Nodes;
}
