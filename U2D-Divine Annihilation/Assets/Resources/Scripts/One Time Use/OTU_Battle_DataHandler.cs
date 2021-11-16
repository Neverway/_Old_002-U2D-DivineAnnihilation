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
    public GameObject battleUIRoot;
    public GameObject activeBattleZone;

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
                Debug.Log("The enemy " + enemyPartyEntities[i].name + ", in party slot " + i + " was not found! Please add it to OTU_Battle_DataHandler, or choose a valid entity name.");
                enemyPartyEntities[i].name = "FallbackEnemy";
                enemyPartyEntities[i].SetActive(true);
                enemyPartyEntities[i].GetComponent<SpriteRenderer>().sprite = enemySpriteID[0];
                
                textboxManager.TextboxAutoSingleText("*An enemy draws near!");
            }
        }
    }


    public void getCurrentCharacterAttacks()
    {
        // Get currently equipped weapon
        if (saveManager.activeSave2.equippedW != 0)
        {
            string currentlyEquippedWeapon = saveManager.activeSave2.equipment[saveManager.activeSave2.equippedW-1];
            print(currentlyEquippedWeapon);
            print(saveManager.activeSave2.equippedW-1);

            // Update attack select menu
            if (currentlyEquippedWeapon == "Rsty. Sword")
            {
                cleanMenuArray(battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>());
                resizeMenuArray(battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>(), 2);

                // Attack names
                battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>().baseText[0] = "S-Slash";
                battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>().baseText[1] = "S-Strike";
            }


            if (currentlyEquippedWeapon == "---")
            {
                Debug.LogWarning("No weapon is currently equipped! (But the system reports that the equppied item is not zero?)");
                cleanMenuArray(battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>());
                resizeMenuArray(battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>(), 1);

                // Attack names
                battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>().baseText[0] = "M-Melee";
            }

            /*
            else
            {
                print(currentlyEquippedWeapon);
                print(saveManager.activeSave2.equippedW-1);
                Debug.LogWarning("The currently equipped weapon does not have a moveset! Please add it to OTU_Battle_DataHandler, or choose a valid weapon name.");
                cleanMenuArray(battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>());
                resizeMenuArray(battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>(), 1);

                // Attack names
                battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>().baseText[0] = "M-Melee";
            }*/
            setHoveredText(battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>());
        }

        else
        {
            print(saveManager.activeSave2.equippedW-1);
            Debug.Log("No weapon is currently equipped!");
            cleanMenuArray(battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>());
            resizeMenuArray(battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>(), 1);

            // Attack names
            battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>().baseText[0] = "M-Melee";
            setHoveredText(battleUIRoot.transform.GetChild(2).gameObject.GetComponent<DA_Menu_Control>());
        }
    }


    void cleanMenuArray(DA_Menu_Control menu)
    {
        System.Array.Resize(ref menu.textTargetObjects, 4);
        System.Array.Resize(ref menu.baseText, 4);
        System.Array.Resize(ref menu.hoveredText, 4);
        for (int i = 0; i < 4; i++)
        {
            menu.textTargetObjects[i] = menu.gameObject.transform.GetChild(i).gameObject.GetComponent<Text>();
            menu.baseText[i] = " ";
        }
        setHoveredText(menu);
    }

    void resizeMenuArray(DA_Menu_Control menu, int size)
    {
        System.Array.Resize(ref menu.textTargetObjects, size);
        System.Array.Resize(ref menu.baseText, size);
        System.Array.Resize(ref menu.hoveredText, size);
    }

    void setHoveredText(DA_Menu_Control menu)
    {
        for (int i = 0; i < menu.baseText.Length; i++)
        {
            menu.hoveredText[i] = ">" + menu.baseText[i];
        }
    }
}
