/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pickup : MonoBehaviour
{
    // Variables
    [Header ("Item information")]
    public string itemName;
    public string description;
    public string pickupText;

    public Sprite icon;
    public bool canBeSold;

    public enum ItemCategory {Item, Consumable, Armour, Weapon}
    [Header("Item category")]
    public ItemCategory itemCategory;

    [Header("Consumable")]
    public float healthRestoration;

    [Header("Armour")]
    public float defencePower;

    public enum WeaponType {Melee, Ranged, Explosive}
    [Header("Weapon")]
    public WeaponType weaponType;
    public float attackPower;

    public ItemPickup(string newItemName, string newDescription, string newPickupText, Sprite newIcon, bool newCanBeSold, ItemCategory newItemCategory, float newHealthRestoration, float newDefencePower, WeaponType newWeaponType, float newAttackPower)
    {
        itemName = newItemName;
        description = newDescription;
        pickupText = newPickupText;
        icon = newIcon;
        canBeSold = newCanBeSold;
        itemCategory = newItemCategory;
        healthRestoration = newHealthRestoration;
        defencePower = newDefencePower;
        weaponType = newWeaponType;
        attackPower = newAttackPower;
    }
}
*/