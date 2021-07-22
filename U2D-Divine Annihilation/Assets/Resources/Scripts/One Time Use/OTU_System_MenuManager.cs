//=========== Written by Arthur W. Sheldon AKA Lizband_UCC =============================
//
// Purpose: Checks if any menus are open and if so stop the player from moving
// Applied to: Config object in a scene
// Notes: Some variables names could probably be changed to make this script neater &
// Comments need to be added.
//
//======================================================================================

using UnityEngine;

public class OTU_System_MenuManager : MonoBehaviour
{
    // Public varaibles
    public bool menuActive; // A variable to keep track of whether or not the player is in a menu (If they are then stop them from moving and stuff)

    // Reference variables
    //private OTU_HUD_DialogueManager DialogueManager;
    //private OTU_HUD_ChoiceboxManager ChoiceboxManager;
    //private OTU_HUD_InventoryManager InventoryManager;
    //private OTU_HUD_SpecialInteractionsManager SIManager;
    //private DA_Entity_CharacterController CharacterController;


    void Awake()
    {
        menuActive = false;
        FindReferenceObjects();
    }

    void Update()
    {
        CheckForActiveMenus();
    }

    public void CheckForActiveMenus()
    {
        //if (DialogueManager != null && InventoryManager != null)
        //{
        //    // A menu is active, so stop the player
        //    if (DialogueManager.dialogueBoxActive | InventoryManager.inventoryBoxActive | SIManager.siBoxActive | ChoiceboxManager.choiceBoxActive)
        //    {
        //        menuActive = true;
        //        CharacterController.canMove = false;
        //    }

        //    // No menus are active, allow the player to move
        //    else if (!DialogueManager.dialogueBoxActive && !InventoryManager.inventoryBoxActive && !SIManager.siBoxActive && !ChoiceboxManager.choiceBoxActive)
        //    {
        //        menuActive = false;
        //        CharacterController.canMove = true;
        //    }
        //}
    }

    void FindReferenceObjects()
    {
        //DialogueManager = FindObjectOfType<OTU_HUD_DialogueManager>();
        //ChoiceboxManager = FindObjectOfType<OTU_HUD_ChoiceboxManager>();
        //InventoryManager = FindObjectOfType<OTU_HUD_InventoryManager>();
        //SIManager = FindObjectOfType<OTU_HUD_SpecialInteractionsManager>();
        //CharacterController = FindObjectOfType<DA_Entity_CharacterController>();
    }
}
