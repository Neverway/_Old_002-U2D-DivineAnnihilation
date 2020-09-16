using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_c1_pushbox : MonoBehaviour
{
    public float speed;
    public GameObject player;
    private bool pulling;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = player.GetComponent<Transform>();
    }

    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            pulling = true;
        }
        if (Input.GetKeyUp("z"))
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
