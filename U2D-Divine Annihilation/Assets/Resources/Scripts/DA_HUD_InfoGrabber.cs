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

public class DA_HUD_InfoGrabber : MonoBehaviour
{
    // Public variables
    public Text textTarget;
    public string saveManagerInfoField;

    // Private variables

    // Reference variables
    private OTU_System_SaveManager saveManager;


    void Start()
    {
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
    }


    void Update()
    {
        if (saveManagerInfoField == "playerHealth")
        {
            textTarget.text = saveManager.activeSave2.playerHealth.ToString();
        }
        if (saveManagerInfoField == "playerLevel")
        {
            textTarget.text = saveManager.activeSave2.playerLevel.ToString();
        }
        if (saveManagerInfoField == "playerGold")
        {
            textTarget.text = saveManager.activeSave2.playerGold.ToString();
        }
    }
}
