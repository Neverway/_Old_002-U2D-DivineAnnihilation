//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: AKC
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OTU_Overworld_SavePoint : MonoBehaviour
{
    private OTU_System_TextboxManager textboxManager;
    private OTU_System_SaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        textboxManager = FindObjectOfType<OTU_System_TextboxManager>();
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SaveNo()
    {
        textboxManager.CloseChoicebox();
        gameObject.GetComponent<DA_Trigger_Interact>().EnableTrigger();
        gameObject.GetComponent<DA_Menu_Control>().enabled = false;
    }

    public void SaveYes()
    {
        textboxManager.CloseChoicebox();
        saveManager.Save();
        gameObject.GetComponent<DA_Trigger_Interact>().EnableTrigger();
        gameObject.GetComponent<DA_Menu_Control>().enabled = false;
    }
}
