//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Create a dust effect when a entity is moving fast
// Applied to: An entity with movement and speed control in an overworld scene
//
//=============================================================================

using UnityEngine;

public class Entity_Effect_Dust : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 5f;
    public float movementSpeed;
    public bool sprintDust = true;
    public Rigidbody2D Rigidbody;
    public ParticleSystem dustParticleSystem;

    Vector2 movement;

    void Start()
    {
        movementSpeed = walkSpeed;  // Set the starting movement speed
    }


    void Update()
    {
        /*
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
        */
    }
}
