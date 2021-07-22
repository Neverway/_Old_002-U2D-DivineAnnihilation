//=========== Written by Arthur W. Sheldon AKA Lizband_UCC =============================
//
// Purpose: Controlls the game configuration and settings
// Applied to: Config object in a scene
//
//======================================================================================

using UnityEngine;

public class System_Config_Manager : MonoBehaviour
{
    public bool onTitleScreen;
    public bool menuActive;
    public bool canMove;
    public bool overrideCanMove;

    private Hud_Textbox_Manager DialogueManager;
    private Hud_Inventory InventoryManager;
    private Hud_SI_Controller SIController;
    private Hud_Choicebox_Manager ChoiceboxManager;
    private Entity_Character_Movement characterMovement;

    void Start()
    {
        menuActive = false;
        DialogueManager = FindObjectOfType<Hud_Textbox_Manager>();
        InventoryManager = FindObjectOfType<Hud_Inventory>();
        SIController = FindObjectOfType<Hud_SI_Controller>();
        ChoiceboxManager = FindObjectOfType<Hud_Choicebox_Manager>();
        characterMovement = FindObjectOfType<Entity_Character_Movement>();
    }


    void Update()
    {
        DialogueManager = FindObjectOfType<Hud_Textbox_Manager>();
        InventoryManager = FindObjectOfType<Hud_Inventory>();
        SIController = FindObjectOfType<Hud_SI_Controller>();
        ChoiceboxManager = FindObjectOfType<Hud_Choicebox_Manager>();
        characterMovement = FindObjectOfType<Entity_Character_Movement>();
        if (DialogueManager != null && InventoryManager != null)
        {
            // Menu active
            if (DialogueManager.dialogueBoxActive | InventoryManager.inventoryBoxActive | SIController.siBoxActive | ChoiceboxManager.choiceBoxActive | overrideCanMove)
            {
                menuActive = true;
                characterMovement.canMove = false;
            }

            // No Menu active
            else if (!DialogueManager.dialogueBoxActive && !InventoryManager.inventoryBoxActive && !SIController.siBoxActive && !ChoiceboxManager.choiceBoxActive && !overrideCanMove)
            {
                menuActive = false;
                characterMovement.canMove = true;
            }
        }
    }
}
