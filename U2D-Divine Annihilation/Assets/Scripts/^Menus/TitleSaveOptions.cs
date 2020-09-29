using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSaveOptions : MonoBehaviour
{
    public bool onLoadScreen;
    public Text fileName;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
