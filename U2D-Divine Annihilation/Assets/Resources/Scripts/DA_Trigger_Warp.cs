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

public class DA_Trigger_Warp : MonoBehaviour
{
    // Public variables
    public bool playTransition;

    // Reference variables
    private Transform exitPointTarget;
    private Transform playerTarget;
    private OTU_System_TransitionManager transitionManager;

    void Start()
    {
        transitionManager = FindObjectOfType<OTU_System_TransitionManager>();
        exitPointTarget = gameObject.transform.parent.GetChild(1);
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.6f);
        playerTarget.position = new Vector2(exitPointTarget.position.x, exitPointTarget.position.y);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerTarget = other.transform;
            if (playTransition)
            {
                transitionManager.StartCoroutine("TriggerFade");
                StartCoroutine("Teleport");
            }
            else
            {
                playerTarget.position = new Vector2(exitPointTarget.position.x, exitPointTarget.position.y);
            }
        }
    }
}