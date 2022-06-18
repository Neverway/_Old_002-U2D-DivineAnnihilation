//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// SID: 
// Purpose: Setup the parties and UI by pulling information form the player
//  prefs.
// Applied to: The battle manager in a battle scene
// Editor script:
// Notes: (The player prefs are set by the enemy collision in the overworld &
//  the players save data.)
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OTU_Battle_Manager : MonoBehaviour
{
    // Public variables
    [Header ("Read only!")]

    public int currentWave = 0;
    [Range (0,3)]
    public int currentCharacterTurn = 1;
    public int currentExecutionTurn = 1;
    [Range (1,4)]
    public int partySize = 1;

    public string[] partyAction;
    public string[] partySubaction;
    public int[] partyTarget;

    public bool acceptingMenuInput;

    // Private variables

    // Reference variables
    private OTU_System_SaveManager saveManager;
    private OTU_Battle_DataHandler dataHandler;
    private OTU_System_TextboxManager textboxManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}


    void Start()
    {
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
        dataHandler = FindObjectOfType<OTU_Battle_DataHandler>();
        textboxManager = FindObjectOfType<OTU_System_TextboxManager>();
    }


    void Update()
    {
        if (!acceptingMenuInput && !textboxManager.textboxActive)
        {
            acceptingMenuInput = true;
        }
    }


    public void Attack()
    {
        // Display character attack moves
        dataHandler.getCurrentCharacterAttacks();

        // Store action variable
        partyAction[currentCharacterTurn] = "Attack";
    }


    public void Subattack()
    {
        // Store subaction variable
        partySubaction[currentCharacterTurn] = dataHandler.battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>().baseText[dataHandler.attackSelection.GetComponent<DA_Menu_Control>().currentSelection];

        dataHandler.attackSelection.GetComponent<DA_Menu_Control>().DisableMenu(true);
        dataHandler.targetSelection.SetActive(true);
        dataHandler.targetingRetical.SetActive(true);
    }

    public void Target()
    {
        // Store subaction variable
        partyTarget[currentCharacterTurn] = dataHandler.attackSelection.GetComponent<DA_Menu_Control>().currentSelection;

        dataHandler.attackSelection.GetComponent<DA_Menu_Control>().DisableMenu(false);
        dataHandler.attackSelection.SetActive(false);
        dataHandler.actionSelection.GetComponent<DA_Menu_Control>().DisableMenu(false);
        dataHandler.actionSelection.SetActive(false);
        dataHandler.targetSelection.SetActive(false);
        dataHandler.targetingRetical.SetActive(false);
        CheckEndOfTurn();
    }


    public void Action()
    {
        partyAction[currentCharacterTurn] = "Action";
    }


    public void Flee()
    {
        // Store action variable
        for (int i = 0; i < partyAction.Length; i++)
        {
            partyAction[i] = "Flee";
            partySubaction[i] = "";
            partyTarget[i] = 0;
            CheckEndOfTurn();
        }
    }


    public void CheckEndOfTurn()
    {
        if (partySize == currentCharacterTurn)
        {
            textboxManager.TextboxSingleText("*STRIFE!");
        }
    }





    public void Item()
    {
        if (acceptingMenuInput)
        {
            textboxManager.TextboxSingleText("This feature has not been implemented yet, sorry! -Liz");
            acceptingMenuInput = false;
        }
    }

    public void ExecuteEnemyAttack()
    {
        //dataHandler.activeBattleZone.GetComponent<Animator>().Play("FadeIn");
        //Instantiate(dataHandler.enemyAttackWave[currentWave], new Vector2(0,0), Quaternion.identity);
    }
}
