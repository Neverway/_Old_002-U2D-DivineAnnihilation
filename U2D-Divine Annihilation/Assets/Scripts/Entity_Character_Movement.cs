//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
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

    private Hud_Textbox_Manager DialogueManager;
    private Hud_Inventory InventoryManager;
    private SaveManager saveManager;
    Vector2 movement;

    void Start()
    {
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
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        
            // Character animator
            characterAnimator.SetFloat("MoveX", movement.x);
            characterAnimator.SetFloat("MoveY", movement.y);
            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                characterAnimator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
                characterAnimator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));

            }
            //characterAnimator.SetFloat("Speed", movement.sqrMagnitude);

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
