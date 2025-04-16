using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scrtpts.BFS.Nodes
{
    using System.Collections.Generic;
    using UnityEngine;

    public class GraphSession : MonoBehaviour
    {
        public static GraphSession Instance;

        public List<NodeData> TempNodes;
        public List<EdgeData> TempEdges;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }

        public void SaveGraph(GraphData graphData)
        {
            TempNodes = new List<NodeData>(graphData.Nodes);
            TempEdges = new List<EdgeData>(graphData.Edges);
        }

        public void LoadGraph(GraphData graphData)
        {
            if (TempNodes == null || TempEdges == null)
            {
                Debug.LogError("No graph data in session to load!");
                return;
            }

            graphData.Nodes = new List<NodeData>(TempNodes);
            graphData.Edges = new List<EdgeData>(TempEdges);
        }
    }
}
