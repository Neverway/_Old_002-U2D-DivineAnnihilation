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

    public float doublClickSpeed = 0.4f;
    public float textSpeed = 0.1f;
    public string textContent;
    public string textCurrent = "";

    private bool dialogueInitialized;
    private float lastClickedTime;
    public GameObject targetTrigger;
    private System_InputManager inputManager;
    private Entity_Character_Movement characterMovement;   // A reference to the character movement script, used to freeze the player when a textbox is up
    private System_Config_Manager global;     // A reference to the configuration manager script, used to set the global value of if a menu is active


    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
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
            for (int i = 0; i < textContent.Length + 1; i++)
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
        else
        {
            textContent = null;
            textCurrent = "";
        }

        // Continue to next dialogue or start dialogue
        if (dialogueBoxActive && Input.GetKeyDown(inputManager.controls["Interact"]) && !EventTrigger)
        {
            if (!dialogueInitialized)
            {
                if (acceptingInput) currentLine += 1;   // Advance the line count
                StartCoroutine(ShowText());
                textCurrent = "";
                dialogueTextObject.text = "";
                monologueTextObject.text = "";
                StartCoroutine("acceptInput");          // Apply Key press delay
                dialogueInitialized = true;
            }
            //else if (textCurrent != textContent)
            //{
            //    textCurrent = textContent;
            //    StartCoroutine("acceptInput");          // Apply Key press delay
            //}
            else if (textCurrent == textContent)
            {
                if (acceptingInput) currentLine += 1;   // Advance the line count
                StartCoroutine(ShowText());
                textCurrent = "";
                dialogueTextObject.text = "";
                monologueTextObject.text = "";
                StartCoroutine("acceptInput");          // Apply Key press delay
            }
        }

        // Skip dialogue
        //if (dialogueBoxActive && Input.GetKeyDown(inputManager.controls["Interact"]) && !EventTrigger && textCurrent != textContent)
        //{
        //    float timeSinceLastClick = Time.time - lastClickedTime;
        //    if (timeSinceLastClick <= doublClickSpeed)
        //    {
        //        textCurrent = textContent;
        //        StopAllCoroutines();
        //        StartCoroutine("acceptInput");          // Apply Key press delay
        //    }
        //    lastClickedTime = Time.time;
        //}
        if (dialogueBoxActive && Input.GetKeyDown(inputManager.controls["Action"]))
        {
                textCurrent = textContent;
                StopAllCoroutines();
                StartCoroutine("acceptInput");          // Apply Key press delay
        }

        // Start dialogue if it's an event
        if (dialogueBoxActive && EventTrigger && !EventActive)
        {
            Debug.Log("FIRED");
            if (acceptingInput) currentLine += 1;   // Advance the line count
            StartCoroutine(ShowText());
            textCurrent = "";
            dialogueTextObject.text = "";
            monologueTextObject.text = "";
            StartCoroutine("acceptInput");          // Apply Key press delay
            EventActive = true;
        }
        // Continue to next dialogue in an event trigger
        else if (dialogueBoxActive && Input.GetKeyDown(inputManager.controls["Interact"]) && EventTrigger && EventActive)
        {
            if (!dialogueInitialized)
            {
                if (acceptingInput) currentLine += 1;   // Advance the line count
                StartCoroutine(ShowText());
                textCurrent = "";
                dialogueTextObject.text = "";
                monologueTextObject.text = "";
                StartCoroutine("acceptInput");          // Apply Key press delay
                dialogueInitialized = true;
            }
            //else if (textCurrent != textContent)
            //{
            //    textCurrent = textContent;
            //    StartCoroutine("acceptInput");          // Apply Key press delay
            //}
            else if (textCurrent == textContent)
            {
                if (acceptingInput) currentLine += 1;   // Advance the line count
                StartCoroutine(ShowText());
                textCurrent = "";
                dialogueTextObject.text = "";
                monologueTextObject.text = "";
                StartCoroutine("acceptInput");          // Apply Key press delay
            }
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

            dialogueInitialized = false;
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
            textCurrent = "";
            dialogueBoxActive = true;          // Set the active state to true
            dialogueBoxObject.SetActive(true); // Make the dialogue box heirarchy disappear
        }
    }

    public void ForceClearDialogue()
    {
        Debug.Log("Clearing...");
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

        dialogueInitialized = false;
        acceptingInput = false;
    }
}
