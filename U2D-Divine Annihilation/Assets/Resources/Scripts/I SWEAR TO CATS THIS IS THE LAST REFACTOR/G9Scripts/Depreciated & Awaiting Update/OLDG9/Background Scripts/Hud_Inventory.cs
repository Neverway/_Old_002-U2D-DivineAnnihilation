//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Display the players inventory
// Applied to: InventoryManager object in an overworld scene
//
//=============================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Hud_Inventory : MonoBehaviour
{
    public GameObject inventoryBoxObject;   // A referance to the inventory box object
    public Text inventoryNameObject;        // A referance to the inventory name object
    public Image inventoryCharacterObject;  // A referance to the inventory character object
    public Text inventoryGoldObject;        // A referance to the inventory gold object
    public Text inventorylevelObject;       // A referance to the inventory level object
    public bool inventoryBoxActive;
    public bool acceptingInput;
    private System_InputManager inputManager;
    private Entity_Character_Movement characterMovement; // A referance to the player movement so the dialogue box can freeze the player when it opens
    private System_Config_Manager global;
    private SaveManager saveManager;

    public bool inItems = true;
    public Image[] itemSlots;
    public Image[] itemIcons;
    public Text[] itemNames;
    public Image[] equipmentSlots;
    public Image[] equipmentIcons;
    public Text[] equipmentNames;
    public bool wrapAround;
    public int currentFrame;
    public Sprite selected;
    public Sprite notSelected;
    public string test;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
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
        if (inventoryBoxActive && Input.GetKeyDown(inputManager.controls["Select"]))
        {
            if (acceptingInput)
            {
                itemSlots[currentFrame].sprite = notSelected;
                equipmentSlots[currentFrame].sprite = notSelected;
                currentFrame = 0;
                inItems = true;
                Resources.UnloadUnusedAssets();
                characterMovement.movementSpeed = characterMovement.walkSpeed; // Set the players speed so they won't get stuck with a movement speed of zero
                inventoryBoxActive = false;
                inventoryBoxObject.SetActive(false); // Make the box disappear
                acceptingInput = false;
                StartCoroutine("acceptInput");
            }
        }

        // Open inventory
        if (!inventoryBoxActive && Input.GetKeyDown(inputManager.controls["Select"]) && !global.menuActive)
        {
            if (acceptingInput)
            {
                inventoryBoxActive = true;
                inventoryBoxObject.SetActive(true); // Make the box appear
                acceptingInput = false;
                StartCoroutine("acceptInput");
            }
        }


        if (inventoryBoxActive)
        {
            // Display item icons
            itemIcons[0].sprite = Resources.Load<Sprite>("Sprites/Items/" + saveManager.activeSave.item1Icon) as Sprite;
            itemIcons[1].sprite = Resources.Load<Sprite>("Sprites/Items/" + saveManager.activeSave.item2Icon) as Sprite;
            itemIcons[2].sprite = Resources.Load<Sprite>("Sprites/Items/" + saveManager.activeSave.item3Icon) as Sprite;
            itemIcons[3].sprite = Resources.Load<Sprite>("Sprites/Items/" + saveManager.activeSave.item4Icon) as Sprite;
            itemIcons[4].sprite = Resources.Load<Sprite>("Sprites/Items/" + saveManager.activeSave.item5Icon) as Sprite;

            equipmentIcons[0].sprite = Resources.Load<Sprite>("Sprites/Items/" + saveManager.activeSave.equipment1Icon) as Sprite;
            equipmentIcons[1].sprite = Resources.Load<Sprite>("Sprites/Items/" + saveManager.activeSave.equipment2Icon) as Sprite;
            equipmentIcons[2].sprite = Resources.Load<Sprite>("Sprites/Items/" + saveManager.activeSave.equipment3Icon) as Sprite;
            equipmentIcons[3].sprite = Resources.Load<Sprite>("Sprites/Items/" + saveManager.activeSave.equipment4Icon) as Sprite;
            equipmentIcons[4].sprite = Resources.Load<Sprite>("Sprites/Items/" + saveManager.activeSave.equipment5Icon) as Sprite;

            // Display item names
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
            if (Input.GetKeyDown(inputManager.controls["Up"])) // up
            {
                if (currentFrame == 0 && wrapAround)
                {
                    currentFrame = itemSlots.Length; // Up arrow wrap around
                    itemSlots[currentFrame + 1].sprite = notSelected;
                }

                if (currentFrame != 0)
                {
                    currentFrame -= 1; // Up arrow scrolling
                    itemSlots[currentFrame + 1].sprite = notSelected;
                }
            }
            if (Input.GetKeyDown(inputManager.controls["Down"])) // down
            {
                if (currentFrame == itemSlots.Length - 1 && wrapAround)
                {
                    currentFrame = -1; // Up arrow wrap around
                    itemSlots[currentFrame - 1].sprite = notSelected;
                }

                if (currentFrame != itemSlots.Length - 1)
                {
                    currentFrame += 1; // Up arrow scrolling
                    itemSlots[currentFrame - 1].sprite = notSelected;
                }
            }
            itemSlots[currentFrame].sprite = selected;


            if (Input.GetKeyDown(inputManager.controls["Right"])) // Right
            {
                itemSlots[currentFrame].sprite = notSelected;
                inItems = false;
            }
        }


        if (!inItems && inventoryBoxActive)
        {
            if (Input.GetKeyDown(inputManager.controls["Up"])) // Up
            {
                if (currentFrame == 0 && wrapAround)
                {
                    currentFrame = equipmentSlots.Length; // Up arrow wrap around
                    equipmentSlots[currentFrame + 1].sprite = notSelected;
                }

                if (currentFrame != 0)
                {
                    currentFrame -= 1; // Up arrow scrolling
                    equipmentSlots[currentFrame + 1].sprite = notSelected;
                }
            }
            if (Input.GetKeyDown(inputManager.controls["Down"])) // down
            {
                if (currentFrame == equipmentSlots.Length - 1 && wrapAround)
                {
                    currentFrame = -1; // Up arrow wrap around
                    equipmentSlots[currentFrame - 1].sprite = notSelected;
                }

                if (currentFrame != equipmentSlots.Length - 1)
                {
                    currentFrame += 1; // Up arrow scrolling
                    equipmentSlots[currentFrame - 1].sprite = notSelected;
                }
            }
            equipmentSlots[currentFrame].sprite = selected;


            if (Input.GetKeyDown(inputManager.controls["Left"])) // Left
            {
                equipmentSlots[currentFrame].sprite = notSelected;
                inItems = true;
            }
        }
    }
}
