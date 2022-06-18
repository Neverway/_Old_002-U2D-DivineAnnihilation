//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: 
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;   
using System.Collections.Generic;
using UnityEngine;

public class DA_Trigger_Shop : MonoBehaviour
{
    // Public variables
    [Header ("Intro")]
    public string introDialogue;
    public string introName;
    public Sprite introPortrait;

    [Header("Buy")]
    public string buyDialogue;
    public string buyName;
    public Sprite buyPortrait;

    public GameObject[] buyableItems;
    public int[] buyableCosts;

    [Header("Sell")]
    public string sellDialogue;
    public string sellName;
    public Sprite sellPortrait;

    [Header("Talk")]
    public string talkDialogue;
    public string talkName;
    public Sprite talkPortrait;

    public string[] talkOptions;

    [Header("Intro")]
    public string leaveDialogue;
    public string leaveName;
    public Sprite leavePortrait;

    // Private variables
    public bool inTrigger;
    public bool acceptingInput;

    // Reference variables
    private OTU_System_InputManager inputManager;
    private OTU_Overworld_ShopboxManager shopboxManager;
    private OTU_System_MenuManager menuManager;


    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        shopboxManager = FindObjectOfType<OTU_Overworld_ShopboxManager>();
        menuManager = FindObjectOfType<OTU_System_MenuManager>();

    }


    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(1.2f);     // The delay until it is accepting input again
        acceptingInput = true;                  // Allow input again
    }


    void Update()
    {
        if (inTrigger && Input.GetKey(inputManager.controls["Interact"]) && acceptingInput && !menuManager.menuActive)
        {
            shopboxManager.targetTrigger = gameObject.GetComponent<DA_Trigger_Shop>();
            shopboxManager.OpenShopbox();
            acceptingInput = false;
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
}
