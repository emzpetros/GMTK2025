using UnityEngine;
using System;
using TMPro;
using UnityEngine.Events;

public class NumberCodeLineInteracable: MonoBehaviour
{
    private bool currentValue = false;

    public UnityEvent<Boolean> OnSuccessEvent;

     private BlockType validType = BlockType.Boolean;

    private TextMeshProUGUI textMeshPro;

    private void Start() {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }
    public bool TrySetText(IBlock block) {
        bool valid = block.GetBlockType() == validType;

        if (valid) {

            textMeshPro.text = block.GetValue().ToString();


            bool value = (bool)block.GetValue();
            OnSuccessEvent?.Invoke(value);
        }
        return valid;

  }




  //OnSuccess?.Invoke(this, EventArgs.Empty);


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

    
    public bool GetCurrentValue() {
        return currentValue;
    }
}
