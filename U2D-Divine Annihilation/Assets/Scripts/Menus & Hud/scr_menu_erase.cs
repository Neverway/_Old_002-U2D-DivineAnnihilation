using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_menu_erase : MonoBehaviour
{
    public GameObject selfTarget;
    public GameObject loadMenu;
    public GameObject savesMenu;
    public GameObject configTarget;
    private scr_menu_scrollMinusControl menu;
    private scr_system_saveManager saveManager;


    // Start is called before the first frame update
    void Start()
    {

        menu = selfTarget.GetComponent<scr_menu_scrollMinusControl>();
        saveManager = configTarget.GetComponent<scr_system_saveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("x"))
        {
            loadMenu.SetActive(true);
            selfTarget.SetActive(false);
        }

        if (Input.GetKeyDown("z"))
        {
            if (menu.currentFrame == 0)
            {
                selfTarget.SetActive(false);
                loadMenu.SetActive(true);
            }
        }

        if (Input.GetKeyDown("z"))
        {
            if (menu.currentFrame == 1)
            {
                saveManager.DeleteSaveProfile();
                selfTarget.SetActive(false);
                savesMenu.SetActive(true);
            }
        }
    }
}

