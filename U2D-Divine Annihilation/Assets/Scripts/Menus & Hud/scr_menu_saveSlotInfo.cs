﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_menu_saveSlotInfo : MonoBehaviour
{
    public string saveFileString;
    public Image fileImage;
    public Text fileName;
    public Text fileLevel;
    public Text fileChapter;
    public GameObject configTarget;
    private scr_system_saveManager saveManager;
    private string saveFileSlot;


    // Start is called before the first frame update
    void Start()
    {
        saveManager = configTarget.GetComponent<scr_system_saveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set file number
        if (saveFileString == "SlotOne")
        {
            saveFileSlot = "1";
        }
        if (saveFileString == "SlotTwo")
        {
            saveFileSlot = "2";
        }
        if (saveFileString == "SlotThree")
        {
            saveFileSlot = "3";
        }
        if (saveFileString == "SlotFour")
        {
            saveFileSlot = "4";
        }

        // Set file information
        string dataPath = Application.persistentDataPath;
        if (System.IO.File.Exists(dataPath + "/" + saveFileString + ".dasp"))
        {
            fileName.text = "File " + saveFileSlot;

            fileLevel.text = "Lvl. "+"000";
            fileChapter.text = "Chapter "+"1";
        }

        else if (!System.IO.File.Exists(dataPath + "/" + saveFileString + ".dasp"))
        {
            fileName.text = "-New File-";

            fileLevel.text = "";
            fileChapter.text = "";
        }
    }
}
