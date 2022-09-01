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
using TMPro;
using UnityEngine;

public class DAG13_Title_FileErase : MonoBehaviour
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
    [SerializeField] private TMP_Text selectedFileTitle;
    
    
    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
	    saveManager = FindObjectOfType<DAG13_System_SaveManager>();
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
    public void EraseCurrentFile()
    {
	    saveManager.DeleteFile(saveManager.currentFile);
    }
}
