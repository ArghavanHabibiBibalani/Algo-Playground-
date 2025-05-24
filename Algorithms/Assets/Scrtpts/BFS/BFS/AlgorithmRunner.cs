using Assets.Scrtpts.BFS.BFS;
using UnityEngine;

public class AlgorithmRunner : MonoBehaviour
{
    private IGraphAlgorithm algorithm = null;

    [SerializeField] private AlgorithmExecutor algorithmExecutor;

    private void Awake()
    {
        //if (FindObjectOfType<AlgorithmRunner>() != this)
        //{
        //    Destroy(gameObject);
        //}
        //else
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
    }
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
            case "Dijkstra Algorithm":
                RunDijkstra();
                break;
            case "Floyd-Warshall Algorithm":
                RunFloydWarshall();
                break;
            case "Bellman-Ford Algorithm":
                RunBellmanFord();
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

    void RunDijkstra()
    {
        Debug.Log("Running Dijkstra...");
        algorithm = new DijkstraAlgorithm();
    }

    void RunFloydWarshall()
    {
        Debug.Log("Running Floyd-Warshal...");
        algorithm = new FloydWarshallAlgorithm();

    }

    void RunBellmanFord()
    {
        Debug.Log("Running Bellman-Ford...");
        algorithm = new BellmanFordAlgorithm();

    }

}
