using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class System_InputManager : MonoBehaviour
{
    public Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();


    // Start is called before the first frame update
    void Start()
    {
        // Load current keys that are being stored in memory, if there are not any, set some defaults
        controls.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Up", "UpArrow")));
        controls.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Down", "DownArrow")));
        controls.Add("Left", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "LeftArrow")));
        controls.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "RightArrow")));

        controls.Add("Interact", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "Z")));
        controls.Add("Action", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Action", "X")));
        controls.Add("Select", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Select", "C")));
        controls.Add("Menu", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Menu", "Escape")));

        controls.Add("Special 1", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Special 1", "Alpha1")));
        controls.Add("Special 2", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Special 2", "Alpha2")));
        controls.Add("Special 3", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Special 3", "Alpha3")));
        controls.Add("Special 4", (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Special 4", "Alpha4")));
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.F1))
    //    {
    //        Debug.Log("SIM Up = " + controls["Up"]);
    //        Debug.Log("SIM Down = " + controls["Down"]);
    //        Debug.Log("SIM Left = " + controls["Left"]);
    //        Debug.Log("SIM Right = " + controls["Right"]);

    //        Debug.Log("SIM Interact = " + controls["Interact"]);
    //        Debug.Log("SIM Action = " + controls["Action"]);
    //        Debug.Log("SIM Select = " + controls["Select"]);
    //        Debug.Log("SIM Menu = " + controls["Menu"]);

    //        Debug.Log("SIM Special 1 = " + controls["Special 1"]);
    //        Debug.Log("SIM Special 2 = " + controls["Special 2"]);
    //        Debug.Log("SIM Special 3 = " + controls["Special 3"]);
    //        Debug.Log("SIM Special 4 = " + controls["Special 4"]);
    //    }
    //}
}
