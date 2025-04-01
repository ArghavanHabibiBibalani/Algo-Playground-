using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BipartiteGraphData", menuName = "Scriptable Objects/BipartiteGraphData")]
public class BipartiteGraphData : ScriptableObject
{
    public List<int> groupA = new List<int>();
    public List<int> groupB = new List<int>();
    public List<GraphEdge> edges = new List<GraphEdge>();

    public void AddEdge(int origin, int dest)
    {
        edges.Add(new GraphEdge() { Origin = origin, Destination = dest });
    }
}
