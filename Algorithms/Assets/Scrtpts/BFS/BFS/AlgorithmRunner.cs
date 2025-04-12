using Assets.Scrtpts.BFS.BFS;
using UnityEngine;

public class AlgorithmRunner : MonoBehaviour
{
    private IGraphAlgorithm algorithm = null;

    [SerializeField] private AlgorithmExecutor algorithmExecutor;

    void Start()
    {
        SelectAlgorithm(SelectedAlgorithmData.SelectedAlgorithm);

        if (algorithm != null)
        {
            algorithmExecutor.RunAlgorithm(algorithm);
        }
        else
        {
            Debug.LogWarning("Algorithm is null, nothing to run.");
        }
    }

    void SelectAlgorithm(string name)
    {
        switch (name)
        {
            case "BFS Algorithm":
                RunBFS();
                break;
            case "DFS Algorithm":
                RunDFS();
                break;
            default:
                Debug.LogWarning($"Unknown algorithm selected: {name}");
                break;
        }
    }

    void RunBFS()
    {
        Debug.Log("Running BFS...");
        algorithm = new BFSAlgorithm();
    }

    void RunDFS()
    {
        Debug.Log("Running DFS...");
        algorithm = new DFSAlgorithm();
    }
}
