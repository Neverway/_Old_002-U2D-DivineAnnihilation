//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Allow the player to select a name for their save profile
// Applied to: Name menu object in the title scene
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;

public class OTU_Title_NameMessages : MonoBehaviour
{
    // Public Variables
    public Text message;
    public Text actions;

    // Private Variables
    private OTU_System_InputManager inputManager;
    private OTU_Title_NameSelect nameMenu;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        nameMenu = FindObjectOfType<OTU_Title_NameSelect>();
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
                nameMenu.enabled = true;
                gameObject.SetActive(false);
            }
        }

        if (actions.text == "[" + inputManager.controls["Interact"] + "] OK")
        {
            if (Input.GetKeyDown(inputManager.controls["Interact"]))
            {
                nameMenu.enabled = true;
                gameObject.SetActive(false);
            }
        }
    }
}
