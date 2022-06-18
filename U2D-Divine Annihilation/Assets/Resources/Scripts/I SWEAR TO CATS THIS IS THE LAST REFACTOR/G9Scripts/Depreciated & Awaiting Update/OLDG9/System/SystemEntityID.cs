using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemEntityID : MonoBehaviour
{
    public string[] Name;
    public Sprite[] Icon;

    public GameObject configTarget;
    private SaveManager saveManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

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
