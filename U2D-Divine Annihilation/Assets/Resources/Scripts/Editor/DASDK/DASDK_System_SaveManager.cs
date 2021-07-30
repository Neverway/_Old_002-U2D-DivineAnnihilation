//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Handel the save data for the game
// Applied to: The config object in a scene
// Parent script: OTU_System_SaveManager
//
//=============================================================================

using UnityEngine;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;

[CustomEditor(typeof(OTU_System_SaveManager))]
public class DASDK_System_SaveManager : Editor
{
    // Setup dropdown array
    string[] _choices = new[] { "Player", "Inventory", "Party", "Level Data", "Script Values" };
    int _choiceIndex;
    [HideInInspector] public string playerName;
    [HideInInspector] public GameObject player;

    // Variables Inventory
    SerializedProperty items;
    SerializedProperty equipment;
    SerializedProperty itemIcons;
    SerializedProperty equipmentIcons;

    // Variables Party
    SerializedProperty partyMembers;
    SerializedProperty partyMembersHealth;

    // Variables Level Data
    SerializedProperty Chapter;

    // Variables Script Values
    SerializedProperty startingScene;
    SerializedProperty noPortrait;


    // Find parent script variables
    void OnEnable()
    {
        // Find Inventory variables
        items = serializedObject.FindProperty("activeSave2.items");
        equipment = serializedObject.FindProperty("activeSave2.equipment");
        itemIcons = serializedObject.FindProperty("activeSave2.itemIcons");
        equipmentIcons = serializedObject.FindProperty("activeSave2.equipmentIcons");

        // Find Party variables
        partyMembers = serializedObject.FindProperty("activeSave2.partyMembers");
        partyMembersHealth = serializedObject.FindProperty("activeSave2.partyMembersHealth");

        // Find Level Data variables
        Chapter = serializedObject.FindProperty("activeSave2.Chapter");

        // Find Script Value variables
        startingScene = serializedObject.FindProperty("startingScene");
        noPortrait = serializedObject.FindProperty("noPortrait");
    }


    // Draw the custom editor
    public override void OnInspectorGUI()
    {
        OTU_System_SaveManager saveManager = (OTU_System_SaveManager)target;                          // Set the current script to the target class
        _choiceIndex = GUILayout.Toolbar(_choiceIndex, _choices); // Draw the dropdown button

        // Player
        if (_choiceIndex == 0)
        {
            EditorGUILayout.HelpBox("", MessageType.None);        // Description

            // Draw and updated serialized variables
            serializedObject.Update();
            saveManager.activeSave2.saveProfileName = EditorGUILayout.TextField("saveProfileName", saveManager.activeSave2.saveProfileName);
            saveManager.activeSave2.scene = EditorGUILayout.TextField("scene", saveManager.activeSave2.scene);
            saveManager.activeSave2.playerName = EditorGUILayout.TextField("playerName", saveManager.activeSave2.playerName);
            saveManager.activeSave2.playerSavePosition = EditorGUILayout.Vector2Field("playerSavePosition", saveManager.activeSave2.playerSavePosition);
            saveManager.activeSave2.playerHealth = EditorGUILayout.FloatField("playerHealth", saveManager.activeSave2.playerHealth);
            saveManager.activeSave2.playerLevel = EditorGUILayout.IntField("playerLevel", saveManager.activeSave2.playerLevel);
            serializedObject.ApplyModifiedProperties();
        }

        // Inventory
        if (_choiceIndex == 1)
        {
            EditorGUILayout.HelpBox("", MessageType.None);        // Description

            // Draw and updated serialized variables
            serializedObject.Update();
            EditorGUILayout.PropertyField(items);
            EditorGUILayout.PropertyField(equipment);
            EditorGUILayout.PropertyField(itemIcons);
            EditorGUILayout.PropertyField(equipmentIcons);
            serializedObject.ApplyModifiedProperties();
        }

        // Party
        if (_choiceIndex == 2)
        {
            EditorGUILayout.HelpBox("", MessageType.None);        // Description

            // Draw and updated serialized variables
            serializedObject.Update();
            EditorGUILayout.PropertyField(partyMembers);
            EditorGUILayout.PropertyField(partyMembersHealth);
            serializedObject.ApplyModifiedProperties();
        }

        // Level Data
        if (_choiceIndex == 3)
        {
            EditorGUILayout.HelpBox("", MessageType.None);        // Description

            // Draw and updated serialized variables
            serializedObject.Update();
            saveManager.activeSave2.saveChapter = EditorGUILayout.TextField("saveChapter", saveManager.activeSave2.saveChapter);
            EditorGUILayout.PropertyField(Chapter);
            serializedObject.ApplyModifiedProperties();
        }

        // Script Values
        if (_choiceIndex == 4)
        {
            EditorGUILayout.HelpBox("", MessageType.None);        // Description

            // Draw and updated serialized variables
            serializedObject.Update();
            EditorGUILayout.PropertyField(startingScene);
            EditorGUILayout.PropertyField(noPortrait);
            saveManager.hasLoaded = EditorGUILayout.Toggle("hasLoaded", saveManager.hasLoaded);
            saveManager.loadFileOnCreation = EditorGUILayout.Toggle("loadFileOnCreation", saveManager.loadFileOnCreation);
            serializedObject.ApplyModifiedProperties();
        }

        // Update the dropdown
        playerName = _choices[_choiceIndex];
        player = GameObject.Find(playerName);
    }
}
