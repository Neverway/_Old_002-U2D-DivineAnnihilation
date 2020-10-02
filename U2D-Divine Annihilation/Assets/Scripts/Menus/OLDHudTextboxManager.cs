// Included Libraries
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Textbox Manager class
 * ---------------------
 * This script is applied to the textbox manager object in a scene that uses textboxes
 * It get the public variables filled in from things like the interaction triggers
 * All of this is to display the textbox objects on screen properly, such as the box itself, the text & name in the box, as well as the textbox portrait
*/
public class OLDHudTextboxManager : MonoBehaviour
{
    public GameObject dialogueBoxObject;            // The parent to the whole textbox object
    public Text dialogueTextObject;                 // The text field for if there is a portrait
    public Text monologueTextObject;                // The text field for if there is not a portrait
    public GameObject dialogueTextObjectParent;     // The parent to the dialougue text object, used for activating or deactivating it
    public GameObject monologueTextObjectParent;    // The parent to the monologue text object, used for activating or deactivating it
    public Text dialogueNameTextObject;             // The text field for the speakers name
    public Image dialoguePortraitObject;            // The portrait of the character who is talking
    public Sprite noPortrait;                       // A reference to the blank portrait sprite
    public string[] dialogueLines;                  // A passthrough variable for the current dialogue line in the array
    public string[] dialogueLineNames;              // A passthrough variable for the current dialogue line name in the array
    public Sprite[] dialogueLinePortraits;          // A passthrough variable for the current dialogue line portrait in the array
    public int currentLine;                         // The current line in the array
    public bool acceptingInput;                     // If the object is accepting key presses, used for a key press delay
    public bool dialogueBoxActive;                  // If the text box heirarchy is visible
    public bool isMonologue;                        // If the there is not a textbox

    // Other class references
    private CharacterMovement characterMovement;   // A reference to the character movement script, used to freeze the player when a textbox is up
    private SystemConfigManager global;     // A reference to the configuration manager script, used to set the global value of if a menu is active


    // Start is called before the first frame update
    void Start()
    {
        characterMovement = FindObjectOfType<CharacterMovement>();     // Find the character movment script
        global = FindObjectOfType<SystemConfigManager>();       // Find the configuration manager script
    }


    // Key press delay
    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(0.05f);     // The delay until it is accepting input again
        acceptingInput = true;                      // Allow input again
    }


    // Update is called once per frame
    void Update()
    {
        // Continue to next dialogue
        if (dialogueBoxActive && Input.GetKeyDown("z"))
        {
            if (acceptingInput) currentLine += 1;   // Advance the line count
            StartCoroutine("acceptInput");          // Apply Key press delay
        }
       
        // End the dialogue when there are no more lines of text
        if (currentLine >= dialogueLines.Length)
        {
            dialogueBoxObject.SetActive(false);                              // Make the dialogue box heirarchy disappear
            dialogueBoxActive = false;                                       // Set the active state to false
            currentLine = 0;                                                 // Reset the current line count to zero
            StopCoroutine("acceptInput");                                    // Reset the key delay
            acceptingInput = false;                                          // Set the accepting input value to false
            //characterMovement.canMove = true;                                // Allow the player to move again
        }

        // Set text field mode to mono
        if (dialoguePortraitObject.sprite == noPortrait && dialogueBoxActive == true)
        {
            dialogueTextObjectParent.SetActive(false);  // Hide the dialogue field
            monologueTextObjectParent.SetActive(true);  // Show the monologue field
        }

        // Set text field mode to dia
        if (dialoguePortraitObject.sprite != noPortrait && dialogueBoxActive == true)
        {
            dialogueTextObjectParent.SetActive(true);       // Show the dialogue field
            monologueTextObjectParent.SetActive(false);     // Hide the monologue field
        }

        // Set the text on screen the the current line text
        dialogueTextObject.text = dialogueLines[currentLine];
        monologueTextObject.text = dialogueLines[currentLine];
        dialogueNameTextObject.text = dialogueLineNames[currentLine];
        dialoguePortraitObject.sprite = dialogueLinePortraits[currentLine];
    }


    // Setup a function to enable the dialogue boxes
    public void ShowDialogue()
    {
        if (!global.menuActive)
        {
            dialogueBoxActive = true;          // Set the active state to true
            dialogueBoxObject.SetActive(true); // Make the dialogue box heirarchy disappear
        }
    }
}
