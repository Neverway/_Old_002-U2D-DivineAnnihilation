using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleErase : MonoBehaviour
{
    public GameObject selfTarget;
    public GameObject loadMenu;
    public GameObject savesMenu;
    public GameObject configTarget;
    private MenuScrollMinusControl menu;
    private SaveManager saveManager;


    // Start is called before the first frame update
    void Start()
    {

        menu = selfTarget.GetComponent<MenuScrollMinusControl>();
        saveManager = configTarget.GetComponent<SaveManager>();
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

