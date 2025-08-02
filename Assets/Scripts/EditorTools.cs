#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CodeController))]
public class EditorTools : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        CodeController controller = (CodeController)target;

   /*     if (GUILayout.Button("Spawn Code Prefabs")) {
            controller.GenerateCodeLines();
        }*/
/*
        if (GUILayout.Button("Update Code Lines")) {
            controller.UpdateCodeLines();
        }
*/

    }


}
#endif
