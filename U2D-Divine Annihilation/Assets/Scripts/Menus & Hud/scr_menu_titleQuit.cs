using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_menu_titleQuit : MonoBehaviour
{
    private scr_menu_control menuControl;
    public int currentFrame;

    // Start is called before the first frame update
    void Start()
    {
        menuControl = FindObjectOfType<scr_menu_control>(); // Find the character movment script
    }

    // Update is called once per frame
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
