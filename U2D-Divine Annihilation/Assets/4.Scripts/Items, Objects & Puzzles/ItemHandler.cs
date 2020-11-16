using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    private ItemPickup ItemScript;

    // Start is called before the first frame update
    void Start()
    {
        List<ItemPickup> items = new List<ItemPickup>();

        items.Add(new ItemPickup(ItemScript.itemName, ItemScript.description, ItemScript.pickupText, ItemScript.icon, ItemScript.canBeSold, ItemScript.itemCategory, ItemScript.healthRestoration, ItemScript.defencePower, ItemScript.weaponType, ItemScript.attackPower));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
