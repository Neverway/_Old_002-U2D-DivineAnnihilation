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
        activeSave.scene = "SceneC1S1";
        activeSave.playerSavePosition.x = (float)-16.75;
        activeSave.playerSavePosition.y = 43;
        activeSave.playerHealth = 100;
        activeSave.partyMemberOneFollowing = false;
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
    public bool partyMemberOneFollowing;

    // Chapter Data

}
