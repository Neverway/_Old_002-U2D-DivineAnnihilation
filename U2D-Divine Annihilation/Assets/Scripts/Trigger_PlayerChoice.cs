using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger_PlayerChoice : MonoBehaviour
{
    public string[] options;
    public bool eventTrigger;
    public UnityEvent onOption1;
    public UnityEvent onOption2;
    public UnityEvent onOption3;
    public UnityEvent onOption4;
    private bool eventActive;

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

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox")
        {// Check if the player has pressed the action key
            if (Input.GetKeyDown(inputManager.controls["Interact"]) && !eventTrigger)
            {
                if (options.Length > 4 || options.Length < 2)
                {
                    Debug.LogWarning("[ID002 DA]: " + "In Trigger_PlayerChoice options length needs to be between 2-4!");
                }
                choiceBoxManager.currentTarget = gameObject.GetComponent<Trigger_PlayerChoice>();
                choiceBoxManager.enableChoiceBox();
            }
            if (eventTrigger && !eventActive)
            {
                if (options.Length > 4 || options.Length < 2)
                {
                    Debug.LogWarning("[ID002 DA]: " + "In Trigger_PlayerChoice options length needs to be between 2-4!");
                }
                choiceBoxManager.currentTarget = gameObject.GetComponent<Trigger_PlayerChoice>();
                choiceBoxManager.enableChoiceBox();
            }
        }
    }
}
