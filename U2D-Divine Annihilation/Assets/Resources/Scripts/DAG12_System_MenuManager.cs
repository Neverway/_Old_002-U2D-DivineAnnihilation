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


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        topdownController = FindObjectOfType<NUPTopdownController>();
        pauseManager = FindObjectOfType<DAG12_System_PauseManager>();
    }

    private void Update()
    {
        if (pauseManager.pauseMenuOpen)
        {
            if (topdownController.canMove)
            {
                playerWasAbleToMove = true;
                topdownController.SetNewMovement(0,0,0);
                topdownController.canMove = false;
            }
        }
        else if (!pauseManager.pauseMenuOpen)
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
