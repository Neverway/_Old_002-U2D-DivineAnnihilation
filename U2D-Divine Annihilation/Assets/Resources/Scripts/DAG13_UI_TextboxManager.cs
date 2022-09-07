//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DAG13_UI_TextboxManager : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    public DAG13_TextboxData textboxData;
    public bool active;


    //=-----------------=
    // Private variables
    //=-----------------=
    private int currentIndex; // Which textbox data's line is currently being displayed
    private int portraitIndex; // Which portrait is being displayed in the current line's portrait array
    private string displayMode; // Is the textbox displaying monologue or dialogue
    private string currentText = "";
    private DAG13_TextboxData.TextboxLine currentLine;


    //=-----------------=
    // Reference variables
    //=-----------------=
    private NUPInput input;
    [SerializeField] private TMP_Text nameField;
    [SerializeField] private Image portraitField;
    [SerializeField] private TMP_Text diaTextField;
    [SerializeField] private TMP_Text monoTextField;
    [SerializeField] private AudioSource chatter;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        input = FindObjectOfType<NUPInput>();
    }
    
    private IEnumerator TypePrint(string _text, TMP_Text _output)
    {
        var offBeat = true;
        foreach (string letter in MarkupParsedText(_text))
        {
            currentText += letter;
            // Set return text
            _output.text = currentText;
            
            yield return new WaitForSeconds(0.03f);
            
            // Chatter sound
            if (offBeat)
            {
                chatter.Play();
                offBeat = false;
            }
            else
            {
                offBeat = true;
            }
        }
    }    
    
    private static IEnumerable MarkupParsedText(string _text)
    {
        var markup = false;
        var markupTag = "";
        foreach (var letter in _text)
        {
            var currentLetter = letter.ToString();
            if (currentLetter == "<")
            {
                markup = true;
            }
            if (markup)
            {
                markupTag += letter;
                if (currentLetter != ">") continue;
                markup = false;
                yield return markupTag;
                markupTag = "";
            }
            else
            {
                yield return currentLetter;
            }
        }
    }
    
    private void Update()
    {
        if (!input.GetKeyDown("Interact") || active == false) return;
        if (currentText == currentLine.textContent)
            NextLine();
    }


    //=-----------------=
    // Internal Functions
    //=-----------------=
    // Figure out which mode the textbox should be based on the current line's data
    private void SetTextboxDisplayMode(DAG13_TextboxData.TextboxLine _currentLine)
    {
        switch (_currentLine.name)
        {
            case "":
                displayMode = "Mono";
                break;
            default:
                displayMode = "Dia";
                break;
        }
    }

    // Update the values and enable the game objects for the current textbox
    private void DisplayTextbox()
    {
        // Update reference to the current line
        currentLine = textboxData.textboxLines[currentIndex];
        // Clear the current text content
        currentText = "";
        // Set starting portrait index
        portraitIndex = 0;
        
        // Identify the textbox display mode
        SetTextboxDisplayMode(currentLine);

        // Assign starting data and begin typing the current text content
        if (displayMode == "Dia")
        {
            // Hide the unused fields
            monoTextField.gameObject.SetActive(false);
            
            // Show the used fields
            nameField.gameObject.SetActive(true);
            portraitField.gameObject.SetActive(true);
            diaTextField.gameObject.SetActive(true);
            
            // Show the textbox
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            active = true;
            
            // Assign name
            nameField.text = currentLine.name;
            // Assign starting portrait
            portraitField.sprite = currentLine.portrait[portraitIndex];
            // Begin typing the current text content
            StartCoroutine(TypePrint(currentLine.textContent, diaTextField));
        }
        else
        {
            // Hide the unused fields
            nameField.gameObject.SetActive(false);
            portraitField.gameObject.SetActive(false);
            diaTextField.gameObject.SetActive(false);
            
            // Show the used fields
            monoTextField.gameObject.SetActive(true);
            
            // Show the textbox
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            active = true;
            
            // Begin typing the current text content
            StartCoroutine(TypePrint(currentLine.textContent, monoTextField));
        }
    }

    private void NextLine()
    {
        if ((currentIndex + 1) == textboxData.textboxLines.Length)
        {
            textboxData.onFinishTextbox.Invoke();
            CloseTextbox();
        }
        else
        {
            // Increase the current index
            currentIndex++;
        
            DisplayTextbox();
        }
    }

    private void CloseTextbox()
    {
        // Hide the mono fields
        monoTextField.gameObject.SetActive(false);
            
        // Hide the dia fields
        nameField.gameObject.SetActive(false);
        portraitField.gameObject.SetActive(false);
        diaTextField.gameObject.SetActive(false);
            
        // Hide the textbox
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        active = false;
    }


    //=-----------------=
    // External Functions
    //=-----------------=
    public void StartTextbox()
    {
        // Set starting index
        currentIndex = 0;
        
        DisplayTextbox();
    }
}
