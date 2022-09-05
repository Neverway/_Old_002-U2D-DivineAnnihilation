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

public class DAG13_System_MenuManager : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=


    //=-----------------=
    // Private variables
    //=-----------------=
    private bool entitiesPaused;


    //=-----------------=
    // Reference variables
    //=-----------------=
    private NUPTopdownController playerCharacter;
    private DAG13_System_TextboxManager textboxManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
	    textboxManager = FindObjectOfType<DAG13_System_TextboxManager>();
	    playerCharacter = FindObjectOfType<NUPTopdownController>();
    }

    private void Update()
    {
	    if (IsMenuOpen())
	    {
		    PauseEntities();
	    }
	    else
	    {
		    UnPauseEntities();
	    }
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    private bool IsMenuOpen()
    {
	    if (textboxManager.active)
	    {
		    return true;
	    }
	    else
	    {
		    return false;
	    }
    }

    private void PauseEntities()
    {
	    
    }

    private void UnPauseEntities()
    {
	    
    }


    //=-----------------=
    // External Functions
    //=-----------------=
}
