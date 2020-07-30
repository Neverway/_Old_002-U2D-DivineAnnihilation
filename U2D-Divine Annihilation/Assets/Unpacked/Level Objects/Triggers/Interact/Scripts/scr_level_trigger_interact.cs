using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_level_trigger_interact : MonoBehaviour
{
    private scr_system_hud_textbox_manager DialogueManager;
    public string[] dialogueLines;
    public string[] dialogueLineNames;
    //public Sprite[] dialogueLinePortraits;


    // Start is called before the first frame update
    void Start()
    {
        DialogueManager = FindObjectOfType<scr_system_hud_textbox_manager>();
    }


    // Check if something has been in the trigger
    void OnTriggerStay2D(Collider2D other)
    {
        // Check if that something is the player
        if(other.gameObject.name == "pre_entity_main_fox_overworld")
        {
            // Check if the player has pressed the action key
            if(Input.GetKeyUp("z"))
            {
                // Check if the dialogue box is already open
                if(!DialogueManager.dialogueBoxActive)
                {
                    DialogueManager.dialogueLines = dialogueLines; // Pass the dialogue lines value to the manager (don't bother understanding this, it just works so I don't bother messing with it)
                    DialogueManager.dialogueLineNames = dialogueLineNames;
                    //DialogueManager.dialogueLinePortraits = dialogueLinePortraits;
                    DialogueManager.currentLine = 0;               // Reset the current line (in case the dialogue manager failes to)
                    DialogueManager.ShowDialogue();                // Execute the show dialogue function
                }
            }
        }
    }
}
