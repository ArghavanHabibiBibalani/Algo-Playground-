using UnityEngine;

public class EdgeRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public void Setup(Vector3 start, Vector3 end)
    {
        Debug.Log($"Edge from {start} to {end}");
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }

    public void ShowEdge()
    {
        lineRenderer.enabled = true;
    }

    public void HideEdge()
    {
        lineRenderer.enabled = false;
    }
}
