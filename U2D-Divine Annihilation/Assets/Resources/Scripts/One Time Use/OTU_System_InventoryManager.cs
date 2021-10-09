﻿//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: MRC
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OTU_System_InventoryManager : MonoBehaviour
{
    // Public variables
    public bool inventoryOpen;

    public Image playerSprite;
    public Text playerName;

    public Text playerGold;
    public Text playerLevel;
    public Text profileName;

    public GameObject inspectionMenu;

    public string[] consumableText;
    public string[] itemText;
    public string[] puzzleText;
    public string[] weaponText;

    // Private variables
    public bool acceptingInput = true;
    private GameObject[] itemSlots;
    private GameObject[] equipmentSlots;
    private GameObject inventoryRoot;
    private DA_Menu_Control itemsMenuController;
    private DA_Menu_Control equipmentMenuController;
    private string currentlySelecting;
    private string layout;
    private float iconX;
    private float iconY;


    // Reference variables
    public GameObject[] equipIcons;
    public GameObject itemPickupPrefab;
    private OTU_System_MenuManager menuManager;
    private OTU_System_InputManager inputManager;
    private OTU_System_TextboxManager textboxManager;
    private OTU_System_SaveManager saveManager;


    void Start()
    {
        menuManager = FindObjectOfType<OTU_System_MenuManager>();
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        textboxManager = FindObjectOfType<OTU_System_TextboxManager>();
        saveManager = FindObjectOfType<OTU_System_SaveManager>();

        inventoryRoot = gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;
        itemsMenuController = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<DA_Menu_Control>();
        equipmentMenuController = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<DA_Menu_Control>();
        profileName.text = saveManager.activeSave2.playerName;
        System.Array.Resize(ref itemSlots, 5);
        System.Array.Resize(ref equipmentSlots, 5);

        for (int i = 0; i < 4 + 1; i++)
        {
            itemSlots[i] = inventoryRoot.transform.GetChild(i).gameObject;
            equipmentSlots[i] = inventoryRoot.transform.GetChild(i+5).gameObject;
        }

    }


    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(1);     // The delay until it is accepting input again
        acceptingInput = true;                  // Allow input again
    }


    void Update()
    {
        UpdateInventory();
        //InteractTriggerEventOverwriteFix();
        // Open the inventory
        if (!inventoryOpen && !menuManager.menuActive && Input.GetKeyDown(inputManager.controls["Select"]) && acceptingInput)
        {
            acceptingInput = false;
            inventoryOpen = true;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(acceptInput());  // Apply Key press delay
        }

        // Close the inventory
        else if (inventoryOpen && menuManager.menuActive && Input.GetKeyDown(inputManager.controls["Select"]) && acceptingInput)
        {
            acceptingInput = false;
            inventoryOpen = false;
            //gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(0).GetComponent<DA_Menu_Control>().ResetCurrentSelection();
            //gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).transform.GetChild(1).GetComponent<DA_Menu_Control>().ResetCurrentSelection();
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            StartCoroutine(acceptInput());  // Apply Key press delay
        }
    }


    // ========================
    // Main inventory functions
    // ========================
    void UpdateInventory()
    {
        for (int i = 0; i < 4 + 1; i++)
        {
            itemSlots[i].gameObject.transform.GetChild(1).GetComponent<Text>().text = saveManager.activeSave2.items[i];
            itemSlots[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + saveManager.activeSave2.itemIcons[i]) as Sprite;
            equipmentSlots[i].gameObject.transform.GetChild(1).GetComponent<Text>().text = saveManager.activeSave2.equipment[i];
            equipmentSlots[i].gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Items/" + saveManager.activeSave2.equipmentIcons[i]) as Sprite;
        }
    }


    public void UpdatePlayerDescription(Sprite referencePlayerSprite, string referencePlayerName)
    {
        playerSprite.sprite = referencePlayerSprite;
        playerName.text = referencePlayerName;
    }


    public void OpenInventoryInspect()
    {
        if (itemsMenuController.enabled && saveManager.activeSave2.items[itemsMenuController.currentSelection] != "---")
        {
            currentlySelecting = "Items Menu";
            inspectionMenu.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<DA_Menu_Control>().enabled = false;

            if (saveManager.activeSave2.itemCategories[itemsMenuController.currentSelection] == "Item")
            {
                SetInventoryInspect(4, itemText);
                layout = "Item";
            }
            if (saveManager.activeSave2.itemCategories[itemsMenuController.currentSelection] == "Consumable")
            {
                SetInventoryInspect(4, consumableText);
                layout = "Consumable";
            }
            if (saveManager.activeSave2.itemCategories[itemsMenuController.currentSelection] == "Puzzle")
            {
                SetInventoryInspect(2, puzzleText);
                layout = "Puzzle";
            }
            if (itemsMenuController.currentSelection == 0) { inspectionMenu.transform.localPosition = new Vector2(165.5f, 22); }
            if (itemsMenuController.currentSelection == 1) { inspectionMenu.transform.localPosition = new Vector2(165.5f, -6); }
            if (itemsMenuController.currentSelection == 2) { inspectionMenu.transform.localPosition = new Vector2(165.5f, -34); }
            if (itemsMenuController.currentSelection == 3) { inspectionMenu.transform.localPosition = new Vector2(165.5f, -62); }
            if (itemsMenuController.currentSelection == 4) { inspectionMenu.transform.localPosition = new Vector2(165.5f, -90); }
        }

        if (equipmentMenuController.enabled && saveManager.activeSave2.equipment[equipmentMenuController.currentSelection] != "---")
        {
            currentlySelecting = "Equipment Menu";
            inspectionMenu.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<DA_Menu_Control>().enabled = false;

            if (saveManager.activeSave2.equipmentCategories[equipmentMenuController.currentSelection] == "Weapon")
            {
                SetInventoryInspect(4, weaponText);
                layout = "Equipment";
            }
            if (saveManager.activeSave2.equipmentCategories[equipmentMenuController.currentSelection] == "Armour")
            {
                SetInventoryInspect(4, weaponText);
                layout = "Equipment";
            }
            if (saveManager.activeSave2.equipmentCategories[equipmentMenuController.currentSelection] == "Magic")
            {
                SetInventoryInspect(4, weaponText);
                layout = "Equipment";
            }
            if (equipmentMenuController.currentSelection == 0) { inspectionMenu.transform.localPosition = new Vector2(288, 22); }
            if (equipmentMenuController.currentSelection == 1) { inspectionMenu.transform.localPosition = new Vector2(288, -6); }
            if (equipmentMenuController.currentSelection == 2) { inspectionMenu.transform.localPosition = new Vector2(288, -34); }
            if (equipmentMenuController.currentSelection == 3) { inspectionMenu.transform.localPosition = new Vector2(288, -62); }
            if (equipmentMenuController.currentSelection == 4) { inspectionMenu.transform.localPosition = new Vector2(288, -90); }
        }
    }


    public void CloseInventoryInspect()
    {
        inspectionMenu.GetComponent<DA_Menu_Control>().ResetCurrentSelection();
        inspectionMenu.SetActive(false);
        if (currentlySelecting == "Items Menu")
        {
            itemsMenuController.enabled = true;
        }
        else if (currentlySelecting == "Equipment Menu")
        {
            equipmentMenuController.enabled = true;
        }
    }

    void InteractTriggerEventOverwriteFix()
    {
        if (textboxManager.currentlyOverlappedTrigger != null)
        {
            if (inspectionMenu.activeInHierarchy)
            {
                textboxManager.currentlyOverlappedTrigger.GetComponent<DA_Trigger_Interact>().enabled = false;
            }
            else if (!inspectionMenu.activeInHierarchy)
            {
                textboxManager.currentlyOverlappedTrigger.GetComponent<DA_Trigger_Interact>().enabled = true;
            }
        }
    }


    // ==============================
    // Inventory inspect menu actions
    // ==============================
    void SetInventoryInspect(int length, string[] baseText)
    {
        inspectionMenu.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = false;
        inspectionMenu.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().enabled = false;
        inspectionMenu.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().enabled = false;
        inspectionMenu.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().enabled = false;

        if (length == 2)
        {
            inspectionMenu.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = true;
            inspectionMenu.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().enabled = true;
            System.Array.Resize(ref inspectionMenu.GetComponent<DA_Menu_Control>().textTargetObjects, 2);
            System.Array.Resize(ref inspectionMenu.GetComponent<DA_Menu_Control>().baseText, 2);
            System.Array.Resize(ref inspectionMenu.GetComponent<DA_Menu_Control>().hoveredText, 2);
            inspectionMenu.GetComponent<DA_Menu_Control>().textTargetObjects[0] = inspectionMenu.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
            inspectionMenu.GetComponent<DA_Menu_Control>().textTargetObjects[1] = inspectionMenu.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
            inspectionMenu.GetComponent<DA_Menu_Control>().baseText = baseText;
            inspectionMenu.GetComponent<DA_Menu_Control>().hoveredText = baseText;
        }
        if (length == 3)
        {
            inspectionMenu.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = true;
            inspectionMenu.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().enabled = true;
            inspectionMenu.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().enabled = true;
            System.Array.Resize(ref inspectionMenu.GetComponent<DA_Menu_Control>().textTargetObjects, 3);
            System.Array.Resize(ref inspectionMenu.GetComponent<DA_Menu_Control>().baseText, 3);
            System.Array.Resize(ref inspectionMenu.GetComponent<DA_Menu_Control>().hoveredText, 3);
            inspectionMenu.GetComponent<DA_Menu_Control>().textTargetObjects[0] = inspectionMenu.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
            inspectionMenu.GetComponent<DA_Menu_Control>().textTargetObjects[1] = inspectionMenu.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
            inspectionMenu.GetComponent<DA_Menu_Control>().textTargetObjects[2] = inspectionMenu.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
            inspectionMenu.GetComponent<DA_Menu_Control>().baseText = baseText;
            inspectionMenu.GetComponent<DA_Menu_Control>().hoveredText = baseText;
        }
        if (length == 4)
        {
            inspectionMenu.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().enabled = true;
            inspectionMenu.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>().enabled = true;
            inspectionMenu.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().enabled = true;
            inspectionMenu.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().enabled = true;
            System.Array.Resize(ref inspectionMenu.GetComponent<DA_Menu_Control>().textTargetObjects, 4);
            System.Array.Resize(ref inspectionMenu.GetComponent<DA_Menu_Control>().baseText, 4);
            System.Array.Resize(ref inspectionMenu.GetComponent<DA_Menu_Control>().hoveredText, 4);
            inspectionMenu.GetComponent<DA_Menu_Control>().textTargetObjects[0] = inspectionMenu.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
            inspectionMenu.GetComponent<DA_Menu_Control>().textTargetObjects[1] = inspectionMenu.gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
            inspectionMenu.GetComponent<DA_Menu_Control>().textTargetObjects[2] = inspectionMenu.gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
            inspectionMenu.GetComponent<DA_Menu_Control>().textTargetObjects[3] = inspectionMenu.gameObject.transform.GetChild(3).gameObject.GetComponent<Text>();
            inspectionMenu.GetComponent<DA_Menu_Control>().baseText = baseText;
            inspectionMenu.GetComponent<DA_Menu_Control>().hoveredText = baseText;
        }

    }

    public void SetInspectionFunctions(int selectionID)
    {
        if (layout == "Item")
        {
            if (selectionID == 0)
            {
                print("Use");
            }
            else if (selectionID == 1)
            {
                ItemInspect();
            }
            else if (selectionID == 2)
            {
                ItemSwap();
            }
            else if (selectionID == 3)
            {
                ItemDiscard();
            }
        }
        else if (layout == "Consumable")
        {
            if (selectionID == 0)
            {
                print("Use");
            }
            else if (selectionID == 1)
            {
                ItemInspect();
            }
            else if (selectionID == 2)
            {
                ItemSwap();
            }
            else if (selectionID == 3)
            {
                ItemDiscard();
            }
        }
        else if (layout == "Puzzle")
        {
            if (selectionID == 0)
            {
                ItemInspect();
            }
            else if (selectionID == 1)
            {
                ItemSwap();
            }
        }
        else if (layout == "Equipment")
        {
            if (selectionID == 0)
            {
                ItemEquip();
            }
            else if (selectionID == 1)
            {
                ItemInspect();
            }
            else if (selectionID == 2)
            {
                ItemSwap();
            }
            else if (selectionID == 3)
            {
                ItemDiscard();
            }
        }
    }

    public void ItemEquip()
    {
        if (saveManager.activeSave2.equipmentCategories[equipmentMenuController.currentSelection] == "Utility")
        {
            saveManager.activeSave2.equippedU = equipmentMenuController.currentSelection + 1;
            GetEquipIconPositions(out iconX, out iconY);
            equipIcons[0].transform.localPosition = new Vector3(iconX, iconY, 0);
        }
        if (saveManager.activeSave2.equipmentCategories[equipmentMenuController.currentSelection] == "Weapon")
        {
            saveManager.activeSave2.equippedW = equipmentMenuController.currentSelection + 1;
            GetEquipIconPositions(out iconX, out iconY);
            equipIcons[1].transform.localPosition = new Vector3(iconX, iconY, 0);
        }
        if (saveManager.activeSave2.equipmentCategories[equipmentMenuController.currentSelection] == "Magic")
        {
            saveManager.activeSave2.equippedM = equipmentMenuController.currentSelection + 1;
            GetEquipIconPositions(out iconX, out iconY);
            equipIcons[2].transform.localPosition = new Vector3(iconX, iconY, 0);
        }
        if (saveManager.activeSave2.equipmentCategories[equipmentMenuController.currentSelection] == "Defense")
        {
            saveManager.activeSave2.equippedD = equipmentMenuController.currentSelection + 1;
            GetEquipIconPositions(out iconX, out iconY);
            equipIcons[3].transform.localPosition = new Vector3(iconX, iconY, 0);
        }
        CloseInventoryInspect();
    }

    public void SystemEquipItem(int UWMD, int slot, bool equipment)
    {
        if (equipment)
        {
            iconX = 167.25f;
        }
        else
        {
            iconX = 43;
        }
        if (slot == 1) { iconY = -6.5f; }
        if (slot == 2) { iconY = -34.5f; }
        if (slot == 3) { iconY = -62.5f; }
        if (slot == 4) { iconY = -90.5f; }
        if (slot == 5) { iconY = -118.5f; }

        equipIcons[UWMD].transform.localPosition = new Vector3(iconX, iconY, 0);
    }

    void GetEquipIconPositions(out float iconXF, out float iconYF)
    {
        iconXF = 0;
        iconYF = 0;
        if (currentlySelecting == "Items Menu")
        {
            iconXF = 43;
            if (itemsMenuController.currentSelection == 0) { iconYF = -6.5f; }
            if (itemsMenuController.currentSelection == 1) { iconYF = -34.5f; }
            if (itemsMenuController.currentSelection == 2) { iconYF = -62.5f; }
            if (itemsMenuController.currentSelection == 3) { iconYF = -90.5f; }
            if (itemsMenuController.currentSelection == 4) { iconYF = -118.5f; }
        }
        else if (currentlySelecting == "Equipment Menu")
        {
            iconXF = 167.25f;
            if (equipmentMenuController.currentSelection == 0) { iconYF = -6.5f; }
            if (equipmentMenuController.currentSelection == 1) { iconYF = -34.5f; }
            if (equipmentMenuController.currentSelection == 2) { iconYF = -62.5f; }
            if (equipmentMenuController.currentSelection == 3) { iconYF = -90.5f; }
            if (equipmentMenuController.currentSelection == 4) { iconYF = -118.5f; }
        }
        else
        {
            print("failed collection");
        }
    }
    public void ItemInspect()
    {
        if (currentlySelecting == "Items Menu")
        {
            textboxManager.TextboxNonarray(saveManager.activeSave2.itemDescriptions[itemsMenuController.currentSelection], "", textboxManager.noPortrait);
        }
        else if (currentlySelecting == "Equipment Menu")
        {
            textboxManager.TextboxNonarray(saveManager.activeSave2.equipmentDescriptions[equipmentMenuController.currentSelection], "", textboxManager.noPortrait);
        }

        textboxManager.specialUseCase = "Inventory Inspect";
        inspectionMenu.GetComponent<DA_Menu_Control>().enabled = false;
        acceptingInput = false;
    }


    public void ItemSwap()
    {
        textboxManager.TextboxNonarray("Item swaping is not yet available.", "", textboxManager.noPortrait);
        textboxManager.specialUseCase = "Inventory Inspect";
        inspectionMenu.GetComponent<DA_Menu_Control>().enabled = false;
        acceptingInput = false;
    }


    public void ItemAdd(string itemName, string itemCategory, Sprite itemIcon, string itemDescription, bool itemDiscardable)
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
                        print("Equipment was added to the inventory!");
                        break;
                    }
                }
            }
    }


    public void ItemDiscard()
    {
        if (currentlySelecting == "Items Menu")
        {
            if (saveManager.activeSave2.itemDiscardable[itemsMenuController.currentSelection] == "True")
            {
                //Instantiate(itemPickupPrefab, GameObject.FindWithTag("Player").transform, false);

                saveManager.activeSave2.items[itemsMenuController.currentSelection] = "---";
                saveManager.activeSave2.itemIcons[itemsMenuController.currentSelection] = "hud_inventory_blank";
                saveManager.activeSave2.itemCategories[itemsMenuController.currentSelection] = "";
                saveManager.activeSave2.itemDescriptions[itemsMenuController.currentSelection] = "";
                saveManager.activeSave2.itemDiscardable[itemsMenuController.currentSelection] = "false";
                CloseInventoryInspect();
            }
            else
            {
                textboxManager.TextboxNonarray("This item can not be discarded!", "", textboxManager.noPortrait);
                textboxManager.specialUseCase = "Inventory Inspect";
                inspectionMenu.GetComponent<DA_Menu_Control>().enabled = false;
                acceptingInput = false;
            }
        }
        else if (currentlySelecting == "Equipment Menu")
        {
            if (saveManager.activeSave2.equipmentDiscardable[itemsMenuController.currentSelection] == "True")
            {
                //Instantiate(itemPickupPrefab, GameObject.FindWithTag("Player").transform, true);

                saveManager.activeSave2.equipment[equipmentMenuController.currentSelection] = "---";
                saveManager.activeSave2.equipmentIcons[equipmentMenuController.currentSelection] = "hud_inventory_blank";
                saveManager.activeSave2.equipmentCategories[equipmentMenuController.currentSelection] = "";
                saveManager.activeSave2.equipmentDescriptions[equipmentMenuController.currentSelection] = "";
                saveManager.activeSave2.equipmentDiscardable[equipmentMenuController.currentSelection] = "false";
                CloseInventoryInspect();
            }
            else
            {
                textboxManager.TextboxNonarray("This equipment can not be discarded!", "", textboxManager.noPortrait);
                textboxManager.specialUseCase = "Inventory Inspect";
                inspectionMenu.GetComponent<DA_Menu_Control>().enabled = false;
                acceptingInput = false;
            }
        }
    }
}