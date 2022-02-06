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
    SerializedProperty itemIcons;
    SerializedProperty itemCategories;
    SerializedProperty itemDescriptions;
    SerializedProperty itemDiscardable;
    SerializedProperty equipment;
    SerializedProperty equipmentIcons;
    SerializedProperty equipmentCategories;
    SerializedProperty equipmentDescriptions;
    SerializedProperty equipmentDiscardable;

    SerializedProperty equippedU;
    SerializedProperty equippedW;
    SerializedProperty equippedM;
    SerializedProperty equippedD;

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
        itemIcons = serializedObject.FindProperty("activeSave2.itemIcons");
        itemCategories = serializedObject.FindProperty("activeSave2.itemCategories");
        itemDescriptions = serializedObject.FindProperty("activeSave2.itemDescriptions");
        itemDiscardable = serializedObject.FindProperty("activeSave2.itemDiscardable");
        equipment = serializedObject.FindProperty("activeSave2.equipment");
        equipmentIcons = serializedObject.FindProperty("activeSave2.equipmentIcons");
        equipmentCategories = serializedObject.FindProperty("activeSave2.equipmentCategories");
        equipmentDescriptions = serializedObject.FindProperty("activeSave2.equipmentDescriptions");
        equipmentDiscardable = serializedObject.FindProperty("activeSave2.equipmentDiscardable");

        equippedU = serializedObject.FindProperty("activeSave2.equippedU");
        equippedW = serializedObject.FindProperty("activeSave2.equippedW");
        equippedM = serializedObject.FindProperty("activeSave2.equippedM");
        equippedD = serializedObject.FindProperty("activeSave2.equippedD");

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
            saveManager.activeSave2.playerGold = EditorGUILayout.IntField("playerGold", saveManager.activeSave2.playerGold);
            serializedObject.ApplyModifiedProperties();
        }

        // Inventory
        if (_choiceIndex == 1)
        {
            EditorGUILayout.HelpBox("", MessageType.None);        // Description

            // Draw and updated serialized variables
            serializedObject.Update();
            EditorGUILayout.PropertyField(items);
            EditorGUILayout.PropertyField(itemIcons);
            EditorGUILayout.PropertyField(itemCategories);
            EditorGUILayout.PropertyField(itemDiscardable);
            EditorGUILayout.PropertyField(itemDescriptions);
            EditorGUILayout.PropertyField(equipment);
            EditorGUILayout.PropertyField(equipmentIcons);
            EditorGUILayout.PropertyField(equipmentCategories);
            EditorGUILayout.PropertyField(equipmentDiscardable);
            EditorGUILayout.PropertyField(equipmentDescriptions);

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(equippedU);
            EditorGUILayout.PropertyField(equippedW);
            EditorGUILayout.PropertyField(equippedM);
            EditorGUILayout.PropertyField(equippedD);
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
