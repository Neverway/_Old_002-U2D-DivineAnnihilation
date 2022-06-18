//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Starts a dialouge event upon interaction
// Applied to: Interact trigger
//
//=============================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Trigger_SetActiveOnInteract : MonoBehaviour
{
    public GameObject[] activateObjects;
    public GameObject[] deactivateObjects;
    public bool eventTrigger;
    public bool repeating;
    public float repeatDelay;
    private bool eventActive;
    public UnityEvent onTriggered;
    private System_InputManager inputManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}


    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
    }


    IEnumerator resetVariables()
    {
        yield return new WaitForSeconds(repeatDelay);     // The delay until it is accepting input again
        eventActive = true;                  // Allow input again
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox")
        {
            // If the player has pressed the action key then activate
            if (Input.GetKeyDown(inputManager.controls["Interact"]) && !eventTrigger)
            {
                foreach (var obj in activateObjects)
                {
                    obj.SetActive(true);
                }
                foreach (var obj in deactivateObjects)
                {
                    obj.SetActive(false);
                }

                onTriggered.Invoke();

                if (repeating)
                {
                    StartCoroutine("resetVariables");
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox")
        {
            // If the player collides with the trigger then activate
            if (!eventActive && eventTrigger)
            {
                foreach (var obj in activateObjects)
                {
                    obj.SetActive(true);
                }
                foreach (var obj in deactivateObjects)
                {
                    obj.SetActive(false);
                }

                onTriggered.Invoke();

                if (repeating)
                {
                    StartCoroutine("resetVariables");
                }
                eventActive = true;
            }
        }
    }
}
