//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using UnityEngine;

public class DAG13_Trigger_Warp_Level : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    [SerializeField] private Vector2 exitPosition;
    [SerializeField] private string sceneID;


    //=-----------------=
    // Private variables
    //=-----------------=


    //=-----------------=
    // Reference variables
    //=-----------------=
    private DAG13_UI_TransitionManager transitionManager;
    private DAG13_System_SceneManager sceneManager;
    private DAG13_System_SaveManager saveManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
	    transitionManager = FindObjectOfType<DAG13_UI_TransitionManager>();
	    sceneManager = FindObjectOfType<DAG13_System_SceneManager>();
	    saveManager = FindObjectOfType<DAG13_System_SaveManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
	    if (!other.CompareTag("Player")) return;
	    
	    // Find this bastard again in case it failed to find it the first time
	    sceneManager = FindObjectOfType<DAG13_System_SceneManager>();

	    // Save the exit position
	    saveManager.activeSaveFile.savePositionX = exitPosition.x;
	    saveManager.activeSaveFile.savePositionY = exitPosition.y;
	    saveManager.LoadingSavedPosition = true;
	    
	    // Transition Fadeout
	    if (transitionManager != null)
		    transitionManager.Fadeout(0);
	    
	    // Load scene
	    sceneManager.LoadScene(sceneID, 1);
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}
