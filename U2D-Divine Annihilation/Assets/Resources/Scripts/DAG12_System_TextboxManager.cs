//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: textboxdata array starts at 16 length because an error is thrown if
// the first textboxdata sent to it from a trigger is higher than the starting
// length. I have no idea why this happens, but it's now a problem for future me.
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DAG12_System_TextboxManager : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    public bool debugActivate;


    //=-----------------=
    // Private variables
    //=-----------------=
    public bool textboxOpen;
    private int currentLine = 0;    // (0 indexed, 0 = 1st slot in the array) Used for determining the current position in the textboxData array
    private string currentOutput;   // The current text that is being displayed in the textbox's main content area, used for to display letter by letter text
    public float defaultTextSpeed = 0.2f;


    //=-----------------=
    // Reference variables
    //=-----------------=
    [Header ("READ-ONLY")]
    public GameObject currentTrigger;       // Which trigger object has been activated that assigned the information present in the textboxData array
    public TextboxData[] textboxData;       // An array of textbox data assigned by an activated text trigger
    public TextboxData currentTextboxData;  // The current textbox data to display from the textboxData array, determined by the value currentLine

    private GameObject[] textField;     // Monologue & dialogue text bodys references (mono is array position 0, dia is array position 1)
    private GameObject nameField;       // The textbox's name field reference
    private GameObject portraitField;   // The textbox's portrait field reference
    
    private GameObject nextIndicator;   // 
    private GameObject doneIndicator;   // 

    private NUPInput inputManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        System.Array.Resize(ref textField, 2);
        textField[0] = transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;
        textField[1] = transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        nameField = transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;
        portraitField = transform.GetChild(0).gameObject.transform.GetChild(3).gameObject;

        nextIndicator = transform.GetChild(0).gameObject.transform.GetChild(4).gameObject;
        doneIndicator = transform.GetChild(0).gameObject.transform.GetChild(5).gameObject;

        inputManager = FindObjectOfType<NUPInput>();
    }

    public IEnumerator DrawText(string _input, float _speed)
    {
        for (int i = 0; i < _input.Length + 1; i++)
        {
            currentOutput = _input.Substring(0, i);
            if (_speed == 0)
            {
                yield return new WaitForSeconds(defaultTextSpeed); // Text character appear delay
            }
            else if (_speed == -1)
            {
                yield return new WaitForSeconds(0); // Text character appear delay
            }
            else
            {
                yield return new WaitForSeconds(_speed); // Text character appear delay
            }
        }
        // Once done printing, enable the continuing indicator
        if (currentOutput.Length == currentTextboxData.lineText.Length)
        {
            // If this is not the last line
            if ((currentLine + 1) < textboxData.Length)
            {
                nextIndicator.SetActive(true);
            }
            else
            {
                doneIndicator.SetActive(true);
            }
        }
    }

    private void Update()
    {
        textField[0].GetComponent<Text>().text = currentOutput;
        textField[1].GetComponent<Text>().text = currentOutput;
        nameField.GetComponent<Text>().text = currentTextboxData.lineName;
        portraitField.GetComponent<Image>().sprite = currentTextboxData.linePortrait;

        if (debugActivate)
        {
            if (!textboxOpen)
            {
                TextboxActivate();
            }
            else
            {
                TextboxNext();
            }
            debugActivate = false;
        }

        if (Input.GetKeyDown(inputManager.controls["Interact"]) && textboxOpen)
        {
            TextboxNext();
        }
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    [System.Serializable]
    public class TextboxData
    {
        public string lineText;
        public string lineName;
        public Sprite linePortrait;
        public float lineSpeed; // Divide by ten to get a resonable speed, default should be 1 (0.1 after division)
    }

    public void SetCurrentTextboxData()
    {
        // Pull a textboxData value from the textboxData array based off of which line should be printed
        currentTextboxData = textboxData[currentLine];
    }

    public void SetupTextbox()
    {
        // Decide if textbox is mono or dia based off of if a portrait is presented
        if (textboxData[currentLine].linePortrait == null)
        {
            // Mono
            textField[0].SetActive(true);
            textField[1].SetActive(false);
            nameField.SetActive(false);
            portraitField.SetActive(false);

            nextIndicator.SetActive(false);
            doneIndicator.SetActive(false);
        }
        else
        {
            // Dia
            textField[0].SetActive(false);
            textField[1].SetActive(true);
            nameField.SetActive(true);
            portraitField.SetActive(true);

            nextIndicator.SetActive(false);
            doneIndicator.SetActive(false);
        }
    }

    public void OpenTextbox()
    {
        currentOutput = "";
        currentLine = 0;
        transform.GetChild(0).gameObject.SetActive(true);
        textboxOpen = true;
    }
    
    public void CloseTextbox()
    {
        currentOutput = "";
        currentLine = 0;

        // May move these somewhere else since this makes the textbox manager dependent on an interact trigger
        currentTrigger.GetComponent<DAG12_Trigger_Interact>().OnFinish.Invoke();

        transform.GetChild(0).gameObject.SetActive(false);
        textboxOpen = false;
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    // Type a string letter by letter to a Unity text object based off of a speed
    public void TypeText(string _input, float _speed)
    {
        StartCoroutine(DrawText(_input, _speed));     // Start drawing the first line
    }

    public void TextboxActivate()
    {

        SetCurrentTextboxData();
        SetupTextbox();
        OpenTextbox();
        
        TypeText(currentTextboxData.lineText, currentTextboxData.lineSpeed);
    }
    
    public void TextboxNext()
    {
        // Is the current line complete
        if (currentOutput.Length == currentTextboxData.lineText.Length)
        {
            // If this is not the last line
            if ((currentLine + 1) < textboxData.Length)
            {
                currentOutput = "";
                currentLine += 1;

                SetCurrentTextboxData();
                SetupTextbox();

                TypeText(currentTextboxData.lineText, currentTextboxData.lineSpeed);
                nextIndicator.SetActive(false);
                doneIndicator.SetActive(false);
            }
            else
            {
                nextIndicator.SetActive(false);
                doneIndicator.SetActive(false);
                CloseTextbox();
            }
        }
    }
}
