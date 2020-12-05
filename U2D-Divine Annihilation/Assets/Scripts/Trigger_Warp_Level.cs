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
    //public GameObject Player;
    //public bool PlayTransition;

    public void Update()
    {
        PlayerPrefs.SetFloat("NextRoomX", nextRoomX);
        PlayerPrefs.SetFloat("NextRoomY", nextRoomY);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox")
        {
            Debug.Log(PlayerPrefs.GetInt("LoadingNewRoom"));
            PlayerPrefs.SetInt("LoadingNewRoom", 1);
            SceneManager.LoadScene(loadRoom);
        }
    }
}
