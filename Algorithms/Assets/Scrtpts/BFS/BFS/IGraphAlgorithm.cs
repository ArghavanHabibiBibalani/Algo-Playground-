using Assets.Scrtpts.BFS.Nodes;
using System;
using System.Collections;
using UnityEngine;

public interface IGraphAlgorithm
{
    IEnumerator Execute(GraphData graphData, Action<NodeData> onVisitNode);
}
