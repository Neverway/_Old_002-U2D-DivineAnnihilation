using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    public SaveData activeSave;
    public bool hasLoaded;
    public bool loadFileOnCreation = false;
    public GameObject configTarget;
    private SaveManager saveManager;


    private void Awake()
    {
        instance = this;
        if (loadFileOnCreation)
        {
            activeSave.saveProfileName = PlayerPrefs.GetString("Current Save Profile");
            Load();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        saveManager = configTarget.GetComponent<SaveManager>();
    }


    // Update is called once per frame
    void Update()
    {

    }


    // Create a starting save profile
    public void CreateSave()
    {
        // Player Data
        activeSave.scene = "SceneC1S1";
        activeSave.playerSavePosition.x = (float)-16.75;
        activeSave.playerSavePosition.y = 43;
        activeSave.playerHealth = 100;

        // Player Items


        // Party Data
        activeSave.partyMemberOne = "NULL";
        activeSave.partyMemberOneHealth = 100;
        activeSave.partyMemberTwo = "NULL";
        activeSave.partyMemberTwoHealth = 100;
        activeSave.partyMemberThree = "NULL";
        activeSave.partyMemberThreeHealth = 100;
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
        Debug.Log("Saved information to .DASP");
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
            Debug.Log("Loaded information from .DASP");
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
    }


    public void DeleteSaveProfile()
    {
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + activeSave.saveProfileName + ".dasp"))
        {
            File.Delete(dataPath + "/" + activeSave.saveProfileName + ".dasp");

            Debug.Log("Deleted current .DASP");
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
    public string[] playerItems;

    // Party Data
    public string partyMemberOne;
    public float partyMemberOneHealth;
    public string partyMemberTwo;
    public float partyMemberTwoHealth;
    public string partyMemberThree;
    public float partyMemberThreeHealth;


    // Chapter Data

}
