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

public class OTU_Battle_DataHandler : MonoBehaviour
{
    // Public variables
    [Header ("Scene (I should make these private variables :/)")]
    public GameObject[] playerPartyEntities;
    public GameObject[] playerPartyUIShelves;
    public GameObject[] enemyPartyEntities;

    [Header ("System")]
    public GameObject[] enemyAttackWave;
    public Sprite[] enemySpriteID;

    // Reference variables
    private OTU_System_SaveManager saveManager;
    private OTU_System_TextboxManager textboxManager;


    void Start()
    {
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
        textboxManager = FindObjectOfType<OTU_System_TextboxManager>();
        setupPlayerParty();
        setupEnemyParty();
    }


    void Update()
    {
        
    }

    void setupPlayerParty()
    {
        // Entities and shelves
        for (int i = 0; i < saveManager.activeSave2.partyMembers.Length; i++)
        {
            if (saveManager.activeSave2.partyMembers[i] == "NULL")
            {
                playerPartyEntities[i].SetActive(false);
                playerPartyUIShelves[i].SetActive(false);
            }
            
            else
            {
                Debug.Log("The member " + saveManager.activeSave2.partyMembers[i] + ", in party slot " + i + " was not found! Please add it to OTU_Battle_Setup, or choose a valid entity name.");
                playerPartyEntities[i].SetActive(false);
                playerPartyUIShelves[i].SetActive(false);
            }
        }
    }

    void setupEnemyParty()
    {
        // Pull encounter player pref data
        enemyPartyEntities[0].name = PlayerPrefs.GetString("EP0");
        enemyPartyEntities[1].name = PlayerPrefs.GetString("EP1");
        enemyPartyEntities[2].name = PlayerPrefs.GetString("EP2");
        enemyPartyEntities[3].name = PlayerPrefs.GetString("EP3");

        // Entities and shelves
        for (int i = 0; i < enemyPartyEntities.Length; i++)
        {
            if (enemyPartyEntities[i].name == "NULL")
            {
                enemyPartyEntities[i].SetActive(false);
            }
            else if (enemyPartyEntities[i].name == "Purple Cat")
            {
                enemyPartyEntities[i].SetActive(true);
                enemyPartyEntities[i].GetComponent<SpriteRenderer>().sprite = enemySpriteID[1];
                
                textboxManager.TextboxAutoSingleText("*An enemy draws near!");
            }
            else if (enemyPartyEntities[i].name == "Dummy")
            {
                enemyPartyEntities[i].SetActive(true);
                enemyPartyEntities[i].GetComponent<SpriteRenderer>().sprite = enemySpriteID[2];
                
                textboxManager.TextboxAutoSingleText("*An enemy draws near!");
            }
            else
            {
                Debug.Log("The enemy " + enemyPartyEntities[i].name + ", in party slot " + i + " was not found! Please add it to OTU_Battle_Setup, or choose a valid entity name.");
                enemyPartyEntities[i].name = "FallbackEnemy";
                enemyPartyEntities[i].SetActive(true);
                enemyPartyEntities[i].GetComponent<SpriteRenderer>().sprite = enemySpriteID[0];
                
                textboxManager.TextboxAutoSingleText("*An enemy draws near!");
            }
        }
    }
}
