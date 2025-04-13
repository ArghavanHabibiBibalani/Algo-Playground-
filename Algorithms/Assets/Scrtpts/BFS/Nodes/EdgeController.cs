using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scrtpts.BFS.Nodes
{
    public class EdgeController : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;

        public void Setup(NodeController fromNode, NodeController toNode)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, fromNode.transform.position);
            lineRenderer.SetPosition(1, toNode.transform.position);
        }

        public void ChangeColor(Color color)
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
        }
    }
}
