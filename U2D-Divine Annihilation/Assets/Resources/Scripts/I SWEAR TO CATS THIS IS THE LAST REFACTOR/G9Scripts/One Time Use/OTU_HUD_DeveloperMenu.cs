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

public class OTU_HUD_DeveloperMenu : MonoBehaviour
{
    // Public variables
    public GameObject developerMenu;

    // Private variables

    // Reference variables
    public OTU_System_SaveManager saveManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}


    void Start()
    {
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
        if (saveManager.activeSave2.playerName == "DEV_TSTR!")
        {
            developerMenu.SetActive(true);
        }
    }


    void Update()
    {
        
    }
}
