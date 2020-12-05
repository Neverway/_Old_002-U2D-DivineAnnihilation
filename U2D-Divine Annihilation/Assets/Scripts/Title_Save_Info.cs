//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Draw The save profile information on the title screen
// Applied to: The Save menu on the title screen
//
//=============================================================================

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

    void Start()
    {
        saveManager = configTarget.GetComponent<SaveManager>();
    }


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
