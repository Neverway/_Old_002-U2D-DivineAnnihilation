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
    private GameObject loadingScreen;
    //public GameObject Player;
    //public bool PlayTransition;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    public void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();   // Find the dialogue manager script
        loadingScreen = GameObject.FindWithTag("Loading Screen");
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox")
        {
            if(loadingScreen != null)
            {
                loadingScreen.transform.GetChild(0).gameObject.SetActive(true);
            }
            PlayerPrefs.SetFloat("NextRoomX", nextRoomX);
            PlayerPrefs.SetFloat("NextRoomY", nextRoomY);
            PlayerPrefs.SetInt("LoadingNewRoom", 1);
            saveManager.activeSave.scene = loadRoom;
            SceneManager.LoadScene(loadRoom);
        }
    }
}
