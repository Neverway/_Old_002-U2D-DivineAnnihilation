using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Title_Options_Keybind : MonoBehaviour
{
    public Text up, down, left, right, interact, action, select, menu, special1, special2, special3, special4;
    public GameObject upObject, downObject, leftObject, rightObject, interactObject, actionObject, selectObject, menuObject, special1Object, special2Object, special3Object, special4Object;
    public GameObject bindingScreen;
    public bool active = true;
    public GameObject optionsGameObject;
    public GameObject menuGameobject;
    private GameObject currentKey;
    public Menu_Scroll_String menuScrollString;
    private System_InputManager inputManager;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("TOK: " + "Start fired");
        menuScrollString = FindObjectOfType<Menu_Scroll_String>();
        inputManager = FindObjectOfType<System_InputManager>();


        // Display text
        up.text = inputManager.controls["Up"].ToString();
        down.text = inputManager.controls["Down"].ToString();
        left.text = inputManager.controls["Left"].ToString();
        right.text = inputManager.controls["Right"].ToString();
        interact.text = inputManager.controls["Interact"].ToString();
        action.text = inputManager.controls["Action"].ToString();
        select.text = inputManager.controls["Select"].ToString();
        menu.text = inputManager.controls["Menu"].ToString();
        special1.text = inputManager.controls["Special 1"].ToString();
        special2.text = inputManager.controls["Special 2"].ToString();
        special3.text = inputManager.controls["Special 3"].ToString();
        special4.text = inputManager.controls["Special 4"].ToString();

    }


    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.05f);
        active = true;
        menuScrollString.active = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Action") && active)
        {
            menuScrollString.currentSelection = 0;
            optionsGameObject.SetActive(true);
            menuGameobject.SetActive(false);
        }

        if (Input.GetButtonDown("Interact") && active)
        {
            if (menuScrollString.currentSelection == 0) { ChangeKey(upObject); }
            else if (menuScrollString.currentSelection == 1) { ChangeKey(downObject); }
            else if (menuScrollString.currentSelection == 2) { ChangeKey(leftObject); }
            else if (menuScrollString.currentSelection == 3) { ChangeKey(rightObject); }

            else if (menuScrollString.currentSelection == 4) { ChangeKey(interactObject); }
            else if (menuScrollString.currentSelection == 5) { ChangeKey(actionObject); }
            else if (menuScrollString.currentSelection == 6) { ChangeKey(selectObject); }
            else if (menuScrollString.currentSelection == 7) { ChangeKey(menuObject); }

            else if (menuScrollString.currentSelection == 8) { ChangeKey(special1Object); }
            else if (menuScrollString.currentSelection == 9) { ChangeKey(special2Object); }
            else if (menuScrollString.currentSelection == 10) { ChangeKey(special3Object); }
            else if (menuScrollString.currentSelection == 11) { ChangeKey(special4Object); }

            else if (menuScrollString.currentSelection == 12)
            {
                ResetKeys();
            }

            else if (menuScrollString.currentSelection == 13)
            {
                menuScrollString.currentSelection = 0;
                optionsGameObject.SetActive(true);
                menuGameobject.SetActive(false);
            }
        }
    }


    void OnGUI()
    {
        if (currentKey != null && Event.current.isKey && Event.current.type == EventType.KeyDown)
        {
            Debug.Log("TOK: " + "Changed key.");
            inputManager.controls[currentKey.name] = Event.current.keyCode;
            Debug.Log(Event.current.keyCode);
            Debug.Log(inputManager.controls[currentKey.name]);
            SaveKeys();
            bindingScreen.SetActive(false);
            StartCoroutine("Delay");
            currentKey.transform.GetChild(1).GetComponent<Text>().text = Event.current.keyCode.ToString();
            currentKey = null;
        }
    }


    public void ChangeKey(GameObject passKey)
    {
        Debug.Log("TOK: " + "Changing key...");
        currentKey = passKey;
        currentKey.transform.GetChild(1).GetComponent<Text>().text = "...";
        bindingScreen.SetActive(true);
        active = false;
        menuScrollString.active = false;
    }


    public void ResetKeys()
    {
        currentKey = upObject;
        inputManager.controls[currentKey.name] = KeyCode.UpArrow;
        currentKey.transform.GetChild(1).GetComponent<Text>().text = KeyCode.UpArrow.ToString();
        currentKey = downObject;
        inputManager.controls[currentKey.name] = KeyCode.DownArrow;
        currentKey.transform.GetChild(1).GetComponent<Text>().text = KeyCode.DownArrow.ToString();
        currentKey = leftObject;
        inputManager.controls[currentKey.name] = KeyCode.LeftArrow;
        currentKey.transform.GetChild(1).GetComponent<Text>().text = KeyCode.LeftArrow.ToString();
        currentKey = rightObject;
        inputManager.controls[currentKey.name] = KeyCode.RightArrow;
        currentKey.transform.GetChild(1).GetComponent<Text>().text = KeyCode.RightArrow.ToString();


        currentKey = interactObject;
        inputManager.controls[currentKey.name] = KeyCode.Z;
        currentKey.transform.GetChild(1).GetComponent<Text>().text = KeyCode.Z.ToString();
        currentKey = actionObject;
        inputManager.controls[currentKey.name] = KeyCode.X;
        currentKey.transform.GetChild(1).GetComponent<Text>().text = KeyCode.X.ToString();
        currentKey = selectObject;
        inputManager.controls[currentKey.name] = KeyCode.C;
        currentKey.transform.GetChild(1).GetComponent<Text>().text = KeyCode.C.ToString();
        currentKey = menuObject;
        inputManager.controls[currentKey.name] = KeyCode.Escape;
        currentKey.transform.GetChild(1).GetComponent<Text>().text = KeyCode.Escape.ToString();


        currentKey = special1Object;
        inputManager.controls[currentKey.name] = KeyCode.Alpha1;
        currentKey.transform.GetChild(1).GetComponent<Text>().text = KeyCode.Alpha1.ToString();
        currentKey = special2Object;
        inputManager.controls[currentKey.name] = KeyCode.Alpha2;
        currentKey.transform.GetChild(1).GetComponent<Text>().text = KeyCode.Alpha2.ToString();
        currentKey = special3Object;
        inputManager.controls[currentKey.name] = KeyCode.Alpha3;
        currentKey.transform.GetChild(1).GetComponent<Text>().text = KeyCode.Alpha3.ToString();
        currentKey = special4Object;
        inputManager.controls[currentKey.name] = KeyCode.Alpha4;
        currentKey.transform.GetChild(1).GetComponent<Text>().text = KeyCode.Alpha4.ToString();


        SaveKeys();
        currentKey = null;
    }


    public void SaveKeys()
    {
        foreach (var key in inputManager.controls)
        {
            PlayerPrefs.SetString(key.Key, key.Value.ToString());
            Debug.Log("****Key: " + key.Key + " ==> " + key.Value.ToString());
        }
        Debug.Log("TOK: " + "Key saved!");
        PlayerPrefs.Save();
    }
}
