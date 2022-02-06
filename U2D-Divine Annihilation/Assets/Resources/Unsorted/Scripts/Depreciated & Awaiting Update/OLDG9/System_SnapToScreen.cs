using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_SnapToScreen : MonoBehaviour
{
    public Transform cameraObject;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector2(cameraObject.position.x, cameraObject.position.y);
    }
}
