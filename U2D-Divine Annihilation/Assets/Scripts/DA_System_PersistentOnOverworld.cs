//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ===========================
//
// Purpose: Don't destroy objects on scene change
// Applied to: Any overworld object that should not be destroyed when changing scenes
//
//====================================================================================

using UnityEngine;

public class DA_System_PersistentOnOverworld : MonoBehaviour
{

    void Start()
    {
        // Set this item to be persistent (it won't be destroyed when changing scenes)
        DontDestroyOnLoad(transform.gameObject);
    }


    void Update()
    {
        // Destroy any duplicates of this item
        if (GameObject.FindWithTag("DestroyPresistentOverworldObjects"))
        {
            Destroy(this);
        }
    }
}
