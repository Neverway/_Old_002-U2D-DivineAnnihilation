﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_level_trigger_interact : MonoBehaviour
{

    public string dialogue;
    private scr_system_hud_textbox_manager DialogueManager;

    public string[] dialogueLines;

    // Start is called before the first frame update
    void Start()
    {
        DialogueManager = FindObjectOfType<scr_system_hud_textbox_manager>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "pre_entity_main_fox_overworld")
        {

            if(Input.GetKeyUp("z"))
            {
                //DiaMan.ShowBox(dialogue);
                if(!DialogueManager.dialogueBoxActive)
                {
                    DialogueManager.dialogueLines = dialogueLines;
                    DialogueManager.currentLine = 0;
                    DialogueManager.ShowDialogue();
                }
            }
        }
    }
}
