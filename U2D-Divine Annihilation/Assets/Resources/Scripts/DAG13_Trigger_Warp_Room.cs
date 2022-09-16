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

public class DAG13_Trigger_Warp_Room : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    [SerializeField] private Transform exitPoint;


    //=-----------------=
    // Private variables
    //=-----------------=


    //=-----------------=
    // Reference variables
    //=-----------------=
    private DAG13_UI_TransitionManager transitionManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
	    transitionManager = FindObjectOfType<DAG13_UI_TransitionManager>();
    }

    private IEnumerator WarpOnObscured(GameObject _player)
    {
	    yield return new WaitForSeconds(0.75f);
	    _player.transform.position = exitPoint.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
	    if (!other.CompareTag("Player")) return;
	    if (transitionManager != null)
		    transitionManager.FadeTransition(0, 0);
	    StartCoroutine(WarpOnObscured(other.gameObject));
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}
