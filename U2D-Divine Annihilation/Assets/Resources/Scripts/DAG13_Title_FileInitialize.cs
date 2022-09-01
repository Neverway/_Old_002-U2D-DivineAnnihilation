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

public class DAG13_Title_FileInitialize : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    [SerializeField] private FileSlots[] fileSlots;
    [SerializeField] private bool isFileSelectScreen;


    //=-----------------=
    // Private variables
    //=-----------------=
    private string dataPath;


    //=-----------------=
    // Reference variables
    //=-----------------=
    private DAG13_System_SaveManager saveManager;
    private DAG13_Menu_Control_Text menuControlText;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
	    dataPath = Application.persistentDataPath;
	    saveManager = FindObjectOfType<DAG13_System_SaveManager>();
	    menuControlText = gameObject.GetComponent<DAG13_Menu_Control_Text>();
	    InitializeFileSlots();
    }

    public void Update()
    {
	    InitializeFileSlots();
    }

    //=-----------------=
    // Internal Functions
    //=-----------------=
    private void InitializeFileSlots()
    {
	    saveManager.LoadAllFiles();
	    for (int i = 0; i < fileSlots.Length; i++)
	    {
		    var saveFile = dataPath + "/file" + i + ".dasp";
		    if (!File.Exists(saveFile))
		    {
			    if (isFileSelectScreen)
			    {
				    menuControlText.SetNormalText(i, "--New File--");
				    menuControlText.SetSelectedText(i, "--New File--");
			    }
			    else { fileSlots[i].profileName.text = "--New File--"; }
			    fileSlots[i].chapter.text = "Ch.-";
			    fileSlots[i].level.text = "Lv.-";
		    }
		    else
		    {
			    if (isFileSelectScreen)
			    {
				    menuControlText.SetNormalText(i, saveManager.saveFiles[i].profileName);
				    menuControlText.SetSelectedText(i, saveManager.saveFiles[i].profileName);
			    }
			    else { fileSlots[i].profileName.text = saveManager.saveFiles[i].profileName; }
			    fileSlots[i].chapter.text = "Ch."+saveManager.saveFiles[i].saveChapter;
			    fileSlots[i].level.text = "Lv."+saveManager.saveFiles[i].level;
		    }
	    }
    }
    [Serializable]
    public class FileSlots
    {
	    public TMP_Text profileName;
	    public TMP_Text chapter;
	    public TMP_Text level;
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}
