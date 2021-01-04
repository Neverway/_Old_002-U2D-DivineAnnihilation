//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Save the game file to the current slot
// Applied to: WarpLevel trigger
//
//=============================================================================

using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger_Warp_Level : MonoBehaviour
{
    public float nextRoomX;
    public float nextRoomY;
    public string loadRoom;
    private SaveManager saveManager;
    //public GameObject Player;
    //public bool PlayTransition;

    public void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();   // Find the dialogue manager script
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox")
        {
            PlayerPrefs.SetFloat("NextRoomX", nextRoomX);
            PlayerPrefs.SetFloat("NextRoomY", nextRoomY);
            PlayerPrefs.SetInt("LoadingNewRoom", 1);
            saveManager.activeSave.scene = loadRoom;
            SceneManager.LoadScene(loadRoom);
        }
    }
}
