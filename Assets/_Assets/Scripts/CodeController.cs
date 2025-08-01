using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class CodeController : MonoBehaviour {
    [SerializeField] private GameObject codeLinePrefab;
    private List<string> codeLines = new List<string>{
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

    
    private void Start() {
        foreach (Transform child in this.GetComponentInChildren<Transform>()) {
                Destroy(child.gameObject);

        }
        foreach (var line in codeLines) {
            GameObject child = Instantiate(codeLinePrefab, this.transform);
            //Debug.Log(line);
            child.GetComponentInChildren<TextMeshProUGUI>().text = line;

        }
    }
}
