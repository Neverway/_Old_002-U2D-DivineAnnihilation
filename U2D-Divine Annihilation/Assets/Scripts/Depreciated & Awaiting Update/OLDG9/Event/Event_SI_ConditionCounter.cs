using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Event_SI_ConditionCounter : MonoBehaviour
{
    public int flagLimit;
    public int flagsTripped;
    public UnityEvent AllFlagsTripped;
    public UnityEvent Unflagged;

    private bool triggered;

    void Start()
    {
        triggered = false;
    }

    public void FlagAdd()
    {
        flagsTripped += 1;
    }

    public void FlagSubtract()
    {
        flagsTripped -= 1;
        Unflagged.Invoke();
    }

    public void Update()
    {
        if (flagsTripped == flagLimit && !triggered)
        {
            AllFlagsTripped.Invoke();
        }
    }
}
