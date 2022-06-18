//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Give funtionality to an entity
// Applied to: A entity object in an overworld scene
// Parent script: DA_Entity_Control
//
//=============================================================================

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DA_Entity_Control))]
public class DASDK_Entity_Control : Editor
{
    // Setup dropdown array
    string[] _choices = { "Character", "Enemy", "Player" };
    int _choiceIndex;

    // Base variables
    SerializedProperty entityName;
    SerializedProperty entityAnimator;
    SerializedProperty idleUp;
    SerializedProperty idleDown;
    SerializedProperty idleLeft;
    SerializedProperty idleRight;
    SerializedProperty choiceValue;
    DA_Entity_Control entityControl;

    SerializedProperty maxHealth;
    SerializedProperty walkSpeed;
    SerializedProperty sprintSpeed;


    SerializedProperty shelfSprite;
    //SerializedProperty inventory;
    SerializedProperty HUD;
    
    SerializedProperty enemysPartyMembers;


    // Find parent script variables
    void OnEnable()
    {
        // Find Base variables
        entityName = serializedObject.FindProperty("entityName");
        entityAnimator = serializedObject.FindProperty("entityAnimator");
        idleUp = serializedObject.FindProperty("idleUp");
        idleDown = serializedObject.FindProperty("idleDown");
        idleLeft = serializedObject.FindProperty("idleLeft");
        idleRight = serializedObject.FindProperty("idleRight");
        choiceValue = serializedObject.FindProperty("choiceValue");

        maxHealth = serializedObject.FindProperty("maxHealth");
        walkSpeed = serializedObject.FindProperty("walkSpeed");
        sprintSpeed = serializedObject.FindProperty("sprintSpeed");

        shelfSprite = serializedObject.FindProperty("shelfSprite");
        //inventory = serializedObject.FindProperty("inventory");
        HUD = serializedObject.FindProperty("HUD");

        enemysPartyMembers = serializedObject.FindProperty("enemysPartyMembers");
    }


    // Draw the custom editor
    public override void OnInspectorGUI()
    {
        // Drop down and script reference
        entityControl = (DA_Entity_Control)target;                                      // Set the current script to the target class
        _choiceIndex = entityControl.choiceValue;                                       // Save the dropdown state
        _choiceIndex = EditorGUILayout.Popup("Entity Type", _choiceIndex, _choices);    // Draw the dropdown button
        EditorGUILayout.Space();                                                                      // Add a divider

        serializedObject.Update();

        //Display dropdown choices
        ChoiceOne();
        ChoiceTwo();
        ChoiceThree();

        serializedObject.ApplyModifiedProperties();
    }


    // Dropdown choices
    public void ChoiceOne()
    {
        if (_choiceIndex == 0)
        {
            EditorGUILayout.HelpBox("Passive NPC's & party members/followers", MessageType.None);   // Description
            BaseVaraibles();

            entityControl.isFollower = EditorGUILayout.Toggle("Is a follower", entityControl.isFollower);   // Sprites over animator

            // Follower
            if (entityControl.isFollower)
            {
                EditorGUILayout.PropertyField(shelfSprite);
            }

            entityControl.choiceValue = 0;
        }
    }


    public void ChoiceTwo()
    {
        if (_choiceIndex == 1)
        {
            EditorGUILayout.HelpBox("Hostile NPC's that hunt down or attack the player", MessageType.None);     // Description
            BaseVaraibles();
            EditorGUILayout.PropertyField(enemysPartyMembers);
            entityControl.choiceValue = 1;
        }
    }


    public void ChoiceThree()
    {
        if (_choiceIndex == 2)
        {
            EditorGUILayout.HelpBox("Playable characters that are controled by the player", MessageType.None);  // Description
            BaseVaraibles();

            EditorGUILayout.PropertyField(shelfSprite);
            //EditorGUILayout.PropertyField(inventory);
            EditorGUILayout.PropertyField(HUD);
            
            entityControl.choiceValue = 2;
        }
    }


    public void BaseVaraibles()
    {
        EditorGUILayout.PropertyField(entityName);  // Entity name
        EditorGUILayout.PropertyField(maxHealth); // Entity Health
        entityControl.useSpritesOverAnimator = EditorGUILayout.Toggle("Sprites Over Animator", entityControl.useSpritesOverAnimator);   // Sprites over animator

        // Animator
        if (!entityControl.useSpritesOverAnimator)
        {
            EditorGUILayout.PropertyField(entityAnimator);
        }

        // Sprites over animator
        else if (entityControl.useSpritesOverAnimator)
        {
            EditorGUILayout.PropertyField(idleUp);
            EditorGUILayout.PropertyField(idleDown);
            EditorGUILayout.PropertyField(idleLeft);
            EditorGUILayout.PropertyField(idleRight);
        }

        EditorGUILayout.PropertyField(walkSpeed); // Walk Speed
        EditorGUILayout.PropertyField(sprintSpeed); // Sprint Speed

        EditorGUILayout.Space();                                                       // Add a divider
    }
}