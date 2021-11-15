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

public class OTU_Battle_WaveManager : MonoBehaviour
{
    // Public variables
    [Header ("Read only!")]
    public int currentWave = 0;
    public bool acceptingMenuInput;


    // Private variables

    // Reference variables
    private OTU_System_SaveManager saveManager;
    private OTU_Battle_DataHandler dataHandler;
    private OTU_System_TextboxManager textboxManager;


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
        if (acceptingMenuInput)
        {
            textboxManager.TextboxSingleText("Attack > Move > Target > Execute minigame at end of party turn");
            ExecuteEnemyAttack();
            acceptingMenuInput = false;
        }
    }

    public void Action()
    {
        if (acceptingMenuInput)
        {
            textboxManager.TextboxSingleText("Action > [Check > Target > Skip] or [Spell > Move > Target > Execute minigame at end of party turn] or [Defend > Self/Skip]");
            acceptingMenuInput = false;
        }
    }

    public void Item()
    {
        if (acceptingMenuInput)
        {
            textboxManager.TextboxSingleText("Item > Target > Skip");
            acceptingMenuInput = false;
        }
    }

    public void Flee()
    {
        if (acceptingMenuInput)
        {
            textboxManager.TextboxSingleText("Skip party turn");
            acceptingMenuInput = false;
        }
    }

    public void ExecuteEnemyAttack()
    {
        Instantiate(dataHandler.enemyAttackWave[currentWave], new Vector2(0,0), Quaternion.identity);
    }
}
