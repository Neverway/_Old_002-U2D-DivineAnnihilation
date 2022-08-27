//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
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

public class DAG12_Trigger_Interact : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    public TextboxData[] textboxData;

    [Header ("Trigger Data")]
    public UnityEvent OnFinish;
    [SerializeField] private bool eventTrigger; // If true, activation will not require interaction, only collision with the player
    public bool triggerEnabled = true;


    //=-----------------=
    // Private variables
    //=-----------------=
    private bool inTrigger;


    //=-----------------=
    // Reference variables
    //=-----------------=
    private NUPInput inputManager;
    private DAG12_System_TextboxManager textboxManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        inputManager = FindObjectOfType<NUPInput>();
        textboxManager = FindObjectOfType<DAG12_System_TextboxManager>();
    }

    private void Update()
    {
        if (inTrigger)
        {
            if (!eventTrigger && Input.GetKeyDown(inputManager.controls["Interact"]) && !textboxManager.textboxOpen && triggerEnabled || eventTrigger && !textboxManager.textboxOpen && triggerEnabled)
            {
                SendTextboxDataArray();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //textboxManager.currentlyOverlappedTrigger = gameObject;
            inTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //textboxManager.targetTrigger = null;
            //textboxManager.currentlyOverlappedTrigger = null;
            inTrigger = false;
            //eventActive = false;
        }
    }

    public void SendTextboxDataArray()
    {
        textboxManager.currentTrigger = gameObject;
        System.Array.Resize(ref textboxManager.textboxData, textboxData.Length);
        for (int i = 0; i < textboxData.Length; i++)
        {
            textboxManager.textboxData[i].lineText = textboxData[i].lineText;
            textboxManager.textboxData[i].lineName = textboxData[i].lineName;
            textboxManager.textboxData[i].linePortrait = textboxData[i].linePortrait;
            textboxManager.textboxData[i].lineSpeed = textboxData[i].lineSpeed;
        }
        textboxManager.debugActivate = true;
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    [System.Serializable]
    public class TextboxData
    {
        public string lineText;
        public string lineName;
        public Sprite linePortrait;
        public float lineSpeed; // Divide by ten to get a resonable speed, default should be 1 (0.1 after division)
    }
    
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}
