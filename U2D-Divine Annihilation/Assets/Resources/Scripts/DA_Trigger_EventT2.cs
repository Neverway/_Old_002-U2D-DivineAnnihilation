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
    public bool requiresInteraction;
    public int currentStep = 0;
    public int completedSteps = -1;

    // Private variables
    private bool inTrigger;
    private Animator animator;

    // Reference variables


    void Start()
    {
        ResetSteps();
    }


    void Update()
    {
        ExecuteStep();
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

    public void StepForward()
    {
        currentStep++;
    }

    public void ResetSteps()
    {
        currentStep = 0;
        completedSteps = -1;
    }

    private void ExecuteStep()
    {
        // Run one event step
        if (inTrigger && currentStep != completedSteps)
        {
            // Move entities
            if (eventStep[currentStep].targetPosition.Length > 0 && eventStep[currentStep].targetPosition.Length == eventStep[currentStep].targetEntity.Length)
            {
                print("Target position detected!");
                for (int i = 0; i < eventStep[currentStep].targetPosition.Length; i++)
                {
                    if (eventStep[currentStep].targetPosition[i] != null)
                    {
                        eventStep[currentStep].targetEntity[i].GetComponent<DA_Testing>().loneWolf = true;
                        eventStep[currentStep].targetEntity[i].GetComponent<DA_Testing>().target = eventStep[currentStep].targetPosition[i];
                    }
                }
            }

            // Animate entities
            if (eventStep[currentStep].animation.Length > 0 && eventStep[currentStep].animation.Length == eventStep[currentStep].targetEntity.Length)
            {
                print("Animation detected!");
                for (int i = 0; i < eventStep[currentStep].animation.Length; i++)
                {
                    if (eventStep[currentStep].animation[i] != "")
                    {
                        animator = eventStep[currentStep].targetEntity[i].GetComponent<Animator>();
                        animator.Play(eventStep[currentStep].animation[i]);
                    }
                }
            }
            completedSteps = currentStep; // End the loop
        }
    }
}
