using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_entity_character_movement : MonoBehaviour
{
    // Setup configurable variables
    public float walkSpeed = 5f;
    public float sprintSpeed = 5f;
    float movementSpeed;
    public Rigidbody2D Rigidbody;
    public Animator characterAnimator;

    // Input variables
    Vector2 movement;

    void Start()
    {
        movementSpeed = walkSpeed;
    }

    // Update is called once per frame (Speed is called by FPS)
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        

        characterAnimator.SetFloat("Horizontal", movement.x);
        characterAnimator.SetFloat("Vertical", movement.y);
        characterAnimator.SetFloat("Speed", movement.sqrMagnitude);

        // sprinting
        if (Input.GetKeyDown("x"))
        {
            movementSpeed = sprintSpeed;
        }
        else if (Input.GetKeyUp("x"))
        {
            movementSpeed = walkSpeed;
        }
    }

    // Update is not tied to FPS but updates at a constant rate
    void FixedUpdate()
    {
        // Movement
        Rigidbody.MovePosition(Rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
