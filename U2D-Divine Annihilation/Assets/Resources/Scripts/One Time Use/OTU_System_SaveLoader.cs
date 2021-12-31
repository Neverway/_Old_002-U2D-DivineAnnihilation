//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: 
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OTU_System_SaveLoader : MonoBehaviour
{
    // Public variables

    // Private variables
    public bool hasLoadedCurrentLevel;

    // Reference variables
    private OTU_System_SaveManager saveManager;
    private OTU_System_InventoryManager inventoryManager;




    IEnumerator CompleteLoading()
    {
        yield return new WaitForSeconds(0.6f);
        hasLoadedCurrentLevel = true;
    }

    void Start()
    {
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
        inventoryManager = FindObjectOfType<OTU_System_InventoryManager>();
        
        if (saveManager == null)
        {
            SceneManager.LoadScene("Main_Title");
            Debug.LogWarning("The scene was loaded abnormally so the failsafe level was loaded! In the future, please start the game from the title scene to avoid this issue (unless you are just debugging stuff I guess.)");
        }

        if (!hasLoadedCurrentLevel)
        {
            // Load the players position
            if (GameObject.FindWithTag("Player"))
            {
                GameObject.FindWithTag("Player").transform.position = saveManager.activeSave2.playerSavePosition;
            }

            // Load the players currently equipped items
            if (saveManager.activeSave2.equippedU == 1) { inventoryManager.SystemEquipItem(0, 1, true); }
            if (saveManager.activeSave2.equippedU == 2) { inventoryManager.SystemEquipItem(0, 2, true); }
            if (saveManager.activeSave2.equippedU == 3) { inventoryManager.SystemEquipItem(0, 3, true); }
            if (saveManager.activeSave2.equippedU == 4) { inventoryManager.SystemEquipItem(0, 4, true); }
            if (saveManager.activeSave2.equippedU == 5) { inventoryManager.SystemEquipItem(0, 5, true); }

            if (saveManager.activeSave2.equippedW == 1) { inventoryManager.SystemEquipItem(1, 1, true); }
            if (saveManager.activeSave2.equippedW == 2) { inventoryManager.SystemEquipItem(1, 2, true); }
            if (saveManager.activeSave2.equippedW == 3) { inventoryManager.SystemEquipItem(1, 3, true); }
            if (saveManager.activeSave2.equippedW == 4) { inventoryManager.SystemEquipItem(1, 4, true); }
            if (saveManager.activeSave2.equippedW == 5) { inventoryManager.SystemEquipItem(1, 5, true); }

            if (saveManager.activeSave2.equippedM == 1) { inventoryManager.SystemEquipItem(2, 1, true); }
            if (saveManager.activeSave2.equippedM == 2) { inventoryManager.SystemEquipItem(2, 2, true); }
            if (saveManager.activeSave2.equippedM == 3) { inventoryManager.SystemEquipItem(2, 3, true); }
            if (saveManager.activeSave2.equippedM == 4) { inventoryManager.SystemEquipItem(2, 4, true); }
            if (saveManager.activeSave2.equippedM == 5) { inventoryManager.SystemEquipItem(2, 5, true); }

            if (saveManager.activeSave2.equippedD == 1) { inventoryManager.SystemEquipItem(2, 1, true); }
            if (saveManager.activeSave2.equippedD == 2) { inventoryManager.SystemEquipItem(2, 2, true); }
            if (saveManager.activeSave2.equippedD == 3) { inventoryManager.SystemEquipItem(2, 3, true); }
            if (saveManager.activeSave2.equippedD == 4) { inventoryManager.SystemEquipItem(2, 4, true); }
            if (saveManager.activeSave2.equippedD == 5) { inventoryManager.SystemEquipItem(2, 5, true); }

            // Load the level progression data

            hasLoadedCurrentLevel = true;
            //StartCoroutine(CompleteLoading());
        }
    }
}