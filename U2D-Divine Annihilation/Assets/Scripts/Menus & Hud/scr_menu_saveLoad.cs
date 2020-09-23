using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_menu_saveLoad : MonoBehaviour
{
    public GameObject target;
    private scr_menu_scrollMinusControl menu;

    // Start is called before the first frame update
    void Start()
    {
        menu = target.GetComponent<scr_menu_scrollMinusControl>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            if (menu.currentFrame == 0)
            {
                Debug.Log("Save Profile 1");
            }
            if (menu.currentFrame == 1)
            {
                Debug.Log("Save Profile 2");
            }
            if (menu.currentFrame == 2)
            {
                Debug.Log("Save Profile 3");
            }
            if (menu.currentFrame == 3)
            {
                Debug.Log("Save Profile 4");
            }
        }
    }
}
