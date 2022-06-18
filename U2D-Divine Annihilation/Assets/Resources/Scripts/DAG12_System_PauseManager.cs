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
using UnityEngine.SceneManagement;

public class DAG12_System_PauseManager : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    public bool pauseMenuOpen;


    //=-----------------=
    // Private variables
    //=-----------------=
    private bool confirmQuitMenuOpen;


    //=-----------------=
    // Reference variables
    //=-----------------=
    private NUPInput inputManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        inputManager = FindObjectOfType<NUPInput>();
    }

    private void Update()
    {
        if (!pauseMenuOpen && Input.GetKeyDown(inputManager.controls["Start"]))
        {
            OpenPauseMenu();
        }
        else if (pauseMenuOpen && !confirmQuitMenuOpen && Input.GetKeyDown(inputManager.controls["Start"]))
        {
            ClosePauseMenu();
        }
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void OpenPauseMenu()
    {
        pauseMenuOpen = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(true); // Enable the pause screen
    }

    public void ClosePauseMenu()
    {
        gameObject.transform.GetChild(0).GetComponent<DAG12_Menu_Control>().ResetCurrentSelection(); // Reset menu control position for pause screen
        gameObject.transform.GetChild(0).gameObject.SetActive(false); // Disable the pause screen
        pauseMenuOpen = false;
        confirmQuitMenuOpen = false;
    }

    public void OpenConfirmQuitMenu()
    {
        confirmQuitMenuOpen = true;
    }

    public void CloseConfirmQuitMenu()
    {
        confirmQuitMenuOpen = false;
    }

    public void QuitToTitle()
    {
        SceneManager.LoadScene("Main_Title");
    }
}
