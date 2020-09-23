using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_system_saveManager : MonoBehaviour
{
    public static scr_system_saveManager instance;
    public SaveData activeSave;
    public bool hasLoaded;


    private void Awake()
    {
        instance = this;
        Load();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            string dataPath = Application.persistentDataPath;
            Debug.Log(dataPath);
            Save();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Load();
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            DeleteSaveProfile();
        }
    }


    // Save the game data to the current save profile
    public void Save()
    {
        string dataPath = Application.persistentDataPath;
        var serializer = new XmlSerializer(typeof(SaveData));
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
    public Vector2 playerSavePosition;
    public float playerHealth;
    public bool partyMemberOneFollowing;

    // Chapter Data

}
