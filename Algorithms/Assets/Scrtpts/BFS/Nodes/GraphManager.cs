using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.IO;


public class GraphManager : MonoBehaviour
{
    [SerializeField] private GraphData _graphData; 
    [SerializeField] private GameObject _node;  
    [SerializeField] private Transform _graphContainer; 
    [SerializeField] private GameObject _edgePrefab;  

    private Dictionary<int, NodeController> _nodeControllers = new();
    private Dictionary<(int, int), EdgeRenderer> _edges = new();

    public static GraphManager Instance { get; private set; }


    private void Awake()
    {
        if (_graphData.Nodes == null)
            _graphData.Nodes = new List<NodeData>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if (_graphData.Nodes == null)
            _graphData.Nodes = new List<NodeData>();
    }

    public void GenerateGraph()
    {
        foreach (NodeData node in _graphData.Nodes)
        {
            AddNode(node.Value);  
        }

        foreach (NodeData node in _graphData.Nodes)
        {
            foreach (NodeData neighbor in node.Connections)
            {
                AddEdge(node.Value, neighbor.Value);  
            }
        }
    }

    public void AddNode(int value)
    {
        string directoryPath = "Assets/GraphNodes";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        NodeData newNodeData = ScriptableObject.CreateInstance<NodeData>();
        newNodeData.Value = value;
        newNodeData.Connections = new List<NodeData>();

        string assetPath = $"Assets/GraphNodes/Node_{value}.asset";
        #if UNITY_EDITOR
        AssetDatabase.CreateAsset(newNodeData, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        #endif

        _graphData.Nodes.Add(newNodeData);

        GameObject newNode = Instantiate(_node, _graphContainer);
        NodeController controller = newNode.GetComponent<NodeController>();
        controller.Setup(newNodeData);
        _nodeControllers[value] = controller;

        newNode.transform.position = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);

        controller.HideNode();
    }

    public void AddEdge(int nodeA, int nodeB)
    {
        NodeData nodeDataA = _graphData.Nodes.Find(n => n.Value == nodeA);
        NodeData nodeDataB = _graphData.Nodes.Find(n => n.Value == nodeB);

        if (nodeDataA == null || nodeDataB == null)
        {
            Debug.LogError($" Error: NodeData not found for {nodeA} or {nodeB}");
            return;
        }

        nodeDataA.Connections.Add(nodeDataB);
        nodeDataB.Connections.Add(nodeDataA);

        if (_nodeControllers.ContainsKey(nodeA) && _nodeControllers.ContainsKey(nodeB))
        {
            GameObject newEdge = Instantiate(_edgePrefab, _graphContainer);
            EdgeRenderer edgeRenderer = newEdge.GetComponent<EdgeRenderer>();
            edgeRenderer.Setup(_nodeControllers[nodeA].transform.position, _nodeControllers[nodeB].transform.position);

            _edges[(nodeA, nodeB)] = edgeRenderer;
            _edges[(nodeB, nodeA)] = edgeRenderer;
        }
        else
        {
            Debug.LogError($" Error: NodeControllers not found for {nodeA} or {nodeB}");
        }
    }
    private void OnDisable()
    {
        if (!Application.isPlaying)
        {
            _graphData.Nodes.Clear();

            string[] nodeFiles = Directory.GetFiles("Assets/GraphNodes", "*.asset");
            foreach (string file in nodeFiles)
            {
                AssetDatabase.DeleteAsset(file);
            }

            #if UNITY_EDITOR
            EditorUtility.SetDirty(_graphData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            #endif
        }
    }

    public bool TryGetEdge(int nodeA, int nodeB, out EdgeRenderer edge)
    {
        return _edges.TryGetValue((nodeA, nodeB), out edge);
    }


}
