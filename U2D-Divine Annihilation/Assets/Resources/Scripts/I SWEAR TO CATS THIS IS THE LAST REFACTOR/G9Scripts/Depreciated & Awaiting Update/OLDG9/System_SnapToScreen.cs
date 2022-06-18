using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_SnapToScreen : MonoBehaviour
{
    public Transform cameraObject;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = new Vector2(cameraObject.position.x, cameraObject.position.y);
    }
}
