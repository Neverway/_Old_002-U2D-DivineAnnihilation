﻿//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Draw The save profile information on the title screen
// Applied to: The Save menu on the title screen
//
//=============================================================================

using System.IO;
using System.Collections;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Title_Save_Info : MonoBehaviour
{
    public string saveFileString;
    public Image fileImage;
    public Text fileName;
    public Text fileLevel;
    public Text fileChapter;
    public Sprite saveBlankIcon;
    public Sprite saveHasIcon;
    public GameObject configTarget;
    private SaveManager saveManager;
    private string saveFileSlot;
    public SaveData spacedata;
    public string thisChapter;
    public bool firstpass = true;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        saveManager = configTarget.GetComponent<SaveManager>();
    }


    void Update()
    {
        string dataPath = Application.persistentDataPath;
        // Set file number
        if (saveFileString == "SlotOne" && firstpass)
        {
            saveFileSlot = "1";
            saveManager.ThrowData(saveFileString);
            spacedata = saveManager.throwSave;
            firstpass = false;
        }
        if (saveFileString == "SlotTwo" && firstpass)
        {
            saveFileSlot = "2";
            saveManager.ThrowData(saveFileString);
            spacedata = saveManager.throwSave;
            firstpass = false;
        }
        if (saveFileString == "SlotThree" && firstpass)
        {
            saveFileSlot = "3";
            saveManager.ThrowData(saveFileString);
            spacedata = saveManager.throwSave;
            firstpass = false;
        }
        if (saveFileString == "SlotFour" && firstpass)
        {
            saveFileSlot = "4";
            saveManager.ThrowData(saveFileString);
            spacedata = saveManager.throwSave;
            firstpass = false;
        }

        // Set file information
        if (System.IO.File.Exists(dataPath + "/" + saveFileString + ".dasp"))
        {
            fileName.text = "File " + saveFileSlot + "|" + spacedata.playerName;

            fileLevel.text = "Lvl. "+spacedata.playerLevel+" | "+spacedata.saveChapter;
            fileChapter.text = ""+"";
            fileImage.sprite = saveHasIcon;
        }

        else if (!System.IO.File.Exists(dataPath + "/" + saveFileString + ".dasp"))
        {
            fileName.text = "-New File-";
            fileLevel.text = "";
            fileChapter.text = "";
            fileImage.sprite = saveBlankIcon;
        }
    }
}
