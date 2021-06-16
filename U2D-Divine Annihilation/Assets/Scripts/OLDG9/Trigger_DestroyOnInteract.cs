//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Starts a dialouge event upon interaction
// Applied to: Interact trigger
//
//=============================================================================

using System.Collections;
using UnityEngine;

public class Trigger_DestroyOnInteract : MonoBehaviour
{
    public GameObject[] targetObjects;
    public bool eventTrigger;
    private bool eventActive;
    private System_InputManager inputManager;

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox")
        {
            // If the player has pressed the action key then activate
            if (Input.GetKeyDown(inputManager.controls["Interact"]) && !eventTrigger)
            {
                foreach (var obj in targetObjects)
                {
                    Destroy(obj);
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

        // If the player collides with the trigger then activate
        if (!eventActive && eventTrigger)
        {
            foreach (var obj in targetObjects)
            {
                Destroy(obj);
            }
            eventActive = true;
        }
    }
}
