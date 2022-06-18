﻿//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Give control to the players movements
// Applied to: Player entity in an overworld scene
//
//=============================================================================

using UnityEngine;

public class Entity_Character_Movement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 5f;
    public float movementSpeed;
    public float storedSpeed;
    public bool canMove = true;
    public bool sprintDust = true;
    public Rigidbody2D Rigidbody;
    public Animator characterAnimator;
    public ParticleSystem dustParticleSystem;

    private System_InputManager inputManager;
    private Hud_Textbox_Manager DialogueManager;
    private Hud_Inventory InventoryManager;
    private SaveManager saveManager;
    Vector2 movement;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
        DialogueManager = FindObjectOfType<Hud_Textbox_Manager>(); // Find the dialogue manager script
        saveManager = FindObjectOfType<SaveManager>();
        movementSpeed = walkSpeed;                               // Set the starting movement speed
        transform.position = new Vector2(saveManager.activeSave.playerSavePosition.x, saveManager.activeSave.playerSavePosition.y);
    }


    void Update()
    {
        // Update save position
        saveManager.activeSave.playerSavePosition.x = transform.position.x;
        saveManager.activeSave.playerSavePosition.y = transform.position.y;

        // Allow character input if the canMove variable is true
        if (canMove)
        {
            // Movement input
            //movement.x = Input.GetAxisRaw("Horizontal");
            //movement.y = Input.GetAxisRaw("Vertical");

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

            // Character animator
            characterAnimator.SetFloat("MoveX", movement.x);
            characterAnimator.SetFloat("MoveY", movement.y);
            if (Input.GetKey(inputManager.controls["Up"]) || Input.GetKey(inputManager.controls["Down"]) || Input.GetKey(inputManager.controls["Left"]) || Input.GetKey(inputManager.controls["Right"]))
            {
                characterAnimator.SetFloat("LastMoveX", movement.x);
                characterAnimator.SetFloat("LastMoveY", movement.y);

            }
            //characterAnimator.SetFloat("Speed", movement.sqrMagnitude);

            // Sprinting
            if (Input.GetKeyDown(inputManager.controls["Action"]))
            {
                movementSpeed = sprintSpeed;
            }

            // Not sprinting
            if (Input.GetKeyUp(inputManager.controls["Action"]))
            {
                movementSpeed = walkSpeed;
            }
        }

        // Stop character if the canMove variable is false
        if (!canMove)
        {
            movement.x = 0;
            movement.y = 0;
        }

        // Emmit dust when going fast
        if (movementSpeed >= 7 && sprintDust)
        {
            if (!dustParticleSystem.isPlaying)
            {
                if (movement.x > 0 || movement.x < 0 || movement.y > 0 || movement.y < 0)
                { 
                    dustParticleSystem.Play();
                }
            }
        }

        else
        {
            dustParticleSystem.Stop();
        }
    }


    void FixedUpdate()
    {
            Rigidbody.MovePosition(Rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);    // Update the movement for the character
    }
}
