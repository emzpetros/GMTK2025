using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;

[ExecuteInEditMode]

public enum Interactable { None, Boolean, Number }
public class CodeController : MonoBehaviour {
    [SerializeField] private GameObject codeLinePrefab;
    [SerializeField] private GameObject booleanCodeLinePrefab;
    [SerializeField] private GameObject numberCodeLinePrefab;

    [SerializeField] private List<string> codeLines = new List<string>{
        "while(true)", 
        "\t//Author: **** ******", 
        "\t//Date: **/**/****",
        "A Really long line of text to test the max length",
        "",
        "\t\t\t\t\t\t//Comment",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "if(false) spawn enemy"
    };

    [SerializeField] private Interactable[] interactableStatus;

    private TextMeshProUGUI[] childrenTmp;
    private int childrenCount;

    private List<GameObject> currentCodeLines = new List<GameObject>();


    private void Start() {

/*        ClearCodeLines();
        GenerateCodeLines();*/
        //UpdateCodeLines();


    }

    private void ClearCodeLines() {
        CodeLine[] childrenTmp = GetComponentsInChildren<CodeLine>();
        int childrenCount = childrenTmp.Length;

        foreach (var child in childrenTmp) {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                DestroyImmediate(child.gameObject);
            else
                Destroy(child.gameObject);
#else
            Destroy(obj);
#endif
        }
    }
  /*  public void UpdateCodeLines() {
        CodeLine[] codeLineScripts = GetComponentsInChildren<CodeLine>();
        int childrenCount = codeLineScripts.Length;

        for (int i = 0; i < childrenCount; i++) {
            CodeLine script = codeLineScripts[i];
            TextMeshProUGUI child = script.GetComponentInChildren<TextMeshProUGUI>();
            child.text = codeLines[i];
            //Debug.Log(child.gameObject.name);
            script.UpdateSize();
        }

    }
   */
    public void GenerateCodeLines() {
        ClearCodeLines() ;
        // Clear previous lines
        foreach (var obj in currentCodeLines) {
#if UNITY_EDITOR
            if (!Application.isPlaying)
                DestroyImmediate(obj);
            else
                Destroy(obj);
#else
            Destroy(obj);
#endif
        }
        currentCodeLines.Clear();

        for (int i = 0; i < codeLines.Count; i++) {
            GameObject prefabToUse;

            // Safety check if interactableStatus length matches
            if (interactableStatus != null && i < interactableStatus.Length) {
                switch (interactableStatus[i]) {
                    case Interactable.Boolean:
                        prefabToUse = booleanCodeLinePrefab;
                        break;
                    case Interactable.Number:
                        prefabToUse = numberCodeLinePrefab;
                        break;
                    case Interactable.None:
                    default:
                        prefabToUse = codeLinePrefab;
                        break;
                }
            } else {
                prefabToUse = codeLinePrefab;
            }

            GameObject lineObj = Instantiate(prefabToUse, transform);
            // Optionally set text on your prefab's TextMeshPro component if needed, e.g.:
            var tmp = lineObj.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null) tmp.text = codeLines[i];

            lineObj.GetComponentInChildren<CodeLine>().UpdateSize();
            currentCodeLines.Add(lineObj);
        }
    }


}
