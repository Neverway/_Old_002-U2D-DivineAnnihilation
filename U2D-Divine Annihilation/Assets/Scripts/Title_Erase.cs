//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Give functionality to the erase save profile button on the title screen
// Applied to: OverwriteFile menu on the title screen
//
//=============================================================================

using UnityEngine;

public class Title_Erase : MonoBehaviour
{
    public GameObject selfTarget;
    public GameObject loadMenu;
    public GameObject savesMenu;
    public GameObject configTarget;
    private System_InputManager inputManager;
    private Menu_Scroll_MinusControl menu;
    private SaveManager saveManager;

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
        menu = selfTarget.GetComponent<Menu_Scroll_MinusControl>();
        saveManager = configTarget.GetComponent<SaveManager>();
    }


    void Update()
    {
        if (Input.GetKeyDown(inputManager.controls["Action"]))
        {
            menu.currentFrame = 0;
            loadMenu.SetActive(true);
            selfTarget.SetActive(false);
        }

        if (Input.GetKeyDown(inputManager.controls["Interact"]))
        {
            if (menu.currentFrame == 0)
            {
                selfTarget.SetActive(false);
                loadMenu.SetActive(true);
            }
        }

        if (Input.GetKeyDown(inputManager.controls["Interact"]))
        {
            if (menu.currentFrame == 1)
            {
                saveManager.DeleteSaveProfile();
                selfTarget.SetActive(false);
                savesMenu.SetActive(true);
            }
        }
    }
}

