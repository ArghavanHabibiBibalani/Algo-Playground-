using UnityEngine;

public class MatchingProblemGraphVisualizer : MonoBehaviour
{
    public GameObject nodePrefab; 
    public GameObject edgePrefab;

    public BipartiteGraphData graphData; 

    void Start()
    {
        VisualizeGraph();
    }

    void VisualizeGraph()
    {
        Vector3[] positionsA = GetPositions(graphData.groupA.Count, new Vector3(-5, 1.5f, 0));
        Vector3[] positionsB = GetPositions(graphData.groupB.Count, new Vector3(5, 2.5f, 0));

        for (int i = 0; i < graphData.groupA.Count; i++)
        {
            CreateNode(positionsA[i], $"A{graphData.groupA[i]}");
        }

        for (int i = 0; i < graphData.groupB.Count; i++)
        {
            CreateNode(positionsB[i], $"B{graphData.groupB[i]}");
        }

        foreach (var edge in graphData.edges)
        {
            Vector3 fromPosition = positionsA[edge.Origin];
            Vector3 toPosition = positionsB[edge.Destination];
            DrawEdge(fromPosition, toPosition);
        }
    }

    Vector3[] GetPositions(int count, Vector3 startPosition)
    {
        Vector3[] positions = new Vector3[count];
        float spacing = 2.0f;  

        for (int i = 0; i < count; i++)
        {
            float yOffset = -i * spacing;
            positions[i] = new Vector3(startPosition.x, startPosition.y + yOffset, startPosition.z);
        
        }

        return positions;
    }

    void CreateNode(Vector3 position, string label)
    {
        GameObject node = Instantiate(nodePrefab, position, Quaternion.identity);
        node.name = label; 
    }

    void DrawEdge(Vector3 fromPosition, Vector3 toPosition)
    {
        GameObject edge = Instantiate(edgePrefab, Vector3.zero, Quaternion.identity);
        LineRenderer lineRenderer = edge.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, fromPosition);  
        lineRenderer.SetPosition(1, toPosition); 
    }
}
