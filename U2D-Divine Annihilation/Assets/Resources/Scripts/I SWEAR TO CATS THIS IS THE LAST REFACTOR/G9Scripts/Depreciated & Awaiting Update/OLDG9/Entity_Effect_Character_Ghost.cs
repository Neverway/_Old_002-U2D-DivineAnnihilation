//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Create a ghosting effect when the player uses a special ability
// Applied to: The player object in an overworld scene
//
//=============================================================================

using UnityEngine;

public class Entity_Effect_Character_Ghost : MonoBehaviour
{
    public GameObject ghost;
    public float ghostDelay;
    private float ghostDelaySeconds;

    private Entity_Character_Movement characterMovement;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        characterMovement = FindObjectOfType<Entity_Character_Movement>();
        ghostDelaySeconds = ghostDelay;
    }


    void Update()
    {
        if (characterMovement.movementSpeed >= 7)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }

            else
            {
                // Generate a new ghost
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 1f);
            }
        }
    }
}
