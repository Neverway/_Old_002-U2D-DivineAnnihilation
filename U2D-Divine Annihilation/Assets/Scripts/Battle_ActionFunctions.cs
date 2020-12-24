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
    public GameObject actionMenu;
    public GameObject enemySelectionMenu;
    public GameObject configTarget;
    public GameObject battleControllerTarget;
    public bool acceptingInput = true;

    private SaveManager saveManager;
    private Battle_Turn_Manager turnManager;
    private Menu_Scroll_String menuScrollString;

    void Start()
    {
        saveManager = configTarget.GetComponent<SaveManager>();                 // Set a reference to the SaveManager script on the Config object in the scene
        turnManager = battleControllerTarget.GetComponent<Battle_Turn_Manager>(); // Set a reference to the SaveManager script on the Config object in the scene
        menuScrollString = GetComponent<Menu_Scroll_String>();                    // Set a reference to the MenuScrollString script
    }


    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(1); // The delay until it is accepting input again
        acceptingInput = true;              // Allow input again
    }


    void Update()
    {
        if (menuScrollString.currentSelection == 0 && Input.GetKeyDown("z") && acceptingInput == true)
        {
            acceptingInput = false;        // Allow input again
            Attack();                      // Execute the Attack class from this script
            StartCoroutine("acceptInput"); // Activate the keypress delay
        }

        if (menuScrollString.currentSelection == 1 && Input.GetKeyDown("z") && acceptingInput == true)
        {
            acceptingInput = false;        // Allow input again
            Defend();                      // Execute the Defend class from this script
            StartCoroutine("acceptInput"); // Activate the keypress delay
        }

        if (menuScrollString.currentSelection == 3 && Input.GetKeyDown("z") && acceptingInput == true)
        {
            acceptingInput = false;        // Allow input again
            Flee();                        // Execute the Flee class from this script
            StartCoroutine("acceptInput"); // Activate the keypress delay
        }
    }


    public void Attack()
    {
        enemySelectionMenu.SetActive(true); // 
        StopAllCoroutines();
        actionMenu.SetActive(false);        // 
    }


    public void Defend()
    {
        turnManager.SetMoveDefend();
        turnManager.NextTurn(); // 
    }


    public void Flee()
    {
        if (UnityEngine.Random.Range(0, 100) <= PlayerPrefs.GetFloat("fleePercent"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("TempScene")); // Load the pre-battle scene
        }
    }
}
