using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_system_hud_textbox_manager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public bool dialogueActive;
    public string[] dialogueLines;
    public int currentLine;
    private scr_entity_character_movement charMovement;

    // Start is called before the first frame update
    void Start()
    {
        charMovement = FindObjectOfType<scr_entity_character_movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueActive && Input.GetKeyDown("z"))
        {
            currentLine += 1;
        }

        if(currentLine >= dialogueLines.Length)
        {
            dialogueBox.SetActive(false);
            dialogueActive = false;
            currentLine = 0;
            charMovement.canMove = true;
            charMovement.movementSpeed = charMovement.storedSpeed;
        }

        dialogueText.text = dialogueLines[currentLine];

    }

    internal static bool DialogueActive()
    {
        throw new NotImplementedException();
    }

    public void ShowBox(string dialogue)
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = dialogue;
    }

    public void ShowDialogue()
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
    }
}
