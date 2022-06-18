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
    public bool addMember;
    public bool eventTrigger;
    public string partyMemberID;

    [Header ("Join Options")]
    [Range (1, 3)]
    public int partyPosition = 1;
    public bool forceIntoSlot;
    [Header ("0: Exit party, 1: Fallback, 2: Fallback Newcomer")]
    [Range (0, 2)]
    public int forceMethod;
    // public bool collapsePartyOnRemoval;
    public UnityEvent OnFinish;

    // // Private variables
    private bool inTrigger;
    public DA_Entity_Follower[] targets;

    // Reference variables
    private OTU_System_SaveManager saveManager;
    private OTU_System_InputManager inputManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}
    


    void Start()
    {
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
        inputManager = FindObjectOfType<OTU_System_InputManager>();
    }


    void Update()
    {
        if (inTrigger)
        {
            if (eventTrigger || Input.GetKey(inputManager.controls["Up"]))
            {
                if (addMember)
                {
                    PartyAdd();
                }
                else
                {
                    PartySubtract();
                }
            }
        }
    }


    void PartyAdd()
    {
        if (saveManager.activeSave2.partyMembers[partyPosition-1] == "NULL")
        {
            saveManager.activeSave2.partyMembers[partyPosition-1] = partyMemberID;
            OnFinish.Invoke();
        }
        else if (forceIntoSlot)
        {
            // Force exit party method
            if (forceMethod == 0)
            {
                saveManager.activeSave2.partyMembers[partyPosition-1] = partyMemberID;
                OnFinish.Invoke();
            }

            // Force fallback method
            else if (forceMethod == 1)
            {
                if (saveManager.activeSave2.partyMembers[partyPosition+0] == "NULL")
                {
                    saveManager.activeSave2.partyMembers[partyPosition+0] = saveManager.activeSave2.partyMembers[partyPosition-1];
                }
                else
                {
                    Debug.LogWarning("A new party member took over an occupied slot, but the previouse member was not able to be shifted!");
                }
                saveManager.activeSave2.partyMembers[partyPosition-1] = partyMemberID;
                OnFinish.Invoke();
            }
        }
    }


    void PartySubtract()
    {
        // Clear current slot
        if (saveManager.activeSave2.partyMembers[0] == partyMemberID)
        {
            targets = GameObject.FindObjectsOfType<DA_Entity_Follower>();
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].partyPosition == 1)
                {
                    targets[i].GetComponent<DA_Entity_Follower>().partyPosition = 0;
                }
            }
            saveManager.activeSave2.partyMembers[0] = "NULL";
            saveManager.activeSave2.partyMembers[0] = saveManager.activeSave2.partyMembers[1];
            saveManager.activeSave2.partyMembers[1] = saveManager.activeSave2.partyMembers[2];
            saveManager.activeSave2.partyMembers[2] = "NULL";
        }
        if (saveManager.activeSave2.partyMembers[1] == partyMemberID)
        {
            targets = GameObject.FindObjectsOfType<DA_Entity_Follower>();
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].partyPosition == 2)
                {
                    targets[i].GetComponent<DA_Entity_Follower>().partyPosition = 0;
                }
            }
            saveManager.activeSave2.partyMembers[1] = "NULL";
            saveManager.activeSave2.partyMembers[1] = saveManager.activeSave2.partyMembers[2];
            saveManager.activeSave2.partyMembers[2] = "NULL";
        }
        if (saveManager.activeSave2.partyMembers[2] == partyMemberID)
        {
            targets = GameObject.FindObjectsOfType<DA_Entity_Follower>();
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].partyPosition == 3)
                {
                    targets[i].GetComponent<DA_Entity_Follower>().partyPosition = 0;
                }
            }
            saveManager.activeSave2.partyMembers[2] = "NULL";
        }
        OnFinish.Invoke();
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
