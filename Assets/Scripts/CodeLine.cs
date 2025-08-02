using TMPro;
using UnityEngine;
using System;

public class CodeLine : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private GameObject platformChild;

    private float SPACE_SPACING = 60f;
    private int PADDING = 0;


    private int CountLeadingWhitespace(string text) {
        if (string.IsNullOrEmpty(text))
            return 0;

        int count = 0;
        foreach (char c in text) {
            if (c == '\t' || c == ' ')
                count++;
            else
                break; // stop counting at first non-whitespace character
        }
        return count;
    }


    public void UpdateSize() {
        BoxCollider2D boxCollider2D = platformChild.GetComponent<BoxCollider2D>();
        RectTransform rectTransform = textMeshPro.GetComponent<RectTransform>();
        textMeshPro.ForceMeshUpdate();
        float width = textMeshPro.textBounds.size.x;
        float paddedWidth = width + PADDING;

        int tabCount = CountLeadingWhitespace(textMeshPro.text);

        //Debug.Log(width);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, paddedWidth);
        boxCollider2D.size = new Vector2(paddedWidth, boxCollider2D.size.y);
        boxCollider2D.offset = new Vector2(paddedWidth / 2 + tabCount * SPACE_SPACING, boxCollider2D.offset.y);
    }



}
