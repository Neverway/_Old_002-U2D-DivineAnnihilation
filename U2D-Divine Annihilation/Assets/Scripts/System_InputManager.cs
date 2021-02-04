using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class System_InputManager : MonoBehaviour
{
    public Title_Options_Keybind GlobalInput;


    // Start is called before the first frame update
    void Start()
    {
        GlobalInput = FindObjectOfType<Title_Options_Keybind>();
        Debug.Log(GlobalInput.controls["Up"]);
    }
}
