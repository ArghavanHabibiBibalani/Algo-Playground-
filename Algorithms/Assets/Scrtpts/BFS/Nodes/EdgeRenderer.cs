using UnityEngine;

public class EdgeRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;

    private Transform startTransform;
    private Transform endTransform;

    public void Setup(Transform start, Transform end)
    {
        startTransform = start;
        endTransform = end;
    }

    private void LateUpdate()
    {
        if (startTransform != null && endTransform != null)
        {
            lineRenderer.SetPosition(0, startTransform.position);
            lineRenderer.SetPosition(1, endTransform.position);
        }
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
