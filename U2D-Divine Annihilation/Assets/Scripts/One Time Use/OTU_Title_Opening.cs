//=========== Written by Arthur W. Sheldon AKA Lizband_UCC =============================
//
// Purpose: Animate the pre-title message and transition to the opening on a keypress
// Applied to: 
// Notes: Needs comments
//
//======================================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class OTU_Title_Opening : MonoBehaviour
{
    // Variables Script
    public Animator openingAnimator;
    public Animator bookAnimator;
    public Text continueText;
    public UnityEvent OnCompletion;
    private bool active;

    // Variables System
    private OTU_System_InputManager inputManager;


    void Start()
    {
        inputManager = FindObjectOfType<OTU_System_InputManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(inputManager.controls["Interact"]) && active)
        {
            openingAnimator.GetComponent<Animator>().enabled = false;
            bookAnimator.enabled = true;
        }
    }

    public void DelayedStart()
    {
        active = true;
    }

    public void StopAnimation()
    {
        bookAnimator.enabled = false;
    }
}
