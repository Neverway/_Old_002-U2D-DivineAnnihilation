using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Sound_Music_Manager : MonoBehaviour
{
    private static System_Sound_Music_Manager instance;

    
    void Awake()
    {
        Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
