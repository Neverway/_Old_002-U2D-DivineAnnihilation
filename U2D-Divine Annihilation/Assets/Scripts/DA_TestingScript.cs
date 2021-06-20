using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class DA_TestingScript : MonoBehaviour
{
    enum ItemType { TabA, TabB, TabC }
    [SerializeField] ItemType currentMenu;    // Setup Custom Inspector

    bool showTabA;
    bool showTabB;
    bool showTabC;

    public PublicVars publicVars;

    public class PublicVars
    {
        public string[] testingString = new string[0];
    }

    #region Editor
    #if UNITY_EDITOR

    [CustomEditor(typeof(DA_TestingScript))]
    public class DA_TestingScriptEditor : Editor
    {
        SerializedProperty testingString;
        void OnEnable()
        {
            testingString = serializedObject.FindProperty("testingString");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DA_TestingScript menuControl = (DA_TestingScript)target; // Set the current script to the target class

            if (menuControl.currentMenu == ItemType.TabA)
            {
                EditorGUILayout.LabelField("Tab A");
                //serializedObject.Update();
                //EditorGUILayout.PropertyField(testingString);
                //serializedObject.ApplyModifiedProperties();
            }

            if (menuControl.currentMenu == ItemType.TabB)
            {
                EditorGUILayout.LabelField("Tab B");
            }

            if (menuControl.currentMenu == ItemType.TabC)
            {
                EditorGUILayout.LabelField("Tab C");
            }
        }
    }

    #endif
    #endregion
}