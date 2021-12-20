//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// SID: 
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DA_Trigger_PartySet : MonoBehaviour
{
    // Public variables
    public string partyMemberID;
    [Header ("0 = Next open slot")]
    [Range (0,3)]
    public int partyPosition;
    public bool addMember;
    public bool eventTrigger;
    public UnityEvent OnFinish;

    // Private variables
    private bool inTrigger;

    // Reference variables
    private OTU_System_SaveManager saveManager;


    void Start()
    {
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
    }


    void Update()
    {
        if (inTrigger && eventTrigger)
        {
            if (partyPosition == 0)
            {
                if (saveManager.activeSave2.partyMembers[0] == "NULL") { saveManager.activeSave2.partyMembers[0] = partyMemberID; OnFinish.Invoke();}
                else if (saveManager.activeSave2.partyMembers[1] == "NULL") { saveManager.activeSave2.partyMembers[1] = partyMemberID; OnFinish.Invoke();}
                else if (saveManager.activeSave2.partyMembers[2] == "NULL") { saveManager.activeSave2.partyMembers[2] = partyMemberID; OnFinish.Invoke();}
                else { Debug.Log("In " + gameObject.name + ", the party is currently full!"); }
            }
            else if (partyPosition == 1) { saveManager.activeSave2.partyMembers[0] = partyMemberID; OnFinish.Invoke();}
            else if (partyPosition == 2) { saveManager.activeSave2.partyMembers[1] = partyMemberID; OnFinish.Invoke();}
            else if (partyPosition == 3) { saveManager.activeSave2.partyMembers[2] = partyMemberID; OnFinish.Invoke();}
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            inTrigger = false;
        }
    }
}
