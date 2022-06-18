//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// SID: 
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: Current step is the step that's being executed, completed micro steps
//     is whether the first loop of that step has been completed, & completed
//     steps is whether that step is completed (it will continue to loop 
//     through until an exit state has been reached. For example: the timer
//     runs out or all targets have reached their destination).
//
//     Don't freeze player input if there is a textbox event, also unfreeze
//     player input if the next event step is a textbox event (you can refreeze
//     it once that step is done though)
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DA_Trigger_Event : MonoBehaviour
{
    // Public variables
    public EventStep[] eventStep;
    public bool requiresInteraction;
    public int currentStep = 0;
    public int completedMicroSteps = -1;
    public int completedSteps = -1;

    // Private variables
    private bool inTrigger;
    private Animator animator;
    private bool eventActive;
    private int playerEntityFollow = -1;

    // Reference variables
    private OTU_System_MenuManager menuManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}


    void Start()
    {
        ResetSteps();
        menuManager = FindObjectOfType<OTU_System_MenuManager>();
    }    
    
    
    IEnumerator TimerCountdown(float timer)
    {
        yield return new WaitForSeconds(timer);     // The delay until it is accepting input again
        eventStep[currentStep].OnTimerExpired.Invoke();
        Debug.Log("Exit case: Timer expired!");
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


    private void ExecuteStep()
    {
        // Run the current event step
        if (inTrigger && currentStep != completedSteps || eventActive && currentStep != completedSteps)
        {
            eventActive = true; // Allow the event loop to continue even if the player leaves the event trigger
            // Run one micro step per standard step (this acts like a one-time start function for the step)
            if (currentStep != completedMicroSteps)
            {
                if (eventStep[currentStep].freezePlayerInput)
                {
                    menuManager.alternateMenuActive = true;
                }
                // Move entities
                if (eventStep[currentStep].targetPosition.Length > 0 && eventStep[currentStep].targetPosition.Length == eventStep[currentStep].targetEntity.Length)
                {
                    for (int i = 0; i < eventStep[currentStep].targetPosition.Length; i++)
                    {
                        if (eventStep[currentStep].targetPosition[i] != null)
                        {
                            if (eventStep[currentStep].targetEntity[i].GetComponent<DA_Entity_Follower>())
                            {
                                eventStep[currentStep].targetEntity[i].GetComponent<DA_Entity_Follower>().loneWolf = true;
                                eventStep[currentStep].targetEntity[i].GetComponent<DA_Entity_Follower>().target = eventStep[currentStep].targetPosition[i];
                                if (eventStep[currentStep].targetEntity[i].GetComponent<DA_Entity_Follower>().gameObject.tag == "Player")
                                {
                                    eventStep[currentStep].targetEntity[i].GetComponent<DA_Entity_Follower>().followingEnabled = true;
                                    
                                }
                            }

                            if (eventStep[currentStep].teleportToDestination)
                            {
                                eventStep[currentStep].targetEntity[eventStep[currentStep].destinationTarget-1].transform.position = eventStep[currentStep].targetPosition[eventStep[currentStep].destinationTarget-1].transform.position;
                            }
                            /*
                            if (eventStep[currentStep].destinationTarget > 0)
                            {
                                print("beep"+Vector2.Distance(eventStep[currentStep].targetEntity[eventStep[currentStep].destinationTarget-1].transform.position, eventStep[currentStep].targetPosition[eventStep[currentStep].destinationTarget-1].position));
                                if (Vector2.Distance(eventStep[currentStep].targetEntity[eventStep[currentStep].destinationTarget-1].transform.position, eventStep[currentStep].targetPosition[eventStep[currentStep].destinationTarget-1].position) > 50)
                                {
                                    print("The entity is too far away!");
                                    eventStep[currentStep].targetEntity[eventStep[currentStep].destinationTarget-1].transform.position = eventStep[currentStep].targetEntity[eventStep[currentStep].destinationTarget-1].transform.GetChild(0).gameObject.transform.position;
                                }
                            }*/
                        }
                    }
                }

                // Animate entities
                if (eventStep[currentStep].animation.Length > 0 && eventStep[currentStep].animation.Length == eventStep[currentStep].targetEntity.Length)
                {
                    for (int i = 0; i < eventStep[currentStep].animation.Length; i++)
                    {
                        if (eventStep[currentStep].animation[i] != "")
                        {
                            print(eventStep[currentStep].animation[i] + " is the target animation");
                            animator = eventStep[currentStep].targetEntity[i].GetComponent<Animator>();
                            animator.Play(eventStep[currentStep].animation[i]);
                        }
                    }
                }

                // Remote activate triggers
                if (eventStep[currentStep].stepText != null)
                {
                    eventStep[currentStep].stepText.GetComponent<DA_Trigger_Interact>().RemoteActivateTrigger();
                }

                // Countdown timer
                if (eventStep[currentStep].timer != 0)
                {
                    StartCoroutine(TimerCountdown(eventStep[currentStep].timer));
                }

                completedMicroSteps = currentStep; // End the loop
            }
            
            if (eventStep[currentStep].waitForDestination)
            {
                // Destination reached
                CheckDestinationReached();
            }
            
            if (eventStep[currentStep].stepText)
            {
                if (eventStep[currentStep].stepText.GetComponent<DA_Trigger_Interact>().completionBlip)
                {
                    eventStep[currentStep].OnDialogueCompleted.Invoke();
                    Debug.Log("Exit case: Dialougue complete!");
                }
            }
        }
    }

    private void CheckDestinationReached()
    {
        if (currentStep >= 0)
        {
            if (Vector2.Distance(eventStep[currentStep].targetEntity[eventStep[currentStep].destinationTarget-1].transform.position, eventStep[currentStep].targetPosition[eventStep[currentStep].destinationTarget-1].position) <= 0.15)
            {
                eventStep[currentStep].OnDestinationReached.Invoke();
                Debug.Log("Exit case: Destination reached!");
            }
        }
    }


    public void CompleteStep()
    {
        completedSteps = currentStep; // End the loop
        print("step completed!");
    }


    public void StepForward()
    {
        currentStep++;
        print("steping forward!");
    }


    public void ResetSteps()
    {
        currentStep = 0;
        completedSteps = -1;
        playerEntityFollow = -1;
        if (playerEntityFollow > 0)
        {}
    }
    

    public void StopFollowing(int _targetEntity)
    {
        eventStep[currentStep].targetEntity[_targetEntity].GetComponent<DA_Entity_Follower>().followingEnabled = false;
    }

    
    public void UnfreezePlayerInput()
    {
        menuManager.alternateMenuActive = false;
    }

    
    public void PrintDebugMessage(string message)
    {
        if (message != "")
        {
            Debug.Log(message);
        }
        else
        {
            Debug.Log("The step executed successfully!");
        }
    }


    [System.Serializable]
    public class EventStep
    {
        public string stepSummary; // Just a description tag to help me remember what a particular step does. There is no code attached for this.
        public GameObject[] targetEntity;
        public Transform[] targetPosition;
        public string[] animation;
        public bool freezePlayerInput = true;
        public GameObject stepText;
        public bool waitForDestination;
        public bool teleportToDestination;
        public int destinationTarget;
        public float timer;
        [Header ("0 = All entities")]
        public UnityEvent OnDestinationReached;
        public UnityEvent OnTimerExpired;
        public UnityEvent OnDialogueCompleted;
    }
}
