//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Manage saving and loading a profiles data
// Applied to: The Config object in a scene
// Notes: 
//  PastUCC: Needs comments and also I removed the playerpref saving thing for 
//  battles since it sucked. Also also none of the G9 scripts know how to talk
//  to this version of the save manager, so you are going to have to fix that.
//
//  FutureUCC: Wow this is a mess. Are all of these functions even neccisary? 
//  If stuff breaks then don't blame me.
//
//=============================================================================

using System.IO;
using System.Collections;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OTU_System_SaveManager : MonoBehaviour
{
    // Variables Script 
    public SaveData2 activeSave2;                     //  
    public SaveData2 throwSave2;                      // 

    // Variables Reference 
    public string startingScene;                //
    public Sprite noPortrait;                   //
    public bool hasLoaded;                      //
    public bool loadFileOnCreation;     //
    private GameObject playerCharacter;         //
    public GameObject loadingScreen;

    // Variables Editor
    public int currentTab;


    private void Start()
    {
        System.Array.Resize(ref activeSave2.items, 5);
        System.Array.Resize(ref activeSave2.itemIcons, 5);
        System.Array.Resize(ref activeSave2.itemCategories, 5);
        System.Array.Resize(ref activeSave2.itemDescriptions, 5);
        System.Array.Resize(ref activeSave2.itemDiscardable, 5);
        System.Array.Resize(ref activeSave2.equipment, 5);
        System.Array.Resize(ref activeSave2.equipmentIcons, 5);
        System.Array.Resize(ref activeSave2.equipmentCategories, 5);
        System.Array.Resize(ref activeSave2.equipmentDescriptions, 5);
        System.Array.Resize(ref activeSave2.equipmentDiscardable, 5);
        loadingScreen = GameObject.FindWithTag("Loading Screen");
    }


    private void Awake()
    {
        loadingScreen = GameObject.FindWithTag("Loading Screen");
        if (loadFileOnCreation)
        {
            activeSave2.saveProfileName = PlayerPrefs.GetString("Current Save Profile");
            Load();
        }

        // Find the player and set their position when loading a new scene
        if (PlayerPrefs.GetInt("LoadingNewRoom") == 1)
        {
            Debug.Log("[ID002 DA]: " + "A new scene is being loaded...");   // Print a debug message
            playerCharacter = GameObject.FindWithTag("Player");             // Attempt to find the palyer through their tag

            // Confirm that the player was found and update their position and the save managers logged position of them
            if (playerCharacter != null)
            {
                playerCharacter.transform.position = new Vector2(PlayerPrefs.GetFloat("NextRoomX"), PlayerPrefs.GetFloat("NextRoomY"));
                activeSave2.playerSavePosition.x = PlayerPrefs.GetFloat("NextRoomX");
                activeSave2.playerSavePosition.y = PlayerPrefs.GetFloat("NextRoomY");
            }
            PlayerPrefs.SetInt("LoadingNewRoom", 0); // End the LoadingNewRoom sequence
        }
    }


    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.6f);
        PlayerPrefs.SetInt("LoadingNewRoom", 0);
    }


    void Update()
    {
        if (PlayerPrefs.GetFloat("LoadPlayerPref") >= 1)
        {
            //PlayerPrefLoad();
        }
        if (PlayerPrefs.GetInt("LoadingNewRoom") == 1)
        {
            Debug.Log("[ID002 DA]: " + "A new scene is being loaded...");
            playerCharacter = GameObject.FindWithTag("Player");
            playerCharacter.transform.position = new Vector2(PlayerPrefs.GetFloat("NextRoomX"), PlayerPrefs.GetFloat("NextRoomY"));
            activeSave2.playerSavePosition.x = PlayerPrefs.GetFloat("NextRoomX");
            activeSave2.playerSavePosition.y = PlayerPrefs.GetFloat("NextRoomY");
            StartCoroutine("Teleport");
        }
    }


    // Create a starting save profile
    public void CreateSave()
    {
        // Player Data
        activeSave2.scene = startingScene;
        activeSave2.playerName = PlayerPrefs.GetString("PlayerName");
        activeSave2.playerSavePosition = new Vector2(-17.75f, 44);
        activeSave2.playerHealth = 100;
        activeSave2.playerLevel = 000;

        // Player Items
        for (int i = 0; i < activeSave2.items.Length; i++)
        {
            activeSave2.items[i] = "---";
            activeSave2.itemIcons[i] = "hud_inventory_blank";
            activeSave2.itemCategories[i] = "";
            activeSave2.itemDescriptions[i] = "";
            activeSave2.itemDiscardable[i] = "false";
            activeSave2.equipment[i] = "---";
            activeSave2.equipmentIcons[i] = "hud_inventory_blank";
            activeSave2.equipmentCategories[i] = "";
            activeSave2.equipmentDescriptions[i] = "";
            activeSave2.equipmentDiscardable[i] = "false";
        }

        //// Party Data
        for (int i = 0; i < activeSave2.partyMembers.Length; i++)
        {
            activeSave2.partyMembers[i] = "NULL";
            activeSave2.partyMembersHealth[i] = 100;
        }

        // Chapter Data
        activeSave2.saveChapter = "C1 Begining";
        for (int i = 0; i < activeSave2.Chapter.Length; i++)
        {
            for (int ii = 0; ii < activeSave2.Chapter[i].Scene.Length; ii++)
            {
                for (int iii = 0; iii < activeSave2.Chapter[i].Scene[ii].levelData.Length; iii++)
                {
                    activeSave2.Chapter[i].Scene[ii].levelData[iii] = 0;
                }
            }
        }

        // Safty data
        PlayerPrefs.SetFloat("LoadPlayerPref", 0);
        PlayerPrefs.SetInt("LoadingNewRoom", 0);
        PlayerPrefs.SetFloat("NextRoomX", 0);
        PlayerPrefs.SetFloat("NextRoomY", 0);
        loadFileOnCreation = true;

        Debug.Log("[ID002 DA]: " + "Created new .DASP under " + activeSave2.saveProfileName);
    }


    // Save the game data to the current save profile
    public void Save()
    {
        string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(SaveData2));
        //activeSave2.scene = SceneManager.GetActiveScene().name;
        var stream = new FileStream(dataPath + "/" + activeSave2.saveProfileName + ".dasp", FileMode.Create);
        serializer.Serialize(stream, activeSave2);
        stream.Close();
        Debug.Log("[ID002 DA]: " + "Saved information to .DASP");
    }


    // Load the game data to the current save profile
    public void Load()
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave2.saveProfileName + ".dasp"))
        {
            var serializer = new XmlSerializer(typeof(SaveData2));
            var stream = new FileStream(dataPath + "/" + activeSave2.saveProfileName + ".dasp", FileMode.Open);
            activeSave2 = serializer.Deserialize(stream) as SaveData2;
            stream.Close();
            Debug.Log("[ID002 DA]: " + "Loaded information from .DASP");
            hasLoaded = true;
        }
    }


    public void DeleteSaveProfile()
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave2.saveProfileName + ".dasp"))
        {
            File.Delete(dataPath + "/" + activeSave2.saveProfileName + ".dasp");
            Debug.Log("[ID002 DA]: " + "Deleted current .DASP");
        }
    }


    public void LoadLevel()
    {
        loadingScreen.GetComponent<Image>().enabled = true;
        loadingScreen.transform.GetChild(0).GetComponent<Image>().enabled = true;
        loadingScreen.transform.GetChild(1).GetComponent<Text>().enabled = true;
        loadFileOnCreation = true;
        SceneManager.LoadScene(activeSave2.scene);
    }


    public void ThrowData(string saveFile)
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + saveFile + ".dasp"))
        {
            var serializer = new XmlSerializer(typeof(SaveData2));
            var stream = new FileStream(dataPath + "/" + saveFile + ".dasp", FileMode.Open);
            throwSave2 = serializer.Deserialize(stream) as SaveData2;
            stream.Close();
            Debug.Log("[ID002 DA]: " + "Menu Loaded information from .DASP");
        }
    }

    public void FindInventorySlot(bool isEquipment, int slotID, string slot)
    {
        if (!isEquipment)
        {
            slot = activeSave2.items[slotID];
        }
        if (isEquipment)
        {
            slot = activeSave2.equipment[slotID];
        }
    }
}



[System.Serializable]
public class SaveData2
{
    // Player Data
    public string saveProfileName;
    public string scene;
    public string playerName;
    public Vector2 playerSavePosition;
    public float playerHealth;
    public int playerLevel;

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
    public float[] partyMembersHealth;

    // Chapter Data
    // These are level specific flags to keep track of if a player has picked up items, killed an enemy, completed a cutscene, ect.
    // By default these values will be boolean, meaning they can only be true (1) or false (0), but if they have a tag that looks like this: [0, 1, 2]
    // then that means that it has several states, for example: if a player were to skip past an enemy the value would be 0, if they killed them the value would be 1
    // and if they helped them or finished the fight in a peaceful way the value would be 2
    public string saveChapter;
    public Chapter[] Chapter;
}

[System.Serializable]
public class Chapter
{
    public Scene[] Scene;
}

[System.Serializable]
public class Scene
{
    public int[] levelData;
}