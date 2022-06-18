//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: An entity object in an overworld scene
// Parent script: DA_Entity_Control
//
//=============================================================================
using System.Collections;
using UnityEngine;
using UnityEditor;

public class DASDK_Tool_EntityEditor : EditorWindow
{
    // Base idle variables
    public AnimationClip IdleFront;
    public AnimationClip IdleBack;
    public AnimationClip IdleLeft;
    public AnimationClip IdleRight;

    // Base walk variables
    public AnimationClip walkFront;
    public AnimationClip walkBack;
    public AnimationClip walkLeft;
    public AnimationClip walkRight;
    public float walkSpeed = 5f;

    // Base sprint variables
    public AnimationClip sprintFront;
    public AnimationClip sprintBack;
    public AnimationClip sprintLeft;
    public AnimationClip sprintRight;
    public float sprintSpeed = 5f;

    // Base variables
    public float maxHealth = 100f;

    // Player variables
    public Sprite shelfSprite;

    // Character variables
    public bool isFollower;
    public string followerName;

    // Character variables
    public float senseRange = 20f;
    public float attack = 15;
    public int goldDrop = 5;
    public float expDrop = 5;

    // Private variables


    // Reference variables



    [MenuItem("DASDK/Entity Editor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(DASDK_Tool_EntityEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Entity Editor", EditorStyles.largeLabel);
        EditorGUILayout.Space(); // Add a divider

        // Base Idle
        GUILayout.Label("Idle", EditorStyles.boldLabel);
        EditorGUILayout.ObjectField("Idle Front", IdleFront, typeof(AnimationClip), false);
        EditorGUILayout.ObjectField("Idle Back", IdleBack, typeof(AnimationClip), false);
        EditorGUILayout.ObjectField("Idle Left", IdleLeft, typeof(AnimationClip), false);
        EditorGUILayout.ObjectField("Idle Right", IdleRight, typeof(AnimationClip), false);
        EditorGUILayout.Space(); // Add a divider

        // Base Walk
        GUILayout.Label("Walk", EditorStyles.boldLabel);
        EditorGUILayout.ObjectField("Walk Front", walkFront, typeof(AnimationClip), false);
        EditorGUILayout.ObjectField("Walk Back", walkBack, typeof(AnimationClip), false);
        EditorGUILayout.ObjectField("Walk Left", walkLeft, typeof(AnimationClip), false);
        EditorGUILayout.ObjectField("Walk Right", walkRight, typeof(AnimationClip), false);
        EditorGUILayout.FloatField("Walk Speed", walkSpeed);
        EditorGUILayout.Space(); // Add a divider

        // Base Sprint
        GUILayout.Label("Sprint", EditorStyles.boldLabel);
        EditorGUILayout.ObjectField("Sprint Front", sprintFront, typeof(AnimationClip), false);
        EditorGUILayout.ObjectField("Sprint Back", sprintBack, typeof(AnimationClip), false);
        EditorGUILayout.ObjectField("Sprint Left", sprintLeft, typeof(AnimationClip), false);
        EditorGUILayout.ObjectField("Sprint Right", sprintRight, typeof(AnimationClip), false);
        EditorGUILayout.FloatField("Sprint Speed", sprintSpeed);
        EditorGUILayout.Space(); // Add a divider

        GUILayout.BeginHorizontal();
        GUILayout.Button("Create as prefab");
        GUILayout.Button("Create as scene object");
    }
}
