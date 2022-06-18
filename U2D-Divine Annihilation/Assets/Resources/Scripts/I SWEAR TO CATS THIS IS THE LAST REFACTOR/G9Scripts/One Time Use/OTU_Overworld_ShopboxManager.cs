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
using UnityEngine.UI;

public class OTU_Overworld_ShopboxManager : MonoBehaviour
{
    // Public variables
    public bool shopboxActive;

    [Header("Intro")]
    public string introDialogue;
    public string introName;
    public Sprite introPortrait;

    [Header("Buy")]
    public string buyDialogue;
    public string buyName;
    public Sprite buyPortrait;

    public GameObject[] buyableItems;
    public int[] buyableCosts;
    public Image[] buyableIcons;

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

    // Reference variables
    public Text textboxDialogueBox;
    public Text shopboxDialogueBox;
    public DA_Trigger_Shop targetTrigger;
    private OTU_System_TextboxManager textboxManager;
    private OTU_System_InventoryManager inventoryManager;
    private OTU_System_SaveManager saveManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}


    void Start()
    {
        textboxManager = FindObjectOfType<OTU_System_TextboxManager>();
        inventoryManager = FindObjectOfType<OTU_System_InventoryManager>();
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
    }


    void Update()
    {

    }


    public void OpenShopbox()
    {
        PassTriggerData();
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        shopboxActive = true;
        textboxManager.TextboxNonarray(introDialogue, introName, introPortrait);
        textboxManager.dialogueText = shopboxDialogueBox;
    }


    public void CloseShopbox()
    {
        gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<DA_Menu_Control>().ResetCurrentSelection();
        textboxManager.dialogueText = textboxDialogueBox;
        targetTrigger.StopCoroutine("acceptInput");
        targetTrigger.StartCoroutine("acceptInput");
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        shopboxActive = false;
    }


    public void Buy()
    {
        textboxManager.TextboxNonarray(buyDialogue, buyName, buyPortrait);
        gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(true);
        for (int i = 0; i < buyableItems.Length; i++)
        {
            buyableIcons[i].sprite = buyableItems[i].GetComponent<DA_Trigger_PickupItem>().itemIcon;
            gameObject.transform.GetChild(2).transform.GetChild(1).gameObject.GetComponent<DA_Menu_Control>().baseText[i] = "    G" + buyableCosts[i] + " - " + buyableItems[i].GetComponent<DA_Trigger_PickupItem>().itemName;
            gameObject.transform.GetChild(2).transform.GetChild(1).gameObject.GetComponent<DA_Menu_Control>().hoveredText[i] = "    G" + buyableCosts[i] + " - " + buyableItems[i].GetComponent<DA_Trigger_PickupItem>().itemName;
        }
    }


    public void Sell()
    {
        textboxManager.TextboxNonarray(sellDialogue, sellName, sellPortrait);
        gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(true);
    }


    public void Talk()
    {
        textboxManager.TextboxNonarray(talkDialogue, talkName, talkPortrait);
        gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(false);
        for (int i = 0; i < talkOptions.Length; i++)
        {
            gameObject.transform.GetChild(2).transform.GetChild(3).gameObject.GetComponent<DA_Menu_Control>().baseText[i] = talkOptions[i];
            gameObject.transform.GetChild(2).transform.GetChild(3).gameObject.GetComponent<DA_Menu_Control>().hoveredText[i] = ">" + talkOptions[i];
        }
        gameObject.transform.GetChild(2).transform.GetChild(3).gameObject.SetActive(true);
    }


    public void BuyItem()
    {
        int buyableCurrentSelection = gameObject.transform.GetChild(2).transform.GetChild(1).gameObject.GetComponent<DA_Menu_Control>().currentSelection;
        DA_Trigger_PickupItem itemToBuy = buyableItems[buyableCurrentSelection].GetComponent<DA_Trigger_PickupItem>();
        if (buyableCosts[buyableCurrentSelection] <= saveManager.activeSave2.playerGold)
        {
            inventoryManager.ItemAdd(itemToBuy.itemName, itemToBuy.itemCategory, itemToBuy.itemIcon, itemToBuy.itemDescription, itemToBuy.itemDiscardable);
            saveManager.activeSave2.playerGold -= buyableCosts[buyableCurrentSelection];
        }
        else
        {
            textboxManager.TextboxNonarray("You don't have enough money for this item.", talkName, talkPortrait);
        }
    }


    public void Leave()
    {
        textboxManager.ForceCloseTextbox();
        CloseShopbox();
        gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).transform.GetChild(3).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<DA_Menu_Control>().ResetCurrentSelection();
        gameObject.transform.GetChild(2).transform.GetChild(1).gameObject.GetComponent<DA_Menu_Control>().ResetCurrentSelection();
        gameObject.transform.GetChild(2).transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>().ResetCurrentSelection();
        gameObject.transform.GetChild(2).transform.GetChild(3).gameObject.GetComponent<DA_Menu_Control>().ResetCurrentSelection();
    }


    public void Return()
    {
        textboxManager.ForceCloseTextbox();

        textboxManager.dialogueText = textboxDialogueBox;
        gameObject.transform.GetChild(2).gameObject.SetActive(false);
        shopboxActive = false;

        OpenShopbox();
        gameObject.transform.GetChild(2).transform.GetChild(0).gameObject.SetActive(true);
        gameObject.transform.GetChild(2).transform.GetChild(1).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).transform.GetChild(2).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).transform.GetChild(3).gameObject.SetActive(false);
        gameObject.transform.GetChild(2).transform.GetChild(1).gameObject.GetComponent<DA_Menu_Control>().ResetCurrentSelection();
        gameObject.transform.GetChild(2).transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>().ResetCurrentSelection();
        gameObject.transform.GetChild(2).transform.GetChild(3).gameObject.GetComponent<DA_Menu_Control>().ResetCurrentSelection();
    }


    public void PassTriggerData()
    {
        introDialogue = targetTrigger.introDialogue;
        introName = targetTrigger.introName;
        introPortrait = targetTrigger.introPortrait;

        buyDialogue = targetTrigger.buyDialogue;
        buyName = targetTrigger.buyName;
        buyPortrait = targetTrigger.buyPortrait;

        buyableItems = targetTrigger.buyableItems;
        buyableCosts = targetTrigger.buyableCosts;

        sellDialogue = targetTrigger.sellDialogue;
        sellName = targetTrigger.sellName;
        sellPortrait = targetTrigger.sellPortrait;

        talkDialogue = targetTrigger.talkDialogue;
        talkName = targetTrigger.talkName;
        talkPortrait = targetTrigger.talkPortrait;

        talkOptions = targetTrigger.talkOptions;

        leaveDialogue = targetTrigger.leaveDialogue;
        leaveName = targetTrigger.leaveName;
        leavePortrait = targetTrigger.leavePortrait;
    }
}
