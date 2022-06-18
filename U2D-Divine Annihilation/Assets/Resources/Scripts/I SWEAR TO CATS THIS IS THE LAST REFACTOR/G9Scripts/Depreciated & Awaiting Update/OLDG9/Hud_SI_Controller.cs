using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hud_SI_Controller : MonoBehaviour
{
    public bool siBoxActive;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}
    
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
