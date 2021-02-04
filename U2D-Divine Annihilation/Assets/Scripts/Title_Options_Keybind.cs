using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Title_Options_Keybind : MonoBehaviour
{
    private Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();
    public Text up, down, left, right, interact, action, select, menu, special1, special2, special3, special4;
    public GameObject upObject, downObject, leftObject, rightObject, interactObject, actionObject, selectObject, menuObject, special1Object, special2Object, special3Object, special4Object;
    public GameObject bindingScreen;
    public bool active = true;
    public GameObject optionsGameObject;
    public GameObject menuGameobject;
    private GameObject currentKey;
    private Menu_Scroll_String menuScrollString;


    // Start is called before the first frame update
    void Start()
    {
        menuScrollString = FindObjectOfType<Menu_Scroll_String>();

        // Controls
        controls.Add("Up", KeyCode.UpArrow);
        controls.Add("Down", KeyCode.DownArrow);
        controls.Add("Left", KeyCode.LeftArrow);
        controls.Add("Right", KeyCode.RightArrow);

        controls.Add("Interact", KeyCode.Z);
        controls.Add("Action", KeyCode.X);
        controls.Add("Select", KeyCode.C);
        controls.Add("Menu", KeyCode.Escape);

        controls.Add("Special1", KeyCode.Z);
        controls.Add("Special2", KeyCode.X);
        controls.Add("Special3", KeyCode.C);
        controls.Add("Special4", KeyCode.V);


        // Display text
        up.text = controls["Up"].ToString();
        down.text = controls["Down"].ToString();
        left.text = controls["Left"].ToString();
        right.text = controls["Right"].ToString();
        interact.text = controls["Interact"].ToString();
        action.text = controls["Action"].ToString();
        select.text = controls["Select"].ToString();
        menu.text = controls["Menu"].ToString();
        special1.text = controls["Special1"].ToString();
        special2.text = controls["Special2"].ToString();
        special3.text = controls["Special3"].ToString();
        special4.text = controls["Special4"].ToString();

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
            if (menuScrollString.currentSelection == 1) { ChangeKey(downObject); }
            if (menuScrollString.currentSelection == 2) { ChangeKey(leftObject); }
            if (menuScrollString.currentSelection == 3) { ChangeKey(rightObject); }

            if (menuScrollString.currentSelection == 4) { ChangeKey(interactObject); }
            if (menuScrollString.currentSelection == 5) { ChangeKey(actionObject); }
            if (menuScrollString.currentSelection == 6) { ChangeKey(selectObject); }
            if (menuScrollString.currentSelection == 7) { ChangeKey(menuObject); }

            if (menuScrollString.currentSelection == 8) { ChangeKey(special1Object); }
            if (menuScrollString.currentSelection == 9) { ChangeKey(special2Object); }
            if (menuScrollString.currentSelection == 10) { ChangeKey(special3Object); }
            if (menuScrollString.currentSelection == 11) { ChangeKey(special4Object); }

            if (menuScrollString.currentSelection == 13)
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
            controls[currentKey.name] = Event.current.keyCode;
            bindingScreen.SetActive(false);
            StartCoroutine("Delay");
            currentKey.transform.GetChild(1).GetComponent<Text>().text = Event.current.keyCode.ToString();
            currentKey = null;
        }
    }


    public void ChangeKey(GameObject passKey)
    {
        currentKey = passKey;
        currentKey.transform.GetChild(1).GetComponent<Text>().text = "...";
        bindingScreen.SetActive(true);
        active = false;
        menuScrollString.active = false;
    }
}
