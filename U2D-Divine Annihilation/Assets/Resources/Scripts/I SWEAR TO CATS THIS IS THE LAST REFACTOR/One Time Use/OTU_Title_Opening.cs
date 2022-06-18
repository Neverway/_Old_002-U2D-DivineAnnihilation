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
    private bool firstPass = true;

    // Variables System
    private OTU_System_InputManager inputManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}


    void Start()
    {
        inputManager = FindObjectOfType<OTU_System_InputManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(inputManager.controls["Interact"]) && active && firstPass)
        {
            openingAnimator.GetComponent<Animator>().enabled = false;
            bookAnimator.enabled = true;
            firstPass = false;
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
