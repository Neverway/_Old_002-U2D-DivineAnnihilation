//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: MRC
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OTU_System_TransitionManager : MonoBehaviour
{
    // Public variables
    public bool isBattleScene;
    public bool playTransition;

    // Reference variables
    private Transform exitPointTarget;
    private Transform playerTarget;
    private OTU_System_MenuManager menuManager;

    public Image fadeTransitionObject;
    public Image deathTransitionObject1;
    public Image deathTransitionObject2;
    public GameObject battleTransitionObject;
    public float fadeSpeed = 0.5f;
    public float deathFadeSpeed = 0.5f;
    public float fadeStayDelay = 0.6f;

    void Start()
    {
        menuManager = FindObjectOfType<OTU_System_MenuManager>();
        fadeTransitionObject.color = new Color(0, 0, 0, 1);
        if (deathTransitionObject1 != null)
        {
            deathTransitionObject1.color = new Color(255, 0, 0, 1);
            deathTransitionObject1.canvasRenderer.SetAlpha(0.0f);
            deathTransitionObject2.color = new Color(255, 0, 0, 1);
            deathTransitionObject2.canvasRenderer.SetAlpha(0.0f);
        }
        if (!isBattleScene)
        {
            StartCoroutine("TriggerFadeIn");
        }
        if (isBattleScene)
        {
            fadeTransitionObject.CrossFadeAlpha(0.0f, 0, false);
            BattleTransition(1);
        }
    }

    IEnumerator TriggerFade()
    {
        fadeTransitionObject.canvasRenderer.SetAlpha(0.0f);
        FadeIn("");
        yield return new WaitForSeconds(fadeStayDelay);
        FadeOut();
        yield return new WaitForSeconds(fadeStayDelay);
    }

    IEnumerator TriggerFadeOut()
    {
        fadeTransitionObject.canvasRenderer.SetAlpha(0.0f);
        FadeIn("");
        yield return new WaitForSeconds(fadeStayDelay);
    }

    IEnumerator TriggerFadeIn()
    {
        fadeTransitionObject.canvasRenderer.SetAlpha(1.0f);
        FadeOut();
        yield return new WaitForSeconds(fadeStayDelay);
    }

    IEnumerator DeathFadeOut()
    {
        yield return new WaitForSeconds(fadeStayDelay);
        deathTransitionObject1.CrossFadeAlpha(0.0f, fadeSpeed, false);
        deathTransitionObject2.CrossFadeAlpha(0.0f, fadeSpeed, false);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main_GameOver");
    }

    IEnumerator BattleLoadLevel()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Main_Battle");
    }

    public void FadeIn(string specialMode)
    {
        if (specialMode == "knockout")
        {
            menuManager.characterController.SetCameraIdleNoise(0.00f);
            fadeTransitionObject.CrossFadeAlpha(1.0f, deathFadeSpeed, false);
            deathTransitionObject1.CrossFadeAlpha(1.0f, deathFadeSpeed, false);
            deathTransitionObject2.CrossFadeAlpha(1.0f, deathFadeSpeed, false);
            StartCoroutine(DeathFadeOut());
        }
        else
        {
            fadeTransitionObject.CrossFadeAlpha(1.0f, fadeSpeed, false);
        }
    }

    void FadeOut()
    {
        fadeTransitionObject.CrossFadeAlpha(0.0f, fadeSpeed, false);
    }

    public void BattleTransition(int transitionPart)
    {
        if (transitionPart == 0)
        {
            battleTransitionObject.GetComponent<Animator>().Play("Trip");
            StartCoroutine(BattleLoadLevel());
            // Set a persistent variable to play the arrive animation on the start of the next scene <== Why the heck would you do this? There will never be a moment where
            //     you need don't have the transition when arriving in the battle scene! Just make it play on the start of that scene.
            // Change scene to battle scene
        }
        if (transitionPart == 1)
        {
            battleTransitionObject.GetComponent<Animator>().Play("Arrive");
        }
    }
}