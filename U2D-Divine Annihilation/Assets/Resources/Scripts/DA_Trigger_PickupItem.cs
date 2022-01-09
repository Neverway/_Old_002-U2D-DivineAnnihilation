//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: MRC
// Purpose: Add an item to the players inventory on interaction
// Applied to: An item pickup in an overworld scene
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DA_Trigger_PickupItem : MonoBehaviour
{
    // Public variables
    public string itemName;
    public string itemCategory;
    public Sprite itemIcon;
    public string itemDescription;
    public bool itemDiscardable;
    public UnityEvent OnPickup;

    // Private variables
    public bool inTrigger;
    public bool acceptingInput = true;

    // Reference variables
    private OTU_System_InputManager inputManager;
    private OTU_System_SaveManager saveManager;


    void Start()
    {
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
    }

    void Update()
    {
        if (inTrigger && Input.GetKeyDown(inputManager.controls["Interact"]) && acceptingInput == true)
        {
            if (itemCategory == "Item" || itemCategory == "Consumable" || itemCategory == "Puzzle")
            {
                for (int i = 0; i < 5 + 1; i++)
                {
                    if (saveManager.activeSave2.items[i] == "---")
                    {
                        saveManager.activeSave2.items[i] = itemName;
                        saveManager.activeSave2.itemIcons[i] = itemIcon.name;
                        saveManager.activeSave2.itemCategories[i] = itemCategory;
                        saveManager.activeSave2.itemDescriptions[i] = itemDescription;
                        saveManager.activeSave2.itemDiscardable[i] = itemDiscardable.ToString();
                        acceptingInput = false;
                        gameObject.SetActive(false);
                        print("An item was added to the inventory!");
                        break;
                    }
                }
            }
            else if (itemCategory == "Weapon" || itemCategory == "Armour" || itemCategory == "Magic")
            {
                for (int i = 0; i < 5 + 1; i++)
                {
                    if (saveManager.activeSave2.equipment[i] == "---")
                    {
                        saveManager.activeSave2.equipment[i] = itemName;
                        saveManager.activeSave2.equipmentIcons[i] = itemIcon.name;
                        saveManager.activeSave2.equipmentCategories[i] = itemCategory;
                        saveManager.activeSave2.equipmentDescriptions[i] = itemDescription;
                        saveManager.activeSave2.equipmentDiscardable[i] = itemDiscardable.ToString();
                        acceptingInput = false;
                        gameObject.SetActive(false);
                        print("Equipment was added to the inventory!");
                        break;
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = false;
        }
    }
}