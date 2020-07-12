using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_system_required_camera_controller : MonoBehaviour
{
    // Setup camera variables
    public Transform followTarget;
    public float zoom;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, followTarget.transform.position.z-zoom);
    }
}
