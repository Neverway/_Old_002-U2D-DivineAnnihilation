//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: MRC
// Purpose: Make an entity make look like it's walking through water on entry
// Applied to: A volume trigger in an overworld scene
// Editor script: 
// Notes: 
//
//=============================================================================

using UnityEngine;

public class DA_Trigger_Effect_Water : MonoBehaviour
{
    // Enable the water effect
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DA_Entity_Effects>() != null)
        {
            other.gameObject.GetComponent<DA_Entity_Effects>().EnterWater();
            other.gameObject.transform.position = new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.y - 0.25f);
        }
    }

    // Keep the water effect enabled if you walk between two water triggers
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DA_Entity_Effects>() != null)
        {
            other.gameObject.GetComponent<DA_Entity_Effects>().EnterWater();
        }
    }

    // Disable the water trigger
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<DA_Entity_Effects>() != null)
        {
            other.gameObject.GetComponent<DA_Entity_Effects>().ExitEffectVolume();
            other.gameObject.transform.position = new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 0.25f);
        }
    }
}
