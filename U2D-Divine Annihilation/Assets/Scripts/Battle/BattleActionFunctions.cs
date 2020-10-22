using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleActionFunctions : MonoBehaviour
{
    public GameObject configTarget;
    private SaveManager saveManager;
    private MenuScrollString menuScrollString;
    //public bool PlayTransition;


    // Start is called before the first frame update
    void Start()
    {
        saveManager = configTarget.GetComponent<SaveManager>();
        menuScrollString = GetComponent<MenuScrollString>();
    }

    void Update()
    {
        if (menuScrollString.currentSelection == 3 && Input.GetKeyDown("z"))
        {
            Flee();
        }
    }

    public void Flee()
    {
        if (Random.Range(0, 100) <= PlayerPrefs.GetFloat("fleePercent"))
        {
            saveManager.activeSave.playerSavePosition.x = PlayerPrefs.GetFloat("PreBattleX");
            saveManager.activeSave.playerSavePosition.y = PlayerPrefs.GetFloat("PreBattleY");
            PlayerPrefs.SetFloat("LoadPlayerPref", 4);
            saveManager.PlayerPrefSave();
            SceneManager.LoadScene(PlayerPrefs.GetString("TempScene"));
        }
        else
        {
            Debug.Log("FAILED TO FLEE!!");
        }
    }
}
