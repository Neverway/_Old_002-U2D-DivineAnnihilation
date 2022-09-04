//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
// Text Markups
// Color:     {c(#ffffff)}
// Bold:      {b}
// Italic:    {i}
// Underline: {u}
// Regular:   {r} (returns the text to it's default color and removes any styles)
    
// Textbox content Markups
// Speed:     {s(1)} (1 is the default speed, 2 is the maximum speed)
// Portrait:  {p} (goes to the next portrait in the portrait array)
//
//=============================================================================

using System;
using UnityEngine;
using UnityEngine.Events;

public class DAG13_Trigger_Interact : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    [SerializeField]public DAG13_TextboxData textboxData;


    //=-----------------=
    // Private variables
    //=-----------------=
    public bool inTrigger;


    //=-----------------=
    // Reference variables
    //=-----------------=
    private NUPInput input;
    private DAG13_System_TextboxManager textboxManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
	    input = FindObjectOfType<NUPInput>();
	    textboxManager = FindObjectOfType<DAG13_System_TextboxManager>();
    }

    private void Update()
    {
	    if (!inTrigger || !input.GetKeyDown("Interact")) return;
	    TransmitDataToTextboxManager();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
	    if (!other.CompareTag("Player")) return;
	    inTrigger = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
	    if (!other.CompareTag("Player")) return;
	    inTrigger = false;
    }


    //=-----------------=
    // Internal Functions
    //=-----------------=
    private void TransmitDataToTextboxManager()
    {
	    textboxManager.textboxData = textboxData;
    }
    
    //=-----------------=
    // External Functions
    //=-----------------=
}
