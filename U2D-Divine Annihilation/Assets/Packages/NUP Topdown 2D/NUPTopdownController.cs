//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NUPTopdownController : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    [Header("Options")]
    public bool useBuiltInAnimationTags = true; // Should the 
    public bool oldPhysics = false; // New Physics requires the movement to accellerate to it's top speeds as well as adding drag to the player rigidbody
    public bool canMove = true;     // Enable the ability to move (Can be used externaly to stop the player during events or to exclude this feature from being used)
    public bool canSprint = true;   // Increases player movement speed from walkspeed to sprint speed

    [Header("Movement")]
    public float movementSpeed = 6f;        // [READ-ONLY] The base movement speed before drag and other physics calculation (sets to walk or sprint speed on start)
    public float walkSpeed = 5f;            // The speed of the player when walking
    public float movementMultiplier = 6f;   // Used to counteract the physics drag
    public float airMultiplier = 0.4f;      // Used to counteract the physics drag

    [Header("Sprinting")]
    public bool sprinting;              // [READ-ONLY] Is the player currently sprinting?
    public float sprintSpeed = 9f;      // The speed of the player when sprinting
    public float acceleration = 10f;    // How quickly the player changes speed

    [Header("Drag")]
    public float groundDrag = 6f;   // How much friction is applied when moving normally
    public float airDrag = 2f;      // How much air resistence is applied when not grounded


    //=-----------------=
    // Private variables
    //=-----------------=
    [Header("Private Variables")]
    public float horizontalMovement;    // [READ-ONLY]
    public float verticalMovement;      // [READ-ONLY]
    public float lastX;                 // [READ-ONLY] What was the last movement key that was pressed (used to get the players facing direction even if they are not moving)
    public float lastY;                 // [READ-ONLY] What was the last movement key that was pressed (used to get the players facing direction even if they are not moving)
    public Vector3 lastGroundedPosition;

    public bool isGrounded = true;      // [READ-ONLY] Since this is a 2D system, the ground check is done by other scripts (for example, dodgerolling or a ledgeTrigger)

    public bool couldMove = true;       // Used to store if the player controller had the ability to move before modifying it
    public bool couldSprint = true;     // Used to store if this player controller had the ability to sprint before modifying it


    //=-----------------=
    // Reference variables
    //=-----------------=
    private NUPInput input;
    public Rigidbody2D playerRigidbody;
    private BoxCollider2D playerCollider;
    private Animator animator;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        input = FindObjectOfType<NUPInput>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
        if (useBuiltInAnimationTags) animator = gameObject.GetComponent<Animator>();
        playerRigidbody.freezeRotation = true; // Keep the physics player from rotating
    }

    private void Update()
    {
        if (canMove) PlayerInput();
        if (canSprint) Sprint();
        PhysicsDrag();
        gameObject.GetComponent<SpriteMask>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }
    

    //=-----------------=
    // Internal Functions
    //=-----------------=
    private void PlayerInput()
    {
        // Horizontal control
        if (input.GetKey("Right") && horizontalMovement < 1)                         { horizontalMovement += 1; }
        if (input.GetKey("Left") && horizontalMovement > -1)                         { horizontalMovement -= 1; }
        if (!input.GetKey("Left") && !input.GetKey("Right"))  { horizontalMovement = 0; }

        // Vertical control
        if (input.GetKey("Up") && verticalMovement < 1)                              { verticalMovement += 1; }
        if (input.GetKey("Down") && verticalMovement > -1)                           { verticalMovement -= 1; }
        if (!input.GetKey("Down") && !input.GetKey("Up"))     { verticalMovement = 0; }

        if (input.GetKey("Up") || input.GetKey("Down") || input.GetKey("Left") || input.GetKey("Right"))
        {
            lastX = horizontalMovement;
            lastY = verticalMovement;
        }

        if (useBuiltInAnimationTags)
        {
            animator.SetFloat("LastX", lastX);
            animator.SetFloat("LastY", lastY);
            animator.SetFloat("MoveX", horizontalMovement);
            animator.SetFloat("MoveY", verticalMovement);
        }
    }

    private void PlayerMovement()
    {
        playerRigidbody.MovePosition(playerRigidbody.position + new Vector2(horizontalMovement, verticalMovement) * (movementSpeed * Time.fixedDeltaTime));    // Update the movement for the character
    }

    private void Sprint()
    {
        if (input.GetKey("Action") && isGrounded)
        {
            sprinting = true;
            if (!oldPhysics)
            {
                movementSpeed = Mathf.Lerp(sprintSpeed, walkSpeed, acceleration * Time.deltaTime);
            }
            else
            {
                movementSpeed = sprintSpeed;
            }
        }
        else
        {
            sprinting = false;
            if (!oldPhysics)
            {
                movementSpeed = Mathf.Lerp(walkSpeed, sprintSpeed, acceleration * Time.deltaTime);
            }
            else
            {
                movementSpeed = walkSpeed;
            }
        }
    }

    private void PhysicsDrag()
    {
        if (!oldPhysics)
        {
            if (isGrounded)
            {
                playerRigidbody.drag = groundDrag;
            }
            else if (!isGrounded)
            {
                playerRigidbody.drag = airDrag;
            }
        }
        else
        {
            playerRigidbody.drag = 0;
        }
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void SetNewMovement(float _horizontalMovement, float _verticalMovement, float _movementSpeed)
    {
        // Disable current movement so it doesn't imediately overwrite our new movement
        if (canMove) { canMove = false; couldMove = true;}
        else couldMove = false;
        if (canSprint) { canSprint = false; couldSprint = true; }
        else couldSprint = false;

        // Set new movement
        horizontalMovement = _horizontalMovement;
        verticalMovement = _verticalMovement;
        movementSpeed = _movementSpeed;
    }

    public void ResetMovement()
    {
        // Disable current movement so it doesn't imediately overwrite our new movement
        if (couldMove) { canMove = true; }
        if (couldSprint) { canSprint = true;}

        // Set new movement
        movementSpeed = walkSpeed;
    }

    public void SetGrounded(bool _grounded)
    {
        if (_grounded)
        {
            isGrounded = true;
            playerCollider.enabled = true;
        }
        else if (!_grounded)
        {
            isGrounded = false;
            lastGroundedPosition = gameObject.transform.position;
            playerCollider.enabled = false;
        }
    }

    public void ResetToGroundPosition()
    {
        gameObject.transform.position = lastGroundedPosition;
    }
}
