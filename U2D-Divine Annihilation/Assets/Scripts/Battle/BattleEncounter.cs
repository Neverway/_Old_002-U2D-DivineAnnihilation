using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleEncounter : MonoBehaviour
{
    public string loadRoom;
    public string enemy0 = "Purple Cat";
    public string enemy1 = "NULL";
    public string enemy2 = "NULL";
    public string enemy3 = "NULL";
    public float enemy1Percent = 25;
    public float enemy2Percent = 25;
    public float enemy3Percent = 25;
    public GameObject player;
    public GameObject configTarget;
    private SaveManager saveManager;
    //public bool PlayTransition;


    // Start is called before the first frame update
    void Start()
    {
        saveManager = configTarget.GetComponent<SaveManager>();
    }

    public void Update()
    {
        PlayerPrefs.SetFloat("PreBattleX", player.transform.position.x);
        PlayerPrefs.SetFloat("PreBattleY", player.transform.position.y);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if that something is the player
        if (other.gameObject.name == "Entity Fox")
        {
            if (Random.Range(0, 100) <= enemy1Percent)
            {
                PlayerPrefs.SetString("Enemy1", enemy1);
                if (Random.Range(0, 100) <= enemy2Percent)
                {
                    PlayerPrefs.SetString("Enemy2", enemy2);
                    if (Random.Range(0, 100) <= enemy3Percent)
                    {
                        PlayerPrefs.SetString("Enemy3", enemy3);
                    }
                    else
                    {
                        PlayerPrefs.SetString("Enemy3", "NULL");
                    }
                }
                else
                {
                    PlayerPrefs.SetString("Enemy2", "NULL");
                    PlayerPrefs.SetString("Enemy3", "NULL");
                }

            }
            else
            {
                PlayerPrefs.SetString("Enemy1", "NULL");
                PlayerPrefs.SetString("Enemy2", "NULL");
                PlayerPrefs.SetString("Enemy3", "NULL");
            }
            PlayerPrefs.SetString("Enemy0", enemy0);
            saveManager.PlayerPrefSave();
            SceneManager.LoadScene(loadRoom);
        }
    }
}
