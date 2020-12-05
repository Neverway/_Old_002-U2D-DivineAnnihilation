//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Chase the player character
// Applied to: An entity with movement in an overworld scene
//
//=============================================================================

using UnityEngine;
using Pathfinding;

public class Entity_Follow_Flip : MonoBehaviour
{
    public AIPath aiPath;

    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
