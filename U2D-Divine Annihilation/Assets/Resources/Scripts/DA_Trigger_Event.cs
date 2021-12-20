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
using UnityEngine.Playables;

public class DA_Trigger_Event : MonoBehaviour
{
    // Public variables
    public bool interactionRequiredToStartEvent;
    //public bool freezePlayer;
    public EventStep[] eventStep;

    // Private variables
    public int step = 0;
    private bool inTrigger;

    // Reference variables


    private void Start()
    {

    }


    private void Update()
    {
        // Start Event Trigger
        if (inTrigger && !interactionRequiredToStartEvent && step == 0)
        {
            eventStep[step].cutscene.Play();
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


    [System.Serializable]
    public class EventStep
    {

        //[Header ("Change animations")]
        //public GameObject[] entity;
        //public Animator[] animation; 
        [Header ("Start Animation")]
        public PlayableDirector cutscene;

        [Header ("Next step activates on")]
        public bool interaction;
        public bool animationFinished;
        public float timer;
    }
}
