using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudInventory : MonoBehaviour
{
    // Referances
    public GameObject inventoryBoxObject;   // A referance to the inventory box object
    public Text inventoryNameObject;        // A referance to the inventory name object
    public Image inventoryCharacterObject;  // A referance to the inventory character object
    public Text inventoryGoldObject;        // A referance to the inventory gold object
    public Text inventorylevelObject;       // A referance to the inventory level object
    public bool inventoryBoxActive;
    public bool acceptingInput;
    private CharacterMovement characterMovement; // A referance to the player movement so the dialogue box can freeze the player when it opens
    private SystemConfigManager global;

    public bool inItems = true;
    public Image[] items;
    public Image[] equipment;
    public bool wrapAround;
    public int currentFrame;
    public Sprite selected;
    public Sprite notSelected;

    // Start is called before the first frame update
    void Start()
    {
        // selectedButton.Select();
        characterMovement = FindObjectOfType<CharacterMovement>(); // Find the character movment script
        global = FindObjectOfType<SystemConfigManager>(); // Find the config script
        inItems = true;
    }

    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(0.05f);
        acceptingInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Close inventory
        if (inventoryBoxActive && Input.GetKeyDown("c"))
        {
            if (acceptingInput)
            {
                //items[currentFrame].sprite = notSelected;
                //equipment[currentFrame].sprite = notSelected;
                //currentFrame = 0;
                //inItems = true;
                characterMovement.movementSpeed = characterMovement.storedSpeed; // Set the players speed so they won't get stuck with a movement speed of zero
                inventoryBoxActive = false;
                inventoryBoxObject.SetActive(false); // Make the box disappear
                acceptingInput = false;
                StartCoroutine("acceptInput");
            }
        }

        // Open inventory
        if (!inventoryBoxActive && Input.GetKeyDown("c") && !global.menuActive)
        {
            if (acceptingInput)
            {
                inventoryBoxActive = true;
                inventoryBoxObject.SetActive(true); // Make the box appear
                acceptingInput = false;
                StartCoroutine("acceptInput");
            }
        }


        // Vertical scrolling
        if (inItems && inventoryBoxActive)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (currentFrame == 0 && wrapAround)
                {
                    currentFrame = items.Length; // Up arrow wrap around
                    items[currentFrame + 1].sprite = notSelected;
                }

                if (currentFrame != 0)
                {
                    currentFrame -= 1; // Up arrow scrolling
                    items[currentFrame + 1].sprite = notSelected;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (currentFrame == items.Length - 1 && wrapAround)
                {
                    currentFrame = -1; // Up arrow wrap around
                    items[currentFrame - 1].sprite = notSelected;
                }

                if (currentFrame != items.Length - 1)
                {
                    currentFrame += 1; // Up arrow scrolling
                    items[currentFrame - 1].sprite = notSelected;
                }
            }
            items[currentFrame].sprite = selected;


            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                items[currentFrame].sprite = notSelected;
                inItems = false;
            }
        }


        if (!inItems && inventoryBoxActive)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (currentFrame == 0 && wrapAround)
                {
                    currentFrame = equipment.Length; // Up arrow wrap around
                    equipment[currentFrame + 1].sprite = notSelected;
                }

                if (currentFrame != 0)
                {
                    currentFrame -= 1; // Up arrow scrolling
                    equipment[currentFrame + 1].sprite = notSelected;
                }
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (currentFrame == equipment.Length - 1 && wrapAround)
                {
                    currentFrame = -1; // Up arrow wrap around
                    equipment[currentFrame - 1].sprite = notSelected;
                }

                if (currentFrame != equipment.Length - 1)
                {
                    currentFrame += 1; // Up arrow scrolling
                    equipment[currentFrame - 1].sprite = notSelected;
                }
            }
            equipment[currentFrame].sprite = selected;


            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                equipment[currentFrame].sprite = notSelected;
                inItems = true;
            }
        }
    }
}
