using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemConfigManager : MonoBehaviour
{
    // Globals
    public bool menuActive;
    public bool canMove;
    public bool overrideCanMove;

    // Referances
    private HudTextboxManager DialogueManager;
    private HudInventory InventoryManager;
    private CharacterMovement characterMovement;


    // Start is called before the first frame update
    void Start()
    {
        menuActive = false;
        DialogueManager = FindObjectOfType<HudTextboxManager>();
        InventoryManager = FindObjectOfType<HudInventory>();
        characterMovement = FindObjectOfType<CharacterMovement>();
    }


    // Update is called once per frame
    void Update()
    {
        // Menu active
        if(DialogueManager.dialogueBoxActive | InventoryManager.inventoryBoxActive | overrideCanMove)
        {
            menuActive = true;
            characterMovement.canMove = false;
        }

        // No Menu active
        else if(!DialogueManager.dialogueBoxActive && !InventoryManager.inventoryBoxActive && !overrideCanMove)
        {
            menuActive = false;
            characterMovement.canMove = true;
        }
    }
}
