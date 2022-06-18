//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DAG12_System_ConfigurationManager : MonoBehaviour
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
    [Header ("Program Version")]
    [SerializeField] private string pretag;
    [SerializeField] private int developmentGeneration;
    [SerializeField] private int major;
    [SerializeField] private int minor;

    [SerializeField] private Text titleVersionText;
    
    [Header ("(READ-ONLY) Runtime compiled version")]
    [SerializeField] private string programVersion;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
        programVersion = (pretag + "[G" + developmentGeneration + "." + major + "." + minor + "]");
        if (titleVersionText != null) titleVersionText.text = programVersion;
    }

    private void Update()
    {
	
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}
