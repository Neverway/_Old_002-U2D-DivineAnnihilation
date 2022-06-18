//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// SID: MRC
// Purpose: Give functionality to in-game menus
// Applied to: A UI menu object
// Editor script: N/A
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTU_Title_MainMenuManager : MonoBehaviour
{
    public bool enabledScript;

    // Private variables
    private float transitionSpeed = 55; // Speed the menu will move at during transition
    private float swapMenuDelay = 0.75f; // Time it takes before the menus are changed (Timed to be in the black screen mask)
    private float transitionDuration = 1.3f; // Time it takes for transition from scene to scene
    private string transitioning;
    private bool onTitle;

    // Reference variables
    public GameObject _camera; // Reference to camera
    public GameObject title; // Reference to the title menu (used for disabling/enabling visibility during transition)
    public GameObject mainMenu; // Reference to the main menu (used for disabling/enabling visibility during transition)
    private OTU_System_InputManager inputManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}


    void Start()
    {
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        onTitle = true;
        transitioning = "";
    }


    IEnumerator changeActiveMenu()
    {
        yield return new WaitForSeconds(swapMenuDelay);
        if (transitioning == "title")
        {
            title.SetActive(true);
            mainMenu.SetActive(false);
        }
        else if (transitioning == "main menu")
        {
            title.SetActive(false);
            mainMenu.SetActive(true);
        }
    }


    IEnumerator moveCamera()
    {
        yield return new WaitForSeconds(transitionDuration);
        transitioning = "";
    }


    void Update()
    { 
        if (Input.GetKeyDown(inputManager.controls["Interact"]) && onTitle && transitioning == "" && enabled) 
        { 
            StopCoroutine(changeActiveMenu()); 
            StopCoroutine(moveCamera()); 
            transitioning = "main menu"; 
            StartCoroutine(changeActiveMenu()); 
            StartCoroutine(moveCamera()); 
        }
    }


    void FixedUpdate()
    {
        if (transitioning == "title")
        {
            _camera.transform.Translate(Vector3.right * transitionSpeed * Time.deltaTime);
        }
        else if (transitioning == "main menu")
        {
            _camera.transform.Translate(Vector3.left * transitionSpeed * Time.deltaTime);
            onTitle = false;
        }
    }


    public void EnableScript() { enabledScript = true; }


    public void BackToTitle()
    {
        if (transitioning == "")
        {
            onTitle = true;
            StopCoroutine(changeActiveMenu()); 
            StopCoroutine(moveCamera()); 
            transitioning = "title";
            StartCoroutine(changeActiveMenu()); 
            StartCoroutine(moveCamera());
        }
    }
}
