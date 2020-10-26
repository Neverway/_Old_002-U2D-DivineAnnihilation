using UnityEngine;
using UnityEditor;

public class DASDK : EditorWindow
{
    private float _space = 20f;

    [MenuItem("Divine Annihilation SDK/DA Workspace")]
    public static void EditorWindow()
    {
        GetWindow<DASDK>("Divine Annihilation Tool Kit");
    }
    private void OnGUI()
    {
        GUILayout.Label("(C) Neverway 2020", EditorStyles.largeLabel);

        GUILayout.Space(_space);
        GUILayout.Label("Quick Access", EditorStyles.boldLabel);
        GUILayout.Label("Quickly jump to folders in the project directory. If you do not understand how to use an object or just need help then look for asset in the Help & Documentation tab.", EditorStyles.helpBox);
        if (GUILayout.Button("Scenes", GUILayout.MaxWidth(180)))
        {
            Debug.Log("");
        }
        if (GUILayout.Button("Entities", GUILayout.MaxWidth(180)))
        {
            Debug.Log("");
        }
        if (GUILayout.Button("Items", GUILayout.MaxWidth(180)))
        {
            Debug.Log("");
        }
        if (GUILayout.Button("Decorations", GUILayout.MaxWidth(180)))
        {
            Debug.Log("");
        }
        if (GUILayout.Button("Lights", GUILayout.MaxWidth(180)))
        {
            Debug.Log("");
        }
        if (GUILayout.Button("Triggers", GUILayout.MaxWidth(180)))
        {
            Debug.Log("");
        }


        GUILayout.Space(_space);
        GUILayout.Label("Scene Creator", EditorStyles.boldLabel);
        if (GUILayout.Button("New Overworld Scene", GUILayout.MaxWidth(180)))
        {
            Debug.Log("");
        }
        if (GUILayout.Button("New Battle Scene", GUILayout.MaxWidth(180)))
        {
            Debug.Log("");
        }
        if (GUILayout.Button("New Menu Scene", GUILayout.MaxWidth(180)))
        {
            Debug.Log("");
        }

        GUILayout.Space(_space);
        GUILayout.Label("Entity Creator", EditorStyles.boldLabel);

        GUILayout.Space(_space);
        GUILayout.Label("Pickup Creator", EditorStyles.boldLabel);

        GUILayout.Space(_space);
        GUILayout.Label("Trigger Creator", EditorStyles.boldLabel);
    }
}

public class DASceneCreator : EditorWindow
{
    [MenuItem("Divine Annihilation SDK/Scene Creator/New Overworld Scene")]
    public static void ShowWindow()
    {
    }

    [MenuItem("Divine Annihilation SDK/Scene Creator/New Battle Scene")]
    public static void ShowWindow1()
    {
    }

    [MenuItem("Divine Annihilation SDK/Scene Creator/New Menu Scene")]
    public static void ShowWindow2()
    {
    }
}