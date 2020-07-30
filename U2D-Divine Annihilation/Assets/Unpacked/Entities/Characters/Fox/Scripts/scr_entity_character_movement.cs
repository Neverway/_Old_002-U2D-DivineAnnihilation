using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_entity_character_movement : MonoBehaviour
{
    // Setup configurable variables
    public bool canMove = true;
    public float walkSpeed = 5f;
    public float sprintSpeed = 5f;
    public float movementSpeed;
    public float storedSpeed;
    public Rigidbody2D Rigidbody;
    public Animator characterAnimator;

    private scr_system_hud_textbox_manager DialogueManager;

    // Input variables
    Vector2 movement;

    void Start()
    {
        movementSpeed = walkSpeed;
        DialogueManager = FindObjectOfType<scr_system_hud_textbox_manager>();
    }

    // Update is called once per frame (Speed is called by FPS)
    void Update()
    {
        if(DialogueManager.dialogueBoxActive)
        {
            canMove = false;
            movementSpeed = 0;
        }

        if(!DialogueManager.dialogueBoxActive)
        {
            canMove = true;
        }

        // Input
        if(canMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        

            characterAnimator.SetFloat("Horizontal", movement.x);
            characterAnimator.SetFloat("Vertical", movement.y);
            characterAnimator.SetFloat("Speed", movement.sqrMagnitude);

            // Sprinting
            if (Input.GetKeyDown("x"))
            {
                movementSpeed = sprintSpeed;
                storedSpeed = sprintSpeed;
            }
        
            else if (Input.GetKeyUp("x"))
            {
                movementSpeed = walkSpeed;
                storedSpeed = sprintSpeed;
            }
        }
    }

    // Update is not tied to FPS but updates at a constant rate
    void FixedUpdate()
    {
        // Movement
        Rigidbody.MovePosition(Rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
