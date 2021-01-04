//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Manage saving and loading a profiles data
// Applied to: The Config object in a scene
//
//=============================================================================

using System.IO;
using System.Collections;
using System.Xml.Serialization;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public SaveData activeSave;
    public bool hasLoaded;
    public bool loadFileOnCreation = false;
    private GameObject playerRef;
    private SaveManager saveManager;


    private void Awake()
    {
        instance = this;
        if (loadFileOnCreation)
        {
            activeSave.saveProfileName = PlayerPrefs.GetString("Current Save Profile");
            Load();
        }
        if (PlayerPrefs.GetInt("LoadingNewRoom") == 1)
        {
            Debug.Log("[ID002 DA]: " + "A new scene is being loaded...");
            playerRef = GameObject.FindWithTag("Player");
            playerRef.transform.position = new Vector2(PlayerPrefs.GetFloat("NextRoomX"), PlayerPrefs.GetFloat("NextRoomY"));
            activeSave.playerSavePosition.x = PlayerPrefs.GetFloat("NextRoomX");
            activeSave.playerSavePosition.y = PlayerPrefs.GetFloat("NextRoomY");
            PlayerPrefs.SetInt("LoadingNewRoom", 0);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }


    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.6f);
        PlayerPrefs.SetInt("LoadingNewRoom", 0);
    }


    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetFloat("LoadPlayerPref") >= 1)
        {
            PlayerPrefLoad();
        }
        if (PlayerPrefs.GetInt("LoadingNewRoom") == 1)
        {
            Debug.Log("[ID002 DA]: " + "A new scene is being loaded...");
            playerRef = GameObject.FindWithTag("Player");
            playerRef.transform.position = new Vector2(PlayerPrefs.GetFloat("NextRoomX"), PlayerPrefs.GetFloat("NextRoomY"));
            activeSave.playerSavePosition.x = PlayerPrefs.GetFloat("NextRoomX");
            activeSave.playerSavePosition.y = PlayerPrefs.GetFloat("NextRoomY");
            StartCoroutine("Teleport");
        }
    }


    // Create a starting save profile
    public void CreateSave()
    {
        // Player Data
        activeSave.scene = "C1S1";
        activeSave.playerSavePosition.x = (float)-17.75;
        activeSave.playerSavePosition.y = 44;
        activeSave.playerHealth = 100;

        // Player Items
        activeSave.item1 = "---";
        activeSave.item2 = "---";
        activeSave.item3 = "---";
        activeSave.item4 = "---";
        activeSave.item5 = "---";
        activeSave.equipment1 = "---";
        activeSave.equipment2 = "---";
        activeSave.equipment3 = "---";
        activeSave.equipment4 = "---";
        activeSave.equipment5 = "---";

        // Party Data
        activeSave.partyMemberOne = "NULL";
        activeSave.partyMemberOneHealth = 100;
        activeSave.partyMemberTwo = "NULL";
        activeSave.partyMemberTwoHealth = 100;
        activeSave.partyMemberThree = "NULL";
        activeSave.partyMemberThreeHealth = 100;

        // Chapter Data
        activeSave.saveChapter = "C1 Hometown";
        activeSave.hasWokenUp = false;

        // Safty data
        PlayerPrefs.SetFloat("LoadPlayerPref", 0);
        PlayerPrefs.SetInt("LoadingNewRoom", 0);
        PlayerPrefs.SetFloat("NextRoomX", 0);
        PlayerPrefs.SetFloat("NextRoomY", 0);

        Debug.Log("[ID002 DA]: " + "Created new .DASP under " + activeSave.saveProfileName);
    }


    // Save the game data to the current save profile
    public void Save()
    {
        string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(SaveData));
        //activeSave.scene = SceneManager.GetActiveScene().name;
        var stream = new FileStream(dataPath + "/" + activeSave.saveProfileName + ".dasp", FileMode.Create);
        serializer.Serialize(stream, activeSave);
        stream.Close();
        Debug.Log("[ID002 DA]: " + "Saved information to .DASP");
    }


    // Load the game data to the current save profile
    public void Load()
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveProfileName + ".dasp"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + activeSave.saveProfileName + ".dasp", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();
            Debug.Log("[ID002 DA]: " + "Loaded information from .DASP");
            hasLoaded = true;
        }
    }


    // Save profile to temporary playerprefs
    public void PlayerPrefSave()
    {
        // Player Data
        PlayerPrefs.SetString("TempScene", activeSave.scene);
        PlayerPrefs.SetFloat("TempPlayerSavePositionX", activeSave.playerSavePosition.x);
        PlayerPrefs.SetFloat("TempPlayerSavePositionY", activeSave.playerSavePosition.y);
        PlayerPrefs.SetFloat("TempPlayerHealth", activeSave.playerHealth);

        // Party Data
        PlayerPrefs.SetString("TempPartyMemberOne", activeSave.partyMemberOne);
        PlayerPrefs.SetFloat("TempPartyMemberOneHealth", activeSave.partyMemberOneHealth);
        PlayerPrefs.SetString("TempPartyMemberTwo", activeSave.partyMemberTwo);
        PlayerPrefs.SetFloat("TempPartyMemberTwoHealth", activeSave.partyMemberTwoHealth);
        PlayerPrefs.SetString("TempPartyMemberThree", activeSave.partyMemberThree);
        PlayerPrefs.SetFloat("TempPartyMemberThreeHealth", activeSave.partyMemberThreeHealth);

        PlayerPrefs.SetFloat("LoadPlayerPref", PlayerPrefs.GetFloat("LoadPlayerPref"));
    }


    // Save profile to temporary playerprefs
    public void PlayerPrefLoad()
    {
        // Player Data
        activeSave.scene = PlayerPrefs.GetString("TempScene");
        activeSave.playerSavePosition.x = PlayerPrefs.GetFloat("TempPlayerSavePositionX");
        activeSave.playerSavePosition.y = PlayerPrefs.GetFloat("TempPlayerSavePositionY");
        activeSave.playerHealth = PlayerPrefs.GetFloat("TempPlayerHealth");

        // Party Data
        activeSave.partyMemberOne = PlayerPrefs.GetString("TempPartyMemberOne");
        activeSave.partyMemberOneHealth = PlayerPrefs.GetFloat("TempPartyMemberOneHealth");
        activeSave.partyMemberTwo = PlayerPrefs.GetString("TempPartyMemberTwo");
        activeSave.partyMemberTwoHealth = PlayerPrefs.GetFloat("TempPartyMemberTwoHealth");
        activeSave.partyMemberThree = PlayerPrefs.GetString("TempPartyMemberThree");
        activeSave.partyMemberThreeHealth = PlayerPrefs.GetFloat("TempPartyMemberThreeHealth");
        PlayerPrefs.SetFloat("LoadPlayerPref", PlayerPrefs.GetFloat("LoadPlayerPref"));

        // Safty data
        PlayerPrefs.SetFloat("LoadPlayerPref", PlayerPrefs.GetFloat("LoadPlayerPref")-1);
    }


    public void DeleteSaveProfile()
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveProfileName + ".dasp"))
        {
            File.Delete(dataPath + "/" + activeSave.saveProfileName + ".dasp");
            Debug.Log("[ID002 DA]: " + "Deleted current .DASP");
        }
    }


    public void ThrowData(string fileDataID)
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + fileDataID + ".dasp"))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(dataPath + "/" + fileDataID + ".dasp", FileMode.Open);
            activeSave = serializer.Deserialize(stream) as SaveData;
            stream.Close();
            Debug.Log("[ID002 DA]: " + "Menu Loaded information from .DASP");
        }
    }
}


[System.Serializable]
public class SaveData
{
    // Player Data
    public string saveProfileName;
    public string scene;
    public Vector2 playerSavePosition;
    public float playerHealth;

    // Player Items
    public string item1;
    public string item2;
    public string item3;
    public string item4;
    public string item5;
    public string equipment1;
    public string equipment2;
    public string equipment3;
    public string equipment4;
    public string equipment5;

    // Party Data
    public string partyMemberOne;
    public float partyMemberOneHealth;
    public string partyMemberTwo;
    public float partyMemberTwoHealth;
    public string partyMemberThree;
    public float partyMemberThreeHealth;

    // Chapter Data
    public string saveChapter;
    public bool hasWokenUp;
}