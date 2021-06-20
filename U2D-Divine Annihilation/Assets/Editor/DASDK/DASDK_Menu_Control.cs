using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DA_Menu_Control))]
public class DASDK_Menu_Control : Editor
{
    string[] _choices = new [] { "Scroll Sprites", "Scroll Strings", "Menu Control"};
    int _choiceIndex;
    [HideInInspector] public string playerName;
    [HideInInspector] public GameObject player;

    SerializedProperty testingString;
    void OnEnable()
    {
        testingString = serializedObject.FindProperty("testingString");
    }

    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        DA_Menu_Control menuControl = (DA_Menu_Control)target; // Set the current script to the target class
        _choiceIndex = EditorGUILayout.Popup("Player", _choiceIndex, _choices);

        if (_choiceIndex == 0)
        {
            EditorGUILayout.LabelField("Tab A");
            serializedObject.Update(); 
            serializedObject.ApplyModifiedProperties();
        }

        if (_choiceIndex == 1)
        {
            EditorGUILayout.LabelField("Tab B");
        }

        if (_choiceIndex == 2)
        {
            EditorGUILayout.LabelField("Tab C");
        }

        playerName = _choices[_choiceIndex];
        player = GameObject.Find(playerName);
    }
}
