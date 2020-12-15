using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Pickup : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcon;
    public string itemCategory;
    //public string itemDescription;

    private bool triggered;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Entity Fox")
        {
            if (Input.GetKeyDown("z") && !triggered)
            {
                triggered = true;
            }
        }
    }
}
