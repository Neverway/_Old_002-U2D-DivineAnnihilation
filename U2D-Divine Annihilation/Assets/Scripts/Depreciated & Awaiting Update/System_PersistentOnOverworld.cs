//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ===========================
//
// Purpose: Don't destroy objects on scene change
// Applied to: Any overworld object that should not be destroyed when changing scenes
//
//====================================================================================

using UnityEngine;

public class System_PersistentOnOverworld : MonoBehaviour
{

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }


    void Update()
    {
        if (GameObject.FindWithTag("DestroyPresistentOverworldObjects"))
        {
            Destroy(this);
        }
    }
}
