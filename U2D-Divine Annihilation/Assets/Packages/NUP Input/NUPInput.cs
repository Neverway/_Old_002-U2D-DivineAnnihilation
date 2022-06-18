//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes:
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NUPInput : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    [Header ("https://docs.unity3d.com/Manual/class-InputManager.html")]
    [Header ("Vaild keybind can be found here:")]
    public string[] action;
    public string[] defaultKeybind;
    public Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>(); // Actions and their keybind
    public Dictionary<string, KeyCode> controlsBuffered = new Dictionary<string, KeyCode>(); // Same as above, but used to store possible keys when binding in the options menu

    public string[] axisAction;
    public string[] defaultAxisbind;
    public Dictionary<string, string> axisControls = new Dictionary<string, string>(); // Axis actions and their keybind
    public Dictionary<string, string> axisControlsBuffered = new Dictionary<string, string>(); // Same as above, but used to store possible keys when binding in the options menu


    //=-----------------=
    // Private variables
    //=-----------------=



    //=-----------------=
    // Reference variables
    //=-----------------=



    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        // Load current keys that are being stored in memory, if there are not any, set some defaults
        for (int i = 0; i < action.Length; i++)
        {
            controls.Add(action[i], (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(action[i], defaultKeybind[i])));
        }
        
        // Temporary storage of controls for the controller bindings menu
        for (int i = 0; i < action.Length; i++)
        {
            controlsBuffered.Add((action[i] + "Buffered"), (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(action[i], defaultKeybind[i])));
        }

        // Load current axis keys that are being stored in memory, if there are not any, set some defaults
        for (int i = 0; i < axisAction.Length; i++)
        {
            axisControls.Add(axisAction[i], PlayerPrefs.GetString(axisAction[i], defaultAxisbind[i]));
        }
        
        // Temporary storage of axis controls for the controller bindings menu
        for (int i = 0; i < axisAction.Length; i++)
        {
            axisControlsBuffered.Add(axisAction[i], PlayerPrefs.GetString(axisAction[i], defaultAxisbind[i]));
        }
    }


    private void Update()
    {
	
    }
    
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}