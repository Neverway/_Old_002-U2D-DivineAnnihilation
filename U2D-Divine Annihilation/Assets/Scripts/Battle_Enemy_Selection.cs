//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Show the names of the selectable enemies in a battle
// Applied to: A enemy selection menu in a battle scene
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;

public class Battle_Enemy_Selection : MonoBehaviour
{
    // Public Variabless
    public Text enemy0;
    public Text enemy1;
    public Text enemy2;
    public Text enemy3;
    public GameObject ActionMenu;

    // Private Variables
    private Menu_Scroll_String menu;
    private Battle_Turn_Manager turnManager;
    private System_InputManager inputManager;

    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
        menu = gameObject.GetComponent<Menu_Scroll_String>();
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
        if (Input.GetKeyDown(inputManager.controls["Action"]))
        {
            ActionMenu.GetComponent<Battle_ActionFunctions>().acceptingInput = true;
            ActionMenu.SetActive(true);
            gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(inputManager.controls["Interact"]))
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