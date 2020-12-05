//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Causes the player to enter a battle on collision with the enemy
// Applied to: An enemy in an overworld scene
//
//=============================================================================

using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle_Encounter : MonoBehaviour
{
    public string loadRoom;
    public string enemy0 = "Purple Cat";
    public string enemy1 = "NULL";
    public string enemy2 = "NULL";
    public string enemy3 = "NULL";
    public float enemy1Percent = 25;
    public float enemy2Percent = 25;
    public float enemy3Percent = 25;
    public float fleePercent = 60;

    public GameObject player;
    public GameObject configTarget;
    private SaveManager saveManager;

    void Start()
    {
        saveManager = configTarget.GetComponent<SaveManager>(); // Set a reference to the SaveManager script on the Config object in the scene
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox")
        {
            // Set enemy party based off of percentages
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

            PlayerPrefs.SetFloat("PreBattleX", player.transform.position.x); // Save the player's pre-battle X position
            PlayerPrefs.SetFloat("PreBattleY", player.transform.position.y); // Save the player's pre-battle Y position
            PlayerPrefs.SetFloat("fleePercent", fleePercent);                // Passthrough variable for the party's flee chance
            PlayerPrefs.SetString("Enemy0", enemy0);                         // Passthrough for assigning enemy party leader
            saveManager.PlayerPrefSave();                                    // Save the player prefs temporary save file
            SceneManager.LoadScene(loadRoom);                                // Load the battle room
        }
    }
}
