using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using Assets.Scrtpts.BFS.Nodes;

public class BFSAlgorithm : IGraphAlgorithm
{
    public IEnumerator Execute(GraphData graphData, Action<NodeData> onVisitNode)
    {
        if (graphData.Nodes.Count == 0) yield break;

        // Reset all nodes to white at the start
        foreach (var node in graphData.Nodes)
        {
            if (GraphManager.Instance.TryGetNodeController(node.Value, out var controller))
            {
                controller.ChangeColor(Color.white);

                controller.ShowNode();
            }
        }

        var visited = new HashSet<NodeData>();
        var queue = new Queue<NodeData>();
        var startNode = graphData.Nodes[0];

        queue.Enqueue(startNode);
        visited.Add(startNode);

        Debug.Log("Number of edges: " + GraphManager.Instance._edges.Count);
        foreach (var edge in GraphManager.Instance._edges.Values)
        {
            edge.ShowEdge();
        }

        if (GraphManager.Instance.TryGetNodeController(startNode.Value, out var startController))
            startController.ChangeColor(Color.gray); // Gray when added to queue

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (GraphManager.Instance.TryGetNodeController(current.Value, out var controller))
            {
                controller.ChangeColor(Color.green); // Green when visited
            }

            onVisitNode?.Invoke(current);
            yield return new WaitForSeconds(3f);

            foreach (var neighbor in current.Connections)
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);

                    if (GraphManager.Instance.TryGetNodeController(neighbor.Value, out var neighborController))
                        neighborController.ChangeColor(Color.gray); // Gray when added to queue
                }
            }
        }
    }
}
