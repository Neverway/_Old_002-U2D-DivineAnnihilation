//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Acts as a passthrough reference to the outside player prefs
// Applied to: BattleController object in a battle scene
//
//=============================================================================
using UnityEngine;

public class Battle_Controller : MonoBehaviour
{
    // Private Variables
    private SaveManager saveManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>(); // Set a reference to the SaveManager script on the Config object in the scene
        saveManager.PlayerPrefLoad();                           // Load the player prefs temporary save file
    }
}
