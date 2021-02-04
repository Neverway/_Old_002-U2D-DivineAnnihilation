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

    private Hud_Textbox_Manager DialogueManager;
    private Hud_Inventory InventoryManager;
    Vector2 movement;

    void Start()
    {
        DialogueManager = FindObjectOfType<Hud_Textbox_Manager>();   // Find the dialogue manager script
        movementSpeed = walkSpeed;                                      // Set the starting movement speed
    }


    void Update()
    {
        // Allow character input if the canMove variable is true
        if (canMove)
        {
            // Movement input
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        
            // Character animator
            characterAnimator.SetFloat("Horizontal", movement.x);
            characterAnimator.SetFloat("Vertical", movement.y);
            characterAnimator.SetFloat("Speed", movement.sqrMagnitude);

            // Sprinting
            if (Input.GetButtonDown("Action"))
            {
                movementSpeed = sprintSpeed;
            }

            // Not sprinting
            if (Input.GetButtonUp("Action"))
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
