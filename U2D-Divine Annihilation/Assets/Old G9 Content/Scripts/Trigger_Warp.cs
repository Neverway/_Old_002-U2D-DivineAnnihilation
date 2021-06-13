//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Transition the player between rooms
// Applied to: Warp trigger
//
//=============================================================================

using System.Collections;
using UnityEngine;

public class Trigger_Warp : MonoBehaviour
{
    public GameObject ExitTarget;
    private GameObject Player;
    public bool PlayTransition;

    private SystemFadeTransition Transition;

    void Start()
    {
        Transition = FindObjectOfType<SystemFadeTransition>(); // Find the dialogue manager script
    }


    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.6f);
        Player.transform.position = new Vector2(ExitTarget.transform.position.x, ExitTarget.transform.position.y);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player = other.gameObject;
            if (PlayTransition)
            {
                Transition.StartCoroutine("TriggerFade");
                StartCoroutine("Teleport");
            }

           else
           {
                Player.transform.position = new Vector2(ExitTarget.transform.position.x, ExitTarget.transform.position.y);
           }
        }
    }
}