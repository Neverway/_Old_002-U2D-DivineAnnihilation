using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePushbox : MonoBehaviour
{
    public float speed;
    public GameObject player;
    private bool pulling;
    private Transform target;
    private System_InputManager inputManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    // Start is called before the first frame update
    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
        target = player.GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(inputManager.controls["Interact"]))
        {
            pulling = true;
        }
        if (Input.GetKeyDown(inputManager.controls["Interact"]))
        {
            pulling = false;
        }
    }

    // Update is called once per frame
    void OnTriggerStay2D(Collider2D triggerCollider)
    {
        if (pulling == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
