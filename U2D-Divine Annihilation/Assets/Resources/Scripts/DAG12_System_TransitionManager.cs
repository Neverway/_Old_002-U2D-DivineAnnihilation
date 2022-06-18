//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Manage screen transion effects
// Applied to: The GameManager object
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DAG12_System_TransitionManager : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    public float fadeSpeed = 0.6f; // The default speed of the fade transition


    //=-----------------=
    // Private variables
    //=-----------------=


    //=-----------------=
    // Reference variables
    //=-----------------=
    private GameObject transitions; 
    private NUPInput inputManager;
    private DAG12_UI_HUD HUD;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        transitions = GameObject.FindWithTag("Transitions");
        inputManager = FindObjectOfType<NUPInput>();
        HUD = FindObjectOfType<DAG12_UI_HUD>();
        TransitionFade("fadein", 2f);   // Fade the title screen in
    }

    IEnumerator Gameover()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Main_GameOver");
    }

    private void Update()
    {
        if (transitions == null)
        {
            transitions = GameObject.FindWithTag("Transitions");
        }
        if (HUD == null)
        {
            HUD = FindObjectOfType<DAG12_UI_HUD>();
        }
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void TransitionFade(string forceMode, float fadespeed)
    {
        // Set either the default fade speed, or the set speed if specified
        if(fadespeed != 0) { transitions.GetComponent<Animator>().speed = fadespeed; }                   
        else { transitions.GetComponent<Animator>().speed = fadeSpeed; }

        // Overwrite modes that will only play one part of the fade transition
        if (forceMode == "fadein") { transitions.GetComponent<Animator>().Play("Fadein"); }
        else if (forceMode == "fadeout") { transitions.GetComponent<Animator>().Play("Fadeout"); }
        else { transitions.GetComponent<Animator>().Play("Fade"); }
    }

    public void TransitionGameover()
    {
        transitions.GetComponent<Animator>().Play("Gameover");
        HUD.SetCameraIdleNoise(0f);
        StartCoroutine(Gameover());
    }
}
