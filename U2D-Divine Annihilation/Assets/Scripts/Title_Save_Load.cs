//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Give functionality to selecting a profile on the title screen
// Applied to: SaveFile menu on the title screen
//
//=============================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Save_Load : MonoBehaviour
{
    public GameObject selfTarget;
    public GameObject titleMenu;
    public GameObject loadingScreen;
    public GameObject loadFileScreen;
    public GameObject configTarget;
    private Menu_Scroll_MinusControl menu;
    private SaveManager saveManager;

    void Start()
    {
        //DontDestroyOnLoad(selfTarget.gameObject);
        menu = selfTarget.GetComponent<Menu_Scroll_MinusControl>();
        saveManager = configTarget.GetComponent<SaveManager>();
    }

    IEnumerator DoubleCheck()
    {
        yield return new WaitForSeconds(0.6f);
        saveManager.activeSave.saveProfileName = "SlotOne";
    }


    void Update()
    {
        string dataPath = Application.persistentDataPath;
        if (Input.GetKeyDown("z"))
        {
            // File 1
            if (menu.currentFrame == 0)
            {
                if (!System.IO.File.Exists(dataPath + "/" + "SlotOne" + ".dasp"))
                {
                    saveManager.activeSave.saveProfileName = "SlotOne";
                    StartCoroutine("DoubleCheck");
                    PlayerPrefs.SetString("Current Save Profile", saveManager.activeSave.saveProfileName);
                    saveManager.CreateSave();
                    saveManager.Save();
                    loadingScreen.SetActive(true);
                    saveManager.loadFileOnCreation = true;
                    saveManager.activeSave.scene = "C1S1";
                    SceneManager.LoadScene("C1S1");
                }

                if (System.IO.File.Exists(dataPath + "/" + "SlotOne" + ".dasp"))
                {
                    saveManager.activeSave.saveProfileName = "SlotOne";
                    PlayerPrefs.SetString("Current Save Profile", saveManager.activeSave.saveProfileName);
                    loadFileScreen.SetActive(true);
                    saveManager.loadFileOnCreation = true;
                    selfTarget.SetActive(false);
                }
            }

            // File 2
            if (menu.currentFrame == 1)
            {
                if (!System.IO.File.Exists(dataPath + "/" + "SlotTwo" + ".dasp"))
                {
                    saveManager.activeSave.saveProfileName = "SlotTwo";
                    PlayerPrefs.SetString("Current Save Profile", saveManager.activeSave.saveProfileName);
                    saveManager.CreateSave();
                    saveManager.Save();
                    loadingScreen.SetActive(true);
                    saveManager.activeSave.scene = "C1S1";
                    SceneManager.LoadScene("C1S1");
                }

                if (System.IO.File.Exists(dataPath + "/" + "SlotTwo" + ".dasp"))
                {
                    saveManager.activeSave.saveProfileName = "SlotTwo";
                    PlayerPrefs.SetString("Current Save Profile", saveManager.activeSave.saveProfileName);
                    loadFileScreen.SetActive(true);
                    selfTarget.SetActive(false);
                }
            }

            // File 3
            if (menu.currentFrame == 2)
            {
                if (!System.IO.File.Exists(dataPath + "/" + "SlotThree" + ".dasp"))
                {
                    saveManager.activeSave.saveProfileName = "SlotThree";
                    PlayerPrefs.SetString("Current Save Profile", saveManager.activeSave.saveProfileName);
                    saveManager.CreateSave();
                    saveManager.Save();
                    loadingScreen.SetActive(true);
                    saveManager.activeSave.scene = "C1S1";
                    SceneManager.LoadScene("C1S1");
                }

                if (System.IO.File.Exists(dataPath + "/" + "SlotThree" + ".dasp"))
                {
                    saveManager.activeSave.saveProfileName = "SlotThree";
                    PlayerPrefs.SetString("Current Save Profile", saveManager.activeSave.saveProfileName);
                    loadFileScreen.SetActive(true);
                    selfTarget.SetActive(false);
                }
            }

            // File 4
            if (menu.currentFrame == 3)
            {
                if (!System.IO.File.Exists(dataPath + "/" + "SlotFour" + ".dasp"))
                {
                    saveManager.activeSave.saveProfileName = "SlotFour";
                    PlayerPrefs.SetString("Current Save Profile", saveManager.activeSave.saveProfileName);
                    saveManager.CreateSave();
                    saveManager.Save();
                    loadingScreen.SetActive(true);
                    saveManager.activeSave.scene = "C1S1";
                    SceneManager.LoadScene("C1S1");
                }

                if (System.IO.File.Exists(dataPath + "/" + "SlotFour" + ".dasp"))
                {
                    saveManager.activeSave.saveProfileName = "SlotFour";
                    PlayerPrefs.SetString("Current Save Profile", saveManager.activeSave.saveProfileName);
                    loadFileScreen.SetActive(true);
                    selfTarget.SetActive(false);
                }
            }
        }

        if (Input.GetKeyDown("x"))
        {
            menu.currentFrame = 0;
            titleMenu.SetActive(true);
            selfTarget.SetActive(false);
        }
    }
}
