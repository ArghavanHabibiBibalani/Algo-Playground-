
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif
using Assets.Scrtpts.BFS.Nodes;

public class GraphManager : MonoBehaviour
{
    [SerializeField] public GraphData _graphData;
    [SerializeField] public GameObject _node;
    [SerializeField] public Transform _graphContainer;
    [SerializeField] public GameObject _edgePrefab;

    private Dictionary<int, NodeController> _nodeControllers = new();
    public Dictionary<(int, int), EdgeRenderer> _edges = new();

    public static GraphManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _nodeControllers.Clear();
        _edges.Clear();

        if (_graphData.Nodes == null)
            _graphData.Nodes = new List<NodeData>();

        ClearData();

        _graphData.Nodes.RemoveAll(node => node == null);


    }

    private void Start()
    {

        GenerateGraph();
    }

    public void ClearData()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) return;

#if UNITY_EDITOR
        string nodeDir = "Assets/Data/Nodes";
        if (Directory.Exists(nodeDir))
        {
            string[] nodeFiles = Directory.GetFiles(nodeDir, "*.asset");
            foreach (string file in nodeFiles)
            {
                AssetDatabase.DeleteAsset(file);
            }
        }

        string edgeDir = "Assets/Data/Edges";
        if (Directory.Exists(edgeDir))
        {
            string[] edgeFiles = Directory.GetFiles(edgeDir, "*.asset");
            foreach (string file in edgeFiles)
            {
                AssetDatabase.DeleteAsset(file);
            }
        }
#endif

        _nodeControllers.Clear();
        _edges.Clear();
        _graphData.Edges.Clear();
    }
    public void GenerateGraph()
    {
        List<NodeData> nodesCopy = new(_graphData.Nodes);
        foreach (NodeData node in nodesCopy)
        {
            if (node != null && !_nodeControllers.ContainsKey(node.Value))
                AddNode(node.Value);
        }

        foreach (EdgeData edge in _graphData.Edges)
        {
            AddEdge(edge.From, edge.To, edge.Weight);
        }

        foreach (NodeData node in nodesCopy)
        {
            if (node == null) continue;

            foreach (NodeData neighbor in node.Connections)
            {
                if (neighbor == null)
                {
                    Debug.LogError($"NodeData not found for {node.Value}");
                    continue;
                }

                if (!_edges.ContainsKey((node.Value, neighbor.Value)))
                    AddEdge(node.Value, neighbor.Value);
            }
        }

        foreach (var node in _graphData.Nodes)
        {
            node.Connections.Clear();
            foreach (var id in node.ConnectionIDs)
            {
                var connectedNode = _graphData.Nodes.Find(n => n.Value == id);
                if (connectedNode != null)
                {
                    node.Connections.Add(connectedNode);
                }
            }
        }
        ArrangeNodesInGrid(5, 2f);
    }

    public void ArrangeNodesInGrid(int columns = 5, float spacing = 2f)
    {
        int row = 0;
        int column = 0;

        foreach (var nodeController in _nodeControllers.Values)
        {
            float xPos = column * spacing;
            float yPos = -row * spacing;

            nodeController.transform.position = new Vector3(xPos, yPos, 0);

            column++;
            if (column >= columns)
            {
                column = 0;
                row++;
            }
        }
    }



    public void AddNode(int value)
    {
        string directoryPath = "Assets/Data/Nodes";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        NodeData newNodeData = ScriptableObject.CreateInstance<NodeData>();
        newNodeData.Value = value;
        newNodeData.Connections = new List<NodeData>();

#if UNITY_EDITOR
        string assetPath = $"Assets/Data/Nodes/Node_{value}.asset";
        AssetDatabase.CreateAsset(newNodeData, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif

        _graphData.Nodes.Add(newNodeData);

        GameObject newNode = Instantiate(_node, _graphContainer);
        NodeController controller = newNode.GetComponent<NodeController>();
        controller.Setup(newNodeData);
        _nodeControllers[value] = controller;

        //newNode.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);

        controller.HideNode();
    }

    public void AddEdge(int nodeA, int nodeB, float weight = 1f)
    {
        NodeData nodeDataA = _graphData.Nodes.Find(n => n.Value == nodeA);
        NodeData nodeDataB = _graphData.Nodes.Find(n => n.Value == nodeB);

        if (nodeDataA == null)
        {
            Debug.Log($"Node {nodeA} not found. Creating...");
            AddNode(nodeA);
            nodeDataA = _graphData.Nodes.Find(n => n.Value == nodeA);
        }

        if (nodeDataB == null)
        {
            Debug.Log($"Node {nodeB} not found. Creating...");
            AddNode(nodeB);
            nodeDataB = _graphData.Nodes.Find(n => n.Value == nodeB);
        }

        if (!nodeDataA.Connections.Contains(nodeDataB))
            nodeDataA.Connections.Add(nodeDataB);
        if (!nodeDataB.Connections.Contains(nodeDataA))
            nodeDataB.Connections.Add(nodeDataA);

        bool alreadyExists = _graphData.Edges.Exists(e =>
            (e.From == nodeA && e.To == nodeB) || (e.From == nodeB && e.To == nodeA));

        if (!alreadyExists)
        {
            EdgeData newEdgeData = ScriptableObject.CreateInstance<EdgeData>();
            newEdgeData.From = nodeA;
            newEdgeData.To = nodeB;
            newEdgeData.Weight = weight;

#if UNITY_EDITOR
            string assetPath = $"Assets/Data/Edges/Edge_{nodeA}_{nodeB}.asset";
            AssetDatabase.CreateAsset(newEdgeData, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif

            _graphData.Edges.Add(newEdgeData);
        }

        if (_nodeControllers.ContainsKey(nodeA) && _nodeControllers.ContainsKey(nodeB))
        {
            GameObject edgeGO = Instantiate(_edgePrefab, transform);
            EdgeRenderer edgeRenderer = edgeGO.GetComponent<EdgeRenderer>();

            EdgeData edgeData = _graphData.Edges.Find(e =>
    (e.From == nodeA && e.To == nodeB) || (e.From == nodeB && e.To == nodeA));

            edgeRenderer.Setup(_nodeControllers[nodeA].transform, _nodeControllers[nodeB].transform, edgeData.Weight);

            _edges[(nodeA, nodeB)] = edgeRenderer;
            _edges[(nodeB, nodeA)] = edgeRenderer;

            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                edgeRenderer.HideEdge();
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                edgeRenderer.ShowEdge();
            }
        }
        else
        {
            Debug.LogError($"Error: NodeControllers not found for {nodeA} or {nodeB}");
        }
    }

    public bool TryGetEdge(int nodeA, int nodeB, out EdgeRenderer edge)
    {
        return _edges.TryGetValue((nodeA, nodeB), out edge);
    }

    public bool TryGetNodeController(int value, out NodeController controller)
    {
        return _nodeControllers.TryGetValue(value, out controller);
    }

}