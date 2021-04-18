using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger_PlayerChoice : MonoBehaviour
{
    public string[] options;
    //public bool eventTrigger;
    public bool active;
    public UnityEvent onOption1;
    public UnityEvent onOption2;
    public UnityEvent onOption3;
    public UnityEvent onOption4;
    //private bool eventActive;

    private System_InputManager inputManager;
    private Hud_Choicebox_Manager choiceBoxManager;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
        choiceBoxManager = FindObjectOfType<Hud_Choicebox_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (choiceBoxManager.choiceBoxActive && active)
        {
            if (Input.GetKeyDown(inputManager.controls["Interact"]) && choiceBoxManager.choiceBoxObject.GetComponent<Menu_Scroll_String>().currentSelection == 0)
            {
                onOption1.Invoke();
            }
            if (Input.GetKeyDown(inputManager.controls["Interact"]) && choiceBoxManager.choiceBoxObject.GetComponent<Menu_Scroll_String>().currentSelection == 1)
            {
                onOption2.Invoke();
            }
            if (Input.GetKeyDown(inputManager.controls["Interact"]) && choiceBoxManager.choiceBoxObject.GetComponent<Menu_Scroll_String>().currentSelection == 2)
            {
                onOption3.Invoke();
            }
            if (Input.GetKeyDown(inputManager.controls["Interact"]) && choiceBoxManager.choiceBoxObject.GetComponent<Menu_Scroll_String>().currentSelection == 3)
            {
                onOption4.Invoke();
            }
        }
    }
    /*
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox")
        {
            if (options.Length > 4 || options.Length < 2)
            {
                Debug.LogWarning("[ID002 DA]: " + "In Trigger_PlayerChoice options length needs to be between 2-4!");
            }

            // Interact trigger
            if (Input.GetKeyDown(inputManager.controls["Interact"]) && !eventTrigger)
            {
                choiceBoxManager.currentTarget = gameObject.GetComponent<Trigger_PlayerChoice>();
                choiceBoxManager.enableChoiceBox();
            }

            // Event trigger
            if (eventTrigger)
            {
                choiceBoxManager.currentTarget = gameObject.GetComponent<Trigger_PlayerChoice>();
                choiceBoxManager.enableChoiceBox();
            }
        }
    }
    */
    public void EnterChoiceBox()
    {
        Debug.Log("EnteredCB");
        active = true;
        choiceBoxManager.currentTarget = gameObject.GetComponent<Trigger_PlayerChoice>();
        choiceBoxManager.enableChoiceBox();
    }

    public void ExitChoiceBox()
    {
        Debug.Log("ExitedCB");
        active = false;
        choiceBoxManager.currentTarget = null;
        choiceBoxManager.disableChoiceBox();
        gameObject.SetActive(false);
    }
}
