﻿// Included Libraries
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Trigger Interact class
 * ----------------------
 * This script is applied to a interact trigger in a scene
 * The developer fills in the arrays with the info that should appear in the text box
 * All of these variables are then passed to the dialogue manager when the trigger is activated
*/
public class TriggerInteract : MonoBehaviour
{
    private HudTextboxManager DialogueManager;
    public string[] dialogueLines;
    public string[] dialogueLineNames;
    public Sprite[] dialogueLinePortraits;
    public bool EventTrigger;
    public bool destroyOnFinish;
    private bool EventActive;
    public bool acceptingInput;

    // Other class references
    private SystemConfigManager global;


    // Start is called before the first frame update
    void Start()
    {
        DialogueManager = FindObjectOfType<HudTextboxManager>();   // Find the dialogue manager script
    }


    // Key press delay
    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(1);     // The delay until it is accepting input again
        acceptingInput = true;                  // Allow input again
    }


    // Check if something has been in the trigger
    void OnTriggerStay2D(Collider2D other)
    {
        // Check if that something is the player
        if(other.gameObject.name == "Entity Fox")
        {
            DialogueManager.EventTrigger = EventTrigger;
            DialogueManager.EventActive = EventActive;
            // Check if the player has pressed the action key
            if (Input.GetKeyDown("z") && acceptingInput == true && !EventTrigger)
            {
                acceptingInput = false;     // Enable the keypress delay
                // Check if the dialogue box is already open
                if (!DialogueManager.dialogueBoxActive)
                {
                    DialogueManager.dialogueLines = dialogueLines;                  // Pass the dialogue lines value to the manager (don't bother understanding this, it just works so I don't bother messing with it)
                    DialogueManager.dialogueLineNames = dialogueLineNames;          // Pass the dialogue line names value to the manager
                    DialogueManager.dialogueLinePortraits = dialogueLinePortraits;  // Pass the dialogue line portraits value to the manager
                    DialogueManager.currentLine = 0;                                // Reset the current line (in case the dialogue manager failes to)
                    DialogueManager.ShowDialogue();                                 // Execute the show dialogue function
                    StartCoroutine("acceptInput");                                  // Activate the keypress delay
                    DialogueManager.targetTrigger = gameObject;
                    DialogueManager.destroyOnFinish = destroyOnFinish;
                }
            }

            // Check if the player has pressed the action key
            if (EventTrigger && !EventActive)
            {
                acceptingInput = false;     // Enable the keypress delay
                // Check if the dialogue box is already open
                if (!DialogueManager.dialogueBoxActive)
                {
                    DialogueManager.dialogueLines = dialogueLines;                  // Pass the dialogue lines value to the manager (don't bother understanding this, it just works so I don't bother messing with it)
                    DialogueManager.dialogueLineNames = dialogueLineNames;          // Pass the dialogue line names value to the manager
                    DialogueManager.dialogueLinePortraits = dialogueLinePortraits;  // Pass the dialogue line portraits value to the manager
                    DialogueManager.currentLine = 0;                                // Reset the current line (in case the dialogue manager failes to)
                    DialogueManager.ShowDialogue();                                 // Execute the show dialogue function
                    EventActive = true;
                    StartCoroutine("acceptInput");                                  // Activate the keypress delay
                    DialogueManager.targetTrigger = gameObject;
                    DialogueManager.destroyOnFinish = destroyOnFinish;
                }
            }
            // Check if the player has pressed the action key
            else if (Input.GetKeyDown("z") && acceptingInput == true && EventActive)
            {
                acceptingInput = false;     // Enable the keypress delay
                // Check if the dialogue box is already open
                if (!DialogueManager.dialogueBoxActive)
                {
                    DialogueManager.dialogueLines = dialogueLines;                  // Pass the dialogue lines value to the manager (don't bother understanding this, it just works so I don't bother messing with it)
                    DialogueManager.dialogueLineNames = dialogueLineNames;          // Pass the dialogue line names value to the manager
                    DialogueManager.dialogueLinePortraits = dialogueLinePortraits;  // Pass the dialogue line portraits value to the manager
                    //DialogueManager.currentLine = 0;                                // Reset the current line (in case the dialogue manager failes to)
                    DialogueManager.ShowDialogue();                                 // Execute the show dialogue function
                    StartCoroutine("acceptInput");                                  // Activate the keypress delay
                }
            }
        }
    }
}
