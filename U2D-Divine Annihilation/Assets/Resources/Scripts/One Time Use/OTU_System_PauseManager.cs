//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: MRC
// Purpose: Control the pause menu
// Applied to: Pause Manager object in an overworld scene
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OTU_System_PauseManager : MonoBehaviour
{
    public bool pauseMenuOpen;
    public bool acceptingInput = true;

    private bool inSubMenu;

    private OTU_System_MenuManager menuManager;
    private OTU_System_InputManager inputManager;


    void Start()
    {
        menuManager = FindObjectOfType<OTU_System_MenuManager>();
        inputManager = FindObjectOfType<OTU_System_InputManager>();
    }


    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(1);     // The delay until it is accepting input again
        acceptingInput = true;                  // Allow input again
    }


    void Update()
    {
        // Open the inventory
        if (!pauseMenuOpen && !menuManager.menuActive && Input.GetKeyDown(inputManager.controls["Menu"]) && acceptingInput)
        {
            OpenPauseMenu();
        }

        // Close the inventory
        else if (pauseMenuOpen && menuManager.menuActive && Input.GetKeyDown(inputManager.controls["Menu"]) && acceptingInput && !inSubMenu)
        {
            ClosePauseMenu();
        }
    }


    public void OpenPauseMenu()
    {
        acceptingInput = false;
        pauseMenuOpen = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        StartCoroutine(acceptInput());  // Apply Key press delay
    }


    public void ClosePauseMenu()
    {
        gameObject.transform.GetChild(0).GetComponent<DA_Menu_Control>().ResetCurrentSelection();
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        acceptingInput = false;
        pauseMenuOpen = false;
        inSubMenu = false;
        StartCoroutine(acceptInput());  // Apply Key press delay
    }


    public void OpenSubMenu()
    {
        inSubMenu = true;
    }


    public void CloseSubMenu()
    {
        inSubMenu = false;
    }


    public void QuitToTitle()
    {
        SceneManager.LoadScene("Main_Title");
    }
}