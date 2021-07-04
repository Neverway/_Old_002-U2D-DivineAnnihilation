//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Scroll through menu objects
// Applied to: A menu parent object in a scene
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;

public class Menu_Scroll_Control : MonoBehaviour
{
    public Sprite[] selections;
    public Image spriteRenderer;
    public bool horizontalScrolling;
    public bool wrapAround;
    public int currentSelection;

    void Update()
    {
        // Vertical scrolling
        if (!horizontalScrolling)
        {
            // Up arrow
            if (Input.GetAxis("Vertical") > 0)
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
            if (Input.GetAxis("Vertical") < 0)
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
            if (Input.GetAxis("Horizontal") > 0)
            {
                currentSelection += 1;
            }

            if (Input.GetAxis("Horizontal") < 0)
            {
                currentSelection -= 1;
            }
        }

        // Draw current image
        spriteRenderer.sprite = selections[currentSelection];
    }
}
