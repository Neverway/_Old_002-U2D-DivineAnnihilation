using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hud_SI_Button : MonoBehaviour
{
    public UnityEvent onClicked;

    private bool isHovering;
    private System_InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputManager.controls["Interact"]) && isHovering)
        {
            onClicked.Invoke();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SI Hand")
        {
            isHovering = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "SI Hand")
        {
            isHovering = false;
        }
    }
}
