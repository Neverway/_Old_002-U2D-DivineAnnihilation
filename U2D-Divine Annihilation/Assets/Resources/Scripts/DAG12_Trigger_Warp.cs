//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Move the player from one room to another with an optional fade transition
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAG12_Trigger_Warp : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    public bool playTransition = true;  // Should a fade animation play when entering/exiting this trigger


    //=-----------------=
    // Private variables
    //=-----------------=


    //=-----------------=
    // Reference variables
    //=-----------------=
    private Transform exitPointTarget;                          // The exit position of the warp trigger
    private Transform playerTarget;                             // The overlapped player who's position is being modified
    public DAG12_System_TransitionManager transitionManager;    // The transition effect for playing the fade animation


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        exitPointTarget = gameObject.transform.GetChild(0);
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.6f);
        playerTarget.position = new Vector2(exitPointTarget.position.x, exitPointTarget.position.y);
    }

    private void Update()
    {
        if (transitionManager == null)
        {
            transitionManager = FindObjectOfType<DAG12_System_TransitionManager>();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerTarget = other.transform;
            if (playTransition)
            {
                transitionManager.TransitionFade("", 0); // Do a fade transition with no overrides (which means it will do a fade to black and then a fade from black, the zero means default fade speed)
                StartCoroutine("Teleport");
            }
            else
            {
                playerTarget.position = new Vector2(exitPointTarget.position.x, exitPointTarget.position.y);
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