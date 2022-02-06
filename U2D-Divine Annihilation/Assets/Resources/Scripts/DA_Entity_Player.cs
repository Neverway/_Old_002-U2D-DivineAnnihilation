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
using UnityEngine.SceneManagement;
using Cinemachine;
using Pathfinding;

public class DA_Entity_Player : MonoBehaviour
{
    // Public variables
    public float walkSpeed = 5f;
    public float sprintSpeed = 7f;
    public Sprite shelfSprite;
    public GameObject HUD;
    public bool canMove = true;
    public bool alternateCanMove = true;
    private Vector2 movement;

    // Private variables

    // Reference variables


    void Start()
    {
        if (HUD != null )
        {
            if (HUD.transform.GetChild(2).GetComponent<Text>() != null)
            {
                HUD.transform.GetChild(2).GetComponent<Text>().text = entityName;
            }
            if (HUD.transform.GetChild(3).GetComponent<Image>() != null)
            {
                HUD.transform.GetChild(3).GetComponent<Image>().sprite = shelfSprite;
            }
        }
        if (inventoryManager != null)
        {
            inventoryManager.UpdatePlayerDescription(spriteRenderer.sprite, entityName);
        }
    }


    void Update()
    {
            PlayerEntity();
    }
    
    
    void PlayerEntity()
    {
        if (canMove)
        {
            if (saveLoader.hasLoadedCurrentLevel)
            {
                saveManager.activeSave2.playerSavePosition = gameObject.transform.position;
            }
            // Horizontal control
            if (Input.GetKey(inputManager.controls["Right"]) && movement.x < 1)
            {
                movement.x += 1;
            }
            if (Input.GetKey(inputManager.controls["Left"]) && movement.x > -1)
            {
                movement.x -= 1;
            }
            if (!Input.GetKey(inputManager.controls["Left"]) && !Input.GetKey(inputManager.controls["Right"]))
            {
                movement.x = 0;
            }

            // Vertical control
            if (Input.GetKey(inputManager.controls["Up"]) && movement.y < 1)
            {
                movement.y += 1;
            }
            if (Input.GetKey(inputManager.controls["Down"]) && movement.y > -1)
            {
                movement.y -= 1;
            }
            if (!Input.GetKey(inputManager.controls["Down"]) && !Input.GetKey(inputManager.controls["Up"]))
            {
                movement.y = 0;
            }

            if (Input.GetKey(inputManager.controls["Up"]) || Input.GetKey(inputManager.controls["Down"]) || Input.GetKey(inputManager.controls["Left"]) || Input.GetKey(inputManager.controls["Right"]))
            {
                animator.SetFloat("LastX", movement.x);
                animator.SetFloat("LastY", movement.y);

            }
            if (Input.GetKey(inputManager.controls["Special 3"]) && saveManager.activeSave2.playerHealth >=0)
            {
                saveManager.activeSave2.playerHealth -= 1;
            }
            if (Input.GetKey(inputManager.controls["Special 4"]) && saveManager.activeSave2.playerHealth <= maxHealth)
            {
                saveManager.activeSave2.playerHealth += 1;
            }

        }
        else
        {
            movement.x = 0;
            movement.y = 0;
        }

        // Entity animator
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);

        // Sprinting
        if (Input.GetKey(inputManager.controls["Action"]))
        {
            currentSpeed = sprintSpeed;
            //animator.speed = 1.5f;
        }
        else
        {
            currentSpeed = walkSpeed;
            //animator.speed = 1;
        }


        // can/can't move check
        if (!menuManager.menuActive && alternateCanMove)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }

        if (saveManager.activeSave2.playerHealth <= 0 && firstPass)
        {
            menuManager.alternateMenuActive = true;
            animator.Play("Knockout");
            transitionManager.FadeIn("knockout");
            spriteRenderer.sortingLayerName = "UI";
            spriteRenderer.sortingOrder = 5;
            firstPass = false;
        }
    }
}
