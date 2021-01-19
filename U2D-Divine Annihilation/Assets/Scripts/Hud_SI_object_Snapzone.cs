using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hud_SI_object_Snapzone : MonoBehaviour
{
    public string snapObjectsTag = "SI Pickup";
    public Transform snapPoint;
    private bool slotFilled;
    private GameObject target;
    private Hud_SI_hand_controller handController;
    public UnityEvent onSlotFilled;
    public UnityEvent onSlotEmpty;

    void Start()
    {
        handController = FindObjectOfType<Hud_SI_hand_controller>();
       //highlightChild = this.transform.GetChild(0).gameObject;
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == snapObjectsTag && !slotFilled && !handController.isGrabbing)
        {
            other.transform.position = new Vector2 (snapPoint.position.x, snapPoint.position.y);
            target = other.gameObject;
            slotFilled = true;
            onSlotFilled.Invoke();
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == target)
        {
            slotFilled = false;
            onSlotEmpty.Invoke();
        }
    }
}
