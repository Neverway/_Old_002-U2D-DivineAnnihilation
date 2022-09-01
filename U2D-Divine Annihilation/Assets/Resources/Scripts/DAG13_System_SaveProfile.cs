//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System;

[Serializable]
public class DAG13_System_SaveProfile
{
    // Character data
    public string profileName = "";
    public float health = 0;
    public int level = 0;
    public int gold = 0;
    
    // Scene data
    public string saveScene = "";
    public string saveChapter = "";
    public float savePositionX = 0f;
    public float savePositionY = 0f;
    
    // Character inventory
    public int equippedUtility;
    public int equippedWeapon;
    public int equippedMagic;
    public int equippedDefense;
    
    public string itemSlot0 = "";
    public string itemSlot1 = "";
    public string itemSlot2 = "";
    public string itemSlot3 = "";
    public string itemSlot4 = "";
    
    public string equipmentSlot0 = "";
    public string equipmentSlot1 = "";
    public string equipmentSlot2 = "";
    public string equipmentSlot3 = "";
    public string equipmentSlot4 = "";
}
