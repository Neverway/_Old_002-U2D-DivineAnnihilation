//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// SID: 
// Purpose: Show the enemies the current character can target
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OTU_Battle_EnemyTarget : MonoBehaviour
{
    
    // Public variables
    public GameObject[] enemyTargets;
    public GameObject[] partyTargets;
    public GameObject targetingRetical;

    // Private variables
    private string target = "Enemy";

    // Reference variables
    private OTU_System_SaveManager saveManager;
    private OTU_Battle_DataHandler dataHandler;
    private OTU_Battle_Manager battleManager;


    void Start()
    {
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
        dataHandler = FindObjectOfType<OTU_Battle_DataHandler>();
        battleManager = FindObjectOfType<OTU_Battle_Manager>();
    }


    void Update()
    {
        // Update menu
        dataHandler.cleanMenuArray(gameObject.GetComponent<DA_Menu_Control>());

        if (target == "Enemy")
        {
            dataHandler.cleanMenuArray(gameObject.GetComponent<DA_Menu_Control>());
            dataHandler.resizeMenuArray(gameObject.GetComponent<DA_Menu_Control>(), dataHandler.enemyPartyEntities.Length);
            for (int i = 0; i < dataHandler.enemyPartyEntities.Length; i++)
            {
                gameObject.GetComponent<DA_Menu_Control>().textTargetObjects[i].text = enemyTargets[i].name;
                gameObject.GetComponent<DA_Menu_Control>().baseText[i] = enemyTargets[i].name;
                gameObject.GetComponent<DA_Menu_Control>().hoveredText[i] = ">" + enemyTargets[i].name;
            }

            // Draw targeting retical
            targetingRetical.transform.position = enemyTargets[gameObject.GetComponent<DA_Menu_Control>().currentSelection].transform.position;
        }

        if (target == "Party")
        {
            dataHandler.cleanMenuArray(gameObject.GetComponent<DA_Menu_Control>());
            dataHandler.resizeMenuArray(gameObject.GetComponent<DA_Menu_Control>(), battleManager.partySize+1);
            for (int i = 0; i < battleManager.partySize+1; i++)
            {
                gameObject.GetComponent<DA_Menu_Control>().textTargetObjects[i].text = partyTargets[i].name;
                gameObject.GetComponent<DA_Menu_Control>().baseText[i] = partyTargets[i].name;
                gameObject.GetComponent<DA_Menu_Control>().hoveredText[i] = ">" + partyTargets[i].name;
            }

            // Draw targeting retical
            targetingRetical.transform.position = partyTargets[gameObject.GetComponent<DA_Menu_Control>().currentSelection].transform.position;
        }
    }

    public void SetTarget(string targets)
    {
        target = targets;
    }
}
