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

public class DAG12_System_MenuManager : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=


    //=-----------------=
    // Private variables
    //=-----------------=
    private bool playerWasAbleToMove;


    //=-----------------=
    // Reference variables
    //=-----------------=
    private NUPTopdownController topdownController;
    private DAG12_System_PauseManager pauseManager;
    private DAG12_System_TextboxManager textboxManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        topdownController = FindObjectOfType<NUPTopdownController>();
        pauseManager = FindObjectOfType<DAG12_System_PauseManager>();
        textboxManager = FindObjectOfType<DAG12_System_TextboxManager>();
    }

    private void Update()
    {
        if (pauseManager.pauseMenuOpen || textboxManager.textboxOpen)
        {
            if (topdownController.canMove)
            {
                playerWasAbleToMove = true;
                topdownController.SetNewMovement(0,0,0);
                topdownController.canMove = false;
            }
        }
        else if (!pauseManager.pauseMenuOpen && !textboxManager.textboxOpen)
        {
            if (playerWasAbleToMove)
            {
                topdownController.canMove = true;
                topdownController.ResetMovement();
                playerWasAbleToMove = false;
            }
        }
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}
