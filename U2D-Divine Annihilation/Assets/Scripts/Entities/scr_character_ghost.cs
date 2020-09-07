﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_character_ghost : MonoBehaviour
{
    public GameObject ghost;
    public float ghostDelay;
    private float ghostDelaySeconds;

    private scr_character_movement characterMovement;

    // Start is called before the first frame update
    void Start()
    {
        characterMovement = FindObjectOfType<scr_character_movement>();
        ghostDelaySeconds = ghostDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (characterMovement.movementSpeed >= 7)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                // Generate a new ghost
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 1f);
            }
        }
    }
}
