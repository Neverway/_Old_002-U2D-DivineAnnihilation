using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_menu_scroll : MonoBehaviour
{
    public Sprite[] selections;
    public Image spriteRenderer;
    public bool horizontalScrolling;
    public bool wrapAround;
    public int currentSelection;


    // Update is called once per frame
    void Update()
    {
        // Vertical scrolling
        if (!horizontalScrolling)
        {
            // Up arrow
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (currentSelection == 0 && wrapAround)
                {
                    currentSelection = selections.Length; // Up arrow wrap around
                }

                if (currentSelection != 0)
                {
                    currentSelection -= 1; // Up arrow scrolling
                }
            }


            // Up arrow
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (currentSelection == selections.Length - 1 && wrapAround)
                {
                    currentSelection = -1; // Up arrow wrap around
                }

                if (currentSelection != selections.Length - 1)
                {
                    currentSelection += 1; // Up arrow scrolling
                }
            }
        }


        // Horizontal scrolling
        else if (horizontalScrolling)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentSelection += 1;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentSelection -= 1;
            }
        }

        // Draw current image
        spriteRenderer.sprite = selections[currentSelection];
    }
}
