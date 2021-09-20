//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: 
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTU_System_SaveLoader : MonoBehaviour
{
    // Public variables

    // Private variables
    public bool hasLoadedCurrentLevel;

    // Reference variables
    private OTU_System_SaveManager saveManager;




    IEnumerator CompleteLoading()
    {
        yield return new WaitForSeconds(0.6f);
        hasLoadedCurrentLevel = true;
    }

    void Start()
    {
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
        if (!hasLoadedCurrentLevel)
        {
            // Load the players position
            GameObject.FindWithTag("Player").transform.position = saveManager.activeSave2.playerSavePosition;

            // Load the players currently equipped items

            // Load the level progression data

            hasLoadedCurrentLevel = true;
            //StartCoroutine(CompleteLoading());
        }
    }
}