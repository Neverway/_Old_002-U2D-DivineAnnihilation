// Included Libraries
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
    public GameObject configTarget;                 // Get a reference to the Config object in the scene
    private SaveManager saveManager;                // Get a reference to the SaveManager script
    private MenuScrollString menuScrollString;      // Get a reference to the MenuScrollString script


    // Start is called before the first frame update
    void Start()
    {
        saveManager = configTarget.GetComponent<SaveManager>(); // Set a reference to the SaveManager script on the Config object in the scene
        menuScrollString = GetComponent<MenuScrollString>();    // Set a reference to the MenuScrollString script
    }


    // Update is called once per frame
    void Update()
    {
        // Check to see if the Flee menu option is selected
        if (menuScrollString.currentSelection == 3 && Input.GetKeyDown("z"))
        {
            Flee(); // Execute the Flee class from this script
        }
    }


    // Test if the party is able to flee, and if so set them to their pre-battle locations
    public void Flee()
    {
        if (Random.Range(0, 100) <= PlayerPrefs.GetFloat("fleePercent"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("TempScene")); // Load the pre-battle scene
        }
    }
}
