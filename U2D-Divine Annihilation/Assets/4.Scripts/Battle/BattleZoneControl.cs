// Included Libraries
using UnityEngine;

/* Battle zone control
 * ---------------------
 * This script is applied to the battle zone object in a scene
 * It allows for the active battle zone to appear and disappear
*/
public class BattleZoneControl : MonoBehaviour
{
    // Public variables
    public bool abzActive;
    public float speed;
    public GameObject activeBattleZone;
    public GameObject onScreenLocationTarget;
    public GameObject offScreenLocationTarget;
    public Transform onScreenLocation;
    public Transform offScreenLocation;

    // Start is called before the first frame update
    void Start()
    {
        abzActive = false;
        onScreenLocation = onScreenLocationTarget.GetComponent<Transform>();
        offScreenLocation = offScreenLocationTarget.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (abzActive) transform.position = Vector2.MoveTowards(transform.position, onScreenLocation.position, speed * Time.deltaTime);
        if (!abzActive) transform.position = Vector2.MoveTowards(transform.position, offScreenLocation.position, speed * Time.deltaTime);
    }
}
