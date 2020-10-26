// Included Libraries
using UnityEngine;

/* Battle background movement
 * ---------------------
 * This script is applied to the background looper trigger in a battle scene
 * It seemlessly loops the background object along the X-axis
*/
public class BattleBackgroundMovement : MonoBehaviour
{    
    // Public variables
    public float speed = 0.01f; // How fast will the object move


    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x-speed, transform.position.y); // Move the object along the X-axis
    }
}
