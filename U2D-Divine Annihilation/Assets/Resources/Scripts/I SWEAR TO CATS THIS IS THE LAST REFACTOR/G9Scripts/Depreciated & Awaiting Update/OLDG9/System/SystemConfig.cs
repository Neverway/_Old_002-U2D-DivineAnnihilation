﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemConfig : MonoBehaviour
{
    public string saveprofile;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
