// Included Libraries
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Battle action menu functions
 * ---------------------
 * This script is applied to the battle Action Menu object in a scene
 * It gives functionality to the Flee action in a battle menu
 * All of this is to test if the party can flee, and if so put them to their "pre-battle" locations
*/
public class BattleActionFunctions : MonoBehaviour
{
    // Class references
    public GameObject actionMenu;
    public GameObject enemySelectionMenu;
    public GameObject configTarget;                 // Get a reference to the Config object in the scene
    private SaveManager saveManager;                // Get a reference to the SaveManager script
    public GameObject battleControllerTarget;                 // Get a reference to the Config object in the scene
    private BattleTurnManager turnManager;                // Get a reference to the SaveManager script
    private MenuScrollString menuScrollString;      // Get a reference to the MenuScrollString script
    public bool acceptingInput = true;


    // Start is called before the first frame update
    void Start()
    {
        saveManager = configTarget.GetComponent<SaveManager>(); // Set a reference to the SaveManager script on the Config object in the scene
        turnManager = battleControllerTarget.GetComponent<BattleTurnManager>(); // Set a reference to the SaveManager script on the Config object in the scene
        menuScrollString = GetComponent<MenuScrollString>();    // Set a reference to the MenuScrollString script
    }


    // Key press delay
    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(1);     // The delay until it is accepting input again
        acceptingInput = true;                  // Allow input again
    }


    // Update is called once per frame
    void Update()
    {
        // Check to see if the Defend menu option is selected
        if (menuScrollString.currentSelection == 0 && Input.GetKeyDown("z") && acceptingInput == true)
        {
            acceptingInput = false;                  // Allow input again
            StartCoroutine("acceptInput");                                  // Activate the keypress delay
            Attack(); // Execute the Defend class from this script
        }
        // Check to see if the Defend menu option is selected
        if (menuScrollString.currentSelection == 1 && Input.GetKeyDown("z") && acceptingInput == true)
        {
            acceptingInput = false;                  // Allow input again
            Defend(); // Execute the Defend class from this script
            StartCoroutine("acceptInput");                                  // Activate the keypress delay
        }
        // Check to see if the Flee menu option is selected
        if (menuScrollString.currentSelection == 3 && Input.GetKeyDown("z") && acceptingInput == true)
        {
            acceptingInput = false;                  // Allow input again
            Flee(); // Execute the Flee class from this script
            StartCoroutine("acceptInput");                                  // Activate the keypress delay
        }
    }

    // Test if the party is able to flee, and if so set them to their pre-battle locations
    public void Attack()
    {
        enemySelectionMenu.SetActive(true);
        actionMenu.SetActive(false);
    }


    // Test if the party is able to flee, and if so set them to their pre-battle locations
    public void Defend()
    {
        turnManager.NextTurn();
    }


    // Test if the party is able to flee, and if so set them to their pre-battle locations
    public void Flee()
    {
        if (UnityEngine.Random.Range(0, 100) <= PlayerPrefs.GetFloat("fleePercent"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("TempScene")); // Load the pre-battle scene
        }
    }
}
