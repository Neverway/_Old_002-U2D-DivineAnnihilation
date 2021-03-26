//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Move the background along the X-axis
// Applied to: Background object in a battle scene
//
//=============================================================================

using UnityEngine;

public class Battle_Background_Movement : MonoBehaviour
{
    // Public Variables
    public float speed = 0.01f; // How fast the object will move

    void Update()
    {
        transform.position = new Vector2(transform.position.x - speed, transform.position.y); // Move the object along the X-axis
    }
}
