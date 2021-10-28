//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: AKC
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DA_Trigger_WarpLevel : MonoBehaviour
{
    public float nextRoomX;
    public float nextRoomY;
    public string loadRoom;
    private OTU_System_SaveManager saveManager;
    private OTU_System_TransitionManager transitionManager;


    void Start()
    {
        saveManager = FindObjectOfType<OTU_System_SaveManager>();   // Find the dialogue manager script
        transitionManager = FindObjectOfType<OTU_System_TransitionManager>();   // Find the dialogue manager script
    }


    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(transitionManager.fadeStayDelay);     // The delay until it is accepting input again
        PlayerPrefs.SetInt("LoadingNewRoom", 1);
        saveManager.activeSave2.scene = loadRoom;
        SceneManager.LoadScene(loadRoom);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            transitionManager.FadeIn("");
            PlayerPrefs.SetFloat("NextRoomX", nextRoomX);
            PlayerPrefs.SetFloat("NextRoomY", nextRoomY);
            StartCoroutine("ChangeScene");
        }
    }
}
