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
    private Menu_Scroll_String menu;

    void Start()
    {
        menu = self.GetComponent<Menu_Scroll_String>();
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
}