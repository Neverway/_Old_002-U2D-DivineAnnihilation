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

public class DAG12_Trigger_Choice : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    public ChoiceboxData choiceboxData;

    [Header ("Trigger Data")]
    public UnityEvent OnFinish;
    public bool triggerEnabled = true;


    //=-----------------=
    // Private variables
    //=-----------------=


    //=-----------------=
    // Reference variables
    //=-----------------=
    private NUPInput inputManager;
    private DAG12_System_ChoiceboxManager choiceboxManager;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        inputManager = FindObjectOfType<NUPInput>();
        choiceboxManager = FindObjectOfType<DAG12_System_ChoiceboxManager>();
    }

    public void SendChoiceboxData()
    {
        print("Shoot");
        choiceboxManager.currentTrigger = gameObject;
        choiceboxManager.choiceboxData.promptText = choiceboxData.promptText;
        choiceboxManager.choiceboxData.response1 = choiceboxData.response1;
        choiceboxManager.choiceboxData.response2 = choiceboxData.response2;
        //choiceboxManager.debugActivate = true;
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    [System.Serializable]
    public class ChoiceboxData
    {
        public string promptText;
        public string response1;
        public string response2;
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}
