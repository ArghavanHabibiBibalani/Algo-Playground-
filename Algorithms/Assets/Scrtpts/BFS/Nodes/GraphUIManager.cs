using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Assets.Scrtpts.BFS.Nodes;
using System.Collections.Generic;

public class GraphUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Dropdown algorithmDropdown;  
    [SerializeField] private TMP_Text algorithmText;
    [SerializeField] private TMP_InputField nodeInputField;
    [SerializeField] private TMP_InputField edgeInputField;
    [SerializeField] private Button addNodeButton;
    [SerializeField] private Button addEdgeButton;
    [SerializeField] private Button executeAlgorithmButton;

    [Header("Graph Manager")]
    [SerializeField] private GraphManager graphManager;

    private void Start()
    {
        OnAlgorithmDropdownChanged(0);
        algorithmDropdown.onValueChanged.AddListener(OnAlgorithmDropdownChanged);  
        addNodeButton.onClick.AddListener(OnAddNodeClicked);
        addEdgeButton.onClick.AddListener(OnAddEdgeClicked);
        executeAlgorithmButton.onClick.AddListener(OnExecuteAlgorithmClicked);
    }

    private void OnAlgorithmDropdownChanged(int value) 
    {
        switch (value)
        {
            case 0:
                algorithmText.text = "DFS Algorithm";
                break;
            case 1:
                algorithmText.text = "BFS Algorithm";
                break;
            case 2:
                algorithmText.text = "Dijkstra Algorithm";
                break;
            case 3:
                algorithmText.text = "Bellman-Ford Algorithm";
                break;
            case 4:
                algorithmText.text = "Floyd-Warshall Algorithm";
                break;
            default:
                algorithmText.text = "DFS Algorithm";
                break;
        }
    }

    private void OnAddNodeClicked()
    {
        if (int.TryParse(nodeInputField.text, out int nodeValue))
        {
            graphManager.AddNode(nodeValue);
            nodeInputField.text = "";
        }
        else
        {
            Debug.LogError("Invalid node value.");
        }
    }

    private void OnAddEdgeClicked()
    {
        string[] edgeData = edgeInputField.text.Split(',');

        if (edgeData.Length == 2 &&
            int.TryParse(edgeData[0].Trim(), out int nodeA) &&
            int.TryParse(edgeData[1].Trim(), out int nodeB))
        {
            graphManager.AddEdge(nodeA, nodeB);
            edgeInputField.text = "";
        }
        else
        {
            Debug.LogError("Invalid edge format. Please use 'nodeA,nodeB'.");
        }
    }

    private void OnExecuteAlgorithmClicked()
    {
        Debug.Log("Number of edges: " + GraphManager.Instance._edges.Count);

        string selectedAlgorithm = algorithmText.text;
        SelectedAlgorithmData.SelectedAlgorithm = selectedAlgorithm;

        SceneManager.LoadScene(1);

    }

    public void CreateGraph(GraphData graphData)
    {
        var nodeCopy = new List<NodeData>(graphData.Nodes);
        var edgeCopy = new List<EdgeData>(graphData.Edges);

        foreach (var nodeData in nodeCopy)
        {
            graphManager.AddNode(nodeData.Value);
        }

        foreach (var edgeData in edgeCopy)
        {
            graphManager.AddEdge(edgeData.From, edgeData.To);
        }
    }
}
