//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Provide various events that can be fired from other scripts
// Applied to: The screen space object in the title scene
//
//=============================================================================

using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

public class OTU_Title_Events : MonoBehaviour
{
    public UnityEvent OnLoadFileDoesNotExists;
    public UnityEvent OnLoadFileExists;

    private OTU_System_SaveManager saveManager;
    private string dataPath;


    void Start()
    {
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
        dataPath = Application.persistentDataPath;
    }


    public void Quit()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
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

        if (System.IO.File.Exists(dataPath + "/" + fileName + ".dasp"))
        {
            OnLoadFileExists.Invoke();
        }
    }
}
