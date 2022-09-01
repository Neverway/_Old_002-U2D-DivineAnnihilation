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
using TMPro;
using UnityEngine;

public class DAG13_Title_FileOptions : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=


    //=-----------------=
    // Private variables
    //=-----------------=


    //=-----------------=
    // Reference variables
    //=-----------------=
    private DAG13_System_SaveManager saveManager;
    private DAG13_System_SceneManager sceneManager;
    [SerializeField] private TMP_Text selectedFileTitle;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
	    saveManager = FindObjectOfType<DAG13_System_SaveManager>();
	    sceneManager = FindObjectOfType<DAG13_System_SceneManager>();
    }

    private void Update()
    {
	    selectedFileTitle.text = "_File " + saveManager.currentFile + "_";
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void ContinueGame()
    {
	    // Create the file
	    saveManager.LoadFile(saveManager.currentFile);
	    
	    // Load saved scene
	    sceneManager.LoadScene(saveManager.activeSaveFile.saveScene, 0f);
    }
}
