﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_system_hud_textbox_manager : MonoBehaviour
{
    // Referances
    public GameObject dialogueBoxObject;                     // A referance to the dialogue box object
    public Text dialogueTextObject;                          // A referance to the dialogue text object
    public Text dialogueNameTextObject;
    public Image dialoguePortraitObject;

    // Code set value referances
    public string[] dialogueLines;                           // An array of the text that should be drawn in the dialogue box
    public string[] dialogueLineNames;
    public Sprite[] dialogueLinePortraits;
    public bool acceptingInput;
    public int currentLine;                                  // A number to check which set of text should be called from the text array
    public bool dialogueBoxActive;                           // A true or false statment of if the dialogue box is open
    private scr_entity_character_movement characterMovement; // A referance to the player movement so the dialogue box can freeze the player when it opens


    // Start is called before the first frame update
    void Start()
    {
        characterMovement = FindObjectOfType<scr_entity_character_movement>(); // Find the character movment script
    }


    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(0.05f);
        acceptingInput = true;
    }


    // Update is called once per frame
    void Update()
    {
        // Continue to next dialogue
        if (dialogueBoxActive && Input.GetKeyDown("z")) 
        {
            if(acceptingInput)
            {
                currentLine += 1; // Advance the line count
            }
            StartCoroutine("acceptInput");
        }
       
        // End the dialogue when there are no more lines of text
        if (currentLine >= dialogueLines.Length)
        {
            dialogueBoxObject.SetActive(false);                              // Make the box disappear
            dialogueBoxActive = false;                                       // Set the active state to false
            currentLine = 0;                                                 // Reset the current line count to zero
            StopCoroutine("acceptInput");
            acceptingInput = false;
            characterMovement.canMove = true;                                // Allow the player to move
            characterMovement.movementSpeed = characterMovement.storedSpeed; // Set the players speed so they won't get stuck with a movement speed of zero
        }

        // Set the text on screen the the current line text
        dialogueTextObject.text = dialogueLines[currentLine];
        dialogueNameTextObject.text = dialogueLineNames[currentLine];
        dialoguePortraitObject.sprite = dialogueLinePortraits[currentLine];

    }


    // Setup a function to enable the dialogue boxes
    public void ShowDialogue()
    {
        dialogueBoxActive = true;          // Set the active state to true
        dialogueBoxObject.SetActive(true); // Make the box appear
    }
}
