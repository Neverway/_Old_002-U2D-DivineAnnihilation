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

    void Start()
    {
        menuControl = FindObjectOfType<Menu_Control>(); // Find the character movment script
    }


    void Update()
    {
        currentFrame = menuControl.currentFrame;
        if(currentFrame == 2)
        {
            if (Input.GetKeyDown("z"))
            {
                Application.Quit();
                Debug.Log("Quiting application...");
            }
        }
    }
}
