// Included Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Textbox Manager class
 * ---------------------
 * This script is applied to the textbox manager object in a scene that uses textboxes
 * It get the public variables filled in from things like the interaction triggers
 * All of this is to display the textbox objects on screen properly, such as the box itself, the text & name in the box, as well as the textbox portrait
*/
public class CharacterMovement : MonoBehaviour
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

    // Other class references
    private HudTextboxManager DialogueManager;
    private HudInventory InventoryManager;
    public GameObject configTarget;
    private SaveManager saveManager;

    Vector2 movement;


    // Start is called before the first frame update
    void Start()
    {
        DialogueManager = FindObjectOfType<HudTextboxManager>();   // Find the dialogue manager script
        saveManager = configTarget.GetComponent<SaveManager>();
        movementSpeed = walkSpeed;                                      // Set the starting movement speed
        transform.position = new Vector2(saveManager.activeSave.playerSavePosition.x, saveManager.activeSave.playerSavePosition.y);
    }


    // Update is called once per frame
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
            if (Input.GetKeyDown("x"))
            {
                movementSpeed = sprintSpeed;
            }

            // Not sprinting
            if (Input.GetKeyUp("x"))
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


    // Update is not tied to FPS but updates at a constant rate
    void FixedUpdate()
    {
            Rigidbody.MovePosition(Rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);    // Update the movement for the character
    }
}
