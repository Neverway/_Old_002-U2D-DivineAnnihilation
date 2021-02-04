//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Display the players inventory
// Applied to: DialogueManager object in an overworld scene
//
//=============================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Textbox_Manager : MonoBehaviour
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
    public bool destroyOnFinish;
    public bool EventTrigger;
    public bool EventActive;

    public float textSpeed = 0.1f;
    public string textContent;
    public string textCurrent = "";

    public GameObject targetTrigger;
    private Entity_Character_Movement characterMovement;   // A reference to the character movement script, used to freeze the player when a textbox is up
    private System_Config_Manager global;     // A reference to the configuration manager script, used to set the global value of if a menu is active


    void Start()
    {
        characterMovement = FindObjectOfType<Entity_Character_Movement>();     // Find the character movment script
        global = FindObjectOfType<System_Config_Manager>();       // Find the configuration manager script
    }


    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(0.05f);     // The delay until it is accepting input again
        acceptingInput = true;                      // Allow input again
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < textContent.Length+1; i++)
        {
            textCurrent = textContent.Substring(0, i);
            yield return new WaitForSeconds(textSpeed);
        }
    }


    void Update()
    {
        if (dialogueBoxActive)
        {
            textContent = dialogueLines[currentLine];
        }

        // Continue to next dialogue
        if (dialogueBoxActive && Input.GetButtonDown("Interact") && !EventTrigger)
        {
            if (acceptingInput) currentLine += 1;   // Advance the line count
            StartCoroutine(ShowText());
            textCurrent = "";
            StartCoroutine("acceptInput");          // Apply Key press delay
        }
        // Continue to next dialogue
        if (dialogueBoxActive && EventTrigger && !EventActive)
        {
            Debug.Log("FIRED");
            if (acceptingInput) currentLine += 1;   // Advance the line count
            StartCoroutine(ShowText());
            textCurrent = "";
            StartCoroutine("acceptInput");          // Apply Key press delay
            EventActive = true;
        }
        // Continue to next dialogue
        else if (dialogueBoxActive && Input.GetButtonDown("Interact") && EventTrigger && EventActive)
        {
            if (acceptingInput) currentLine += 1;   // Advance the line count
            StartCoroutine(ShowText());
            textCurrent = "";
            StartCoroutine("acceptInput");          // Apply Key press delay
        }


        // End the dialogue when there are no more lines of text
        if (currentLine >= dialogueLines.Length)
        {
            dialogueBoxObject.SetActive(false);                              // Make the dialogue box heirarchy disappear
            dialogueBoxActive = false;                                       // Set the active state to false
            currentLine = 0;                                                 // Reset the current line count to zero
            StopCoroutine("acceptInput");                                    // Reset the key delay
            StopCoroutine("ShowText");
            if (destroyOnFinish)
            {
                targetTrigger.GetComponent<Trigger_Interact>().startDestroy = true;
            }

            dialogueTextObject.text = "";
            monologueTextObject.text = "";
            dialogueNameTextObject.text = "";
            dialoguePortraitObject.sprite = noPortrait;

            acceptingInput = false;                                          // Set the accepting input value to false
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

        dialogueTextObject.text = textCurrent;
        monologueTextObject.text = textCurrent;
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
