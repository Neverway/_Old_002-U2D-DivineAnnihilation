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
    public bool scrollSprites;              // Enable scrolling with sprites
    public GameObject spriteTargetObject;   // The target sprite object to change
    public Sprite[] sprites;                // List of sprites to use for the menu (in order)

    // Variables String Scrolling
    public bool scrollStrings;                                      // Enable scrolling with strings
    public bool singleTextObjectScrolling;                          // Enable scrolling with a single text object (Base Text is not used in this case)
    public Color hoverColor = new Vector4(1, 1, 1, 1);              // Color for when a menu object is selected
    public Color baseColor = new Vector4(0.25f, 0.25f, 0.25f, 1);   // Color for when a menu object is not selected
    public Text[] textTargetObjects;                                // The target text objects to change
    public string[] baseText;                                       // The text that the menu options should be when not selected
    public string[] hoveredText;                                    // The text that the menu options should be when selected
    public string disabledText;                                   // The text that the menu options should be when selected

    // Variables MenuControl
    public bool menuControl;            // Enable menu control
    public bool canGoBack;              // Allow the player to go to the previouse menu (if there was one)
    public GameObject previousMenu;     // If you want to scroll to other menus then this would be the "previous" menu (Don't asign this unless, you want to wrap around, or this is not the first menu)
    public GameObject nextMenu;         // If you want to scroll to other menus then this would be the "next" menu
    public UnityEvent[] OnInteract;     // A unity event for use with the activation of the interact button
    public UnityEvent onBack;           // A unity event for use with the activation of the back button if canGoBack is enabled

    // Variables System
    private OTU_System_InputManager inputManager;


    void Start()
    {
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        ErrorCheck();
    }

    void Update()
    {
        if (previousMenu != null)
        {
            previousMenu.GetComponent<DA_Menu_Control>().textTargetObjects[0].text = previousMenu.GetComponent<DA_Menu_Control>().baseText[0];
        }
        if (nextMenu != null)
        {
            nextMenu.GetComponent<DA_Menu_Control>().textTargetObjects[0].text = nextMenu.GetComponent<DA_Menu_Control>().baseText[0];
        }
        // Vertical Scrolling
        if (!horizontalScrolling)
        {
            // Up
            if (Input.GetKeyDown(inputManager.controls["Up"]))
            {
                if (currentSelection == 0 && wrapAround) { currentSelection = selectionLength - 1; }    // Wrap around
                else if (currentSelection != 0) { currentSelection -= 1; }                              // Go Up
            }

            // Down
            if (Input.GetKeyDown(inputManager.controls["Down"]))
            {
                if (currentSelection == selectionLength - 1 && wrapAround) { currentSelection = 0; }    // Wrap around
                else if (currentSelection != selectionLength - 1) { currentSelection += 1; }            // Go Down
            }

            // Left
            if (Input.GetKeyDown(inputManager.controls["Left"]) && previousMenu != null)
            {
                previousMenu.GetComponent<DA_Menu_Control>().enabled = true;
                previousMenu.GetComponent<DA_Menu_Control>().currentSelection = gameObject.GetComponent<DA_Menu_Control>().currentSelection;
                gameObject.GetComponent<DA_Menu_Control>().enabled = false;
            }

            // Right
            if (Input.GetKeyDown(inputManager.controls["Right"]) && nextMenu != null)
            {
                nextMenu.GetComponent<DA_Menu_Control>().enabled = true;
                nextMenu.GetComponent<DA_Menu_Control>().currentSelection = gameObject.GetComponent<DA_Menu_Control>().currentSelection;
                gameObject.GetComponent<DA_Menu_Control>().enabled = false;
            }
        }

        // Horizontal Scrolling
        else if (horizontalScrolling)
        {
            // Left
            if (Input.GetKeyDown(inputManager.controls["Left"]))
            {
                if (currentSelection == 0 && wrapAround) { currentSelection = selectionLength - 1; }    // Wrap around
                else if (currentSelection != 0) { currentSelection -= 1; }                              // Go Left
            }

            // Right
            if (Input.GetKeyDown(inputManager.controls["Right"]))
            {
                if (currentSelection == selectionLength - 1 && wrapAround) { currentSelection = 0; }    // Wrap around
                else if (currentSelection != selectionLength - 1) { currentSelection += 1; }            // Go Right
            }

            // Up
            if (Input.GetKeyDown(inputManager.controls["Up"]) && previousMenu != null)
            {
                previousMenu.GetComponent<DA_Menu_Control>().enabled = true;
                previousMenu.GetComponent<DA_Menu_Control>().currentSelection = gameObject.GetComponent<DA_Menu_Control>().currentSelection;
                gameObject.GetComponent<DA_Menu_Control>().enabled = false;
            }

            // Down
            if (Input.GetKeyDown(inputManager.controls["Down"]) && nextMenu != null)
            {
                nextMenu.GetComponent<DA_Menu_Control>().enabled = true;
                nextMenu.GetComponent<DA_Menu_Control>().currentSelection = gameObject.GetComponent<DA_Menu_Control>().currentSelection;
                gameObject.GetComponent<DA_Menu_Control>().enabled = false;
            }
        }


        // Scrolling Strings & !Scrolling Sprites
        if (scrollStrings && !scrollSprites)
        {
            if (!singleTextObjectScrolling)
            {
                selectionLength = textTargetObjects.Length;
                for (int i = 0; i < selectionLength; i++)
                {
                    if (i != currentSelection)
                    {
                        if (baseText.Length == textTargetObjects.Length)
                        {
                            textTargetObjects[i].text = baseText[i];
                        }
                        textTargetObjects[i].color = baseColor;
                    }
                }
                if (hoveredText.Length == textTargetObjects.Length)
                {
                    textTargetObjects[currentSelection].text = hoveredText[currentSelection];
                }
                textTargetObjects[currentSelection].color = hoverColor;
            }
            else if (singleTextObjectScrolling)
            {
                selectionLength = hoveredText.Length;
                textTargetObjects[0].text = hoveredText[currentSelection];
            }
        }

        // !Scrolling Strings & Scrolling Sprites
        else if (!scrollStrings && scrollSprites)
        {
            selectionLength = sprites.Length;
            if (spriteTargetObject.GetComponent<SpriteRenderer>() != null)
            {
                spriteTargetObject.GetComponent<SpriteRenderer>().sprite = sprites[currentSelection];
            }
            else if (spriteTargetObject.GetComponent<Image>() != null)
            {
                spriteTargetObject.GetComponent<Image>().sprite = sprites[currentSelection];
            }
            else
            {
                Debug.LogError("DASDK: In [" + this.gameObject.name + "] under DA_Menu_Control, the spriteTargetObject did not have a SpriteRenderer or Image component attached! One of these is required for SpriteScrolling!");
            }
        }

        // Scrolling Strings & Scrolling Sprites
        else if (scrollStrings && scrollSprites)
        {
            if (!singleTextObjectScrolling)
            {
                selectionLength = textTargetObjects.Length;
                for (int i = 0; i < selectionLength; i++)
                {
                    if (i != currentSelection)
                    {
                        if (baseText.Length == textTargetObjects.Length)
                        {
                            textTargetObjects[i].text = baseText[i];
                        }
                        textTargetObjects[i].color = baseColor;
                    }
                }
                if (hoveredText.Length == textTargetObjects.Length)
                {
                    textTargetObjects[currentSelection].text = hoveredText[currentSelection];
                }
                textTargetObjects[currentSelection].color = hoverColor;
            }
            else if (singleTextObjectScrolling)
            {
                selectionLength = hoveredText.Length;
                textTargetObjects[0].text = hoveredText[currentSelection];
            }


            if (spriteTargetObject.GetComponent<SpriteRenderer>() != null)
            {
                spriteTargetObject.GetComponent<SpriteRenderer>().sprite = sprites[currentSelection];
            }
            else if (spriteTargetObject.GetComponent<Image>() != null)
            {
                spriteTargetObject.GetComponent<Image>().sprite = sprites[currentSelection];
            }
            else
            {
                Debug.LogError("DASDK: In the game object [" + this.gameObject.name + "] under DA_Menu_Control, the spriteTargetObject did not have a SpriteRenderer or Image component attached! One of these is required for SpriteScrolling!");
            }
        }


        // Menu control
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

    public void ResetCurrentSelection()
    {
        currentSelection = 0;
    }

    void ErrorCheck()
    {
        if (textTargetObjects.Length != sprites.Length && scrollStrings && scrollSprites)
        {
            Debug.LogError("DASDK: In the game object [" + this.gameObject.name + "] under DA_Menu_Control, the length of Sprite Scrolling and Text Scrolling must be the same!");
        }
    }
}
 