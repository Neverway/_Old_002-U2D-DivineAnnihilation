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
    public bool menuActive;     // A variable to keep track of whether or not the player is in a menu (If they are then stop them from moving and stuff)

    // Private varaibles
    private Hud_Textbox_Manager DialogueManager;
    private Hud_Inventory InventoryManager;
    private Hud_SI_Controller SIController;
    private Hud_Choicebox_Manager ChoiceboxManager;
    private Entity_Character_Movement characterMovement;


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

        if (DialogueManager != null && InventoryManager != null)
        {
            // Menu active
            if (DialogueManager.dialogueBoxActive | InventoryManager.inventoryBoxActive | SIController.siBoxActive | ChoiceboxManager.choiceBoxActive)
            {
                menuActive = true;
                characterMovement.canMove = false;
            }

            // No Menu active
            else if (!DialogueManager.dialogueBoxActive && !InventoryManager.inventoryBoxActive && !SIController.siBoxActive && !ChoiceboxManager.choiceBoxActive)
            {
                menuActive = false;
                characterMovement.canMove = true;
            }
        }
    }

    void FindReferenceObjects()
    {
        DialogueManager = FindObjectOfType<Hud_Textbox_Manager>();
        InventoryManager = FindObjectOfType<Hud_Inventory>();
        SIController = FindObjectOfType<Hud_SI_Controller>();
        ChoiceboxManager = FindObjectOfType<Hud_Choicebox_Manager>();
        characterMovement = FindObjectOfType<Entity_Character_Movement>();
    }
}
