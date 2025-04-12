using UnityEngine;

public class AlgorithmExecutor : MonoBehaviour
{
    [SerializeField] private GraphManager graphManager;

    public void RunAlgorithm(IGraphAlgorithm algorithm)
    {
        //foreach (var edge in GraphManager.Instance._edges.Values)
        //{
        //    edge.ShowEdge();
        //}

        StartCoroutine(algorithm.Execute(graphManager._graphData, OnVisitNode));
    }

    private void OnVisitNode(NodeData node)
    {
        if (GraphManager.Instance != null &&
            GraphManager.Instance.TryGetNodeController(node.Value, out var controller))
        {
            controller.ChangeColor(Color.green);
            controller.ShowNode(); 
        }
    }

}
