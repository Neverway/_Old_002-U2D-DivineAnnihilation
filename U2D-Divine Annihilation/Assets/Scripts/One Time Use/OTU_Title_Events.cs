//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Provide various events that can be fired from other scripts
// Applied to: The screen space object in the title scene
//
//=============================================================================

using UnityEngine;
using UnityEditor;

public class OTU_Title_Events : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }
}
