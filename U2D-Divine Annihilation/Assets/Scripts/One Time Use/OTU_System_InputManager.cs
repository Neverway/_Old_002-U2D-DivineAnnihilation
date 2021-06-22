//=========== Written by Arthur W. Sheldon AKA Lizband_UCC =============================
//
// Purpose: Set the controls and store them to PlayerPrefs
// Applied to: The config object in the title scene
//
//======================================================================================

using System.Collections.Generic;
using UnityEngine;

public class OTU_System_InputManager : MonoBehaviour
{
    // Variables Script
    public Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();


    // Load current keys that are being stored in memory, if there are not any, set some defaults
    void Start()
    {
        controls.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "UpArrow")));
        controls.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "DownArrow")));
        controls.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "LeftArrow")));
        controls.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "RightArrow")));

        controls.Add("Interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "Z")));
        controls.Add("Action", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Action", "X")));
        controls.Add("Select", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Select", "C")));
        controls.Add("Menu", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Menu", "Escape")));

        controls.Add("Special 1", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Special 1", "Alpha1")));
        controls.Add("Special 2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Special 2", "Alpha2")));
        controls.Add("Special 3", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Special 3", "Alpha3")));
        controls.Add("Special 4", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Special 4", "Alpha4")));
    }
}
