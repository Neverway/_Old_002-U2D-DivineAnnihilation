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

public class OTU_Title_PretitleMessage : MonoBehaviour
{
    // Variables Script
    public Text continueText;
    public UnityEvent OnCompletion;
    private bool active = true;
    private bool skippable = true;

    // Variables System
    private System_InputManager inputManager;


    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(0.2f);
        continueText.text = "Press [" + inputManager.controls["Interact"].ToString() + "] to continue";
    }

    IEnumerator Finish()
    {
        yield return new WaitForSeconds(0.2f);
        skippable = false;
    }

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
        StartCoroutine("DelayedStart");
    }

    void Update()
    {
        if (Input.GetKeyDown(inputManager.controls["Interact"]) && skippable && active)
        {
            gameObject.GetComponent<Animator>().Play("title_pretitle_skip");
            StartCoroutine("Finish");
        }
        if (Input.GetKeyDown(inputManager.controls["Interact"]) && !skippable && active)
        {
            gameObject.GetComponent<Animator>().Play("title_pretitle_fadeout");
            active = false;
        }
    }

    public void Fadeout()
    {
        skippable = false;
    }

    public void StopAnimation()
    {
        OnCompletion.Invoke();
    }
}
