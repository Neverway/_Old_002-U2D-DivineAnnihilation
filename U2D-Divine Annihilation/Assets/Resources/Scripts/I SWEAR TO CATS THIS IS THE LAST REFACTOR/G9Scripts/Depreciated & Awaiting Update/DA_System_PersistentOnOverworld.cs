//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: CAC
// Purpose: Don't destroy objects on scene change
// Applied to: Any object that should not be destroyed when changing scenes
// Editor script: 
// Notes: 
//
//=============================================================================


using UnityEngine;

public class DA_System_PersistentOnOverworld : MonoBehaviour
{

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);    // Set this item to be persistent (it won't be destroyed when changing scenes)
    }

    void Update()
    {
        if (GameObject.FindWithTag("DestroyPersistentOverworldObjects"))
        {
            print("Duplicate persistant object has been found and removed!");
            Destroy(this);  // Destroy any duplicates of this item
        }   
    }
}
