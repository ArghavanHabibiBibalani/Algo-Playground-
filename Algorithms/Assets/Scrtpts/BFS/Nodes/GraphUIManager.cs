using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
        if (value == 0)
            algorithmText.text = "DFS Algorithm";
        else if (value == 1)
            algorithmText.text = "BFS Algorithm";
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
        if (edgeData.Length == 2 && int.TryParse(edgeData[0], out int nodeA) && int.TryParse(edgeData[1], out int nodeB))
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
}
