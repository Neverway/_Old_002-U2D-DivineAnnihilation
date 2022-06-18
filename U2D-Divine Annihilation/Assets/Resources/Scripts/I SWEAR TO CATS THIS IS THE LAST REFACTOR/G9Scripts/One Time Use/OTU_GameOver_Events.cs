//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// SID: 
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

public class OTU_GameOver_Events : MonoBehaviour
{
    // Public variables

    // Private variables

    // Reference variables
    OTU_System_MenuManager menuManager;
    OTU_System_SaveFinder saveManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}


    void Start()
    {
        menuManager = FindObjectOfType<OTU_System_MenuManager>();
        saveManager = FindObjectOfType<OTU_System_SaveFinder>();
    }


    public void Retry()
    {
        menuManager.alternateMenuActive = false;
        saveManager.Load();
        saveManager.LoadLevel();
    }


    public void ReturnToTitle()
    {
        menuManager.alternateMenuActive = false;
        SceneManager.LoadScene("Main_Title");
    }
}