//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using UnityEngine;

public class NUPTopdownGap : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=



    //=-----------------=
    // Private variables
    //=-----------------=



    //=-----------------=
    // Reference variables
    //=-----------------=



    //=-----------------=
    // Mono Functions
    //=-----------------=

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        
        // Player fell in the gap, subtract some HP and set them back to where they were before they went airborn
        other.GetComponent<NUPTopdownController>().ResetToGroundPosition(); 
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}
