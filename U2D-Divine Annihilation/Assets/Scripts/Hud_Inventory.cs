//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Display the players inventory
// Applied to: InventoryManager object in an overworld scene
//
//=============================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Inventory : MonoBehaviour
{
    public GameObject inventoryBoxObject;   // A referance to the inventory box object
    public Text inventoryNameObject;        // A referance to the inventory name object
    public Image inventoryCharacterObject;  // A referance to the inventory character object
    public Text inventoryGoldObject;        // A referance to the inventory gold object
    public Text inventorylevelObject;       // A referance to the inventory level object
    public bool inventoryBoxActive;
    public bool acceptingInput;
    private Entity_Character_Movement characterMovement; // A referance to the player movement so the dialogue box can freeze the player when it opens
    private System_Config_Manager global;
    private SaveManager saveManager;

    public bool inItems = true;
    public Image[] items;
    public Image[] equipment;
    public Text[] itemNames;
    public Text[] equipmentNames;
    public bool wrapAround;
    public int currentFrame;
    public Sprite selected;
    public Sprite notSelected;

    void Start()
    {
        // selectedButton.Select();
        characterMovement = FindObjectOfType<Entity_Character_Movement>(); // Find the character movment script
        global = FindObjectOfType<System_Config_Manager>(); // Find the config script
        saveManager = FindObjectOfType<SaveManager>();
        inItems = true;
    }


    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(0.05f);
        acceptingInput = true;
    }


    void Update()
    {
        // Close inventory
        if (inventoryBoxActive && Input.GetButtonDown("Select"))
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
        if (!inventoryBoxActive && Input.GetButtonDown("Select") && !global.menuActive)
        {
            if (acceptingInput)
            {
                inventoryBoxActive = true;
                inventoryBoxObject.SetActive(true); // Make the box appear
                acceptingInput = false;
                StartCoroutine("acceptInput");
            }
        }

        // Display item names
        if (inventoryBoxActive)
        {
            itemNames[0].text = saveManager.activeSave.item1;
            itemNames[1].text = saveManager.activeSave.item2;
            itemNames[2].text = saveManager.activeSave.item3;
            itemNames[3].text = saveManager.activeSave.item4;
            itemNames[4].text = saveManager.activeSave.item5;

            equipmentNames[0].text = saveManager.activeSave.equipment1;
            equipmentNames[1].text = saveManager.activeSave.equipment2;
            equipmentNames[2].text = saveManager.activeSave.equipment3;
            equipmentNames[3].text = saveManager.activeSave.equipment4;
            equipmentNames[4].text = saveManager.activeSave.equipment5;
        }

        // Vertical scrolling
        if (inItems && inventoryBoxActive)
        {
            if (Input.GetAxis("Vertical") > 0) // up
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
            if (Input.GetAxis("Vertical") < 0) // down
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


            if (Input.GetAxis("Horizontal") > 0) // Right
            {
                items[currentFrame].sprite = notSelected;
                inItems = false;
            }
        }


        if (!inItems && inventoryBoxActive)
        {
            if (Input.GetAxis("Vertical") > 0) // Up
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
            if (Input.GetAxis("Vertical") < 0) // down
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


            if (Input.GetAxis("Horizontal") < 0) // Left
            {
                equipment[currentFrame].sprite = notSelected;
                inItems = true;
            }
        }
    }
}
