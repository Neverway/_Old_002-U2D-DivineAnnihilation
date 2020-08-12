using System.Collections;
using System.IO;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class scr_system_required_config_saveLoad : MonoBehaviour
{
    EasyFileSave testFile;

    // Start is called before the first frame update
    void Start()
    {
        testFile = new EasyFileSave("test");
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.F1))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
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

        testFile.Dispose();
        Debug.Log(playerHealth);
        Debug.Log(playerName);
    }
}
