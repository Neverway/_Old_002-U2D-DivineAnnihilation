
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_trigger_warp : MonoBehaviour
{
    public GameObject ExitTarget;
    public GameObject Player;
    public bool PlayTransition;
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if that something is the player
        //if (other.gameObject.name == "pre_entity_main_fox_overworld")
        //{
            other.transform.position = new Vector2(ExitTarget.transform.position.x, ExitTarget.transform.position.y);
        //}
    }
}
