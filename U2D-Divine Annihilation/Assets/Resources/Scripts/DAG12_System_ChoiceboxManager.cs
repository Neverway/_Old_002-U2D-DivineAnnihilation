//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DAG12_System_ChoiceboxManager : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    public bool debugActivate;


    //=-----------------=
    // Private variables
    //=-----------------=
    public bool choiceboxOpen;
    private bool keypressDelayed;
    private string currentOutput;   // The current text that is being displayed in the textbox's main content area, used for to display letter by letter text
    public float defaultTextSpeed = 0.2f;


    //=-----------------=
    // Reference variables
    //=-----------------=
    [Header ("READ-ONLY")]
    public GameObject currentTrigger;       // Which trigger object has been activated that assigned the information present in the textboxData array
    public ChoiceboxData choiceboxData;       // An array of textbox data assigned by an activated text trigger

    private GameObject promptField;
    private GameObject response1Field;
    private GameObject response2Field;

    private NUPInput inputManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        promptField = transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        response1Field = transform.GetChild(1).gameObject.transform.GetChild(1).gameObject;
        response2Field = transform.GetChild(1).gameObject.transform.GetChild(2).gameObject;

        inputManager = FindObjectOfType<NUPInput>();
    }

    public IEnumerator KeypressDelay(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        keypressDelayed = true;
    }

    private void Update()
    {
        promptField.GetComponent<Text>().text = choiceboxData.promptText;
        response1Field.GetComponent<Text>().text = choiceboxData.response1;
        response2Field.GetComponent<Text>().text = choiceboxData.response2;

        if (debugActivate)
        {
            if (!choiceboxOpen)
            {
                ChoiceboxActivate();
            }
            debugActivate = false;
        }

        if (Input.GetKeyDown(inputManager.controls["Interact"]) && choiceboxOpen && keypressDelayed)
        {
            ///TextboxNext();
            CloseChoicebox();
        }
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    [System.Serializable]
    public class ChoiceboxData
    {
        public string promptText;
        public string response1;
        public string response2;
    }

    public void SetupChoicebox()
    {
        // Decide how many choices the player is given (not used right now)
        /*
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
        }*/
    }

    public void OpenChoicebox()
    {
        keypressDelayed = false; // Enable the keypress delay to keep the box from imediately closing
        StartCoroutine(KeypressDelay(0.1f));     // Start the keypress delay countdown

        transform.GetChild(1).gameObject.SetActive(true);
        choiceboxOpen = true;
    }
    
    public void CloseChoicebox()
    {
        //choiceboxData.promptText = "";
        //choiceboxData.response1 = "";
        //choiceboxData.response2 = "";

        // May move these somewhere else since this makes the textbox manager dependent on an interact trigger
        currentTrigger.GetComponent<DAG12_Trigger_Choice>().OnFinish.Invoke();

        transform.GetChild(1).gameObject.SetActive(false);
        choiceboxOpen = false;
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void ChoiceboxActivate()
    {
        SetupChoicebox();
        OpenChoicebox();
    }
}
