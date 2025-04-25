using UnityEngine;

namespace Assets.Scrtpts.BFS.Nodes
{
    [CreateAssetMenu(fileName = "NewEdgeData", menuName = "Graph/EdgeData")]
    public class EdgeData : ScriptableObject
    {
        public int From;
        public int To;

        public EdgeData() { }

        public EdgeData(int from, int to)
        {
            From = from;
            To = to;
        }
    }
}
