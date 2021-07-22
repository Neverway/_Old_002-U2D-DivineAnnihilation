//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
//
//=============================================================================

using UnityEngine;
using UnityEditor;

public class DASDK_Tool_AssetBrowser : EditorWindow
{
    // Public variables

    // Private variables

    // Reference variables


    [MenuItem("DASDK/Asset Browser")]
    public static void ShowWindow()
    {
        GetWindow(typeof(DASDK_Tool_AssetBrowser));
    }

    private void OnGUI()
    {
        GUILayout.Label("AssetBrowser", EditorStyles.boldLabel);
    }
}
