//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Save the game file to the current slot
// Applied to: Savepoint trigger
//
//=============================================================================

using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger_Save : MonoBehaviour
{
    public string saveChapterName = "???";
    private System_InputManager inputManager;
    private SaveManager saveManager;

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
        saveManager = FindObjectOfType<SaveManager>();   // Find the dialogue manager script
    }


    void Update()
    {
        
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(inputManager.controls["Interact"]))
        {
            saveManager.activeSave.scene = SceneManager.GetActiveScene().name;
            saveManager.activeSave.saveChapter = saveChapterName;
            saveManager.Save();
        }
    }
}
