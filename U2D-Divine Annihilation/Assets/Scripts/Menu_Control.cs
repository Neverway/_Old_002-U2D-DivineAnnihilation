//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Scroll through and toggle menu objects
// Applied to: A menu parent object in a scene
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;

public class Menu_Control : MonoBehaviour
{
    public Sprite[] frames;
    public GameObject[] activateOnAction;
    public Image spriteRenderer;
    public bool horizontalScrolling;
    public bool wrapAround;
    public bool canGoBack;
    public GameObject activeOnBack;
    public GameObject menuGameobject;

    public int currentFrame;

    void Update()
    {
        // Vertical scrolling
        if (!horizontalScrolling)
        {
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

        if (Input.GetKeyDown("x") && canGoBack)
        {
            activeOnBack.SetActive(true);
            menuGameobject.SetActive(false);
        }

        // Draw current image
        spriteRenderer.sprite = frames[currentFrame];
    }
}
