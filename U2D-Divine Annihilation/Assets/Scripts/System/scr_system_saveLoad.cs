using System.Collections;
using System.IO;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;
using UnityEngine.UI;

public class scr_system_saveLoad : MonoBehaviour
{
    EasyFileSave testFile;
    public string fname;
    public Text inventoryName;


    // Start is called before the first frame update
    void Start()
    {
        var dataPath = Application.dataPath + "/saves/";
        Debug.Log("start ini");
        Debug.Log("dataPath : " + dataPath);
        testFile = new EasyFileSave("/temp/testing" + "saves");
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("update ini");
        if (Input.GetKeyDown(KeyCode.F1))
        {
            //Debug.Log("save ini");
            Save();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            //Debug.Log("load ini");
            Load();
        }
    } 

    private void Save()
    {
        testFile.Add("name", "Fox");
        testFile.Add("health", 100);
        testFile.Add("gold", 0);
        testFile.Add("level", 0.00f);

        testFile.Add("roomX", 0f);
        testFile.Add("roomY", 0f);
        testFile.Add("room", "c1s1");

        testFile.Add("item1", "");
        testFile.Add("item2", "");
        testFile.Add("item3", "");
        testFile.Add("item4", "");
        testFile.Add("item5", "");
        testFile.Add("equipment1", "");
        testFile.Add("equipment2", "");
        testFile.Add("equipment3", "");
        testFile.Add("equipment4", "");
        testFile.Add("equipment5", "");

        testFile.Add("partyMember1", "pm1");
        testFile.Add("partyMember1Health", 100);
        testFile.Add("partyMember2", "pm2");
        testFile.Add("partyMember2Health", 100);

        testFile.Add("partyMember3", "pm3");
        testFile.Add("partyMember3Health", 100);

        testFile.Save();
        Debug.Log("Saved test file!");
    }

    private void Load()
    {
        var playerName = testFile.GetString("name");
        var playerHealth = testFile.GetInt("health");
        var playerGold = testFile.GetInt("gold");
        var playerLevel = testFile.GetFloat("level");

        var roomX = testFile.GetFloat("roomX");
        var roomY = testFile.GetFloat("roomY");
        var roomID = testFile.GetString("roomID");

        var item1 = testFile.GetString("item1");
        var item2 = testFile.GetString("item2");
        var item3 = testFile.GetString("item3");
        var item4 = testFile.GetString("item4");
        var item5 = testFile.GetString("item5");
        var equipment1 = testFile.GetString("equipment1");
        var equipment2 = testFile.GetString("equipment2");
        var equipment3 = testFile.GetString("equipment3");
        var equipment4 = testFile.GetString("equipment4");
        var equipment5 = testFile.GetString("equipment5");

        var partyMember1 = testFile.GetString("partyMember1");
        var partyMember1Health = testFile.GetInt("partyMember1Health");

        var partyMember2 = testFile.GetString("partyMember2");
        var partyMember2Health = testFile.GetInt("partyMember2Health");

        var partyMember3 = testFile.GetString("partyMember3");
        var partyMember3Health = testFile.GetInt("partyMember3Health");

        Debug.Log("Test file results");
        Debug.Log("Health : " + playerHealth);
        Debug.Log("Name   : " + playerName);
        //testFile.Dispose();
    }
}
