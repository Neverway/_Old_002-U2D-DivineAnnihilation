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
    private bool acceptingInput;
    private SaveManager saveManager;

    void Start()
    {
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


    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(1);     // The delay until it is accepting input again
        acceptingInput = true;                  // Allow input again
    }


    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox")
        {
            if (Input.GetKeyDown("z") && !unlocked)
            {
                // Slot 1
                if (saveManager.activeSave.item1 == keyItemName)
                {
                    if (removeFromInventory)
                    {
                        saveManager.activeSave.item1 = "---";
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
                        gameObject.GetComponent<Trigger_Interact>().acceptingInput = true;
                    }
                    unlocked = true;
                }
            }
        }
    }
}