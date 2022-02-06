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

public class DA_Trigger_GameEvent : MonoBehaviour
{
    // Public variables
    public EventStep[] eventSteps;
    public bool freezePlayerInput;
    public bool requiresInteraction;
    
    public int currentStep; // The current index of which step is being executed
    public int completedSteps; // Up to which steps should be inactive
    public int independentStep; // Up to which step is waiting for an exit condition

    // Private variables
    private bool triggered;

    // Reference variables


    void Start()
    {
	
    }


    void FixedUpdate()
    {
        // Triggered via interaction/Triggered via collision
        if (triggered)
        {
            if (eventSteps[currentStep].targetDestination != null)
            {
                print("Event goto location");
                // Target entity is a player
                if (eventSteps[currentStep].targetEntity.GetComponent<DA_Entity_Control>())
                {
                    
                }
                
                // Target entity is a follower/NPC
                else if (eventSteps[currentStep].targetEntity.GetComponent<DA_Entity_Follower>())
                {

                }
            }
            if (eventSteps[currentStep].animation != "")
            {
                print("Event play animation");
            }
            if (eventSteps[currentStep].remoteTextbox != null)
            {
                print("Event remote textbox");
            }
            if (eventSteps[currentStep].countdownTimer != -1)
            {
                print("Event countdown timer");
            }
        }
    }
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            triggered = true;
        }
    }


    [System.Serializable]
    public class EventStep
    {
        public string stepSummary;
        [Header ("Entity")]
        public GameObject targetEntity;
        public Transform targetDestination;
        public string animation;

        [Header ("Remotes")]
        public DA_Trigger_Interact remoteTextbox;
        public float countdownTimer = -1;

        [Header ("Exit Conditions")]
        public UnityEvent OnTimerExpired;
        public UnityEvent OnDestinationReached;
        public UnityEvent OnTextboxFinish;
    }
}
