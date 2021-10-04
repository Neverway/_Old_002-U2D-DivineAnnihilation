//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: MRC
// Purpose: Checks if any menus are open and if so stop the player from moving
// Applied to: Config object in a scene
// Editor script: 
// Notes: Some variables names could probably be changed to make this script
//  neater & Comments need to be added.
//
//=============================================================================
using UnityEngine;

public class OTU_System_MenuManager : MonoBehaviour
{
    // Public varaibles
    public bool menuActive; // A variable to keep track of whether or not the player is in a menu (If they are then stop them from moving and stuff)

    // Reference variables
    private OTU_System_TextboxManager textboxManager;
    private OTU_Overworld_ShopboxManager shopboxManager;
    private OTU_System_InventoryManager inventoryManager;
    private OTU_System_PauseManager pauseManager;
    private DA_Entity_Control characterController;
    private GameObject player;


    void Awake()
    {
        FindReferenceObjects();
        menuActive = false;
    }

    void Update()
    {
        FindReferenceObjects();
        CheckForActiveMenus();
    }

    public void CheckForActiveMenus()
    {
        if (textboxManager != null && shopboxManager != null && inventoryManager != null && characterController != null)
        {
            // A menu is active, so stop the player
            if (textboxManager.textboxActive || shopboxManager.shopboxActive || textboxManager.otherboxActive || inventoryManager.inventoryOpen || pauseManager.pauseMenuOpen)
            {
                menuActive = true;
                characterController.canMove = false;
            }

            // No menus are active, allow the player to move
            else if (!textboxManager.textboxActive && !shopboxManager.shopboxActive && !textboxManager.otherboxActive && !inventoryManager.inventoryOpen && !pauseManager.pauseMenuOpen)
            {
                menuActive = false;
                characterController.canMove = true;
            }
        }
    }

    void FindReferenceObjects()
    {
        textboxManager = FindObjectOfType<OTU_System_TextboxManager>();
        shopboxManager = FindObjectOfType<OTU_Overworld_ShopboxManager>();
        inventoryManager = FindObjectOfType<OTU_System_InventoryManager>();
        pauseManager = FindObjectOfType<OTU_System_PauseManager>();
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            characterController = player.GetComponent<DA_Entity_Control>();
        }
    }
}
