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
}
