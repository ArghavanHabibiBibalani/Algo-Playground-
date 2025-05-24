using Assets.Scrtpts.BFS.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Assets.Scrtpts.BFS.XML
{
    public class GraphXMLLoader : MonoBehaviour
    {
        [SerializeField] private TextAsset xmlFile;
        [SerializeField] private GraphData graphData;
        [SerializeField] private GraphUIManager graphUIManager;

#if UNITY_EDITOR
        private const string nodeBasePath = "Assets/Data/Nodes";
        private const string edgeBasePath = "Assets/Data/Edges";
#endif

        private void Awake()
        {
            LoadFromXML();
        }

        private void LoadFromXML()
        {
            XmlGraphContainer container = DeserializeXML(xmlFile.text);

            graphData.Nodes.Clear();
            graphData.Edges.Clear();

            Dictionary<int, NodeData> nodeLookup = new Dictionary<int, NodeData>();

#if UNITY_EDITOR
            if (!Directory.Exists(nodeBasePath))
            {
                Directory.CreateDirectory(nodeBasePath);
                AssetDatabase.Refresh();
            }

            if (!Directory.Exists(edgeBasePath))
            {
                Directory.CreateDirectory(edgeBasePath);
                AssetDatabase.Refresh();
            }
#endif

            foreach (var nodeXml in container.Nodes)
            {
                int parsedValue = int.Parse(nodeXml.Value);
                NodeData node = null;

#if UNITY_EDITOR
                string nodePath = $"{nodeBasePath}/Node_{parsedValue}.asset";
                node = AssetDatabase.LoadAssetAtPath<NodeData>(nodePath);

                if (node == null)
                {
                    node = ScriptableObject.CreateInstance<NodeData>();
                    node.Value = parsedValue;
                    AssetDatabase.CreateAsset(node, nodePath);
                }
#else
                node = ScriptableObject.CreateInstance<NodeData>();
                node.Value = parsedValue;
#endif

                node.ConnectionIDs.Clear();
                node.Connections.Clear();

                nodeLookup[node.Value] = node;
                graphData.Nodes.Add(node);
            }

            HashSet<string> createdEdges = new HashSet<string>();

            foreach (var nodeXml in container.Nodes)
            {
                int nodeValue = int.Parse(nodeXml.Value);
                NodeData node = nodeLookup[nodeValue];

                foreach (var conn in nodeXml.Connections)
                {
                    string[] parts = conn.Split(',');
                    int connValue = int.Parse(parts[0]);

                    float weight = 1f;
                    if (parts.Length > 1)
                        float.TryParse(parts[1], out weight);

                    if (!node.ConnectionIDs.Contains(connValue))
                        node.ConnectionIDs.Add(connValue);

                    string edgeKey = nodeValue < connValue ? $"{nodeValue}-{connValue}" : $"{connValue}-{nodeValue}";

                    if (createdEdges.Contains(edgeKey))
                        continue;

                    createdEdges.Add(edgeKey);

                    EdgeData edge = null;

#if UNITY_EDITOR
                    string edgePath = $"{edgeBasePath}/Edge_{edgeKey}.asset";
                    edge = AssetDatabase.LoadAssetAtPath<EdgeData>(edgePath);

                    if (edge == null)
                    {
                        edge = ScriptableObject.CreateInstance<EdgeData>();
                        edge.From = nodeValue;
                        edge.To = connValue;
                        edge.Weight = weight; 
                        AssetDatabase.CreateAsset(edge, edgePath);
                    }
#else
                    edge = ScriptableObject.CreateInstance<EdgeData>();
                    edge.From = nodeValue;
                    edge.To = connValue;
                    edge.Weight = weight;
#endif

                    graphData.Edges.Add(edge);
                }
            }

            foreach (var node in graphData.Nodes)
            {
                node.Connections.Clear();
                foreach (var id in node.ConnectionIDs)
                {
                    if (nodeLookup.TryGetValue(id, out var target))
                        node.Connections.Add(target);
                }
            }

#if UNITY_EDITOR
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif

            graphUIManager.CreateGraph(graphData);
        }

        private XmlGraphContainer DeserializeXML(string xmlContent)
        {
            XmlSerializer serializer = new(typeof(XmlGraphContainer));
            using StringReader reader = new(xmlContent);
            return serializer.Deserialize(reader) as XmlGraphContainer;
        }
    }
}
