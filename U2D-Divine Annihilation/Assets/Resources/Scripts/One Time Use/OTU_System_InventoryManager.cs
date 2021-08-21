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

    // Private variables
    public bool acceptingInput = true;
    private GameObject[] itemSlots;
    private GameObject[] equipmentSlots;
    private GameObject inventoryRoot;

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
}