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

public class DAG12_Gameover_Events : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=


    //=-----------------=
    // Private variables
    //=-----------------=


    //=-----------------=
    // Reference variables
    //=-----------------=
    private DAG12_System_SaveManager saveManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        saveManager = FindObjectOfType<DAG12_System_SaveManager>();
    }

    private void Update()
    {
	
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void Retry()
    {
        //menuManager.alternateMenuActive = false;
        if (saveManager.activeSaveFile.scene != "")
        {
            SceneManager.LoadScene(saveManager.activeSaveFile.scene);
        }
        else
        {
            SceneManager.LoadScene("Main_Title");
            Debug.LogWarning("[DAT:WRN] The saveManager did not have a scene loaded into the active buffer! If the scene was loaded abnormally, make sure the fallback GameManager object has the Scene field set to the current scene's name");
        }
        //saveManager.Load();
        //saveManager.LoadLevel();
    }


    public void GiveUp()
    {
        //menuManager.alternateMenuActive = false;
        SceneManager.LoadScene("Main_Title");
    }
}
