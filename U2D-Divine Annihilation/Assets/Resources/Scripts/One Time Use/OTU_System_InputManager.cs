//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: CAC
// Purpose: Set the controls and store them to PlayerPrefs
// Applied to: The config object in the title scene
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections.Generic;
using UnityEngine;

public class OTU_System_InputManager : MonoBehaviour
{
    // Public variables
    public Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();
    public Dictionary<string, KeyCode> controlsBuffered = new Dictionary<string, KeyCode>();


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
        
        controls.Add("L", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("L", "A")));
        controls.Add("R", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("R", "D")));


        // Temporary storage of controls for the controler bindings menu
        controlsBuffered.Add("UpBuffered", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "UpArrow")));
        controlsBuffered.Add("DownBuffered", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "DownArrow")));
        controlsBuffered.Add("LeftBuffered", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "LeftArrow")));
        controlsBuffered.Add("RightBuffered", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "RightArrow")));

        controlsBuffered.Add("InteractBuffered", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "Z")));
        controlsBuffered.Add("ActionBuffered", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Action", "X")));
        controlsBuffered.Add("SelectBuffered", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Select", "C")));
        controlsBuffered.Add("MenuBuffered", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Menu", "Escape")));

        controlsBuffered.Add("Special 1Buffered", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Special 1", "Alpha1")));
        controlsBuffered.Add("Special 2Buffered", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Special 2", "Alpha2")));
        controlsBuffered.Add("Special 3Buffered", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Special 3", "Alpha3")));
        controlsBuffered.Add("Special 4Buffered", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Special 4", "Alpha4")));

        controlsBuffered.Add("LBuffered", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("L", "A")));
        controlsBuffered.Add("RBuffered", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("R", "D")));
    }
}
