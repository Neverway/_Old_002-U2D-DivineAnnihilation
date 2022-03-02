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

public class DA_HUD_InteractMenu : MonoBehaviour
{
    // Public variables
    public Text interactText;
    public Color active;
    public Color inactive;
    public Image[] interactIcons;
    public Sprite[] interactSprite;
    public int currentAction;
    public int storedActionPosition;

    // Private variables

    // Reference variables
    private OTU_System_InputManager inputManager;


    void Start()
    {
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        interactText.text = inputManager.controls["Interact"].ToString();
        UpdateIcons();
    }


    void Update()
    {
            if(Input.GetKeyDown(inputManager.controls["L"]))
            {
                // if((Time.time - LlastTapTime) > tapSpeed)
                // { 
                    if (currentAction == 2 || currentAction == 3)
                    {
                        currentAction -= 1;
                        UpdateIcons();
                    }
                    else if (currentAction == 1)
                    {
                        currentAction = 3;
                        UpdateIcons();
                    }
                // }
                // LlastTapTime = Time.time; 
            }
            
            if(Input.GetKeyDown(inputManager.controls["R"]))
            {
                // if((Time.time - RlastTapTime) > tapSpeed)
                // { 
                    if (currentAction == 1 || currentAction == 2)
                    {
                        currentAction += 1;
                        UpdateIcons();
                    }
                    else if (currentAction == 3)
                    {
                        currentAction = 1;
                        UpdateIcons();
                    }
                // }
                // RlastTapTime = Time.time; 
            }
    }

    private void UpdateIcons()
    {
        if (currentAction != 0)
        {
            for (int i = 0; i < interactIcons.Length; i++)
            {
                interactIcons[i].color = inactive;
            }
            interactIcons[currentAction - 1].color = active;
            // if (currentAction == 1)
            // {
            //     interactIcons[0].overrideSprite = interactSprite[0];
            //     interactIcons[1].overrideSprite = interactSprite[1];
            //     interactIcons[2].overrideSprite = interactSprite[2];
            // }
            // else if (currentAction == 2)
            // {
            //     interactIcons[0].overrideSprite = interactSprite[1];
            //     interactIcons[1].overrideSprite = interactSprite[2];
            //     interactIcons[2].overrideSprite = interactSprite[0];
            // }
            // else if (currentAction == 3)
            // {
            //     interactIcons[0].overrideSprite = interactSprite[2];
            //     interactIcons[1].overrideSprite = interactSprite[0];
            //     interactIcons[2].overrideSprite = interactSprite[1];
            // }

        }
        else
        {
            for (int i = 0; i < interactIcons.Length; i++)
            {
                interactIcons[i].color = inactive;
            }
        }
    }
}
