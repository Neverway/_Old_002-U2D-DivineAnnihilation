using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScrollString : MonoBehaviour
{
    public Text[] optionsUIReference;
    public string[] optionsHoverText;
    public string[] optionsBaseText;
    public Color hoverColor = new Vector4 (1,1,1,1);
    public Color baseColor = new Vector4(0.25f, 0.25f, 0.25f, 1);
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
                    currentSelection = optionsUIReference.Length; // Up arrow wrap around
                }

                if (currentSelection != 0)
                {
                    currentSelection -= 1; // Up arrow scrolling
                }
            }


            // Up arrow
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (currentSelection == optionsUIReference.Length - 1 && wrapAround)
                {
                    currentSelection = -1; // Up arrow wrap around
                }

                if (currentSelection != optionsUIReference.Length - 1)
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

        // Set current selection to hovered
        for (int i=0; i < optionsUIReference.Length; i++)
        {
            if (i != currentSelection)
            {
                optionsUIReference[i].text = optionsBaseText[i];
            }
        }

        for (int i = 0; i < optionsUIReference.Length; i++)
        {
            if (i != currentSelection)
            {
                optionsUIReference[i].color= baseColor;
            }
        }

        optionsUIReference[currentSelection].text = optionsHoverText[currentSelection];
        optionsUIReference[currentSelection].color = hoverColor;
    }
}
