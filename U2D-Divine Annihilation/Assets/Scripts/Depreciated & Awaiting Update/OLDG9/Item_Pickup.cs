using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item_Pickup : MonoBehaviour
{
    [Header("The itemName is the name that will appear in the players inventory, so abreviate it or keep it short")]
    public string itemName;
    [Header("The itemIcon sprite must be in Resources/Sprites/Items otherwise it won't load properly")]
    public Sprite itemIcon;
    [Header("Item cats: Item, Consumable, Puzzle | Equipment cats: Weapon, Armour, Magic")]
    public string itemCategory;

    private bool triggered;
    public UnityEvent onPickup;
    private System_InputManager inputManager;
    private SaveManager saveManager;

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
        saveManager = FindObjectOfType<SaveManager>();
        triggered = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox")
        {
            if (Input.GetKeyDown(inputManager.controls["Interact"]) && !triggered)
            {
                if (itemCategory == "Item" || itemCategory == "Consumable" || itemCategory == "Puzzle")
                {
                    if (saveManager.activeSave.item1 == "---")
                    {
                        saveManager.activeSave.item1 = itemName;
                        saveManager.activeSave.item1Icon = itemIcon.name;
                        onPickup.Invoke();
                        triggered = true;
                    }

                    else if (saveManager.activeSave.item2 == "---")
                    {
                        saveManager.activeSave.item2 = itemName;
                        saveManager.activeSave.item2Icon = itemIcon.name;
                        onPickup.Invoke();
                        triggered = true;
                    }

                    else if (saveManager.activeSave.item3 == "---")
                    {
                        saveManager.activeSave.item3 = itemName;
                        saveManager.activeSave.item3Icon = itemIcon.name;
                        onPickup.Invoke();
                        triggered = true;
                    }

                    else if (saveManager.activeSave.item4 == "---")
                    {
                        saveManager.activeSave.item4 = itemName;
                        saveManager.activeSave.item4Icon = itemIcon.name;
                        onPickup.Invoke();
                        triggered = true;
                    }

                    else if (saveManager.activeSave.item5 == "---")
                    {
                        saveManager.activeSave.item5 = itemName;
                        saveManager.activeSave.item5Icon = itemIcon.name;
                        onPickup.Invoke();
                        triggered = true;
                    }
                }

                else if (itemCategory == "Weapon" || itemCategory == "Armour" || itemCategory == "Magic")
                {
                    if (saveManager.activeSave.equipment1 == "---")
                    {
                        saveManager.activeSave.equipment1 = itemName;
                        saveManager.activeSave.equipment1Icon = itemIcon.name;
                        onPickup.Invoke();
                        triggered = true;
                    }

                    else if (saveManager.activeSave.equipment2 == "---")
                    {
                        saveManager.activeSave.equipment2 = itemName;
                        saveManager.activeSave.equipment2Icon = itemIcon.name;
                        onPickup.Invoke();
                        triggered = true;
                    }

                    else if (saveManager.activeSave.equipment3 == "---")
                    {
                        saveManager.activeSave.equipment3 = itemName;
                        saveManager.activeSave.equipment3Icon = itemIcon.name;
                        onPickup.Invoke();
                        triggered = true;
                    }

                    else if (saveManager.activeSave.equipment4 == "---")
                    {
                        saveManager.activeSave.equipment4 = itemName;
                        saveManager.activeSave.equipment4Icon = itemIcon.name;
                        onPickup.Invoke();
                        triggered = true;
                    }

                    else if (saveManager.activeSave.equipment5 == "---")
                    {
                        saveManager.activeSave.equipment5 = itemName;
                        saveManager.activeSave.equipment5Icon = itemIcon.name;
                        onPickup.Invoke();
                        triggered = true;
                    }
                }

                else
                {
                    Debug.LogError("[ID002 DA]: " + "The item category for this item could not be found. Please use Item, Consumable, or Puzzle for an item, or Weapon, Armour, or Magic for equipment.");
                }

                if (triggered)
                {
                    Object.Destroy(this.gameObject);
                }
            }
        }
    }
}
