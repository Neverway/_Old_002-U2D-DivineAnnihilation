//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Sets the active battle zone to appear and disappear
// Applied to: Active Battle Zone object in a battle scene
//
//=============================================================================

using UnityEngine;

public class Battle_Zone_Control : MonoBehaviour
{
    public bool abzActive;
    public float speed;
    public GameObject activeBattleZone;
    public GameObject onScreenLocationTarget;
    public GameObject offScreenLocationTarget;
    public Transform onScreenLocation;
    public Transform offScreenLocation;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        abzActive = false;
        onScreenLocation = onScreenLocationTarget.GetComponent<Transform>();
        offScreenLocation = offScreenLocationTarget.GetComponent<Transform>();
    }


    void Update()
    {
        if (abzActive) transform.position = Vector2.MoveTowards(transform.position, onScreenLocation.position, speed * Time.deltaTime);
        if (!abzActive) transform.position = Vector2.MoveTowards(transform.position, offScreenLocation.position, speed * Time.deltaTime);
    }
}
