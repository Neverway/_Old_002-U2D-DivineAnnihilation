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
    private GameObject target;

    Vector2 movement;

    void Start()
    {
    }


    void Update()
    {
        // Allow character input if the canMove variable is true
        if (canMove)
        {
            // Movement input
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
        }

        // Stop character if the canMove variable is false
        if (!canMove)
        {
            movement.x = 0;
            movement.y = 0;
        }

        if (Input.GetKeyDown("z") && isHovering)
        {
            if (!isGrabbing)
            {
                isGrabbing = true;
            }
        }
        if (Input.GetKeyUp("z"))
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
