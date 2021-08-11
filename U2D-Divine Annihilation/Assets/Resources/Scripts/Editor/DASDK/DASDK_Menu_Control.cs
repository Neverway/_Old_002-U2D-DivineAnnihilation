//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Scroll through and toggle menu objects
// Applied to: A menu parent object in a scene
// Parent script: DA_Menu_Control
//
//=============================================================================

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DA_Menu_Control))]
public class DASDK_Menu_Control : Editor
{
    // Setup dropdown array
    string[] _choices = new[] { "Scroll Sprites", "Scroll Strings", "Scroll Events", "Menu Control" };
    int _choiceIndex;
    [HideInInspector] public string playerName;
    [HideInInspector] public GameObject player;

    // Variables Sprite Scrolling
    SerializedProperty spriteTargetObject;
    SerializedProperty sprites;

    // Variables String Scrolling
    SerializedProperty hoverColor;
    SerializedProperty baseColor;
    SerializedProperty textTargetObjects;
    SerializedProperty baseText;
    SerializedProperty hoveredText;

    // Variables Event Scrolling
    SerializedProperty events;

    // Variables Menu Control
    SerializedProperty OnInteract;
    SerializedProperty onBack;
    SerializedProperty onMenuChange;
    SerializedProperty previousMenu;
    SerializedProperty nextMenu;
    SerializedProperty currentSelection;


    // Find parent script variables
    void OnEnable()
    {
        // Find Sprite Scrolling variables
        spriteTargetObject = serializedObject.FindProperty("spriteTargetObject");
        sprites = serializedObject.FindProperty("sprites");

        // Find String Scrolling variables
        hoverColor = serializedObject.FindProperty("hoverColor");
        baseColor = serializedObject.FindProperty("baseColor");
        textTargetObjects = serializedObject.FindProperty("textTargetObjects");
        baseText = serializedObject.FindProperty("baseText");
        hoveredText = serializedObject.FindProperty("hoveredText");

        // Find Event Scrolling variables
        events = serializedObject.FindProperty("events");

        // Find Menu Control variables
        OnInteract = serializedObject.FindProperty("OnInteract");
        onBack = serializedObject.FindProperty("onBack");
        onMenuChange = serializedObject.FindProperty("onMenuChange");
        previousMenu = serializedObject.FindProperty("previousMenu");
        nextMenu = serializedObject.FindProperty("nextMenu");
        currentSelection = serializedObject.FindProperty("currentSelection");
    }


    // Draw the custom editor
    public override void OnInspectorGUI()
    {
        DA_Menu_Control menuControl = (DA_Menu_Control)target;                          // Set the current script to the target class
        _choiceIndex = EditorGUILayout.Popup("Current Menu", _choiceIndex, _choices);   // Draw the dropdown button

        // Scroll sprites
        if (_choiceIndex == 0)
        {
            EditorGUILayout.HelpBox("Cycle through a list of sprites using the directional buttons.", MessageType.None);        // Description
            menuControl.horizontalScrolling = EditorGUILayout.Toggle("Horizontal Scrolling", menuControl.horizontalScrolling);  // Horizontal scrolling bool
            menuControl.wrapAround = EditorGUILayout.Toggle("Wrap Around", menuControl.wrapAround);                             // Wrap Around bool
            EditorGUILayout.Space();                                                                                            // Add a divider
            menuControl.scrollSprites = EditorGUILayout.Toggle("Enable Sprite Scrolling", menuControl.scrollSprites);           // Enable Scroll Sprites

            // Draw and updated serialized variables
            serializedObject.Update();
            EditorGUILayout.PropertyField(spriteTargetObject);
            EditorGUILayout.PropertyField(sprites);
            serializedObject.ApplyModifiedProperties();
        }

        // Scroll strings
        if (_choiceIndex == 1)
        {
            EditorGUILayout.HelpBox("Cycle through a set of strings (text) using the directional buttons, if the player is selecting a option then the text will be set to it's Hovered Text if not then it will be set to it's Base Text.", MessageType.None);
            menuControl.horizontalScrolling = EditorGUILayout.Toggle("Horizontal Scrolling", menuControl.horizontalScrolling);  // Horizontal scrolling bool
            menuControl.wrapAround = EditorGUILayout.Toggle("Wrap Around", menuControl.wrapAround);                             // Wrap Around bool
            EditorGUILayout.Space();                                                                                            // Add a divider
            menuControl.scrollStrings = EditorGUILayout.Toggle("Enable String Scrolling", menuControl.scrollStrings);           // Enable Scroll Strings
            menuControl.singleTextObjectScrolling = EditorGUILayout.Toggle("Enable Single Text Object Scrolling", menuControl.singleTextObjectScrolling);           // Enable Scroll Strings

            // Draw and updated serialized variables
            serializedObject.Update();
            EditorGUILayout.PropertyField(textTargetObjects);
            EditorGUILayout.PropertyField(baseText);
            EditorGUILayout.PropertyField(hoveredText);
            EditorGUILayout.PropertyField(baseColor);
            EditorGUILayout.PropertyField(hoverColor);
            serializedObject.ApplyModifiedProperties();
        }

        // Scroll events
        if (_choiceIndex == 2)
        {
            EditorGUILayout.HelpBox("Cycle through a list of events using the directional buttons.", MessageType.None);         // Description
            menuControl.horizontalScrolling = EditorGUILayout.Toggle("Horizontal Scrolling", menuControl.horizontalScrolling);  // Horizontal scrolling bool
            menuControl.wrapAround = EditorGUILayout.Toggle("Wrap Around", menuControl.wrapAround);                             // Wrap Around bool
            EditorGUILayout.Space();                                                                                            // Add a divider
            menuControl.scrollEvents = EditorGUILayout.Toggle("Enable Event Scrolling", menuControl.scrollEvents);           // Enable Scroll Sprites

            // Draw and updated serialized variables
            serializedObject.Update();
            EditorGUILayout.PropertyField(events);
            serializedObject.ApplyModifiedProperties();
        }

        // Menu control
        if (_choiceIndex == 3)
        {
            menuControl.menuControl = EditorGUILayout.Toggle("Enable Menu Control", menuControl.menuControl);   // Enable Menu Control
            menuControl.canGoBack = EditorGUILayout.Toggle("Can Go Back", menuControl.canGoBack);               // Can Go Back bool

            // Draw and updated serialized variables
            serializedObject.Update();
            EditorGUILayout.PropertyField(previousMenu);
            EditorGUILayout.PropertyField(nextMenu);
            EditorGUILayout.PropertyField(currentSelection);
            EditorGUILayout.PropertyField(OnInteract);
            EditorGUILayout.PropertyField(onBack);
            EditorGUILayout.PropertyField(onMenuChange);
            serializedObject.ApplyModifiedProperties();
        }

        // Update the dropdown
        playerName = _choices[_choiceIndex];
        player = GameObject.Find(playerName);
    }
}