//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DAG13_Title_FileSelect : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=


    //=-----------------=
    // Private variables
    //=-----------------=
    private string dataPath;


    //=-----------------=
    // Reference variables
    //=-----------------=
    private DAG13_System_SaveManager saveManager;
    private DAG13_System_SceneManager sceneManager;
    [SerializeField] private GameObject nameSelectionScreen;
    [SerializeField] private GameObject fileOptionsScreen;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void OnEnable()
    {
	    saveManager = FindObjectOfType<DAG13_System_SaveManager>();
	    sceneManager = FindObjectOfType<DAG13_System_SceneManager>();
	    dataPath = Application.persistentDataPath;
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=

    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void SelectFile(int _fileIndex)
    {
	    var saveFile = dataPath + "/file" + _fileIndex + ".dasp";
	    if (!File.Exists(saveFile))
	    {
		    // Store the selected file index so it can be referenced in place of _fileIndex
		    saveManager.currentFile = _fileIndex;
		    // Create the file
		    saveManager.CreateNewFile(_fileIndex);
		    
		    // Show the name selection screen
		    //nameSelectionScreen.SetActive(true);
		    // Hide the file select screen
		    //gameObject.SetActive(false);
		    
		    // Load default scene
		    sceneManager.LoadScene(saveManager.activeSaveFile.saveScene, 0f);
	    }
	    else
	    {
		    // Store the selected file index so it can be referenced in place of _fileIndex
		    saveManager.currentFile = _fileIndex;
		    // Show the file options screen
		    fileOptionsScreen.SetActive(true);
		    // Hide the file select screen
		    gameObject.SetActive(false);
	    }
    }
}
