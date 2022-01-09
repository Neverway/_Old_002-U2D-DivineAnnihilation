//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: MRC
// Purpose: Start a text event upon interaction
// Applied to: An interaction trigger in an overworld scene
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DA_Trigger_Interact : MonoBehaviour
{
    // Public variables
    public string[] lineText;
    public string[] lineName;
    public Sprite[] linePortrait;

    public bool eventTrigger;
    public bool eventActive;
    public bool destroyOnFinish;

    public UnityEvent OnFinish;

    // Private variables
    public bool inTrigger;
    public bool acceptingInput;
    public bool initialized;
    public bool completionBlip; // A bool to track if this trigger has been completed for event triggers

    // Reference variables
    private OTU_System_InputManager inputManager;
    private OTU_System_TextboxManager textboxManager;
    private OTU_System_MenuManager menuManager;


    void Start()
    {
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        textboxManager = FindObjectOfType<OTU_System_TextboxManager>();
        menuManager = FindObjectOfType<OTU_System_MenuManager>();
    }


    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(1);     // The delay until it is accepting input again
        acceptingInput = true;                  // Allow input again
    }


    void Update()
    {
        if (inTrigger && Input.GetKeyDown(inputManager.controls["Interact"]) && acceptingInput == true && !initialized && !menuManager.menuActive && !eventTrigger)
        {
            textboxManager.targetTrigger = gameObject;
            acceptingInput = false;     // Enable the keypress delay
                                        // Check if the dialogue box is already open
            if (!textboxManager.textboxActive)
            {
                textboxManager.lineText = lineText;                  // Pass the dialogue lines value to the manager (don't bother understanding this, it just works so I don't bother messing with it)
                textboxManager.lineName = lineName;          // Pass the dialogue line names value to the manager
                textboxManager.linePortrait = linePortrait;  // Pass the dialogue line portraits value to the manager
                textboxManager.currentTextLine = 0;                            // Reset the current line (in case the dialogue manager failes to)
                textboxManager.ShowDialogue();                                 // Execute the show dialogue function
                StartCoroutine(acceptInput());                                 // Activate the keypress delay
                textboxManager.textboxActive = true;
                //textboxManager.targetTrigger = gameObject;
                //textboxManager.destroyOnFinish = destroyOnFinish;
            }
        }
        if (inTrigger && acceptingInput == true && !initialized && !menuManager.menuActive && eventTrigger && !eventActive)
        {
            textboxManager.targetTrigger = gameObject;
            acceptingInput = false;     // Enable the keypress delay
            eventActive = true;
                                        // Check if the dialogue box is already open
            if (!textboxManager.textboxActive)
            {
                textboxManager.lineText = lineText;                  // Pass the dialogue lines value to the manager (don't bother understanding this, it just works so I don't bother messing with it)
                textboxManager.lineName = lineName;          // Pass the dialogue line names value to the manager
                textboxManager.linePortrait = linePortrait;  // Pass the dialogue line portraits value to the manager
                textboxManager.currentTextLine = 0;                            // Reset the current line (in case the dialogue manager failes to)
                textboxManager.ShowDialogue();                                 // Execute the show dialogue function
                StartCoroutine(acceptInput());                                 // Activate the keypress delay
                textboxManager.textboxActive = true;
                //textboxManager.targetTrigger = gameObject;
                //textboxManager.destroyOnFinish = destroyOnFinish;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            textboxManager.currentlyOverlappedTrigger = gameObject;
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            textboxManager.targetTrigger = null;
            textboxManager.currentlyOverlappedTrigger = null;
            inTrigger = false;
            eventActive = false;
        }
    }
    public void EnableTrigger()
    {
        StartCoroutine(acceptInput());                                 // Activate the keypress delay
    }

    public void DisableTrigger()
    {
        StopAllCoroutines();
        acceptingInput = false;
    }

    public void RemoteActivateTrigger()
    {
        textboxManager.targetTrigger = gameObject;
        acceptingInput = false;     // Enable the keypress delay
                                    // Check if the dialogue box is already open
        if (!textboxManager.textboxActive)
        {
            textboxManager.lineText = lineText;                  // Pass the dialogue lines value to the manager (don't bother understanding this, it just works so I don't bother messing with it)
            textboxManager.lineName = lineName;          // Pass the dialogue line names value to the manager
            textboxManager.linePortrait = linePortrait;  // Pass the dialogue line portraits value to the manager
            textboxManager.currentTextLine = 0;                            // Reset the current line (in case the dialogue manager failes to)
            textboxManager.ShowDialogue();                                 // Execute the show dialogue function
            StartCoroutine(acceptInput());                                 // Activate the keypress delay
            textboxManager.textboxActive = true;
            //textboxManager.targetTrigger = gameObject;
            //textboxManager.destroyOnFinish = destroyOnFinish;
        }
    }
    public void SendCompletionBlip()
    {
        completionBlip = true;
    }
}