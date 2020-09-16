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
public class scr_character_movement : MonoBehaviour
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
    private scr_hud_textboxManager DialogueManager;
    private scr_menu_inventoryManager InventoryManager;
    Vector2 movement;


    // Start is called before the first frame update
    void Start()
    {
        DialogueManager = FindObjectOfType<scr_hud_textboxManager>();   // Find the dialogue manager script
        movementSpeed = walkSpeed;                                      // Set the starting movement speed
    }


    // Update is called once per frame
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
