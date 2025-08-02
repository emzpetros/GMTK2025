using UnityEngine;
using System;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    //[SerializeField] private GameObject blockSLot;
/*    public class OnSuccessEventArgsBool : EventArgs {
        public bool value;
    }

    public class OnSuccessEventArgsNum : EventArgs {
        public int value;
    }*/
    private CodeLineInteractable script;
    public static SceneController Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        //script = blockSLot.GetComponent<CodeLineInteractable>();  
    }
    public void CodeLine_OnSuccess(bool value) {

        SetSpawner(value);
    }

    public void ChangeSpawnInterval(float interval) {
        spawner.GetComponent<Spawner>().SetTimer(interval);
        Debug.Log(interval);
    }
    private void CodeLine_OnSucess(object sender, EventArgs e) {
        CodeLineInteractable codeLine = (CodeLineInteractable) sender;

        SetSpawner(codeLine.GetCurrentValue());
    }

    /*    private void CodeLine_OnSucess(object sender, EventArgs e) {
            Debug.Log("interacted with line");

            if (e is OnSuccessEventArgsBool boolArgs) {
                SetSpawner(boolArgs.value);
            } else if (e is OnSuccessEventArgsNum numArgs) {
                // Handle number version here if needed
                Debug.LogError("Wrong type, should never happen");
            }
        }
    */
    /*    private void CodeLine_OnSucess(object sender, EventArgs e) {
            Debug.Log("interacted with line");
            OnSuccessEventArgsBool args = e as OnSuccessEventArgsBool;
            SetSpawner(args.value);
        }
    */
    public void SetSpawner(bool state) {
        spawner.SetActive(state);
    }


}
