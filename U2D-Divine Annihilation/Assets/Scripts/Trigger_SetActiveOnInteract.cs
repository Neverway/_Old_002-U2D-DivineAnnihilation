//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Starts a dialouge event upon interaction
// Applied to: Interact trigger
//
//=============================================================================

using System.Collections;
using UnityEngine;

public class Trigger_SetActiveOnInteract : MonoBehaviour
{
    public GameObject[] activateObjects;
    public GameObject[] deactivateObjects;
    public bool eventTrigger;
    public bool repeating;
    public float repeatDelay;
    private bool eventActive;


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
            if (Input.GetKeyDown("z") && !eventTrigger)
            {
                foreach (var obj in activateObjects)
                {
                    obj.SetActive(true);
                }
                foreach (var obj in deactivateObjects)
                {
                    obj.SetActive(false);
                }
                if (repeating)
                {
                    StartCoroutine("resetVariables");
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
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
            if (repeating)
            {
                StartCoroutine("resetVariables");
            }
            eventActive = true;
        }
    }
}
