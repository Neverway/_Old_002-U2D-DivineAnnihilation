using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud_SI_hand_controller : MonoBehaviour
{
    public float movementSpeed;
    public bool canMove = true;
    public Rigidbody2D Rigidbody;
    public Transform gripPoint;
    private bool isHovering;
    public bool isGrabbing;
    public GameObject target;
    private System_InputManager inputManager;

    Vector2 movement;

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
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
        }

        // Stop character if the canMove variable is false
        if (!canMove)
        {
            movement.x = 0;
            movement.y = 0;
        }

        if (Input.GetKeyDown(inputManager.controls["Interact"]) && isHovering)
        {
            if (!isGrabbing)
            {
                isGrabbing = true;
            }
        }
        if (Input.GetKeyUp(inputManager.controls["Interact"]))
        {
            isGrabbing = false;
        }

        if (isGrabbing)
        {
            target.transform.position = new Vector2(gripPoint.position.x, gripPoint.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SI Pickup" && !isGrabbing)
        {
            isHovering = true;
            target = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "SI Pickup")
        {
            isHovering = false;
        }
    }

    void FixedUpdate()
    {
        Rigidbody.MovePosition(Rigidbody.position + movement * movementSpeed * Time.fixedDeltaTime);    // Update the movement for the character
    }
}
