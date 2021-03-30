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
    // Public Variables
    public string loadRoom;                 // The battle room you want to load
    public string enemy0 = "Purple Cat";    // The enemy string ID that the player party is fighting
    public string enemy1 = "NULL";          // The enemy string ID that the player party is fighting
    public string enemy2 = "NULL";          // The enemy string ID that the player party is fighting
    public string enemy3 = "NULL";          // The enemy string ID that the player party is fighting
    public float enemy1Percent = 25;        // The chance, in precent of the enemys 1st party memeber to spawn
    public float enemy2Percent = 25;        // The chance, in precent of the enemys 2nd party memeber to spawn
    public float enemy3Percent = 25;        // The chance, in precent of the enemys 3rd party memeber to spawn
    public float fleePercent = 60;          // The chance, in precent of the players party to be able to abscond from a fight

    // Private Variables
    private GameObject player;
    private SaveManager saveManager;        // Pull a reference to the savemanager so the players pre-battle state can be saved for later

    void Start()
    {
        // Auto find the player in the scene
        player = GameObject.FindWithTag("Player");
        if (player == null)
        {
            Debug.LogError("[ID002 DA]: " + "An enemy was unable to find the player target. Make sure your player has the tag 'Player' set.");
        }

        saveManager = FindObjectOfType<SaveManager>(); // Set a reference to the SaveManager script on the Config object in the scene
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
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
            saveManager.PlayerPrefSave();                                    // Save the player prefs temporary save file for storage of the players pre-battle state
            SceneManager.LoadScene(loadRoom);                                // Load the battle room
        }
    }
}
