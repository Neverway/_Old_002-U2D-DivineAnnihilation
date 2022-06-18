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
    public string saveChapter;
    private bool inFinalTextbox;

    private OTU_System_InputManager inputManager;
    private OTU_System_TextboxManager textboxManager;
    private OTU_System_SaveManager saveManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    // Start is called before the first frame update
    void Start()
    {
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        textboxManager = FindObjectOfType<OTU_System_TextboxManager>();
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inFinalTextbox && !textboxManager.textboxActive)
        {
            inFinalTextbox = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(0).GetComponent<DA_Trigger_Interact>().EnableTrigger();
            gameObject.transform.GetChild(0).GetComponent<DA_Menu_Control>().enabled = false;
        }
    }

    public void SaveNo()
    {
        textboxManager.CloseChoicebox();
        gameObject.transform.GetChild(0).GetComponent<DA_Trigger_Interact>().EnableTrigger();
        gameObject.transform.GetChild(0).GetComponent<DA_Menu_Control>().enabled = false;
    }

    public void SaveYes()
    {
        print("SaveYes");
        saveManager.activeSave2.saveChapter = saveChapter;
        saveManager.Save();
        gameObject.transform.GetChild(0).GetComponent<DA_Menu_Control>().enabled = false;
        textboxManager.CloseChoicebox();
        inFinalTextbox = true;
    }
}
