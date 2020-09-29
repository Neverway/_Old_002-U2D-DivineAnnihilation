// Included Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_trigger_partySet : MonoBehaviour
{
    public GameObject Target;
    public bool addMember;
    void OnTriggerEnter2D(Collider2D other)
    {
        // Set party on collision with trigger
        if (other.gameObject.name == "pre_entity_fox")
        {
            if (addMember)
            {
                // Add a entity as a party member
                Debug.Log("PartySet: Party +Member");
            }

            if (!addMember)
            {
                // Remove a entity as a party member
                Debug.Log("PartySet: Party -Member");
            }
        }
    }
}
