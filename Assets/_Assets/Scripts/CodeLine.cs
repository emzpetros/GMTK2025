using TMPro;
using UnityEngine;

public class CodeLine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private GameObject platformChild;

    private float TAB_SPACING = 280f;
    private int PADDING = 0;

    private void Start() {
        BoxCollider2D boxCollider2D = platformChild.GetComponent<BoxCollider2D>();
        RectTransform rectTransform = textMeshPro.GetComponent<RectTransform>();
        textMeshPro.ForceMeshUpdate();
        float width = textMeshPro.textBounds.size.x;
        float paddedWidth = width + PADDING;

        int tabCount = CountTabs(textMeshPro.text);
        
        //Debug.Log(width);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, paddedWidth);
        boxCollider2D.size = new Vector2(paddedWidth, boxCollider2D.size.y);
        boxCollider2D.offset = new Vector2(paddedWidth / 2 + tabCount * TAB_SPACING, boxCollider2D.offset.y );

    }
    private int CountTabs(string text) {
        if (string.IsNullOrEmpty(text))
            return 0;

        int count = 0;
        foreach (char c in text) {
            if (c == '\t')
                count++;
        }
        return count;
    }


}
