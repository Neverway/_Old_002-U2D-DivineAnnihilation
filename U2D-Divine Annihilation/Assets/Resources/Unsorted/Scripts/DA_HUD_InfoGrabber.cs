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
    public GameObject UITarget;
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
            RectTransform rectTransform = UITarget.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(saveManager.activeSave2.playerHealth, rectTransform.sizeDelta.y);
        }
        if (saveManagerInfoField == "playerLevel")
        {
            UITarget.GetComponent<Text>().text = saveManager.activeSave2.playerLevel.ToString();
        }
        if (saveManagerInfoField == "playerGold")
        {
            UITarget.GetComponent<Text>().text = saveManager.activeSave2.playerGold.ToString();
        }
    }
}
