using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // Variables
    [Header ("Item information")]
    public string itemName;
    public string itemDescription;
    public string itemPickupText;

    public Sprite itemIcon;
    public bool itemDroppable;

    
    public enum itemCategory {Item, Equipment}
    [Header ("Item category")]
    public itemCategory ItemCategory;

    public enum itemTypeIfCatItem {Consumable, Crafting, KeyItem, Other, NULL}
    [Header ("If category is item")]
    public itemTypeIfCatItem ItemTypeIfCatItem;
    public int itemHPGivenOnConsume;

    public enum itemTypeIfCatEquipment {Weapon, Armour, Other, NULL}
    [Header ("If category is equipment")]
    public itemTypeIfCatEquipment ItemTypeIfCatEquipment;
    public int itemAtkIfTypeWeapon;
    public int itemDefIfTypeArmour;
}
