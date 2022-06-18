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

public class NUPTopdownDodgeroll : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    [Header("Options")]
    public bool useBuiltInAnimationTags = true; // (Requires the entity/gameobject this is script is on, to have the animation tags setup in the referenced animator)

    [Header("Roll Settings")]
    public float tapSpeed = 0.23f;          // The maximum amount of time that can pass (in seconds) before another keypress would no longer be considered a "double-tap"
    public float dodgeMovementSpeed = 16f;
    public float dodgeDuration = 0.3f;             // How long does the dodge last
    public float dodgeCooldownDuration = 1.3f;     // How long before the dodge can be used again.
    public bool dodgeCooldown;              // [READ-ONLY] Is the dodge currently on cooldown


    //=-----------------=
    // Private variables
    //=-----------------=
    private Vector2 dodgeMovement;
    private float BLastTapTime = 0;     // The time that has passed since the last time the [B]/[Action] button was pressed


    //=-----------------=
    // Reference variables
    //=-----------------=
    private NUPInput inputManager;
    private NUPTopdownController topdownController;
    private Animator animator;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        inputManager = FindObjectOfType<NUPInput>();
        topdownController = FindObjectOfType<NUPTopdownController>();
        if (useBuiltInAnimationTags) animator = gameObject.GetComponent<Animator>();
    }

    IEnumerator DodgeRoll()
    {
        topdownController.SetGrounded(false);
        yield return new WaitForSeconds(dodgeDuration);
        topdownController.SetGrounded(true);
        dodgeMovement = new Vector2(0,0);
        topdownController.ResetMovement();
        dodgeCooldown = true;
        yield return new WaitForSeconds(dodgeCooldownDuration);
        dodgeCooldown = false;
    }

    private void FixedUpdate()
    {
        if (topdownController.canMove && dodgeMovement.x != 0 || topdownController.canMove && dodgeMovement.y != 0)
        {
            topdownController.SetNewMovement(dodgeMovement.x, dodgeMovement.y, dodgeMovementSpeed); 
        }
    }

    private void Update()
    {
        // Roll
        if(topdownController.canMove && Input.GetKeyDown(inputManager.controls["Action"]) && dodgeMovement.x == 0 && dodgeMovement.y == 0 && !dodgeCooldown)
        {
            if((Time.time - BLastTapTime) < tapSpeed)
            { 
                StartCoroutine("DodgeRoll");
                // Use the lastX & lastY variables to figure out which direction the player was last moving in (this should also be their facing direction)
                if (topdownController.lastX <= -0.1)
                {
                    dodgeMovement.x = -1;
                    if (useBuiltInAnimationTags) animator.Play("Roll Left");
                }
                else if (topdownController.lastX >= 0.1)
                {
                    dodgeMovement.x = 1;
                    if (useBuiltInAnimationTags) animator.Play("Roll Right");
                }
                if (topdownController.lastY <= -0.1)
                {
                    dodgeMovement.y = -1;
                    if (useBuiltInAnimationTags) animator.Play("Roll Up");
                }
                else if (topdownController.lastY >= 0.1)
                {
                    dodgeMovement.y = 1;
                    if (useBuiltInAnimationTags) animator.Play("Roll Down");
                }
            }

            BLastTapTime = Time.time; 
        }
    }
    
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}
