using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_LevelData_Manager : MonoBehaviour
{
    public string levelDataID;
    public int newFlagValue;
    private SaveManager saveManager;

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }

    public void UpdatelevelDataFlag()
    {
        if (levelDataID == "c1s1_0") saveManager.activeSave.c1s1_0 = newFlagValue;
        if (levelDataID == "c1s1_1") saveManager.activeSave.c1s1_1 = newFlagValue;
        if (levelDataID == "c1s1_2") saveManager.activeSave.c1s1_2 = newFlagValue;
        if (levelDataID == "c1s1_3") saveManager.activeSave.c1s1_3 = newFlagValue;
    }
}

