//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class DAG13_TextboxData
{
    //=-----------------=
    // Public variables
    //=-----------------=
    public TextboxLine[] textboxLines;
    public UnityEvent onFinishTextbox;


    //=-----------------=
    // Private variables
    //=-----------------=


    //=-----------------=
    // Reference variables
    //=-----------------=


    //=-----------------=
    // Mono Functions
    //=-----------------=
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    [Serializable]
    public class TextboxLine
    {
	    [Tooltip("Name of the character that's talking (Leave blank to switch to a monologue textbox)")]
	    public string name;
	    
	    [Tooltip("Sprite of the character that's talking (Won't display if textbox is in monologue mode)")]
	    public Sprite[] portrait;
	    
	    [Tooltip("Text that appears in the textbox")][TextArea]
	    public string textContent;
	    
	    [Tooltip("Events to fire once this line has finished appearing")]
	    public UnityEvent onFinishLine;
    }
}
