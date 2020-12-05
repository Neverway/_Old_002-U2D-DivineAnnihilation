//=========== Written by Arthur W. Sheldon AKA Lizband_UCC =============================
//
// Purpose: Give functionality to the load and delete file buttons on the title screen
// Applied to: SaveFile menu on the title screen
//
//======================================================================================

using UnityEngine;
using UnityEngine.UI;

public class Title_Save_Options : MonoBehaviour
{
    public bool onLoadScreen;
    public Text fileName;

    void Update()
    {
        if (PlayerPrefs.GetString("Current Save Profile") == "SlotOne")
        {
            if (onLoadScreen)
            {
                fileName.text = "File 1";
            }

            if (!onLoadScreen)
            {
                fileName.text = "Delete file 1?";
            }
        }

        if (PlayerPrefs.GetString("Current Save Profile") == "SlotTwo")
        {
            if (onLoadScreen)
            {
                fileName.text = "File 2";
            }

            if (!onLoadScreen)
            {
                fileName.text = "Delete file 2?";
            }
        }

        if (PlayerPrefs.GetString("Current Save Profile") == "SlotThree")
        {
            if (onLoadScreen)
            {
                fileName.text = "File 3";
            }

            if (!onLoadScreen)
            {
                fileName.text = "Delete file 3?";
            }
        }

        if (PlayerPrefs.GetString("Current Save Profile") == "SlotFour")
        {
            if (onLoadScreen)
            {
                fileName.text = "File 4";
            }

            if (!onLoadScreen)
            {
                fileName.text = "Delete file 4?";
            }
        }
    }
}
