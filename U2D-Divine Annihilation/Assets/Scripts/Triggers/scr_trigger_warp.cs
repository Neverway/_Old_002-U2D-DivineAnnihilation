
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
        Player.transform.position = new Vector2 (ExitTarget.transform.position.x, ExitTarget.transform.position.y);
    }
}
