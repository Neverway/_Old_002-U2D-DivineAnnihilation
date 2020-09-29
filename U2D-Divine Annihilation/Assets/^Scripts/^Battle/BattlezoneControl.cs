using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_battle_abz_control : MonoBehaviour
{
    private bool acceptingInput;
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

    // Key press delay
    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(1);     // The delay until it is accepting input again
        acceptingInput = true;                  // Allow input again
    }

    // Update is called once per frame
    void Update()
    {
        if (abzActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, onScreenLocation.position, speed * Time.deltaTime);
        }

        if (!abzActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, offScreenLocation.position, speed * Time.deltaTime);
        }
    }
}
