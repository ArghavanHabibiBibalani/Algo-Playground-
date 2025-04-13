using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NodeController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _nodeText;

    [SerializeField]
    private SpriteRenderer _nodeSprite;

    private NodeData _nodeData;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //public void CreateEdgeTo(NodeData toNode)
    //{
    //    GraphManager.CreateEdge(nodeData, toNode);
    //}

    public void Setup(NodeData nodeData)
    {
        _nodeData = nodeData;
        _nodeText.text = _nodeData.Value.ToString();
        _nodeText.transform.SetParent(transform);
        _nodeText.transform.localPosition = Vector3.zero;
        _nodeText.alignment = TextAlignmentOptions.Center;
    }

    public void ChangeColor(Color color)
    {
        _nodeSprite.color = color;
    }

    public void HideNode()
    {
        _nodeSprite.enabled = false;
        _nodeText.enabled = false;

        if (GraphManager.Instance != null)
        {
            foreach (var connection in _nodeData.Connections)
            {
                if (GraphManager.Instance.TryGetEdge(_nodeData.Value, connection.Value, out EdgeRenderer edge))
                {
                    edge.HideEdge();
                }
            }
        }
    }

    public void ShowNode()
    {
        _nodeSprite.enabled = true;
        _nodeText.enabled = true;

        if (GraphManager.Instance != null)
        {
            foreach (var connection in _nodeData.Connections)
            {
                if (GraphManager.Instance.TryGetEdge(_nodeData.Value, connection.Value, out EdgeRenderer edge))
                {
                    edge.ShowEdge();
                }
            }
        }
    }
}
