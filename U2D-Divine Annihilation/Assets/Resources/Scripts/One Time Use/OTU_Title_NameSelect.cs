using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OTU_Title_NameSelect : MonoBehaviour
{
    public Text nameTextObject;
    public Text controlToolTipObject;
    public GameObject systemMessageScreen;
    public Text message;
    public Text actions;

    private string nameTakenError = "That user is already active in the system!";
    private OTU_System_InputManager inputManager;
    private OTU_System_SaveManager saveManager;
    private GameObject loadingScreen;

    void Start()
    {
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
        loadingScreen = GameObject.FindWithTag("Loading Screen");
    }


    void Update()
    {
        controlToolTipObject.text = "[" + inputManager.controls["Interact"] + "] Select   [" + inputManager.controls["Action"] + "] Backspace   [" + inputManager.controls["Select"] + "] Clear";
        ClearName();
        DeleteLetter();
    }


    public void EnterLetter(string letter)
    {
        if (nameTextObject.text.Length < 12)
        {
            nameTextObject.text = nameTextObject.text + letter;
        }
    }


    public void DeleteLetter()
    {
        if (Input.GetKeyDown(inputManager.controls["Action"]) && nameTextObject.text.Length != 0) { nameTextObject.text = nameTextObject.text.Remove(nameTextObject.text.Length - 1); }
    }


    public void ClearName()
    {
        if (Input.GetKeyDown(inputManager.controls["Select"]) && nameTextObject.text.Length != 0) { nameTextObject.text = ""; }
    }

    public void AttemptConfirm()
    {

        if (nameTextObject.text == "DEV_TSTR!")
        {
            systemMessageScreen.SetActive(true);
            message.text = "Continue in developer mode?";
            actions.text = "[" + inputManager.controls["Interact"] + "] Confirm   [" + inputManager.controls["Action"] + "] Cancel";
            gameObject.GetComponent<OTU_Title_NameSelect>().enabled = false;
        }
        else if (nameTextObject.text == "DEV_SRV_HST!")
        {
            systemMessageScreen.SetActive(true);
            message.text = "Continue in server host mode?";
            actions.text = "[" + inputManager.controls["Interact"] + "] Confirm   [" + inputManager.controls["Action"] + "] Cancel";
            gameObject.GetComponent<OTU_Title_NameSelect>().enabled = false;
        }
        else if (nameTextObject.text == "DEV_SRV_CLI!")
        {
            systemMessageScreen.SetActive(true);
            message.text = "Continue in server client mode?";
            actions.text = "[" + inputManager.controls["Interact"] + "] Confirm   [" + inputManager.controls["Action"] + "] Cancel";
            gameObject.GetComponent<OTU_Title_NameSelect>().enabled = false;
        }
        else if (nameTextObject.text == "FOX" || nameTextObject.text == "FOX_YUNAMORA" || nameTextObject.text == "YUNAMORA")
        {
            systemMessageScreen.SetActive(true);
            message.text = "This will set the system to HARD MODE! Continue?";
            actions.text = "[" + inputManager.controls["Interact"] + "] Confirm   [" + inputManager.controls["Action"] + "] Cancel";
            gameObject.GetComponent<OTU_Title_NameSelect>().enabled = false;
        }
        else if (nameTextObject.text == "MIYU" || nameTextObject.text == "SAM" || nameTextObject.text == "CASEY" || nameTextObject.text == "NIRNA" || nameTextObject.text == "STRIKER" || nameTextObject.text == "LURURY")
        {
            systemMessageScreen.SetActive(true);
            message.text = nameTakenError;
            actions.text = "[" + inputManager.controls["Interact"] + "] Confirm   [" + inputManager.controls["Action"] + "] Cancel";
            gameObject.GetComponent<OTU_Title_NameSelect>().enabled = false;
        }
        else if (nameTextObject.text == "FOX_IS_DEAD")
        {
            systemMessageScreen.SetActive(true);
            message.text = "-_-";
            actions.text = "[" + inputManager.controls["Interact"] + "] OK";
            gameObject.GetComponent<OTU_Title_NameSelect>().enabled = false;
        }
        else if (nameTextObject.text == "ZOOSMELL" || nameTextObject.text == "POOPLORD")
        {
            systemMessageScreen.SetActive(true);
            message.text = "Try again, smartass!";
            actions.text = "[" + inputManager.controls["Interact"] + "] OK";
            gameObject.GetComponent<OTU_Title_NameSelect>().enabled = false;
        }
        else if (nameTextObject.text == "")
        {
            systemMessageScreen.SetActive(true);
            message.text = "The name can't be blank!";
            actions.text = "[" + inputManager.controls["Interact"] + "] OK";
            gameObject.GetComponent<OTU_Title_NameSelect>().enabled = false;
        }
        else
        {
            AcceptConfirm();
        }
    }

    public void AcceptConfirm()
    {
        PlayerPrefs.SetString("PlayerName", nameTextObject.text);
        saveManager.CreateSave();
        saveManager.Save();
        loadingScreen.GetComponent<Image>().enabled = true;
        loadingScreen.transform.GetChild(0).GetComponent<Image>().enabled = true;
        loadingScreen.transform.GetChild(1).GetComponent<Text>().enabled = true;
        saveManager.loadFileOnCreation = true;
        SceneManager.LoadScene(saveManager.activeSave2.scene);
        gameObject.SetActive(false);
    }
}
