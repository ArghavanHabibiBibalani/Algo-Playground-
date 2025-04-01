using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GraphManager : MonoBehaviour
{
    [SerializeField]
    private GraphData _graphData;
    [SerializeField]
    private GameObject _node;
    [SerializeField]
    private Transform _graphContainer;
    [SerializeField]
    private GameObject edgePrefab;

    private Dictionary<NodeData, NodeController> _nodeControllers = new();

    void Start()
    {
        GenerateGraph();
    }

    void Update()
    {
        
    }

    public void GenerateGraph()
    {
        foreach (NodeData node in _graphData.Nodes)
        {
            GameObject newNode = Instantiate(_node, _graphContainer);
            NodeController controller = newNode.GetComponent<NodeController>();
            controller.Setup(node);
            _nodeControllers[node] = controller;

            newNode.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        }

        foreach (NodeData node in _graphData.Nodes)
        {
            foreach (NodeData neighbor in node.Connections)
            {
                if (_nodeControllers.ContainsKey(neighbor))
                {
                    GameObject newEdge = Instantiate(edgePrefab, _graphContainer);
                    EdgeRenderer edgeRenderer = newEdge.GetComponent<EdgeRenderer>();
                    edgeRenderer.Setup(_nodeControllers[node].transform.position, _nodeControllers[neighbor].transform.position);
                }
            }
        }
    }

}
