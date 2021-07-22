//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Open the DASDK wiki
//
//=============================================================================

using UnityEngine;
using UnityEditor;

public class DASDK_Tool_Help : EditorWindow
{

    [MenuItem("DASDK/Help")]
    public static void ShowWindow()
    {
        Application.OpenURL("https://neverway.github.io/deadend.html");
    }
}
