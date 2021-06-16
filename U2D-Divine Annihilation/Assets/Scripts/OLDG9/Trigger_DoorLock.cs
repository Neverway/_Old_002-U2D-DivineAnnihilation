//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Unlock a door in a scene
// Applied to: Puzzle Lock Interact trigger
//
//=============================================================================

using System.Collections;
using UnityEngine;

public class Trigger_DoorLock : MonoBehaviour
{
    public string keyItemName;
    public bool removeFromInventory = true;
    public GameObject[] collisionRemovedOnUnlock;
    public Sprite unlockedSprite;
    public GameObject lockObject;

    private bool unlocked;
    private System_InputManager inputManager;
    private SaveManager saveManager;

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
        saveManager = FindObjectOfType<SaveManager>();
    }


    public void Update()
    {
        if (unlocked)
        {
            for (int i = 0; i < collisionRemovedOnUnlock.Length; i++)
            {
                Destroy(collisionRemovedOnUnlock[i]);
            }
            lockObject.GetComponent<SpriteRenderer>().sprite = unlockedSprite;
        }
    }


    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox")
        {
            if (Input.GetKeyDown(inputManager.controls["Interact"]) && !unlocked)
            {
                // Slot 1
                if (saveManager.activeSave.item1 == keyItemName)
                {
                    if (removeFromInventory)
                    {
                        saveManager.activeSave.item1 = "---";
                        saveManager.activeSave.item1Icon = "s_hud_inventory_blank";
                        gameObject.GetComponent<Trigger_Interact>().acceptingInput = true;
                    }
                    unlocked = true;
                }

                // Slot 2
                if (saveManager.activeSave.item2 == keyItemName)
                {
                    if (removeFromInventory)
                    {
                        saveManager.activeSave.item2 = "---";
                        saveManager.activeSave.item2Icon = "s_hud_inventory_blank";
                        gameObject.GetComponent<Trigger_Interact>().acceptingInput = true;
                    }
                    unlocked = true;
                }

                // Slot 3
                if (saveManager.activeSave.item3 == keyItemName)
                {
                    if (removeFromInventory)
                    {
                        saveManager.activeSave.item3 = "---";
                        saveManager.activeSave.item3Icon = "s_hud_inventory_blank";
                        gameObject.GetComponent<Trigger_Interact>().acceptingInput = true;
                    }
                    unlocked = true;
                }

                // Slot 4
                if (saveManager.activeSave.item4 == keyItemName)
                {
                    if (removeFromInventory)
                    {
                        saveManager.activeSave.item4 = "---";
                        saveManager.activeSave.item4Icon = "s_hud_inventory_blank";
                        gameObject.GetComponent<Trigger_Interact>().acceptingInput = true;
                    }
                    unlocked = true;
                }

                // Slot 5
                if (saveManager.activeSave.item5 == keyItemName)
                {
                    if (removeFromInventory)
                    {
                        saveManager.activeSave.item5 = "---";
                        saveManager.activeSave.item5Icon = "s_hud_inventory_blank";
                        gameObject.GetComponent<Trigger_Interact>().acceptingInput = true;
                    }
                    unlocked = true;
                }
            }
        }
    }
}