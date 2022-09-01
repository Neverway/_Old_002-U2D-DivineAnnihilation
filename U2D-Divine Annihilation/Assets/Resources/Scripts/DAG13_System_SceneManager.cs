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
using UnityEngine.SceneManagement;

public class DAG13_System_SceneManager : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    [SerializeField] private string sceneID;
    [SerializeField] private float delayBeforeLoad;


    //=-----------------=
    // Private variables
    //=-----------------=


    //=-----------------=
    // Reference variables
    //=-----------------=


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private static IEnumerator Load(string _sceneName, float _delayBeforeLoad)
    {
        PlayerPrefs.SetString("LoadingSceneID", _sceneName);
        yield return new WaitForSeconds(_delayBeforeLoad);
        SceneManager.LoadScene("Loading"); // Switch to loading screen  
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void LoadScene(string _sceneName, float _delayBeforeLoad)
    {
        StartCoroutine(Load(_sceneName, _delayBeforeLoad));
    }
}
