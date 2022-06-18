using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup2 : MonoBehaviour
{
    // Variables
    [Header("Item information")]
    public string itemName;
    public string description;
    public string pickupText;

    public Sprite icon;
    public bool canBeSold;

    public enum ItemCategory { Item, Consumable, Armour, Weapon }
    [Header("Item category")]
    public ItemCategory itemCategory;

    [Header("Consumable")]
    public float healthRestoration;

    [Header("Armour")]
    public float defencePower;

    public enum WeaponType { Melee, Ranged, Explosive }
    [Header("Weapon")]
    public WeaponType weaponType;
    public float attackPower;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    private void Start() {
        
    }
}