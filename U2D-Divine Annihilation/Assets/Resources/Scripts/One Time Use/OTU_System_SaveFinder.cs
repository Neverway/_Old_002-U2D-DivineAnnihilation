//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: MRC
// Purpose: Passthrough functions to the save manager
// Applied to: The Config Finder object in a scene
// Editor script: 
// Notes: This script is needed because sometime you want to run a save manager
//  function from a unity event, but since the save manager moves around then
//  the unity events sometimes loose track of it.
//
//=============================================================================

using UnityEngine;

public class OTU_System_SaveFinder : MonoBehaviour
{
    // Reference Variables
    private OTU_System_SaveManager saveManager;


    private void Start()
    {
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
    }


    public void CreateSave()
    {
        if (saveManager != null)
        {
            saveManager.CreateSave();
        }
        else
        {
            FindSaveManager();
        }
    }


    public void Save()
    {
        if (saveManager != null)
        {
            saveManager.Save();
        }
        else
        {
            FindSaveManager();
        }
    }


    public void Load()
    {
        if (saveManager != null)
        {
            saveManager.Load();
        }
        else
        {
            FindSaveManager();
        }
    }


    public void DeleteSaveProfile()
    {

        if (saveManager != null)
        {
            saveManager.DeleteSaveProfile();
        }
        else
        {
            FindSaveManager();
        }
    }


    public void LoadLevel()
    {
        if (saveManager != null)
        {
            saveManager.LoadLevel();
        }
        else
        {
            FindSaveManager();
        }
    }


    public void ThrowData(string saveFile)
    {
        if (saveManager != null)
        {
            saveManager.ThrowData(saveFile);
        }
        else
        {
            FindSaveManager();
        }
    }

    public void FindInventorySlot(bool isEquipment, int slotID, string slot)
    {
        if (saveManager != null)
        {
            saveManager.FindInventorySlot(isEquipment, slotID, slot);
        }
        else
        {
            FindSaveManager();
        }
    }

    private void FindSaveManager()
    {
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
    }
}