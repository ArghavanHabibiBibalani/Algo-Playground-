using UnityEngine;

public class EdgeRenderer : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    public void Setup(Vector3 start, Vector3 end)
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
    public void HideEdge()
    {
        lineRenderer.enabled = false; 
    }

    public void ShowEdge()
    {
        lineRenderer.enabled = true; 
    }

}
