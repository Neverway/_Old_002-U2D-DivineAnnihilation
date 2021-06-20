using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud_SI_Cable : MonoBehaviour
{
    public Hud_SI_hand_controller handController;
    public Transform handTarget;
    public GameObject pickupTarget;
    public SpriteRenderer wireEnd;

    private Vector3 startPoint;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.parent.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = handTarget.position;//Camera.main.ScreenToWorldPoint(handTarget.position);
        newPosition.z = 0;

        if (handController.isGrabbing && handController.target == pickupTarget)
        {
            Vector3 direction = newPosition - startPoint;
            transform.right = direction;
            //transform.position = newPosition;

            float distance = Vector2.Distance(startPoint, newPosition);
            wireEnd.size = new Vector2(distance, wireEnd.size.y);
        }
        else
        {

        }
    }
}
