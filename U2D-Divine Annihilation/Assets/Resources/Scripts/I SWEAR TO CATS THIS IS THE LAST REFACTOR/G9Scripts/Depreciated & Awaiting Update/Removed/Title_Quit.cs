//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Give functionality to the erase save profile button on the title screen
// Applied to: The title screen
//
//=============================================================================

using UnityEngine;

public class Title_Quit : MonoBehaviour
{
    private Menu_Control menuControl;
    public int currentFrame;
    private System_InputManager inputManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
        menuControl = FindObjectOfType<Menu_Control>(); // Find the character movment script
    }


    void Update()
    {
        currentFrame = menuControl.currentFrame;
        if(currentFrame == 2)
        {
            if (Input.GetKeyDown(inputManager.controls["Interact"]))
            {
                Application.Quit();
                Debug.Log("Quiting application...");
            }
        }
    }
}
