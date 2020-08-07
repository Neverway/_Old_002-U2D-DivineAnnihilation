using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_system_menu_control : MonoBehaviour
{
    public Sprite[] frames;
    public GameObject[] activateOnAction;
    public Image spriteRenderer;
    public bool horizontalScrolling;
    public bool wrapAround;
    public int currentFrame;
    public GameObject menuGameobject;


    // Update is called once per frame
    void Update()
    {
        // Vertical scrolling
        if (!horizontalScrolling)
        {
            // Up arrow
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (currentFrame == 0 && wrapAround)
                {
                    currentFrame = frames.Length; // Up arrow wrap around
                }

                if (currentFrame != 0)
                {
                    currentFrame -= 1; // Up arrow scrolling
                }
            }


            // Up arrow
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (currentFrame == frames.Length - 1 && wrapAround)
                {
                    currentFrame = -1; // Up arrow wrap around
                }

                if (currentFrame != frames.Length - 1)
                {
                    currentFrame += 1; // Up arrow scrolling
                }
            }
        }


        // Horizontal scrolling
        else if (horizontalScrolling)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentFrame += 1;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentFrame -= 1;
            }
        }

        if (Input.GetKeyDown("z"))
        {
            activateOnAction[currentFrame].SetActive(true);
            menuGameobject.SetActive(false);
        }

        // Draw current image
        spriteRenderer.sprite = frames[currentFrame];
    }
}
