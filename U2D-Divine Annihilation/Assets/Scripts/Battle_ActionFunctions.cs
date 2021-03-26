//=========== Written by Arthur W. Sheldon AKA Lizband_UCC =======================
//
// Purpose: Gives functionality to the battle actions, like defending or fighting
// Applied to: BattleController object in a battle scene
//
//================================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle_ActionFunctions : MonoBehaviour
{
    // Public Variables
    public GameObject enemySelectionMenu;       // Enemy selection menu
    public bool acceptingInput = true;          // Allow the player to select options?

    // Private Variables
    private Battle_Turn_Manager turnManager;     // Reference the turn manager
    private Menu_Scroll_String menuScrollString; // Reference the scroll string on self to keep track of what menu item the player selects

    void Start()
    {
        turnManager = FindObjectOfType<Battle_Turn_Manager>(); // Set a reference to the SaveManager script on the Config object in the scene
        menuScrollString = gameObject.GetComponent<Menu_Scroll_String>();         // Set a reference to the MenuScrollString script
    }


    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(1); // The delay until it is accepting input again
        acceptingInput = true;              // Allow input again
    }


    void Update()
    {
        // Set move to attack
        if (menuScrollString.currentSelection == 0 && Input.GetButton("Interact") && acceptingInput == true)
        {
            acceptingInput = false;        // Allow input again
            StartCoroutine("acceptInput"); // Activate the keypress delay
            Attack();                      // Execute the Attack class from this script
        }

        // Set move to defend
        if (menuScrollString.currentSelection == 1 && Input.GetButton("Interact") && acceptingInput == true)
        {
            acceptingInput = false;        // Allow input again
            StartCoroutine("acceptInput"); // Activate the keypress delay
            Defend();                      // Execute the Defend class from this script
        }

        // Set move to flee
        if (menuScrollString.currentSelection == 3 && Input.GetButton("Interact") && acceptingInput == true)
        {
            acceptingInput = false;        // Allow input again
            StartCoroutine("acceptInput"); // Activate the keypress delay
            Flee();                        // Execute the Flee class from this script
        }
    }


    public void Attack()
    {
        enemySelectionMenu.SetActive(true);
        StopAllCoroutines();
        gameObject.SetActive(false);
    }


    public void Defend()
    {
        turnManager.SetMoveDefend();
        turnManager.NextTurn();
    }


    public void Flee()
    {
        if (UnityEngine.Random.Range(0, 100) <= PlayerPrefs.GetFloat("fleePercent"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("TempScene")); // Load the pre-battle scene
        }
    }
}
