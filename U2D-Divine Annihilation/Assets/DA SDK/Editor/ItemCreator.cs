using UnityEditor;
using UnityEngine;

public class ItemPickupCreator : EditorWindow
{
    string ItemName = "";
    string ItemDescription = "";
    Sprite ItemSprite;
    public ItemFunctions itemFunctions;
    int ItemID = 0;

    [MenuItem("Divine Annihilation SDK/ItemPickupCreator")]
    public static void ShowWindow()
    {
        GetWindow(typeof(ItemPickupCreator));
    }
}

[System.Serializable]
public class ItemFunctions
{
    // Consumable effects
    public bool ItemIsConsumable;
    public float HealthOnConsume;

    public bool ItemIsEquipable;
    public float ItemAttackOnEquip;
    public float ItemDefenceOnEquip;
}