using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleEncounter : MonoBehaviour
{
    public string loadRoom;
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
            saveManager.PlayerPrefSave();
            SceneManager.LoadScene(loadRoom);
        }
    }
}
