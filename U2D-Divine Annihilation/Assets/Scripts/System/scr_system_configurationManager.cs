using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_system_configurationManager : MonoBehaviour
{
    // Globals
    public bool menuActive;
    public bool canMove;

    // Referances
    private scr_hud_textboxManager DialogueManager;
    private scr_menu_inventoryManager InventoryManager;
    private scr_character_movement characterMovement;


    // Start is called before the first frame update
    void Start()
    {
        menuActive = false;
        DialogueManager = FindObjectOfType<scr_hud_textboxManager>();
        InventoryManager = FindObjectOfType<scr_menu_inventoryManager>();
        characterMovement = FindObjectOfType<scr_character_movement>();
    }


    // Update is called once per frame
    void Update()
    {
        // Set the global.menuActive state
        if(DialogueManager.dialogueBoxActive | InventoryManager.inventoryBoxActive)
        {
            menuActive = true;
            characterMovement.canMove = false;
            canMove = false;
        }
        else if(!DialogueManager.dialogueBoxActive && !InventoryManager.inventoryBoxActive)
        {
            menuActive = false;
            characterMovement.canMove = true;
            characterMovement.movementSpeed = characterMovement.storedSpeed; // Set the players speed so they won't get stuck with a movement speed of zero
            canMove = true;
        }
    }
}
