//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Scroll through strings or text objects
// Applied to: A menu parent object in a scene
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;

public class Battle_Enemy_Selection : MonoBehaviour
{
    public Text enemy0;
    public Text enemy1;
    public Text enemy2;
    public Text enemy3;
    public GameObject self;
    public GameObject ActionMenu;
    private Menu_Scroll_String menu;
    private Battle_Turn_Manager turnManager;

    void Start()
    {
        menu = self.GetComponent<Menu_Scroll_String>();
        turnManager = FindObjectOfType<Battle_Turn_Manager>();
        if (PlayerPrefs.GetString("Enemy0") != "NULL")
        {
            menu.optionsBaseText[0] = PlayerPrefs.GetString("Enemy0");
            menu.optionsHoverText[0] = ">" + PlayerPrefs.GetString("Enemy0");
        }
        if (PlayerPrefs.GetString("Enemy1") != "NULL")
        {
            menu.optionsBaseText[1] = PlayerPrefs.GetString("Enemy1");
            menu.optionsHoverText[1] = ">" + PlayerPrefs.GetString("Enemy1");
        }
        if (PlayerPrefs.GetString("Enemy2") != "NULL")
        {
            menu.optionsBaseText[2] = PlayerPrefs.GetString("Enemy2");
            menu.optionsHoverText[2] = ">" + PlayerPrefs.GetString("Enemy2");
        }
        if (PlayerPrefs.GetString("Enemy3") != "NULL")
        {
            menu.optionsBaseText[3] = PlayerPrefs.GetString("Enemy3");
            menu.optionsHoverText[3] = ">" + PlayerPrefs.GetString("Enemy3");
        }

    }


    public void Update()
    {
        // Go back to action menu
        if (Input.GetKeyDown("x"))
        {
            ActionMenu.GetComponent<Battle_ActionFunctions>().acceptingInput = true;
            ActionMenu.SetActive(true);
            self.SetActive(false);
        }

        if (Input.GetKeyDown("z"))
        {
            if (menu.currentSelection == 0 && PlayerPrefs.GetString("Enemy0") != "NULL")
            {
                turnManager.SetMoveAttack(0);
            }
            if (menu.currentSelection == 1 && PlayerPrefs.GetString("Enemy1") != "NULL")
            {
                turnManager.SetMoveAttack(1);
            }
            if (menu.currentSelection == 2 && PlayerPrefs.GetString("Enemy2") != "NULL")
            {
                turnManager.SetMoveAttack(2);
            }
            if (menu.currentSelection == 3 && PlayerPrefs.GetString("Enemy3") != "NULL")
            {
                turnManager.SetMoveAttack(3);
            }
        }
    }
}