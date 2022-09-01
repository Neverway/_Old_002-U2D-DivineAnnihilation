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
using System.Xml.Serialization;
using System.IO;
using Unity.Collections;
using UnityEngine;

public class DAG13_System_SaveManager : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    [ReadOnly]
    [Range(0, 3)]
    public int currentFile;


    //=-----------------=
    // Private variables
    //=-----------------=
    private string dataPath;


    //=-----------------=
    // Reference variables
    //=-----------------=
    public DAG13_System_SaveProfile activeSaveFile; // Used for saving and loading to the current file
    public List<DAG13_System_SaveProfile> saveFiles; // Used for displaying file data on the file select screen


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Awake()
    {
	    dataPath = Application.persistentDataPath;
    }


    //=-----------------=
    // Internal Functions
    //=-----------------=
    // Return a loaded file as type of DAG13_System_SaveProfile (used for LoadAllFiles function)
    private DAG13_System_SaveProfile LoadAndReturnFile(int _fileIndex)
    {
	    var serializer = new XmlSerializer(typeof(DAG13_System_SaveProfile));
	    var stream = new FileStream(dataPath + "/file" + _fileIndex + ".dasp", FileMode.Open);
	    DAG13_System_SaveProfile file = serializer.Deserialize(stream) as DAG13_System_SaveProfile;
	    stream.Close();
	    return file;
    }


    //=-----------------=
    // External Functions
    //=-----------------=
    // Create a new file with specified index and assign default parameters
    public void CreateNewFile(int _fileIndex)
    {
	    var saveFile = dataPath + "/file" + _fileIndex + ".dasp";
	    if (File.Exists(saveFile)) { return; }
	    
	    // Set default values
	    activeSaveFile.profileName = "File " + (_fileIndex + 1);
	    activeSaveFile.health = 100f;
	    activeSaveFile.level = 0;
	    activeSaveFile.gold = 0;
	    
	    activeSaveFile.saveScene = "C1S1";
	    activeSaveFile.saveChapter = "1 - Beginning";
	    activeSaveFile.savePositionX = 0f;
	    activeSaveFile.savePositionY = 0f;
	    
	    activeSaveFile.equippedUtility = 0;
	    activeSaveFile.equippedWeapon = 0;
	    activeSaveFile.equippedMagic = 0;
	    activeSaveFile.equippedDefense = 0;
	    
	    activeSaveFile.itemSlot0 = "";
	    activeSaveFile.itemSlot1 = "";
	    activeSaveFile.itemSlot2 = "";
	    activeSaveFile.itemSlot3 = "";
	    activeSaveFile.itemSlot4 = "";
	    
	    activeSaveFile.equipmentSlot0 = "";
	    activeSaveFile.equipmentSlot1 = "";
	    activeSaveFile.equipmentSlot2 = "";
	    activeSaveFile.equipmentSlot3 = "";
	    activeSaveFile.equipmentSlot4 = "";
	    
	    // Create & Write data to file
	    SaveFile(_fileIndex);
    }
    
    // Load a file with specified index as activeSaveFile
    public void LoadFile(int _fileIndex)
    {
	    var serializer = new XmlSerializer(typeof(DAG13_System_SaveProfile));
	    var stream = new FileStream(dataPath + "/file" + _fileIndex + ".dasp", FileMode.Open);
	    activeSaveFile = serializer.Deserialize(stream) as DAG13_System_SaveProfile;
	    stream.Close();
    }
    
    // Load all existing save files as saveFiles
    public void LoadAllFiles()
    {
	    saveFiles.Clear();

	    for (int i = 0; i < 4; i++)
	    {
		    var saveFile = dataPath + "/file" + i + ".dasp";
		    if (!File.Exists(saveFile))
		    { 
			    saveFiles.Add(new DAG13_System_SaveProfile());
		    }
		    else
		    {
			    saveFiles.Add(LoadAndReturnFile(i));
		    }
	    }
    }
    
    // Write the information from activeSaveFile to the file with specified index
    public void SaveFile(int _fileIndex)
    {
	    var serializer = new XmlSerializer(typeof(DAG13_System_SaveProfile));
	    var stream = new FileStream(dataPath + "/file" + _fileIndex + ".dasp", FileMode.Create);
	    serializer.Serialize(stream, activeSaveFile);
	    stream.Close();
    }
    
    // Delete the file with the specified index
    public void DeleteFile(int _fileIndex)
    {
	    var saveFile = dataPath + "/file" + _fileIndex + ".dasp";
	    if (!File.Exists(saveFile)) { return; }
	    File.Delete(saveFile);
    }
}
