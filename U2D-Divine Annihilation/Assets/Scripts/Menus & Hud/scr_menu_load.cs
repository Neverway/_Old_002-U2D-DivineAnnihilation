using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_menu_load : MonoBehaviour
{
    public GameObject selfTarget;
    public GameObject eraseMenu;
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
        if (Input.GetKeyDown("z"))
        {
            if (menu.currentFrame == 0)
            {
                saveManager.Load();
                SceneManager.LoadScene(saveManager.activeSave.scene);
            }
        }

        if (Input.GetKeyDown("z"))
        {
            if (menu.currentFrame == 1)
            {
                selfTarget.SetActive(false);
                eraseMenu.SetActive(true);
            }
        }
    }
}
