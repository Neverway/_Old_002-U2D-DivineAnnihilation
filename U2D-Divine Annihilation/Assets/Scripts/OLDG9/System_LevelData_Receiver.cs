using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_LevelData_Receiver : MonoBehaviour
{
    public string levelDataID;
    public int destructorFlagValue;
    public GameObject[] destructorTargets;
    private SaveManager saveManager;

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }

    void Update()
    {
        if (levelDataID == "c1s1_0" && saveManager.activeSave.c1s1_0 == destructorFlagValue) { foreach (GameObject destructorTarget in destructorTargets) { Destroy(destructorTarget.gameObject); } }
        if (levelDataID == "c1s1_1" && saveManager.activeSave.c1s1_1 == destructorFlagValue) { foreach (GameObject destructorTarget in destructorTargets) { Destroy(destructorTarget.gameObject); } }
        if (levelDataID == "c1s1_2" && saveManager.activeSave.c1s1_2 == destructorFlagValue) { foreach (GameObject destructorTarget in destructorTargets) { Destroy(destructorTarget.gameObject); AstarPath.active.Scan(); } }
        if (levelDataID == "c1s1_3" && saveManager.activeSave.c1s1_3 == destructorFlagValue) { foreach (GameObject destructorTarget in destructorTargets) { Destroy(destructorTarget.gameObject); } }
    }
}
