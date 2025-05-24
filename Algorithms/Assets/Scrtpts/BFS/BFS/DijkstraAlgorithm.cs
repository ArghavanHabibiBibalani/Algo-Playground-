using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scrtpts.BFS.Nodes;

public class DijkstraAlgorithm : IGraphAlgorithm
{
    public IEnumerator Execute(GraphData graphData, Action<NodeData> onVisitNode)
    {
        if (graphData.Nodes.Count == 0) yield break;

        foreach (var node in graphData.Nodes)
        {
            if (GraphManager.Instance.TryGetNodeController(node.Value, out var controller))
            {
                controller.ChangeColor(Color.white);
                controller.ShowNode();
            }
        }

        int nodeCount = graphData.Nodes.Count;
        float[] distances = new float[nodeCount];
        int[] previousNodes = new int[nodeCount];
        List<int> unvisitedNodes = new List<int>();

        for (int i = 0; i < nodeCount; i++)
        {
            distances[i] = Mathf.Infinity;
            previousNodes[i] = -1;
            unvisitedNodes.Add(i);
        }

        distances[0] = 0;

        foreach (var edge in GraphManager.Instance._edges.Values)
        {
            edge.ShowEdge();
        }

        while (unvisitedNodes.Count > 0)
        {
            int currentNodeIndex = GetNodeWithSmallestDistance(distances, unvisitedNodes);
            unvisitedNodes.Remove(currentNodeIndex);

            var currentNode = graphData.Nodes[currentNodeIndex];

            if (GraphManager.Instance.TryGetNodeController(currentNode.Value, out var controller))
            {
                controller.ChangeColor(Color.green);
            }

            onVisitNode?.Invoke(currentNode);
            yield return new WaitForSeconds(3f);

            foreach (var neighbor in currentNode.Connections)
            {
                int neighborIndex = graphData.Nodes.IndexOf(neighbor);
                float newDist = distances[currentNodeIndex] + GetEdgeWeight(graphData, currentNodeIndex, neighborIndex);

                if (newDist < distances[neighborIndex])
                {
                    distances[neighborIndex] = newDist;
                    previousNodes[neighborIndex] = currentNodeIndex;

                    if (GraphManager.Instance.TryGetNodeController(neighbor.Value, out var neighborController))
                        neighborController.ChangeColor(Color.gray);
                }
            }
        }
    }

    private int GetNodeWithSmallestDistance(float[] distances, List<int> unvisitedNodes)
    {
        int smallest = unvisitedNodes[0];
        foreach (int node in unvisitedNodes)
        {
            if (distances[node] < distances[smallest])
            {
                smallest = node;
            }
        }
        return smallest;
    }

    private float GetEdgeWeight(GraphData graphData, int fromIndex, int toIndex)
    {
        foreach (var edge in graphData.Edges)
        {
            if (edge.From == fromIndex && edge.To == toIndex)
            {
                return edge.Weight;
            }
        }
        return Mathf.Infinity;
    }
}
