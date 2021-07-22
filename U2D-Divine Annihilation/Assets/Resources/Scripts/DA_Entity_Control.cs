//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Give functionality to game entities
// Applied to: The entity prefab
// Editor script: DASDK_Tool_EntityEditor
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DA_Entity_Control : MonoBehaviour
{
    // Base variables
    public string entityName;
    public float maxHealth = 100f;
    public RuntimeAnimatorController entityAnimator;

    public bool useSpritesOverAnimator;
    public int choiceValue;

    // Base idle variables
    public Sprite idleUp;
    public Sprite idleDown;
    public Sprite idleLeft;
    public Sprite idleRight;

    // Base walk variables
    public float walkSpeed = 5f;

    // Base sprint variables
    public float sprintSpeed = 7f;

    // Player variables
    public Sprite shelfSprite;
    public GameObject inventoryType;
    private Vector2 movement;

    // Character variables
    public bool isFollower;

    // Enemy variables
    public float senseRange = 20f;
    public float attack = 15; 
    public int goldDrop = 5;
    public float expDrop = 5;

    // Private variables
    public string entityType;
    public float currentSpeed;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D Rigidbody;
    private OTU_System_InputManager inputManager;


    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        inputManager = FindObjectOfType<OTU_System_InputManager>(); 
        currentSpeed = walkSpeed;

        // Set entity type
        if (choiceValue == 0)
        {
            if (isFollower)
            {
                entityType = "follower";
            }
            else
            {
                entityType = "character";
            }
        }

        if (choiceValue == 1)
        {
            entityType = "enemy";
        }

        if (choiceValue == 2)
        {
            entityType = "player";
        }
    }

    void Update()
    {
        if (entityType == "player")
        {
            PlayerEntity();
        }
        else if (entityType == "character")
        {
            CharacterEntity();
        }
        else if (entityType == "follower")
        {
            FollowerEntity();
        }
        else if (entityType == "enemy")
        {
            EnemyEntity();
        }
    }

    void FixedUpdate()
    {
        Rigidbody.MovePosition(Rigidbody.position + movement * currentSpeed * Time.fixedDeltaTime);    // Update the movement for the character
    }

    void PlayerEntity()
    {
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

        // Entity animator
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
    }

    void CharacterEntity()
    {

    }

    void FollowerEntity()
    {

    }

    void EnemyEntity()
    {

    }
}
