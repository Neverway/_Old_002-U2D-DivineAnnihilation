using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWarp : MonoBehaviour
{
    public GameObject ExitTarget;
    public GameObject Player;
    public bool PlayTransition;

    // Other class references
    private SystemFadeTransition Transition;

    // Start is called before the first frame update
    void Start()
    {
        Transition = FindObjectOfType<SystemFadeTransition>();   // Find the dialogue manager script
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(0.6f);
        Player.transform.position = new Vector2(ExitTarget.transform.position.x, ExitTarget.transform.position.y);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if that something is the player
        if (other.gameObject.name == "Entity Fox")
        {
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