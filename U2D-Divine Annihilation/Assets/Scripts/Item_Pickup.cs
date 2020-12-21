using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pickup : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;
    public string itemCategory;
    //public string itemDescription;

    private bool triggered;
    private SaveManager saveManager;

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        triggered = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox")
        {
            if (Input.GetKeyDown("z") && !triggered)
            {
                if (itemCategory == "Item" || itemCategory == "Consumable" || itemCategory == "Puzzle")
                {
                    if (saveManager.activeSave.item1 == "---")
                    {
                        saveManager.activeSave.item1 = itemName;
                        triggered = true;
                    }

                    else if (saveManager.activeSave.item2 == "---")
                    {
                        saveManager.activeSave.item2 = itemName;
                        triggered = true;
                    }

                    else if (saveManager.activeSave.item3 == "---")
                    {
                        saveManager.activeSave.item3 = itemName;
                        triggered = true;
                    }

                    else if (saveManager.activeSave.item4 == "---")
                    {
                        saveManager.activeSave.item4 = itemName;
                        triggered = true;
                    }

                    else if (saveManager.activeSave.item5 == "---")
                    {
                        saveManager.activeSave.item5 = itemName;
                        triggered = true;
                    }
                }

                else if (itemCategory == "Weapon" || itemCategory == "Armour" || itemCategory == "Magic")
                {
                    if (saveManager.activeSave.equipment1 == "---")
                    {
                        saveManager.activeSave.equipment1 = itemName;
                        triggered = true;
                    }

                    else if (saveManager.activeSave.equipment2 == "---")
                    {
                        saveManager.activeSave.equipment2 = itemName;
                        triggered = true;
                    }

                    else if (saveManager.activeSave.equipment3 == "---")
                    {
                        saveManager.activeSave.equipment3 = itemName;
                        triggered = true;
                    }

                    else if (saveManager.activeSave.equipment4 == "---")
                    {
                        saveManager.activeSave.equipment4 = itemName;
                        triggered = true;
                    }

                    else if (saveManager.activeSave.equipment5 == "---")
                    {
                        saveManager.activeSave.equipment5 = itemName;
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
