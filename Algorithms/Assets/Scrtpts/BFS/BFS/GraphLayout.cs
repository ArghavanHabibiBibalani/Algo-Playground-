using System.Collections.Generic;
using UnityEngine;

public class GraphLayout : MonoBehaviour
{
    [SerializeField] private float radius = 5f;

    public void ArrangeCircular(List<NodeController> nodes)
    {
        int count = nodes.Count;
        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            nodes[i].transform.position = pos;
        }
    }
}
