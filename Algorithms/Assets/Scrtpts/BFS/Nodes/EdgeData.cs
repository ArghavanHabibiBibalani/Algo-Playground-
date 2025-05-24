using UnityEngine;

namespace Assets.Scrtpts.BFS.Nodes
{
    [CreateAssetMenu(fileName = "NewEdgeData", menuName = "Graph/EdgeData")]
    public class EdgeData : ScriptableObject
    {
        public int From;
        public int To;
        public float Weight = 1f;

        public EdgeData() { }

        public EdgeData(int from, int to, float weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }
    }
}
