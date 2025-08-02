using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;

[ExecuteInEditMode]
public class CodeController : MonoBehaviour {
    [SerializeField] private GameObject codeLinePrefab;
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

    private TextMeshProUGUI[] childrenTmp;
    private int childrenCount;


    private void Start() {
        /*foreach (Transform child in this.GetComponentInChildren<Transform>()) {
                Destroy(child.gameObject);

        }*/
        TextMeshProUGUI[] childrenTmp = GetComponentsInChildren<TextMeshProUGUI>();
        int childrenCount = childrenTmp.Length;

        UpdateCodeLines();

/*        foreach (var line in codeLines) {
            GameObject child = Instantiate(codeLinePrefab, this.transform);
            //Debug.Log(line);
            child.GetComponentInChildren<TextMeshProUGUI>().text = line;

        }*/
    }

    public void UpdateCodeLines() {

        //  TextMeshProUGUI[] childrenTmp = GetComponentsInChildren<TextMeshProUGUI>();
        //int childrenCount = childrenTmp.Length;


        //Debug.LogError("For " + codeLines.Count + " code lines we have " + childrenCount + " child objects");
        TextMeshProUGUI[] childrenTmp = GetComponentsInChildren<TextMeshProUGUI>();
        int childrenCount = childrenTmp.Length;

        for (int i = 0; i < childrenCount; i++) {
            TextMeshProUGUI child = childrenTmp[i];
            child.text = codeLines[i];
            //Debug.Log(child.gameObject.name);
            child.gameObject.GetComponentInParent<CodeLine>().UpdateSize();
        }
        //Debug.Log("udpates");
    }
/*    public void GenerateCodeLines() {
        //TextMeshProUGUI[] childrenTmp = GetComponentsInChildren<TextMeshProUGUI>();
        //int childrenCount = childrenTmp.Length;
        for (int i = 0; i < codeLines.Count - childrenCount; i++) {
            GameObject codeLineObject = Instantiate(codeLinePrefab, this.transform);

        }

    }*/

    public void GenerateCodeLines() {
        // 1. Find all existing code line children
        List<GameObject> codeLineChildren = new List<GameObject>();
        foreach (Transform child in transform)
            codeLineChildren.Add(child.gameObject);

        // 2. Add if not enough
        while (codeLineChildren.Count < codeLines.Count) {
            var codeLineObj = Instantiate(codeLinePrefab, transform);
            codeLineChildren.Add(codeLineObj);
        }

        // 3. Remove extra
        while (codeLineChildren.Count > codeLines.Count) {
            var toRemove = codeLineChildren[codeLineChildren.Count - 1];
#if UNITY_EDITOR
            if (!Application.isPlaying)
                DestroyImmediate(toRemove);
            else
                Destroy(toRemove);
#else
            Destroy(toRemove);
#endif
            codeLineChildren.RemoveAt(codeLineChildren.Count - 1);
        }
    }

}
