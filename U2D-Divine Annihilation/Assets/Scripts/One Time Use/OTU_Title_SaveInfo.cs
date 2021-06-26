//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Draw The save profile information on the title screen
// Applied to: The Save menu on the title screen
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;

public class OTU_Title_SaveInfo : MonoBehaviour
{
    public string saveFile;
    private int saveFileID;
    public Image fileImage;
    public Text fileName;
    public Text fileLevel;
    public Text fileChapter;

    public Sprite saveBlankIcon;
    public Sprite saveHasIcon;

    private OTU_System_SaveManager saveManager;
    private SaveData2 spacedata;
    private bool firstpass = true;
    private string dataPath;

    void Start()
    {
        dataPath = Application.persistentDataPath;
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
        saveManager.ThrowData(saveFile);
        spacedata = saveManager.throwSave2;
        if (firstpass)
        {
            if (saveFile == "SlotOne")
            {
                saveFileID = 1;
                firstpass = false;
            }
            else if (saveFile == "SlotTwo")
            {
                saveFileID = 2;
                firstpass = false;
            }
            else if (saveFile == "SlotThree")
            {
                saveFileID = 3;
                firstpass = false;
            }
            else if (saveFile == "SlotFour")
            {
                saveFileID = 4;
                firstpass = false;
            }
        }
    }


    void Update()
    {
        // Set file information
        if (System.IO.File.Exists(dataPath + "/" + saveFile + ".dasp"))
        {
            fileName.text = "File " + saveFileID + "|" + spacedata.playerName;
            fileLevel.text = "Lvl. " + spacedata.playerLevel + " | " + spacedata.saveChapter;
            fileChapter.text = "" + "";
            fileImage.sprite = saveHasIcon;
        }

        else if (!System.IO.File.Exists(dataPath + "/" + saveFile + ".dasp"))
        {
            fileName.text = "-New File-";
            fileLevel.text = "";
            fileChapter.text = "";
            fileImage.sprite = saveBlankIcon;
        }
    }
}
