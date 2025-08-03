using UnityEngine;
using System;
using TMPro;
using UnityEngine.Events;

public class BooleanCodeLineInteracable : MonoBehaviour, IInteractableCodeLine {

    [SerializeField] private GameObject spawnBlockTrue;
    [SerializeField] private GameObject spawnPrefabFalse;
    [SerializeField] private bool currentValue;

    public UnityEvent<Boolean> OnSuccessEvent;

    private BlockType validType = BlockType.Boolean;

    private TextMeshProUGUI textMeshPro;

    private void Start() {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }
    public bool TrySetText(IBlock block) {
        bool valid = block.GetBlockType() == validType;

        if (valid) {

            textMeshPro.text = block.GetValue().ToString().ToLower();


            bool value = (bool)block.GetValue();

            if (currentValue) {

                Instantiate(spawnBlockTrue, this.transform.position + new Vector3(-2.5f, 0.5f, 0f), Quaternion.identity);
            }
            else {
                Instantiate(spawnPrefabFalse, this.transform.position + new Vector3(-2.5f, 0.5f, 0f), Quaternion.identity);
            }

            currentValue = value;
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
