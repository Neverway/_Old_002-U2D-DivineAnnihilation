//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Scroll through strings or text objects
// Applied to: A menu parent object in a scene
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;

public class Menu_Scroll_String : MonoBehaviour
{
    public Text[] optionsUIReference;
    public string[] optionsHoverText;
    public string[] optionsBaseText;
    public Color hoverColor = new Vector4 (1,1,1,1);
    public Color baseColor = new Vector4(0.25f, 0.25f, 0.25f, 1);
    public bool horizontalScrolling;
    public bool wrapAround;
    public int currentSelection;
    public bool active = true;
    private System_InputManager inputManager;

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
    }

    void Update()
    {
        // Vertical scrolling
        if (!horizontalScrolling && active)
        {
            // Up arrow
            if (Input.GetKeyDown(inputManager.controls["Up"]))
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
            if (Input.GetKeyDown(inputManager.controls["Down"]))
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
        else if (horizontalScrolling && active)
        {
            if (Input.GetKeyDown(inputManager.controls["Right"]))
            {
                currentSelection += 1;
            }

            if (Input.GetKeyDown(inputManager.controls["Left"]))
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
                optionsUIReference[i].color = baseColor;
            }
        }

        optionsUIReference[currentSelection].text = optionsHoverText[currentSelection];
        optionsUIReference[currentSelection].color = hoverColor;
    }
}
