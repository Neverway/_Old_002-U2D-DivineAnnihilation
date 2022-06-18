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
    private DAG12_System_TransitionManager transitionManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}


    void Start()
    {
        saveManager = FindObjectOfType<OTU_System_SaveManager>();   // Find the dialogue manager script
        transitionManager = FindObjectOfType<DAG12_System_TransitionManager>();   // Find the dialogue manager script
    }


    IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1);     // The delay until it is accepting input again
        PlayerPrefs.SetInt("LoadingNewRoom", 1);
        saveManager.activeSave2.scene = loadRoom;
        SceneManager.LoadScene(loadRoom);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            transitionManager.TransitionFade("fadeout",0);
            PlayerPrefs.SetFloat("NextRoomX", nextRoomX);
            PlayerPrefs.SetFloat("NextRoomY", nextRoomY);
            StartCoroutine("ChangeScene");
        }
    }
}
