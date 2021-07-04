using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemEntityID : MonoBehaviour
{
    public string[] Name;
    public Sprite[] Icon;

    public GameObject configTarget;
    private SaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        saveManager = configTarget.GetComponent<SaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
