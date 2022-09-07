//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Show a textbox when interacting and inside of the trigger
// Applied to: A interaction trigger
// Editor script: 
// Notes: 
//		Text Markups
//		Color:     <color=#ffffff)> or <color="blue">
//		Bold:      <b>
//		Italic:    <i>
//		Underline: <u>
//		Regular:   {r} (returns the text to it's default color and removes any styles)
//
//		Textbox content Markups
//		Speed:     {s=1} (1 is the default speed, 2 is the maximum speed)
//		Portrait:  {p} (goes to the next portrait in the portrait array)
//
//=============================================================================

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DAG13_Trigger_Interact : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    [SerializeField] private DAG13_TextboxData textboxData;
    
    [Tooltip("If true, the trigger will activate when entered")]
    [SerializeField] private bool eventTrigger;


    //=-----------------=
    // Private variables
    //=-----------------=
    private bool inTrigger; // Is true if the player is in the trigger
    private bool activated; // Disables the trigger after activation, can be re-enabled by firing the Reactivate function
    private bool eventTriggered; // Keeps the event trigger from firing again until the player exits the trigger first


    //=-----------------=
    // Reference variables
    //=-----------------=
    private NUPInput input;
    private DAG13_UI_TextboxManager textboxManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
	    // Find references
	    input = FindObjectOfType<NUPInput>();
	    textboxManager = FindObjectOfType<DAG13_UI_TextboxManager>();
    }

    private IEnumerator ActivationDelay()
    {
	    // Re-enable the trigger after a small amount of time has passed
	    // This is so the trigger won't immediately fire again if it's a repeatable interaction
	    yield return new WaitForSeconds(0.5f);
	    activated = false;
    }

    private void Update()
    {
	    if (!inTrigger || textboxManager.active || activated) return;
	    
	    // Triggered via activation
	    if (input.GetKeyDown("Interact") && !eventTrigger)
	    {
		    TransmitDataToTextboxManager();
	    }
	    
	    // Triggered via event
	    else if (eventTrigger && !eventTriggered)
	    {
		    eventTriggered = true;
		    TransmitDataToTextboxManager();
	    }
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
	    if (eventTrigger)
		    eventTriggered = false;
    }


    //=-----------------=
    // Internal Functions
    //=-----------------=
    private void TransmitDataToTextboxManager()
    {
	    activated = true;
	    textboxManager.textboxData = textboxData;
	    textboxManager.StartTextbox();
    }
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void Reactivate()
    {
	    StartCoroutine(ActivationDelay());
    }
}
