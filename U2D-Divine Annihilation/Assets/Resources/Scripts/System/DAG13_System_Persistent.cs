//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Keep the system manager from being destroyed when changing scenes
// Applied to: The system manager
// Editor script: 
// Notes: 
//
//=============================================================================

using UnityEngine;

public class DAG13_System_Persistent : MonoBehaviour
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
    private static DAG13_System_Persistent instance;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Awake()
    {
	    if (instance != null)
	    {
		    Destroy(gameObject);
		    return;
	    }
	    instance = this;
	    DontDestroyOnLoad(gameObject);
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}
