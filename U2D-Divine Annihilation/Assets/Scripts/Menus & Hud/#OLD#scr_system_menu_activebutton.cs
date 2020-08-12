using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_system_menu_activebutton : MonoBehaviour
{
    public Button selectedButton;

    // Start is called before the first frame update
    void Start()
    {
        selectedButton.Select();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
