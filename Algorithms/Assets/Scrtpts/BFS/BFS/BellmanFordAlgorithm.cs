using System.Collections;
using UnityEngine;
using System;
using Assets.Scrtpts.BFS.Nodes;

public class BellmanFordAlgorithm : IGraphAlgorithm
{
    public IEnumerator Execute(GraphData graphData, Action<NodeData> onVisitNode)
    {
        int nodeCount = graphData.Nodes.Count;
        float[] distances = new float[nodeCount];
        int[] previousNodes = new int[nodeCount];

        foreach (var node in graphData.Nodes)
        {
            if (GraphManager.Instance.TryGetNodeController(node.Value, out var controller))
            {
                controller.ChangeColor(Color.white);
                controller.ShowNode();
            }
        }

        foreach (var edge in GraphManager.Instance._edges.Values)
        {
            edge.ShowEdge();
        }

        yield return null;

        for (int i = 0; i < nodeCount; i++)
        {
            distances[i] = Mathf.Infinity;
            previousNodes[i] = -1;
        }

        distances[0] = 0;

        for (int i = 1; i < nodeCount; i++)
        {
            foreach (var edge in graphData.Edges)
            {
                if (distances[edge.From] + edge.Weight < distances[edge.To])
                {
                    distances[edge.To] = distances[edge.From] + edge.Weight;
                    previousNodes[edge.To] = edge.From;

                    if (GraphManager.Instance._edges.TryGetValue((edge.From, edge.To), out var edgeRenderer))
                    {
                        edgeRenderer.ShowEdge();
                    }

                    yield return null;

                    if (GraphManager.Instance.TryGetNodeController(graphData.Nodes[edge.From].Value, out var fromNode))
                    {
                        fromNode.ShowNode();
                    }

                    if (GraphManager.Instance.TryGetNodeController(graphData.Nodes[edge.To].Value, out var toNode))
                    {
                        toNode.ShowNode();
                    }

                    yield return null; 
                }
            }
        }

        foreach (var node in graphData.Nodes)
        {
            onVisitNode?.Invoke(node);
            yield return null; 
        }

        foreach (var edge in GraphManager.Instance._edges.Values)
        {
            edge.ShowEdge();
        }
        yield return null;
    }
}
