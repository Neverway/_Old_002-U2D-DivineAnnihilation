//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DAG12_Title_SaveFileInfo : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    public Sprite noFileCharacter;


    //=-----------------=
    // Private variables
    //=-----------------=
    private string dataPath;


    //=-----------------=
    // Reference variables
    //=-----------------=
    public DAG12_System_SaveManager saveManager;
    [SerializeField] private Image[] fileCharacter;
    [SerializeField] private Text[] fileName;
    [SerializeField] private Text[] fileLevel;
    [SerializeField] private Text[] fileChapter;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Awake()
    {
        saveManager = FindObjectOfType<DAG12_System_SaveManager>();
        dataPath = Application.persistentDataPath;
    }

    private void Update()
    {

    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    private  bool CheckForFile(int fileNumber)
    {
        if (System.IO.File.Exists(dataPath + "/" + "Slot" + fileNumber + ".dasp"))
        {
            return(true);
        }
        else
        {
            return(false);
        }
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void Initialize()
    {
        Debug.Log("[DAT:LOG] Loading save file info...");
        for (int i = 0; i < 4; i++)
        {
            if (CheckForFile(i+1) && saveManager != null)
            {
                Debug.Log("[DAT:LOG] File " + (i+1) + " was found");
                //fileCharacter[i].sprite = saveManager.LoadData("Slot"+i, characterPortrait);
                fileName[i].text = saveManager.LoadStringData("Slot"+(i+1), "playerName");
                fileLevel[i].text = ("Lvl." + saveManager.LoadIntData("Slot"+(i+1), "playerLevel").ToString() + " | " + saveManager.LoadStringData("Slot"+(i+1), "saveChapter"));
                
            }
            else if (!CheckForFile(i+1) && saveManager != null)
            {
                Debug.Log("[DAT:LOG] File " + (i+1) + " was not found");
                fileCharacter[i].sprite = noFileCharacter;
                fileName[i].text = "--EMPTY--";
                fileLevel[i].text = "";
                fileChapter[i].text = "";
            }
            else if (saveManager == null)
            {
                Debug.LogError("[DAT:ERR] SaveManager could not be located! ");
                fileCharacter[i].sprite = noFileCharacter;
                fileName[i].text = "--ERROR--";
                fileLevel[i].text = "";
                fileChapter[i].text = "";
            }
        }
    }
}
