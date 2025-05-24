using System.Collections;
using UnityEngine;
using System;
using Assets.Scrtpts.BFS.Nodes;

public class FloydWarshallAlgorithm : IGraphAlgorithm
{
    public IEnumerator Execute(GraphData graphData, Action<NodeData> onVisitNode)
    {
        int nodeCount = graphData.Nodes.Count;
        float[,] distanceMatrix = new float[nodeCount, nodeCount];

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
            for (int j = 0; j < nodeCount; j++)
            {
                distanceMatrix[i, j] = (i == j) ? 0 : Mathf.Infinity;
            }
        }

        foreach (var edge in graphData.Edges)
        {
            distanceMatrix[edge.From, edge.To] = edge.Weight;
        }

        for (int k = 0; k < nodeCount; k++)
        {
            var kNode = graphData.Nodes[k];

            if (GraphManager.Instance.TryGetNodeController(kNode.Value, out var kController))
                kController.ChangeColor(Color.gray); 

            yield return new WaitForSeconds(10f);

            for (int i = 0; i < nodeCount; i++)
            {
                for (int j = 0; j < nodeCount; j++)
                {
                    float dik = distanceMatrix[i, k];
                    float dkj = distanceMatrix[k, j];

                    if (dik != Mathf.Infinity && dkj != Mathf.Infinity && distanceMatrix[i, j] > dik + dkj)
                    {
                        distanceMatrix[i, j] = dik + dkj;

                        if (GraphManager.Instance.TryGetNodeController(graphData.Nodes[i].Value, out var fromCtrl))
                            fromCtrl.ChangeColor(Color.yellow);

                        if (GraphManager.Instance.TryGetNodeController(graphData.Nodes[j].Value, out var toCtrl))
                            toCtrl.ChangeColor(Color.yellow);

                        yield return new WaitForSeconds(5000f); 

                        fromCtrl?.ChangeColor(Color.white);
                        toCtrl?.ChangeColor(Color.white);
                    }

                    yield return new WaitForSeconds(5000f);
                }

                yield return new WaitForSeconds(2000f);
            }

            onVisitNode?.Invoke(kNode);
            yield return new WaitForSeconds(3000f); 

            kController?.ChangeColor(Color.green);
        }

        foreach (var node in graphData.Nodes)
        {
            if (GraphManager.Instance.TryGetNodeController(node.Value, out var controller))
            {
                controller.ChangeColor(Color.green);
            }
        }
    }
}
