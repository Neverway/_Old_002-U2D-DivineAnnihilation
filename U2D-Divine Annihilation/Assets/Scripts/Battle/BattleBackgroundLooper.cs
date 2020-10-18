using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBackgroundLooper : MonoBehaviour
{
    public float offset = 8;

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if that something is the player
        if (other.gameObject.name == "Background")
        {

                other.transform.position = new Vector2(other.transform.position.x+offset, other.transform.position.y);
        }
    }
}