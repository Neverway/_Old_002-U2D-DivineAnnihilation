//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Give functionality to the erase save profile button on the title screen
// Applied to: The title screen
//
//=============================================================================

using UnityEngine;

public class Title_Options : MonoBehaviour
{
    public Menu_Scroll_String menuControl;
    public Menu_Scroll_String menuControl2;
    public int currentFrame;
    public GameObject titleGameObject;
    public GameObject controlsGameObject;
    public GameObject menuGameobject;
    private System_InputManager inputManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
    }

    void Update()
    {
        currentFrame = menuControl.currentSelection;
        if (Input.GetKeyDown(inputManager.controls["Action"]))
        {
            menuControl.currentSelection = 0;
            menuControl2.currentSelection = 0;
            titleGameObject.SetActive(true);
            menuGameobject.SetActive(false);
        }
        if (currentFrame == 0)
        {
            if (Input.GetKeyDown(inputManager.controls["Left"]))
            {
                Debug.Log("0");
            }
            if (Input.GetKeyDown(inputManager.controls["Right"]))
            {
                Debug.Log("0");
            }
        }
        if (currentFrame == 1)
        {
            if (Input.GetKeyDown(inputManager.controls["Interact"]))
            {
                menuControl.currentSelection = 0;
                menuControl2.currentSelection = 0;
                controlsGameObject.SetActive(true);
                menuGameobject.SetActive(false);
            }
        }
        if (currentFrame == 2)
        {
            if (Input.GetKeyDown(inputManager.controls["Left"]))
            {
                Debug.Log("0");
            }
            if (Input.GetKeyDown(inputManager.controls["Right"]))
            {
                Debug.Log("0");
            }
        }
        if (currentFrame == 3)
        {
            if (Input.GetKeyDown(inputManager.controls["Left"]))
            {
                Debug.Log("0");
            }
            if (Input.GetKeyDown(inputManager.controls["Right"]))
            {
                Debug.Log("0");
            }
        }
        if (currentFrame == 4)
        {
            if (Input.GetKeyDown(inputManager.controls["Left"]))
            {
                Debug.Log("0");
            }
            if (Input.GetKeyDown(inputManager.controls["Right"]))
            {
                Debug.Log("0");
            }
        }
        if (currentFrame == 5)
        {
            if (Input.GetKeyDown(inputManager.controls["Left"]))
            {
                Debug.Log("0");
            }
            if (Input.GetKeyDown(inputManager.controls["Right"]))
            {
                Debug.Log("0");
            }
        }
        if (currentFrame == 6)
        {
            if (Input.GetKeyDown(inputManager.controls["Interact"]))
            {
                Debug.Log("6");
            }
        }
        if (currentFrame == 7)
        {
            if (Input.GetKeyDown(inputManager.controls["Interact"]))
            {
                menuControl.currentSelection = 0;
                menuControl2.currentSelection = 0;
                titleGameObject.SetActive(true);
                menuGameobject.SetActive(false);
            }
        }
    }
}
