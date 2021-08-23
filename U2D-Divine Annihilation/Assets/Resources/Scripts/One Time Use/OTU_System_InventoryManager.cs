//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
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


    // Reference variables
    private OTU_System_MenuManager menuManager;
    private OTU_System_InputManager inputManager;
    private OTU_System_SaveManager saveManager;


    void Start()
    {
        menuManager = FindObjectOfType<OTU_System_MenuManager>();
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        saveManager = FindObjectOfType<OTU_System_SaveManager>();

        inventoryRoot = gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject;
        itemsMenuController = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<DA_Menu_Control>();
        equipmentMenuController = gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<DA_Menu_Control>();
        profileName.text = saveManager.activeSave2.saveProfileName;
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

    public void UpdatePlayerDescription(Sprite referencePlayerSprite, string referencePlayerName)
    {
        playerSprite.sprite = referencePlayerSprite;
        playerName.text = referencePlayerName;
    }

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

    public void ItemInspectionMenu()
    {
        if (itemsMenuController.enabled && saveManager.activeSave2.items[itemsMenuController.currentSelection] != "---")
        {

            inspectionMenu.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<DA_Menu_Control>().enabled = false;

            if (saveManager.activeSave2.itemCategories[itemsMenuController.currentSelection] == "Item")
            {
                setInspectionMenu(3, itemText);
            }
            if (saveManager.activeSave2.itemCategories[itemsMenuController.currentSelection] == "Consumable")
            {
                setInspectionMenu(4, consumableText);
            }
            if (saveManager.activeSave2.itemCategories[itemsMenuController.currentSelection] == "Puzzle")
            {
                setInspectionMenu(2, puzzleText);
            }
            if (itemsMenuController.currentSelection == 0) { inspectionMenu.transform.localPosition = new Vector2(165.5f, 22); }
            if (itemsMenuController.currentSelection == 1) { inspectionMenu.transform.localPosition = new Vector2(165.5f, -6); }
            if (itemsMenuController.currentSelection == 2) { inspectionMenu.transform.localPosition = new Vector2(165.5f, -34); }
            if (itemsMenuController.currentSelection == 3) { inspectionMenu.transform.localPosition = new Vector2(165.5f, -62); }
            if (itemsMenuController.currentSelection == 4) { inspectionMenu.transform.localPosition = new Vector2(165.5f, -90); }
        }

        if (equipmentMenuController.enabled && saveManager.activeSave2.equipment[equipmentMenuController.currentSelection] != "---")
        {

            inspectionMenu.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<DA_Menu_Control>().enabled = false;

            if (saveManager.activeSave2.equipmentCategories[equipmentMenuController.currentSelection] == "Weapon")
            {
                setInspectionMenu(4, weaponText);
            }
            if (saveManager.activeSave2.equipmentCategories[equipmentMenuController.currentSelection] == "Armour")
            {
                setInspectionMenu(4, weaponText);
            }
            if (saveManager.activeSave2.equipmentCategories[equipmentMenuController.currentSelection] == "Magic")
            {
                setInspectionMenu(4, weaponText);
            }
            if (equipmentMenuController.currentSelection == 0) { inspectionMenu.transform.localPosition = new Vector2(288, 22); }
            if (equipmentMenuController.currentSelection == 1) { inspectionMenu.transform.localPosition = new Vector2(288, -6); }
            if (equipmentMenuController.currentSelection == 2) { inspectionMenu.transform.localPosition = new Vector2(288, -34); }
            if (equipmentMenuController.currentSelection == 3) { inspectionMenu.transform.localPosition = new Vector2(288, -62); }
            if (equipmentMenuController.currentSelection == 4) { inspectionMenu.transform.localPosition = new Vector2(288, -90); }
        }
    }

    void setInspectionMenu(int length, string[] baseText)
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
}