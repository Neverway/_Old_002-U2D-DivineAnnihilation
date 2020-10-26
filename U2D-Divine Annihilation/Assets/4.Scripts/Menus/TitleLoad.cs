using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleLoad : MonoBehaviour
{
    public GameObject selfTarget;
    public GameObject eraseMenu;
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
            selfTarget.SetActive(false);
            savesMenu.SetActive(true);
        }

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
