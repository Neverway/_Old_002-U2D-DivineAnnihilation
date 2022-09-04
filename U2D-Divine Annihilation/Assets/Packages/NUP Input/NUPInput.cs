//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Assign an action to an input device for easier control customization
// Applied to: The system manager
// Editor script: 
// Notes: Dictionaries have to have to be declared before usage in the start
//        function, hence the "= new Dictionary"
//
//=============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NUPInput : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    public Actions[] actions;
    public float axisDeadzone = 0.1f;

    // Dictionary with user defined "actions" that reference a "keycode" pulled from the same index in "defaultKeybind"
    private Dictionary<string, KeyCode> deviceKeyboardAndMouseKey = new Dictionary<string, KeyCode>();
    private Dictionary<string, KeyCode> deviceControllerKey = new Dictionary<string, KeyCode>();
    
    // Dictionary with user defined "actions" that reference an "axis" pulled from the same index in "defaultAxisbind"
    private Dictionary<string, string> deviceKeyboardAndMouseAxis = new Dictionary<string, string>();
    private Dictionary<string, string> deviceControllerAxis = new Dictionary<string, string>();

    
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
        InitializeControls();
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    private void InitializeControls()
    {
        // Cycle through each action in the Actions array
        foreach (var action in actions)
        {
            // Assign button information from action
            // ______________________________________
            // Check to see if the action is defined inside of the player preferences, and if it's not, define it with the default controls and add to the input reference dictionary
            deviceKeyboardAndMouseKey.Add(action.actionName, (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(action.actionName+"KeyboardKey", action.keyboardAndMouseKey)));
            // Do the same as above but now for the controller input device instead of the keyboard and mouse
            deviceControllerKey.Add(action.actionName, (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(action.actionName+"ControllerKey", action.controllerKey)));
            
            // Assign axis information from action
            // ______________________________________
            // Check to see if the action is defined inside of the player preferences, and if it's not, define it with the default controls and add to the input reference dictionary
            deviceKeyboardAndMouseAxis.Add(action.actionName, PlayerPrefs.GetString(action.actionName+"KeyboardAxis", action.keyboardAndMouseAxis));
            // Do the same as above but now for the controller input device instead of the keyboard and mouse
            deviceControllerAxis.Add(action.actionName, PlayerPrefs.GetString(action.actionName+"ControllerAxis", action.controllerAxis));
        }
    }

    private int GetActionIndex(string _action)
    {
        var returnIndex = 0;
        for (int i = 0; i < actions.Length; i++)
        {
            if (actions[i].actionName == _action)
            {
                returnIndex = i;
            }
        }
        
        return returnIndex;
    }
    
    // Return if the specified axis has been assigned for an action
    private bool GetAxisBound(string _action, string _device)
    {
        return _device switch
        {
            "keyboard" => actions[GetActionIndex(_action)].keyboardAndMouseAxis != "",
            "controller" => actions[GetActionIndex(_action)].controllerAxis != "",
            _ => false
        };
    }


    //=-----------------=
    // External Functions
    //=-----------------=
    
    // Return if the key or button is being held down
    public bool GetKey(string _action)
    {
        return Input.GetKey(deviceKeyboardAndMouseKey[_action]) || Input.GetKey(deviceControllerKey[_action]);
    }
    
    // Return if the key or button has been released
    public bool GetKeyUp(string _action)
    {
        return Input.GetKeyUp(deviceKeyboardAndMouseKey[_action]) || Input.GetKeyUp(deviceControllerKey[_action]);
    }
    
    // Return if the key or button has been pressed
    public bool GetKeyDown(string _action)
    {
        return Input.GetKeyDown(deviceKeyboardAndMouseKey[_action]) || Input.GetKeyDown(deviceControllerKey[_action]);
    }
    
    // Return the magnitude of a mouse or joystick's axis (Adds a gradual "smoothing" between directions)
    public float GetAxis(string _action)
    {
        // If only keyboard is bound
        if (GetAxisBound(_action, "keyboard") && !GetAxisBound(_action, "controller"))
        {
            if (Input.GetAxis(deviceKeyboardAndMouseAxis[_action]) >= axisDeadzone ||
                Input.GetAxis(deviceKeyboardAndMouseAxis[_action]) <= axisDeadzone * -1)
            {
                return Input.GetAxis(deviceKeyboardAndMouseAxis[_action]);
            }
        }
        
        // If only controller is bound
        if (!GetAxisBound(_action, "keyboard") && GetAxisBound(_action, "controller"))
        {
            if (Input.GetAxis(deviceControllerAxis[_action]) >= axisDeadzone ||
                Input.GetAxis(deviceControllerAxis[_action]) <= axisDeadzone * -1)
            {
                return Input.GetAxis(deviceControllerAxis[_action]);
            }
        }
        
        // If both are bound
        if (GetAxisBound(_action, "keyboard") && GetAxisBound(_action, "controller"))
        {
            if (Input.GetAxis(deviceKeyboardAndMouseAxis[_action]) >= axisDeadzone ||
                Input.GetAxis(deviceKeyboardAndMouseAxis[_action]) <= axisDeadzone * -1 ||
                Input.GetAxis(deviceControllerAxis[_action]) >= axisDeadzone ||
                Input.GetAxis(deviceControllerAxis[_action]) <= axisDeadzone * -1)
            {
                return Input.GetAxis(deviceKeyboardAndMouseAxis[_action]) + Input.GetAxis(deviceControllerAxis[_action]);
            }
        }
        
        // If none are bound
        return 0;
    }
    
    // Return a digital result of a mouse or joystick's axis (Makes an axis result more like button press)
    public float GetAxisRaw(string _action)
    {
        // If only keyboard is bound
        if (GetAxisBound(_action, "keyboard") && !GetAxisBound(_action, "controller"))
        {
            if (Input.GetAxisRaw(deviceKeyboardAndMouseAxis[_action]) >= axisDeadzone ||
                Input.GetAxisRaw(deviceKeyboardAndMouseAxis[_action]) <= axisDeadzone * -1)
            {
                return Input.GetAxisRaw(deviceKeyboardAndMouseAxis[_action]);
            }
        }
        
        // If only controller is bound
        if (!GetAxisBound(_action, "keyboard") && GetAxisBound(_action, "controller"))
        {
            if (Input.GetAxisRaw(deviceControllerAxis[_action]) >= axisDeadzone ||
                Input.GetAxisRaw(deviceControllerAxis[_action]) <= axisDeadzone * -1)
            {
                return Input.GetAxisRaw(deviceControllerAxis[_action]);
            }
        }
        
        // If both are bound
        if (GetAxisBound(_action, "keyboard") && GetAxisBound(_action, "controller"))
        {
            if (Input.GetAxisRaw(deviceKeyboardAndMouseAxis[_action]) >= axisDeadzone ||
                Input.GetAxisRaw(deviceKeyboardAndMouseAxis[_action]) <= axisDeadzone * -1 ||
                Input.GetAxisRaw(deviceControllerAxis[_action]) >= axisDeadzone ||
                Input.GetAxisRaw(deviceControllerAxis[_action]) <= axisDeadzone * -1)
            {
                return Input.GetAxisRaw(deviceKeyboardAndMouseAxis[_action]) + Input.GetAxisRaw(deviceControllerAxis[_action]);
            }
        }
        
        // If none are bound
        return 0;
    }

    [Serializable]
    public class Actions
    {
        // New format
        public string actionName;
        
        [Header("Default Binding")]
        [Header("Keys & Buttons")]
        public string keyboardAndMouseKey;
        public string controllerKey;
        
        [Header("Axis Inputs")]
        public string keyboardAndMouseAxis;
        public string controllerAxis;
        
        [Header("READ-ONLY")]
        public bool wasHeld;
    }
}