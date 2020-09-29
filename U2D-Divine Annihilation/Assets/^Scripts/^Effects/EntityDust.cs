// Included Libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_entity_dust : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 5f;
    public float movementSpeed;
    public bool sprintDust = true;
    public Rigidbody2D Rigidbody;
    public ParticleSystem dustParticleSystem;

    // Other class references
    Vector2 movement;


    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = walkSpeed;  // Set the starting movement speed
    }


    // Update is called once per frame
    void Update()
    {
        // Emmit dust when going fast
        if (sprintDust)
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
}
