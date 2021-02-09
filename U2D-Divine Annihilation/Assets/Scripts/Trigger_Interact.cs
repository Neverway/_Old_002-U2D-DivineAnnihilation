//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Starts a dialouge event upon interaction
// Applied to: Interact trigger
//
//=============================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Trigger_Interact : MonoBehaviour
{
    private Hud_Textbox_Manager DialogueManager;
    public string[] dialogueLines;
    public string[] dialogueLineNames;
    public Sprite[] dialogueLinePortraits;
    public bool EventTrigger;
    public bool destroyOnFinish;
    private bool EventActive;
    public bool acceptingInput;
    public bool startDestroy;
    public bool startDestroyTriggered;
    public UnityEvent onFinish;

    private System_InputManager inputManager;
    private System_Config_Manager global;

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
        DialogueManager = FindObjectOfType<Hud_Textbox_Manager>();   // Find the dialogue manager script
    }


    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(1);     // The delay until it is accepting input again
        acceptingInput = true;                  // Allow input again
    }

    IEnumerator finishDestroy()
    {
        onFinish.Invoke();
        yield return new WaitForSeconds(.08f);     // The delay until it is accepting input again
    }

    public void Test()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (DialogueManager.currentLine >= dialogueLines.Length)
        {
            StartCoroutine("finishDestroy");
        }
        if (other.gameObject.name == "Entity Fox")
        {
            DialogueManager.EventTrigger = EventTrigger;
            DialogueManager.EventActive = EventActive;
            if (startDestroy && !startDestroyTriggered)
            {
                StartCoroutine("finishDestroy");
                Debug.Log("Scream Again?");
                startDestroyTriggered = true;
            }

            // Check if the player has pressed the action key
            if (Input.GetKeyDown(inputManager.controls["Interact"]) && acceptingInput == true && !EventTrigger)
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
            else if (Input.GetKeyDown(inputManager.controls["Interact"]) && acceptingInput == true && EventActive)
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
