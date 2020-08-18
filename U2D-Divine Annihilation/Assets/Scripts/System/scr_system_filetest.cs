using System.Collections;
using System.Collections.Generic;
using TigerForge;
using UnityEngine;

public class scr_system_filetest : MonoBehaviour
{
    EasyFileSave testFile;


    void Start()
    {
        testFile = new EasyFileSave("dasavefile1");
    }


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


    void Save()
    {
        testFile.Add("name", "Fox"); // Save name as Fox
        testFile.Save();
    }


    void Load()
    {
        if(testFile.Load())
        {
            var playerName = testFile.GetString("name"); // Load name information
            var oneanother = testFile.GetFileName();
            Debug.Log(oneanother);
        }
    }
}
