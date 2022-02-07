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
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OTU_System_TransitionManager2 : MonoBehaviour
{
    // Public variables
    public float fadeSpeed = 0.6f;
    public Image fadeTransitionTarget;

    // Private variables

    // Reference variables
        private OTU_System_InputManager inputManager;

    void Start()
    {
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        TransitionFade("fadein", 2f);
    }

    void Update()
    {
        if (Input.GetKeyDown(inputManager.controls["Interact"]))
        {
            TransitionFade("fadein",2f);
        }
        if (Input.GetKeyDown(inputManager.controls["Action"]))
        {
            TransitionFade("fadeout",2f);
        }
    }


    public void TransitionFade(string forceMode, float fadespeed)
    {
        if(fadespeed != null)
        {
            fadeTransitionTarget.GetComponent<Animator>().speed = fadeSpeed;
        }
        if (forceMode == "fadein")
        {
            // print("fadein");
            // fadeTransitionTarget.color = new Color(0, 0, 0, 1);
            // fadeTransitionTarget.CrossFadeAlpha(0.0f, fadespeed, false);
            fadeTransitionTarget.GetComponent<Animator>().Play("fadein");
        }
        if (forceMode == "fadeout")
        {
            fadeTransitionTarget.GetComponent<Animator>().Play("fadeout");
        }
    }
}
