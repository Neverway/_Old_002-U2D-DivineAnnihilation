using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Effect_Water : MonoBehaviour
{
    public bool horizontalEntry;

    // Enable the water effect
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Entity_Effect_Water>() != null)
        {
            other.gameObject.GetComponent<Entity_Effect_Water>().EnterWater();
            other.gameObject.transform.position = new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.y - 0.25f);
        }
    }

    // Keep the water effect enabled if you walk between two water triggers
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Entity_Effect_Water>() != null)
        {
            other.gameObject.GetComponent<Entity_Effect_Water>().EnterWater();
        }
    }

    // Disable the water trigger
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Entity_Effect_Water>() != null)
        {
            other.gameObject.GetComponent<Entity_Effect_Water>().ExitWater();
            other.gameObject.transform.position = new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 0.25f);
        }
    }
}
