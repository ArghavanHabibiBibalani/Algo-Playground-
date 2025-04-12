using Assets.Scrtpts.BFS.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrtpts.BFS.BFS
{
    public class DFSAlgorithm : IGraphAlgorithm
    {
        public IEnumerator Execute(GraphData graphData, Action<NodeData> onVisitNode)
        {
            if (graphData.Nodes.Count == 0) yield break;

            // Reset all node colors
            foreach (var node in graphData.Nodes)
            {
                if (GraphManager.Instance.TryGetNodeController(node.Value, out var controller))
                {
                    controller.ChangeColor(Color.white);
                    controller.ShowNode();
                }
            }

            var visited = new HashSet<NodeData>();
            var stack = new Stack<NodeData>();
            var startNode = graphData.Nodes[0];

            stack.Push(startNode);

            if (GraphManager.Instance.TryGetNodeController(startNode.Value, out var startController))
                startController.ChangeColor(Color.gray); // Gray = discovered but not fully explored

            while (stack.Count > 0)
            {
                var current = stack.Pop();

                if (visited.Contains(current))
                    continue;

                visited.Add(current);

                if (GraphManager.Instance.TryGetNodeController(current.Value, out var controller))
                {
                    controller.ChangeColor(Color.green); // Green = visited
                }

                onVisitNode?.Invoke(current);
                yield return new WaitForSeconds(3f);

                // Traverse neighbors in reverse to simulate recursion order in a stack
                for (int i = current.Connections.Count - 1; i >= 0; i--)
                {
                    var neighbor = current.Connections[i];
                    if (!visited.Contains(neighbor))
                    {
                        stack.Push(neighbor);

                        if (GraphManager.Instance.TryGetNodeController(neighbor.Value, out var neighborController))
                            neighborController.ChangeColor(Color.gray);
                    }
                }
            }
        }
    }
}
