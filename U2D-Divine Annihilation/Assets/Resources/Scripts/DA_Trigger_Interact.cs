//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
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
        if (inTrigger && Input.GetKeyDown(inputManager.controls["Interact"]) && acceptingInput == true && !initialized && !menuManager.menuActive)
        {
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
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            textboxManager.targetTrigger = gameObject;
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = false;
        }
    }
    public void EnableTrigger()
    {
        StartCoroutine(acceptInput());                                 // Activate the keypress delay
    }
}