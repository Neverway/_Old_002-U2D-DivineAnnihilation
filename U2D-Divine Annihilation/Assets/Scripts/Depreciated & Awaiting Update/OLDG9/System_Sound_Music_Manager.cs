using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class System_Sound_Music_Manager : MonoBehaviour
{
    private static System_Sound_Music_Manager instance;

    void Awake()
    {
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
