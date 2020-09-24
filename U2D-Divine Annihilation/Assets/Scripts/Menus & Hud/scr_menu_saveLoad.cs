using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_menu_saveLoad : MonoBehaviour
{
    public GameObject selfTarget;
    public GameObject titleMenu;
    public GameObject loadFileScreen;
    public GameObject configTarget;
    private scr_menu_scrollMinusControl menu;
    private scr_system_saveManager saveManager;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(selfTarget.gameObject);
        menu = selfTarget.GetComponent<scr_menu_scrollMinusControl>();
        saveManager = configTarget.GetComponent<scr_system_saveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        string dataPath = Application.persistentDataPath;
        if (Input.GetKeyDown("z"))
        {
            if (menu.currentFrame == 0)
            {
                if (!System.IO.File.Exists(dataPath + "/" + "SlotOne" + ".dasp"))
                {
                    saveManager.activeSave.saveProfileName = "SlotOne";
                    PlayerPrefs.SetString("Current Save Profile", saveManager.activeSave.saveProfileName);
                    saveManager.CreateSave();
                    saveManager.Save();
                    saveManager.activeSave.scene = "scn_c1s1";
                    SceneManager.LoadScene("scn_c1s1");
                }

                if (System.IO.File.Exists(dataPath + "/" + "SlotOne" + ".dasp"))
                {
                    saveManager.activeSave.saveProfileName = "SlotOne";
                    PlayerPrefs.SetString("Current Save Profile", saveManager.activeSave.saveProfileName);
                    loadFileScreen.SetActive(true);
                    selfTarget.SetActive(false);
                }
            }
            if (menu.currentFrame == 1)
            {
                if (!System.IO.File.Exists(dataPath + "/" + "SlotTwo" + ".dasp"))
                {
                    saveManager.activeSave.saveProfileName = "SlotTwo";
                    PlayerPrefs.SetString("Current Save Profile", saveManager.activeSave.saveProfileName);
                    saveManager.CreateSave();
                    saveManager.Save();
                    saveManager.activeSave.scene = "scn_c1s1";
                    SceneManager.LoadScene("scn_c1s1");
                }

                if (System.IO.File.Exists(dataPath + "/" + "SlotTwo" + ".dasp"))
                {
                    saveManager.activeSave.saveProfileName = "SlotTwo";
                    PlayerPrefs.SetString("Current Save Profile", saveManager.activeSave.saveProfileName);
                    loadFileScreen.SetActive(true);
                    selfTarget.SetActive(false);
                }
            }
            if (menu.currentFrame == 2)
            {
                if (!System.IO.File.Exists(dataPath + "/" + "SlotThree" + ".dasp"))
                {
                    saveManager.activeSave.saveProfileName = "SlotThree";
                    PlayerPrefs.SetString("Current Save Profile", saveManager.activeSave.saveProfileName);
                    saveManager.CreateSave();
                    saveManager.Save();
                    saveManager.activeSave.scene = "scn_c1s1";
                    SceneManager.LoadScene("scn_c1s1");
                }

                if (System.IO.File.Exists(dataPath + "/" + "SlotThree" + ".dasp"))
                {
                    saveManager.activeSave.saveProfileName = "SlotThree";
                    PlayerPrefs.SetString("Current Save Profile", saveManager.activeSave.saveProfileName);
                    loadFileScreen.SetActive(true);
                    selfTarget.SetActive(false);
                }
            }
            if (menu.currentFrame == 3)
            {
                if (!System.IO.File.Exists(dataPath + "/" + "SlotFour" + ".dasp"))
                {
                    saveManager.activeSave.saveProfileName = "SlotFour";
                    PlayerPrefs.SetString("Current Save Profile", saveManager.activeSave.saveProfileName);
                    saveManager.CreateSave();
                    saveManager.Save();
                    saveManager.activeSave.scene = "scn_c1s1";
                    SceneManager.LoadScene("scn_c1s1");
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
            titleMenu.SetActive(true);
            selfTarget.SetActive(false);
        }
    }
}
