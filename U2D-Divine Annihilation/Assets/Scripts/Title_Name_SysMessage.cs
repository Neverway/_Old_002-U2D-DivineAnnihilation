//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Allow the player to select a name for their save profile
// Applied to: Name menu object in the title scene
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;

public class Title_Name_SysMessage : MonoBehaviour
{
    // Public Variables
    public Text message;
    public Text actions;

    // Private Variables
    private System_InputManager inputManager;
    private Title_Name nameMenu;

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
        nameMenu = FindObjectOfType<Title_Name>();
    }


    void Update()
    {
        if (actions.text == "[" + inputManager.controls["Interact"] + "] Confirm   [" + inputManager.controls["Action"] + "] Cancel")
        {
            if (Input.GetKeyDown(inputManager.controls["Interact"]))
            {
                nameMenu.AcceptConfirm();
                gameObject.SetActive(false);
            }

            if (Input.GetKeyDown(inputManager.controls["Action"]))
            {
                nameMenu.active = true;
                gameObject.SetActive(false);
            }
        }

        if (actions.text == "[" + inputManager.controls["Interact"] + "] OK")
        {
            if (Input.GetKeyDown(inputManager.controls["Interact"]))
            {
                nameMenu.active = true;
                gameObject.SetActive(false);
            }
        }
    }
}
