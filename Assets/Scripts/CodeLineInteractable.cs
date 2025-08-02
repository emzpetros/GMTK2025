using UnityEngine;
using System;
using TMPro;

public class CodeLineInteractable : MonoBehaviour
{
    [SerializeField] private BlockType validType;

    private TextMeshProUGUI textMeshPro;

    private void Start() {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }
    public bool TrySetText(IBlock block) {
        bool valid = block.GetBlockType() == validType; 

        if(valid) {
            textMeshPro.text = block.GetValue().ToString();
        }

        return valid;
    }
}
