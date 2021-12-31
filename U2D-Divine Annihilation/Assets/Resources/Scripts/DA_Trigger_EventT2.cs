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

public class DA_Trigger_EventT2 : MonoBehaviour
{
    // Public variables
    public EventStep[] eventStep;

    // Private variables

    // Reference variables


    void Start()
    {
	
    }


    void Update()
    {
	
    }


    [System.Serializable]
    public class EventStep
    {
        public GameObject[] targetEntity;
        public Transform[] targetPosition;
        public string[] animation;
        public GameObject stepText;
        public float timer;
        public UnityEvent OnDestinationReached;
        public UnityEvent OnTimerExpired;
        public UnityEvent OnDialogueCompleted;
    }
}
