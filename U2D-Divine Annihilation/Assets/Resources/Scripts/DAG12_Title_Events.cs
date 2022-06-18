//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class DAG12_Title_Events : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    public UnityEvent OnFileSelected;

    //=-----------------=
    // Private variables
    //=-----------------=
    [SerializeField] private Vector2 startingPosition;
    [SerializeField] private string startingScene;
    [SerializeField] private string startingChapter;


    //=-----------------=
    // Reference variables
    //=-----------------=
    private DAG12_System_SaveManager saveManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        saveManager = FindObjectOfType<DAG12_System_SaveManager>();
    }

    private void Update()
    {
	
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void Quit()
    {
        Application.Quit();
    }

    public void SelectFileSlot(int slot)
    {
        if (saveManager.FileExists("Slot"+slot))
        {
            OnFileSelected.Invoke();
            saveManager.LoadFileAsActive("Slot"+slot);
        }
        else
        {
            saveManager.CreateNewFile("Slot"+slot, startingPosition, startingScene, startingChapter);
            saveManager.LoadFileAsActive("Slot"+slot);
            SceneManager.LoadScene(saveManager.activeSaveFile.scene);
        }
    }

    public void LoadCurrentFile()
    {
        saveManager.LoadFileAsActive(saveManager.activeSaveFile.saveFileName);
        SceneManager.LoadScene(saveManager.activeSaveFile.scene);
    }

    public void DeleteCurrentFile()
    {
        saveManager.DeleteFile(saveManager.activeSaveFile.saveFileName);
    }
}
