using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Choicebox_Manager : MonoBehaviour
{
    public GameObject choiceBoxObject;
    public string[] options;
    public Text choice1;
    public Text choice2;
    public Text choice3;
    public Text choice4;
    public bool choiceBoxActive;
    public Trigger_PlayerChoice currentTarget;
    private System_Config_Manager global;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    // Start is called before the first frame update
    void Start()
    {
        global = FindObjectOfType<System_Config_Manager>(); // Find the config script
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableChoiceBox()
    {
        if (!global.menuActive && !choiceBoxActive)
        {
            Debug.Log("enabledCB");
            choiceBoxObject.SetActive(true);
            choiceBoxActive = true;
            if (currentTarget.options.Length == 2)
            {
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsBaseText[0] = currentTarget.options[0];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsBaseText[1] = currentTarget.options[1];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsBaseText[2] = null;
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsBaseText[3] = null;
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsHoverText[0] = ">" + currentTarget.options[0];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsHoverText[1] = ">" + currentTarget.options[1];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsHoverText[2] = null;
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsHoverText[3] = null;
            }
            if (currentTarget.options.Length == 3)
            {
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsBaseText[0] = currentTarget.options[0];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsBaseText[1] = currentTarget.options[1];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsBaseText[2] = currentTarget.options[2];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsBaseText[3] = null;
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsHoverText[0] = ">" + currentTarget.options[0];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsHoverText[1] = ">" + currentTarget.options[1];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsHoverText[2] = ">" + currentTarget.options[2];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsHoverText[3] = null;
            }
            if (currentTarget.options.Length == 4)
            {
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsBaseText[0] = currentTarget.options[0];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsBaseText[1] = currentTarget.options[1];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsBaseText[2] = currentTarget.options[2];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsBaseText[3] = currentTarget.options[3];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsHoverText[0] = ">" + currentTarget.options[0];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsHoverText[1] = ">" + currentTarget.options[1];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsHoverText[2] = ">" + currentTarget.options[2];
                choiceBoxObject.GetComponent<Menu_Scroll_String>().optionsHoverText[3] = ">" + currentTarget.options[3];
            }
        }
    }
    public void disableChoiceBox()
    {
        if (global.menuActive && choiceBoxActive)
        {
            choiceBoxObject.SetActive(false);
            choiceBoxActive = false;
        }
    }
}
