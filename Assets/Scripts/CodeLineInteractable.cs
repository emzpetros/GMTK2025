using UnityEngine;
using System;
using TMPro;

public class CodeLineInteractable : MonoBehaviour
{
    public EventHandler OnSuccess;
    private bool currentValue = false;

  

    [SerializeField] private BlockType validType;

    private TextMeshProUGUI textMeshPro;

    private void Start() {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }
    public bool TrySetText(IBlock block) {
        bool valid = block.GetBlockType() == validType;

        if (valid) {

            textMeshPro.text = block.GetValue().ToString();
            currentValue = (bool)block.GetValue();

            OnSuccess?.Invoke(this, EventArgs.Empty);

            /*if(block.GetBlockType() == BlockType.Boolean ) {
                bool value = (bool)block.GetValue();

                OnSuccess?.Invoke(this, new SceneController.OnSuccessEventArgsBool {
                    value = value
                }) ;
            } else if(block.GetBlockType() == BlockType.Number ) {
                int value = (int)block.GetValue();


                OnSuccess?.Invoke(this, new SceneController.OnSuccessEventArgsNum {
                    value = value
                });
            }
*/

        }
        return valid;
    }

    public bool GetCurrentValue() {
        return currentValue;
    }
}
