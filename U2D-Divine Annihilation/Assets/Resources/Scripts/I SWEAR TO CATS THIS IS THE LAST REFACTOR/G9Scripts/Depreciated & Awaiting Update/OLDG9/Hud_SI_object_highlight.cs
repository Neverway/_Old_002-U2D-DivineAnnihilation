using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud_SI_object_highlight : MonoBehaviour
{
    private GameObject highlightChild;
    private Hud_SI_hand_controller handController;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        handController = FindObjectOfType<Hud_SI_hand_controller>();
        highlightChild = this.transform.GetChild(0).gameObject;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SI Hand")
        {
            if (!handController.isGrabbing)
            {
                highlightChild.SetActive(true);
            }
            else
            {
                highlightChild.SetActive(false);
            }
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "SI Hand")
        {
            highlightChild.SetActive(false);
        }
    }
}
