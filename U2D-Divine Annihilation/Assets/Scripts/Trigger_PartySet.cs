//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Add or remove a party memeber from the players party
// Applied to: PartySet trigger
//
//=============================================================================

using UnityEngine;

public class Trigger_PartySet : MonoBehaviour
{
    public string partyMemberID;
    public bool addMember;
    private SaveManager saveManager;

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        // Set party on collision with trigger
        if (other.gameObject.name == "Entity Fox")
        {
            if (addMember)
            {
                // Check to make sure they are not already in your party
                if (saveManager.activeSave.partyMemberOne != partyMemberID && saveManager.activeSave.partyMemberTwo != partyMemberID && saveManager.activeSave.partyMemberThree != partyMemberID)
                {
                    // Add a entity as a party member
                    if (saveManager.activeSave.partyMemberOne == "NULL")
                    {
                        saveManager.activeSave.partyMemberOne = partyMemberID;
                    }
                    // Add a entity as a party member
                    else if (saveManager.activeSave.partyMemberTwo == "NULL")
                    {
                        saveManager.activeSave.partyMemberTwo = partyMemberID;
                    }
                    // Add a entity as a party member
                    else if (saveManager.activeSave.partyMemberThree == "NULL")
                    {
                        saveManager.activeSave.partyMemberThree = partyMemberID;
                    }
                }
            }

            if (!addMember)
            {
                // Add a entity as a party member
                if (saveManager.activeSave.partyMemberOne == partyMemberID)
                {
                    saveManager.activeSave.partyMemberOne = "NULL";
                }
                // Add a entity as a party member
                else if (saveManager.activeSave.partyMemberTwo == partyMemberID)
                {
                    saveManager.activeSave.partyMemberTwo = "NULL";
                }
                // Add a entity as a party member
                else if (saveManager.activeSave.partyMemberThree == partyMemberID)
                {
                    saveManager.activeSave.partyMemberThree = "NULL";
                }
            }
        }
    }
}
