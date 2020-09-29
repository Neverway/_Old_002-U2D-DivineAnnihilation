using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_trigger_save : MonoBehaviour
{
    private scr_system_saveManager saveManager;
    // Start is called before the first frame update
    void Start()
    {
        saveManager = FindObjectOfType<scr_system_saveManager>();   // Find the dialogue manager script
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Check if something has been in the trigger
    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown("z"))
        {
            saveManager.activeSave.scene = SceneManager.GetActiveScene().name;
            saveManager.Save();
        }
    }
}
