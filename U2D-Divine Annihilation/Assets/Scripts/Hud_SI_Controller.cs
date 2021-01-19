using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud_SI_Controller : MonoBehaviour
{
    public bool siBoxActive;

    public void toggleSIBoxActive()
    {
        if (siBoxActive)
        {
            siBoxActive = false;
        }
        else if (!siBoxActive)
        {
            siBoxActive = true;
        }
    }
}
