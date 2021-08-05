//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: To handle inputs from interact triggers and other text events
//          so they can be displayed in a textbox
// Applied to: A Dialogue Manager in an overworld scene
// Editor script: DASDK_System_TextboxManager (currently not being used)
// Notes: Still needs more comments, and a little bit of cleaning up
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OTU_System_TextboxManager : MonoBehaviour
{
    // Public variables
    public string[] lineText;
    public string[] lineName;
    public Sprite[] linePortrait;

    // Mark-up prefixes
    public string portraitPrefix = "{/p/}";         // Jump to the next portrait in the list

    public string speedPrefix = "{/s/}";            // Followed by a + for speed up, a - for speed down, or an > for normal speed
    public string autoAdvancePrefix = "{/aa/}";     // Auto advances the textbox to the next line without a keypress being needed (put at the end of a line of text)

    public string coloredTextPrefix = "{/c/}";      // Followed by a six digit hex code for the color (#000000)
    public string boldTextPrefix = "{/b/}";         // Makes the text bold
    public string underlineTextPrefix = "{/u/}";    // Underlines the text
    public string italicsTextPrefix = "{/i/}";      // Makes the text italic
    public string regularTextPrefix = "{/r/}";      // Resets speed, color, bold, underline, and italics


    // Private variables
    public int currentTextLine;
    public int currentPortraitLine;
    public bool textboxActive;
    public bool isMonologue;
    public bool eventTrigger;
    public float textScrollSpeed;
    public string textContent;
    public string textCurrent;

    public bool textboxInitialized;
    public bool acceptingInput;

    public bool eventActive;

    // Reference variables
    public Sprite noPortrait;
    public Text dialogueText;
    public Text monologueText;
    public Text dialogueName;
    public Image characterPortrait;
    public GameObject targetTrigger;
    public OTU_System_InputManager inputManager;


    void Start()
    {
        dialogueText = gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Text>();
        monologueText = gameObject.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Text>();
        dialogueName = gameObject.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Text>();
        characterPortrait = gameObject.transform.GetChild(0).GetChild(3).gameObject.GetComponent<Image>();
        inputManager = FindObjectOfType<OTU_System_InputManager>();
    }


    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(1);     // The delay until it is accepting input again
        acceptingInput = true;                  // Allow input again
    }


    IEnumerator DrawText()
    {
        for (int i = 0; i < textContent.Length + 1; i++)
        {
            textCurrent = textContent.Substring(0, i);
            yield return new WaitForSeconds(textScrollSpeed); // Text character appear delay
        }
    }


    void Update()
    {
        UpdateTextbox();
        AdvanceTextbox();
        AdvanceEventTextbox();
        SkipTextLine();
        FinishTextbox();

        // Set text field mode to mono
        if (characterPortrait.sprite == noPortrait && textboxActive == true)
        {
            Debug.Log("Mono");
            dialogueText.gameObject.SetActive(false);  // Hide the dialogue field
            monologueText.gameObject.SetActive(true);  // Show the monologue field
        }

        // Set text field mode to dia
        if (characterPortrait.sprite != noPortrait && textboxActive == true)
        {
            Debug.Log("Dia");
            dialogueText.gameObject.SetActive(true);  // Hide the dialogue field
            monologueText.gameObject.SetActive(false);  // Show the monologue field
        }

        // Set the text on screen the the current line text

        dialogueText.text = textCurrent;
        monologueText.text = textCurrent;
        dialogueName.text = lineName[currentTextLine];
        characterPortrait.sprite = linePortrait[currentPortraitLine];
    }


    // ================
    // Custom functions
    // ================
    void UpdateTextbox()
    {
        // Store the current line of text so it can slowly be displayed through textCurrent
        if (textboxActive)
        {
            textContent = lineText[currentTextLine];
        }
        // Clear the variables for storing text when the textbox is not active
        // This helps to keep the textbox clean when the player interacts with a new object 
        // Otherwise we would see the text from the previouse interaction quickly appear and disappear before displaying the new text
        else
        {
            textContent = null;
            textCurrent = null;
        }
    }

    void AdvanceTextbox()
    {
        // Initialize textbox by clearing the textbox (just in case there is anything left from a previouse interaction) and drawing the first line
        if (!textboxInitialized && textboxActive)
        {
            // Keypress delay
            if (acceptingInput)
            {
                currentTextLine += 1;       // Advance the text line count
                currentPortraitLine += 1;   // Advance the portrait line count
            }
            StartCoroutine(DrawText());     // Start drawing the first line
            textCurrent = "";               // Clear the current text (just in case)
            dialogueText.text = "";         // Clear the dialogue text (just in case)
            monologueText.text = "";        // Clear the monologue text (just in case)
            StartCoroutine(acceptInput());  // Apply Key press delay
            textboxInitialized = true;      // Finish the initialization
        }

        //  FutureUCC: I don't know what the purpose of having the intialization check is but I'm leaving it in for now because I don't want to break anything just yet
        if (textboxActive && Input.GetKeyDown(inputManager.controls["Interact"]) && !eventTrigger)
        {

            // Advance the text, on a player button press, if the character is done talking
            if (textCurrent == textContent)
            {
                // Keypress delay
                if (acceptingInput)
                {
                    currentTextLine += 1;       // Advance the text line count
                    currentPortraitLine += 1;   // Advance the portrait line count
                    StartCoroutine(DrawText());     // Start drawing the first line
                    textCurrent = "";
                    dialogueText.text = "";         // Clear the dialogue text (just in case)
                    monologueText.text = "";        // Clear the monologue text (just in case)
                    StartCoroutine("acceptInput");  // Apply Key press delay
                }
            }
        }
    }

    void SkipTextLine()
    {
        if (textboxActive && Input.GetKeyDown(inputManager.controls["Action"]))
        {
            textCurrent = textContent;
            StopAllCoroutines();
            acceptingInput = true;
            //StartCoroutine("acceptInput");  // Apply Key press delay
        }
    }

    void AdvanceEventTextbox()
    {
        // Automatically initialize textbox if it's an event
        if (textboxActive && eventTrigger && !eventActive)
        {
            // Keypress delay
            if (acceptingInput)
            {
                currentTextLine += 1;       // Advance the text line count
                currentPortraitLine += 1;   // Advance the portrait line count
            }
            StartCoroutine(DrawText());     // Start drawing the first line
            textCurrent = "";
            dialogueText.text = "";         // Clear the dialogue text (just in case)
            monologueText.text = "";        // Clear the monologue text (just in case)
            StartCoroutine("acceptInput");  // Apply Key press delay
            eventActive = true;
        }

        // Continue to next dialogue in an event trigger
        else if (textboxActive && Input.GetKeyDown(inputManager.controls["Interact"]) && eventTrigger && eventActive)
        {
            // Initialize textbox by clearing the textbox (just in case there is anything left from a previouse interaction) and drawing the first line
            if (!textboxInitialized)
            {
                // Keypress delay
                if (acceptingInput)
                {
                    currentTextLine += 1;       // Advance the text line count
                    currentPortraitLine += 1;   // Advance the portrait line count
                }
                StartCoroutine(DrawText());     // Start drawing the first line
                textCurrent = "";               // Clear the current text (just in case)
                dialogueText.text = "";         // Clear the dialogue text (just in case)
                monologueText.text = "";        // Clear the monologue text (just in case)
                StartCoroutine("acceptInput");  // Apply Key press delay
                textboxInitialized = true;      // Finish the initialization
            }

            // Advance the text, on a player button press, if the character is done talking
            else if (textCurrent == textContent)
            {
                // Keypress delay
                if (acceptingInput)
                {
                    currentTextLine += 1;       // Advance the text line count
                    currentPortraitLine += 1;   // Advance the portrait line count
                }
                StartCoroutine(DrawText());     // Start drawing the first line
                textCurrent = "";
                dialogueText.text = "";         // Clear the dialogue text (just in case)
                monologueText.text = "";        // Clear the monologue text (just in case)
                StartCoroutine("acceptInput");  // Apply Key press delay
            }
        }
    }

    void FinishTextbox()
    {
        // End the dialogue when there are no more lines of text
        if (currentTextLine >= lineText.Length)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);                              // Make the dialogue box heirarchy disappear
            textboxActive = false;                                       // Set the active state to false
            currentTextLine = 0;                                                 // Reset the current line count to zero
            currentPortraitLine = 0;                                                 // Reset the current line count to zero
            StopAllCoroutines();
            StopCoroutine("acceptInput");
            StopCoroutine("ShowText");

            //if (destroyOnFinish)
            //{
            //    targetTrigger.GetComponent<Trigger_Interact>().startDestroy = true;
            //}

            dialogueText.text = "";
            monologueText.text = "";
            dialogueName.text = "";
            characterPortrait.sprite = null;

            textboxInitialized = false;
            acceptingInput = false;                                          // Set the accepting input value to false
            targetTrigger.GetComponent<DA_Trigger_Interact>().OnFinish.Invoke();
        }
    }

    public void ShowDialogue()
    {
            textCurrent = "";
            textboxActive = true;          // Set the active state to true
            gameObject.transform.GetChild(0).gameObject.SetActive(true); // Make the dialogue box heirarchy disappear
    }
}