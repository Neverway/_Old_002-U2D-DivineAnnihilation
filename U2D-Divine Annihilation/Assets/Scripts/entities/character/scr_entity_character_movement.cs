using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_entity_character_movement : MonoBehaviour
{
    // Setup configurable variables
    public float movementSpeed = 5f;
    public Rigidbody2D Rigidbody;

    // Input variables
    Vector2 movement;

    // Update is called once per frame (Speed is called by FPS)
    void Update()
    {
        // Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    // Update is not tied to FPS but updates at a constant rate
    void FixedUpdate()
    {
        // Movement
        Rigidbody.MovePosition(Rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);
    }
}
