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
    public bool entitiesPaused;


    //=-----------------=
    // Reference variables
    //=-----------------=
    public NUPTopdownController playerCharacter;
    private DAG13_UI_TextboxManager textboxManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Awake()
    {
	    textboxManager = FindObjectOfType<DAG13_UI_TextboxManager>();
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
	    if (!textboxManager) textboxManager = FindObjectOfType<DAG13_UI_TextboxManager>();
	    if (!playerCharacter) playerCharacter = FindObjectOfType<NUPTopdownController>();
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    private bool IsMenuOpen()
    {
	    if (textboxManager == null) return false;
	    
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
	    // Keep the function from firing more than once
	    if (entitiesPaused) return;
	    entitiesPaused = true;
	    
	    // Pause player movement
	    playerCharacter.SetNewMovement(0,0,0);
    }

    private void UnPauseEntities()
    {
	    // Keep the function from firing more than once
	    if (!entitiesPaused) return;
	    entitiesPaused = false;
	    
	    // UnPause player movement
	    playerCharacter.ResetMovement();
    }


    //=-----------------=
    // External Functions
    //=-----------------=
}
