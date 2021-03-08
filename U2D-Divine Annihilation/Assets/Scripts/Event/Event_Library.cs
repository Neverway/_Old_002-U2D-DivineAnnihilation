﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Event_Library : MonoBehaviour
{
    public int booksFlag;
    private bool triggered;
    public UnityEvent booksFlagged;

    void Start()
    {
        triggered = false;
    }

    public void BookFlagAdd()
    {
        booksFlag += 1;
    }

    public void BookFlagSubtract()
    {
        booksFlag -= 1;
    }

    public void Update()
    {
        if (booksFlag == 3 && !triggered)
        {
            booksFlagged.Invoke();
        }
    }
}
