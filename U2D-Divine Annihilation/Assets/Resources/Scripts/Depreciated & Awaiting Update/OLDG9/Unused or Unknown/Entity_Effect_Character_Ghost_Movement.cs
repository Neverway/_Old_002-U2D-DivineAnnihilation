//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Alternatative for ghost and movement, create a ghosting effect when the player uses a special ability
// Applied to: The player object in an overworld scene
//
//=============================================================================

using UnityEngine;

public class Entity_Effect_Character_Ghost_Movement : MonoBehaviour
{
    public bool canMove = true;
    public float walkSpeed = 5f;
    public float sprintSpeed = 5f;
    public float movementSpeed;
    public float storedSpeed;
    public Rigidbody2D Rigidbody;
    public Animator characterAnimator;
    public GameObject ghost;

    private System_InputManager inputManager;
    private Hud_Textbox_Manager DialogueManager;
    private Hud_Inventory InventoryManager;
    Vector2 movement;

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
        DialogueManager = FindObjectOfType<Hud_Textbox_Manager>();   // Find the dialogue manager script
        movementSpeed = walkSpeed;                                      // Set the starting movement speed
    }


    void Update()
    {
        // Allow character input if the canMove variable is true
        if (canMove)
        {
            // Movement input
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
            characterAnimator.SetFloat("Horizontal", movement.x);
            characterAnimator.SetFloat("Vertical", movement.y);
            characterAnimator.SetFloat("Speed", movement.sqrMagnitude);

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

    }


    void FixedUpdate()
    {
            Rigidbody.MovePosition(Rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);    // Update the movement for the character
    }
}
