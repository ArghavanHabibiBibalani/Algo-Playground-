using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeData", menuName = "Scriptable Objects/NodeData")]
public class NodeData : ScriptableObject
{
    public int Value;
    public List<NodeData> Connections = new List<NodeData>();
    public List<int> ConnectionIDs = new List<int>();
}
