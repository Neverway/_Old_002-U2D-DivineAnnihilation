//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Scroll through and toggle menu objects
// Applied to: A menu parent object in a scene
// Editor script: DASDK_Menu_Control
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DA_Menu_Control : MonoBehaviour
{
    // Variables Scrolling
    public bool horizontalScrolling;    // Switch from using up and down to move through a menu to left and right
    public bool wrapAround;             // When trying to advance at the begining or end of a menu wrap around to the other side
    public int selectionLength; 
    public int currentSelection;

    // Variables Sprite Scrolling
    public bool scrollSprites;  // Enable scrolling with sprites
    public Sprite[] sprites;    // List of sprites to use for the menu (in order)

    // Variables String Scrolling
    public bool scrollStrings;                                      // Enable scrolling with strings
    public Color hoverColor = new Vector4(1, 1, 1, 1);              // Color for when a menu object is selected
    public Color baseColor = new Vector4(0.25f, 0.25f, 0.25f, 1);   // Color for when a menu object is not selected
    public Text[] textTargetObjects;                                // The target text objects to change
    public string[] baseText;                                       // The text that the menu options should be when not selected
    public string[] hoveredText;                                    // The text that the menu options should be when selected

    // Variables MenuControl
    public bool menuControl;            // Enable menu control
    public bool canGoBack;              // Allow the player to go to the previouse menu (if there was one)
    public UnityEvent[] OnInteract;     // A unity event for use with the activation of the interact button
    public UnityEvent onBack;           // A unity event for use with the activation of the back button if canGoBack is enabled

    // Variables System
    private OTU_System_InputManager inputManager;


    void Start()
    {
        inputManager = FindObjectOfType<OTU_System_InputManager>();
    }


    void Update()
    {
        // Vertical Scrolling
        if (!horizontalScrolling)
        {
            // Up
            if (Input.GetKeyDown(inputManager.controls["Up"]))
            {
                if (currentSelection == 0 && wrapAround) { currentSelection = selectionLength; }    // Wrap around
                if (currentSelection != 0) { currentSelection -= 1; }                               // Go Up
            }

            // Down
            if (Input.GetKeyDown(inputManager.controls["Down"]))
            {
                if (currentSelection == selectionLength && wrapAround) { currentSelection = 0; }    // Wrap around
                if (currentSelection != selectionLength - 1) { currentSelection += 1; }                 // Go Down
            }
        }

        // Horizontal Scrolling
        else if (horizontalScrolling)
        {
            // Left
            if (Input.GetKeyDown(inputManager.controls["Left"]))
            {
                if (currentSelection == 0 && wrapAround) { currentSelection = selectionLength; }    // Wrap around
                if (currentSelection != 0) { currentSelection -= 1; }                               // Go Left
            }

            // Down
            if (Input.GetKeyDown(inputManager.controls["Right"]))
            {
                if (currentSelection == selectionLength && wrapAround) { currentSelection = 0; }    // Wrap around
                if (currentSelection != selectionLength - 1) { currentSelection += 1; }                 // Go Right
            }
        }

        /*
        if (scrollSprites && !scrollStrings)
        {
            for (int i = 0; i < selectionLength; i++)
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
        */

        if (scrollStrings && !scrollSprites)
        {
            selectionLength = textTargetObjects.Length;
            for (int i = 0; i < selectionLength; i++)
            {
                if (i != currentSelection)
                {
                    textTargetObjects[i].text = baseText[i];
                    textTargetObjects[i].color = baseColor;
                }
            }

            textTargetObjects[currentSelection].text = hoveredText[currentSelection];
            textTargetObjects[currentSelection].color = hoverColor;
        }

        if (menuControl)
        {
            if (Input.GetKeyDown(inputManager.controls["Interact"]))
            {
                OnInteract[currentSelection].Invoke();
            }

            if (Input.GetKeyDown(inputManager.controls["Action"]) && canGoBack)
            {
                onBack.Invoke();
            }
        }
    }
}
 