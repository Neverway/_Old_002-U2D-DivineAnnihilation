using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBackgroundMovement : MonoBehaviour
{
    public float speed = 0.01f;

    void Update()
    {
        transform.position = new Vector2(transform.position.x-speed, transform.position.y);
    }
}