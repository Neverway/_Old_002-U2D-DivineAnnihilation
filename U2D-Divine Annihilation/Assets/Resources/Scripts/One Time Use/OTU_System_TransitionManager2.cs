//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// SID: CAC
// Purpose: Manage any transition screen effects
// Applied to: The GameManager object on the title screen
// Editor script: N/A
// Notes:
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OTU_System_TransitionManager2 : MonoBehaviour
{
    // Public variables
    public float fadeSpeed = 0.6f; // The default speed of the fade transition

    // Reference variables
    public Image fadeTransitionTarget; // A reference to the fade transition object in the scene
    private OTU_System_InputManager inputManager; // A reference to the input manager


    void Start()
    {
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        TransitionFade("fadein", 2f); // Fade the title screen in
    }


    public void TransitionFade(string forceMode, float fadespeed)
    {
        // Set either the default fade speed, or the set speed if specified
        if(fadespeed != null) { fadeTransitionTarget.GetComponent<Animator>().speed = fadespeed; }                   
        else { fadeTransitionTarget.GetComponent<Animator>().speed = fadeSpeed; }

        // Overwrite modes that will only play one part of the fade transition
        if (forceMode == "fadein") { fadeTransitionTarget.GetComponent<Animator>().Play("fadein"); }
        else if (forceMode == "fadeout") { fadeTransitionTarget.GetComponent<Animator>().Play("fadeout"); }
    }
}
