#if UNITY_EDITOR

using Wata.CSVData;
using Wata.CSVData.Extensions;
using UnityEngine;
using UnityEditor;

namespace Wata.CSVData {
    [CustomEditor(typeof(CodeGenerateLoaderBase), true)]
    public class CodeGenerateLoaderEditor: Editor {
     
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
 
            if (GUILayout.Button("Generate")) {
                (target as CodeGenerateLoaderBase)!.Generate();
             
                AssetDatabase.Refresh();
                Debug.Log("Complete Making Code"
                    .SetColor(Color.red)
                    .SetFontSizeByPercent(1.5f)
                );
            }
        }
    }   
}

#endif