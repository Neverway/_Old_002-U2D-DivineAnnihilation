//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Seemlessly loops the background object along the X-axis
// Applied to: Background bumper trigger in a battle scene
//
//=============================================================================

using UnityEngine;

public class Battle_Background_Looper : MonoBehaviour
{
    // Public Variables
    public float offset = 8; // How far back the background object be pushed

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Background")
        {
                other.transform.position = new Vector2(other.transform.position.x + offset, other.transform.position.y); // Set the background object to the new X position
        }
    }
}
