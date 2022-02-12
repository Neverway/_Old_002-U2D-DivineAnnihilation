//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// SID: CAC
// Purpose: Provide various events that can be fired from other scripts
// Applied to: The root ui menu object in the title scene
// Editor script: N/A
// Notes:
//
//=============================================================================

using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Events;

public class OTU_Title_Events : MonoBehaviour
{
    public GameObject bindKeyMenu;  // A reference to the bind key menu

    public UnityEvent OnLoadFileDoesNotExists; // The events to run when a player selects an empty game slot
    public UnityEvent OnLoadFileExists; // The events to run when a player selects an game slot with a save file
    public UnityEvent OnDeleteFileFinish; // I don't really remember what this one does

    private OTU_System_SaveManager saveManager; // A reference to the save manager
    private string dataPath; // a string for referencing the persistent data path (Appdata on Windows, .config on Linux, etc.)


    void Start()
    {
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
        dataPath = Application.persistentDataPath;
    }


    public void Quit()
    {
        Application.Quit();
    }


    public void LoadActiveFile(string fileName)
    {
        saveManager.activeSave2.saveProfileName = fileName;
        if (!System.IO.File.Exists(dataPath + "/" + fileName + ".dasp"))
        {
            OnLoadFileDoesNotExists.Invoke();
            saveManager.CreateSave();
            saveManager.Save();
        }

        else if (System.IO.File.Exists(dataPath + "/" + fileName + ".dasp"))
        {
            OnLoadFileExists.Invoke();
            saveManager.Load();
        }
    }


    public void DeleteFile()
    {
        if (System.IO.File.Exists(dataPath + "/" + saveManager.activeSave2.saveProfileName + ".dasp"))
        {
            saveManager.DeleteSaveProfile();
            OnDeleteFileFinish.Invoke();
        }
    }

    public void BindKey()
    {
        bindKeyMenu.SetActive(true);
    }
}
