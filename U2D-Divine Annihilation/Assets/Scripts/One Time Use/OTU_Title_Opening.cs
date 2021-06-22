//=========== Written by Arthur W. Sheldon AKA Lizband_UCC =============================
//
// Purpose: Animate the pre-title message and transition to the opening on a keypress
// Applied to: Config object in a scene
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
    private bool skippable;

    // Variables System
    private System_InputManager inputManager;


    IEnumerator Finish()
    {
        yield return new WaitForSeconds(0.2f);
        skippable = false;
    }

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
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
