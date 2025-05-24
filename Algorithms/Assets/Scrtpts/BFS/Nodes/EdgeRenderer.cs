using TMPro;
using UnityEngine;

public class EdgeRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public TMP_Text edgeWeightText;

    private Transform startTransform;
    private Transform endTransform;

    public void Setup(Transform start, Transform end, float weight)
    {
        startTransform = start;
        endTransform = end;
        edgeWeightText.text = weight.ToString();
    }

    private void LateUpdate()
    {
        if (startTransform != null && endTransform != null)
        {
            Vector3 startCenter = startTransform.GetComponent<Renderer>()?.bounds.center ?? startTransform.position;
            Vector3 endCenter = endTransform.GetComponent<Renderer>()?.bounds.center ?? endTransform.position;

            Vector3 direction = (endCenter - startCenter).normalized;

            float nodeRadius = 0.5f;

            Vector3 startPoint = startCenter + direction * nodeRadius;
            Vector3 endPoint = endCenter - direction * nodeRadius;

            Vector3 controlPoint = (startPoint + endPoint) / 2f + Vector3.up * 1f;

            int segmentCount = 20;
            lineRenderer.positionCount = segmentCount;

            for (int i = 0; i < segmentCount; i++)
            {
                float t = i / (float)(segmentCount - 1);
                Vector3 point = CalculateQuadraticBezierPoint(t, startPoint, controlPoint, endPoint);
                lineRenderer.SetPosition(i, point);
            }

            Vector3 midPoint = CalculateQuadraticBezierPoint(0.5f, startPoint, controlPoint, endPoint);
            edgeWeightText.transform.position = midPoint + Vector3.up * 0.5f;

            if (Camera.main != null)
                edgeWeightText.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);
        }
    }

    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        return uu * p0 + 2 * u * t * p1 + tt * p2;
    }


    public void ShowEdge()
    {
        lineRenderer.enabled = true;
        edgeWeightText.enabled = true;
    }

    public void HideEdge()
    {
        lineRenderer.enabled = false;
        edgeWeightText.enabled = false;
    }
}
