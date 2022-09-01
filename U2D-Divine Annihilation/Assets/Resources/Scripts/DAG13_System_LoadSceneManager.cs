//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Display a loading bar until a target scene is loaded
// Applied to: Local system manager object on the loading screen scene
// Editor script: 
// Notes: The target "LoadingSceneID" is set by FUR_System_LoadScene
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DAG13_System_LoadSceneManager : MonoBehaviour
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
    [SerializeField] private Image loadingBar; // A reference to the loading bar


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
		// Start Async scene load
		StartCoroutine(LoadAsyncOperation());
    }

    private IEnumerator LoadAsyncOperation()
    {
	    // Create an async operation (Will automatically switch to target scene once it's finished loading)
	    AsyncOperation gameLevel = SceneManager.LoadSceneAsync(PlayerPrefs.GetString("LoadingSceneID"));
	    
	    while (gameLevel.progress < 1)
	    {
		    // Set loading bar to reflect async progress
		    loadingBar.fillAmount = gameLevel.progress;
		    yield return new WaitForEndOfFrame();
	    }
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
}
