
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerWarpLevel : MonoBehaviour
{
    public float nextRoomX;
    public float nextRoomY;
    public string loadRoom;
    public GameObject Player;
    public bool PlayTransition;

    public void Update()
    {
        PlayerPrefs.SetFloat("NextRoomX", nextRoomX);
        PlayerPrefs.SetFloat("NextRoomY", nextRoomY);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if that something is the player
        if (other.gameObject.name == "Entity Fox")
        {
            SceneManager.LoadScene(loadRoom);
        }
    }
}
