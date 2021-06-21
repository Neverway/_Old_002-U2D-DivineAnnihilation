//=========== Written by Arthur W. Sheldon AKA Lizband_UCC =============================
//
// Purpose: transition from the opening to the title screen
// Applied to: The screen space object in the title scene
// Type: Ome time use
//
//======================================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Title_Intro_Opening : MonoBehaviour
{
    public GameObject openingAnimator;
    public UnityEvent onFinish;

    private bool firstPass = true;
    private bool active;
    private bool skippable;
    private Animator bookAnimator;
    private System_InputManager inputManager;

    void Start()
    {
        bookAnimator = gameObject.GetComponent<Animator>();
        inputManager = FindObjectOfType<System_InputManager>();
    }

    IEnumerator startPretitleTextAppear()
    {
        yield return new WaitForSeconds(0.5f);
        active = true;
    }

    IEnumerator activateSkipping()
    {
        yield return new WaitForSeconds(0.5f);
        skippable = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(inputManager.controls["Interact"]) && active)
        {
            openingAnimator.GetComponent<Animator>().enabled = false;
            bookAnimator.enabled = true;
            StartCoroutine("activateSkipping");
            active = false;
        }

        if (Input.GetKeyDown(inputManager.controls["Interact"]) && skippable || Input.GetKeyDown(inputManager.controls["Action"]) && skippable)
        {
            bookAnimator.Play("title_book_appear_skip");
            skippable = false;
        }

        if (openingAnimator.activeSelf && firstPass)
        {
            StartCoroutine("startPretitleTextAppear");
            firstPass = false;
        }
    }

    public void StopAnimation()
    {
        bookAnimator.enabled = false;
    }
}
