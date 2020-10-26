// Included Libraries
using UnityEngine;

/* Battle background looper
 * ---------------------
 * This script is applied to the background looper trigger in a battle scene
 * It seemlessly loops the background object along the X-axis
*/
public class BattleBackgroundLooper : MonoBehaviour
{
    // Public variables
    public float offset = 8; // How far back will the background object be pushed


    // Check if something has collided with the trigger
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if that something is the background object
        if (other.gameObject.name == "Background")
        {
                other.transform.position = new Vector2(other.transform.position.x+offset, other.transform.position.y); // Set the background object to the new X position
        }
    }
}
