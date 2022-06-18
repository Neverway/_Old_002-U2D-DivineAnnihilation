//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class DAG12_System_SaveManager : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    public SaveFileData activeSaveFile; // (Sometimes called active buffer) Used for setting the current files information during gameplay or loading it when starting a scene
    public SaveFileData thrownSaveFile; // (Sometimes called thrown buffer) Used for getting file information without using it to load in the game (example: What is the players name in file "whatever")

    //=-----------------=
    // Private variables
    //=-----------------=
    private string dataPath;
    [SerializeField] private bool enableDebugControl; // Allow keyboard inputs to execute some script functions


    //=-----------------=
    // Reference variables
    //=-----------------=


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        dataPath = Application.persistentDataPath; // Set a persistent data path to use as our file save location

        // Set active buffer array sizes (Initial implamentation is for use in the create file function down below)
        System.Array.Resize(ref activeSaveFile.items, 5);
        System.Array.Resize(ref activeSaveFile.itemIcons, 5);
        System.Array.Resize(ref activeSaveFile.itemCategories, 5);
        System.Array.Resize(ref activeSaveFile.itemDescriptions, 5);
        System.Array.Resize(ref activeSaveFile.itemDiscardable, 5);
        System.Array.Resize(ref activeSaveFile.equipment, 5);
        System.Array.Resize(ref activeSaveFile.equipmentIcons, 5);
        System.Array.Resize(ref activeSaveFile.equipmentCategories, 5);
        System.Array.Resize(ref activeSaveFile.equipmentDescriptions, 5);
        System.Array.Resize(ref activeSaveFile.equipmentDiscardable, 5);
    }

    private void Update()
    {
        if (enableDebugControl)
        {
        }
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    [System.Serializable]
    public class SaveFileData
    {
        // Player Data
        public string saveFileName;
        public string playerName;
        public Vector2 playerSavePosition;
        public float playerHealth;
        public int playerLevel;
        public int playerGold;

        // Player Items
        public string[] items;
        public string[] itemIcons;
        public string[] itemCategories;
        public string[] itemDescriptions;
        public string[] itemDiscardable;
        public string[] equipment;
        public string[] equipmentIcons;
        public string[] equipmentCategories;
        public string[] equipmentDescriptions;
        public string[] equipmentDiscardable;
        public int equippedU;
        public int equippedW;
        public int equippedM;
        public int equippedD;

        // Party Data
        public string[] partyMembers;
        
        // Chapter Data
        public string scene;
        public string saveChapter;
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    // CREATE a starting savefile using the active buffer
    // ------
    public void CreateNewFile(string fileName, Vector2 startingPosition, string startingScene, string startingChapter)
    {
        if (!FileExists(fileName))
        {
            // Player Data
            activeSaveFile.saveFileName = fileName;
            activeSaveFile.playerName = "PLAYER";
            activeSaveFile.playerSavePosition = startingPosition;
            activeSaveFile.playerHealth = 100;
            activeSaveFile.playerLevel = 0;
            activeSaveFile.playerGold = 0;

            // Player Items
            for (int i = 0; i < activeSaveFile.items.Length; i++)
            {
                activeSaveFile.items[i] = "---";
                activeSaveFile.itemIcons[i] = "inventroy_icon_blank";
                activeSaveFile.itemCategories[i] = "";
                activeSaveFile.itemDescriptions[i] = "";
                activeSaveFile.itemDiscardable[i] = "false";
                activeSaveFile.equipment[i] = "---";
                activeSaveFile.equipmentIcons[i] = "inventroy_icon_blank";
                activeSaveFile.equipmentCategories[i] = "";
                activeSaveFile.equipmentDescriptions[i] = "";
                activeSaveFile.equipmentDiscardable[i] = "false";
            }
            activeSaveFile.equippedU = 0;
            activeSaveFile.equippedW = 0;
            activeSaveFile.equippedM = 0;
            activeSaveFile.equippedD = 0;
        
            // Chapter Data
            activeSaveFile.scene = startingScene;
            activeSaveFile.saveChapter = startingChapter;
            SaveActiveFile();
            Debug.Log("[DAT:LOG] Created new file '" + fileName + "'");
        }
        else
        {
            Debug.LogError("[DAT:ERR] Attempted to create new file '" + fileName + "', but file already exists!");
        }
    }

    // SAVE the active save buffer to the file it has loaded
    // ------
    public void SaveActiveFile()
    {
        var serializer = new XmlSerializer(typeof(SaveFileData));
        var stream = new FileStream(dataPath + "/" + activeSaveFile.saveFileName + ".dasp", FileMode.Create);
        serializer.Serialize(stream, activeSaveFile);
        stream.Close();
        Debug.Log("[DAT:LOG] Saved '" + activeSaveFile.saveFileName + "'");
    }

    // LOAD a file from a file name to the active buffer
    // ------
    public void LoadFileAsActive(string fileName)
    {
        if (FileExists(fileName))
        {
            var serializer = new XmlSerializer(typeof(SaveFileData));
            var stream = new FileStream(dataPath + "/" + fileName + ".dasp", FileMode.Open);
            activeSaveFile = serializer.Deserialize(stream) as SaveFileData;
            stream.Close();
            Debug.Log("[DAT:LOG] Loaded '" + fileName + "' to the active buffer");
        }
        else
        {
            Debug.LogError("[DAT:ERR] Attempted to load file '" + fileName + "' to active buffer, but file doesn't exists!");
        }
    }

    // LOAD a file from a file name to the thrown buffer
    // ------
    public void LoadFileAsThrown(string fileName)
    {
        if (FileExists(fileName))
        {
            var serializer = new XmlSerializer(typeof(SaveFileData));
            var stream = new FileStream(dataPath + "/" + fileName + ".dasp", FileMode.Open);
            thrownSaveFile = serializer.Deserialize(stream) as SaveFileData;
            stream.Close();
            Debug.Log("[DAT:LOG] Loaded '" + fileName + "' to the thrown buffer");
        }
        else
        {
            Debug.LogError("[DAT:ERR] Attempted to load file '" + fileName + "' to thrown buffer, but file doesn't exists!");
        }
    }

    public void DeleteFile(string fileName)
    {
        if (FileExists(fileName))
        {
            File.Delete(dataPath + "/" + fileName + ".dasp");
            Debug.Log("[DAT:LOG] Deleted '" + fileName + "'");
        }
        else
        {
            Debug.LogError("[DAT:ERR] Attempted to delete file '" + fileName + "', but file doesn't exists!");
        }
    }

    // Pull a string Key-Value Pair from a file using the thrown buffer
    public string LoadStringData(string fileName, string variableKey)
    {
        string outString;
        outString = null;
        if (FileExists(fileName))
        {
            LoadFileAsThrown(fileName);
            if (variableKey == "saveProfileName") outString = thrownSaveFile.saveFileName;
            if (variableKey == "scene") outString = thrownSaveFile.scene;
            if (variableKey == "playerName") outString = thrownSaveFile.playerName;
            if (variableKey == "saveChapter") outString = thrownSaveFile.saveChapter;
        }
        return outString;
    }
    
    // Pull a int Key-Value Pair from a file using the thrown buffer
    public int LoadIntData(string fileName, string variableKey)
    {
        int outVar;
        outVar = 0;
        if (FileExists(fileName))
        {
            //LoadFileAsThrown();
            if (variableKey == "playerLevel") outVar = thrownSaveFile.playerLevel;
            if (variableKey == "playerGold") outVar = thrownSaveFile.playerGold;
        }
        return outVar;
    }

    // Check to see if a file exists from a file name
    public bool FileExists(string fileName)
    {
        if (System.IO.File.Exists(dataPath + "/" + fileName + ".dasp"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
