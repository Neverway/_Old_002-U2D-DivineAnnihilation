//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Give functionality to the load save profile button on the title screen
// Applied to: SelectFile menu on the title screen
//
//=============================================================================

using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Load : MonoBehaviour
{
    public GameObject selfTarget;
    public GameObject eraseMenu;
    public GameObject savesMenu;
    public GameObject configTarget;
    private Menu_Scroll_MinusControl menu;
    private SaveManager saveManager;

    void Start()
    {

        menu = selfTarget.GetComponent<Menu_Scroll_MinusControl>();
        saveManager = configTarget.GetComponent<SaveManager>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Action"))
        {
            menu.currentFrame = 0;
            selfTarget.SetActive(false);
            savesMenu.SetActive(true);
        }

        if (Input.GetButtonDown("Interact"))
        {
            if (menu.currentFrame == 0)
            {
                saveManager.Load();
                SceneManager.LoadScene(saveManager.activeSave.scene);
            }

            if (menu.currentFrame == 1)
            {
                selfTarget.SetActive(false);
                eraseMenu.SetActive(true);
            }
        }
    }
}
