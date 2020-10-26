// Included Libraries
using UnityEngine;

/* Battle controller
 * ---------------------
 * This script is applied to the battle controller object in a scene
 * It acts as a passthrough reference to the outside player prefs
*/
public class BattleController : MonoBehaviour
{
    // Class references
    public GameObject configTarget;     // Get a reference to the Config object in the scene
    private SaveManager saveManager;    // Get a reference to the SaveManager script


    // Start is called before the first frame update
    void Start()
    {
        saveManager = configTarget.GetComponent<SaveManager>(); // Set a reference to the SaveManager script on the Config object in the scene
        saveManager.PlayerPrefLoad();                           // Load the player prefs temporary save file
    }
}
