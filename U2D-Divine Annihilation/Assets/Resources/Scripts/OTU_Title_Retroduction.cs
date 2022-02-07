//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// SID: 
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTU_Title_Retroduction : MonoBehaviour
{
    // Public variables
    public float timeTillReplay = 117.0f;
    public float retroductionDuration = 117.0f;
    public GameObject titleTarget;
    public GameObject retroductionTarget;

    // Private variables

    // Reference variables
    private OTU_System_TransitionManager2 transitionManager;


    void Start()
    {
        transitionManager = FindObjectOfType<OTU_System_TransitionManager2>();
        StartCoroutine("RetroductionReplay");
    }

    IEnumerator RetroductionReplay()
    {
        yield return new WaitForSeconds(timeTillReplay);
        transitionManager.TransitionFade("fadeout",3);
        yield return new WaitForSeconds(4);
        titleTarget.SetActive(false);
        retroductionTarget.SetActive(true);
        transitionManager.TransitionFade("fadein",3);
        StartCoroutine("RetroductionTimer");
    }    
    
    IEnumerator RetroductionTimer()
    {
        yield return new WaitForSeconds(retroductionDuration);
        transitionManager.TransitionFade("fadeout",3);
        yield return new WaitForSeconds(4);
        titleTarget.SetActive(true);
        retroductionTarget.SetActive(false);
        transitionManager.TransitionFade("fadein",3);
        StartCoroutine("RetroductionReplay");
    }
}
