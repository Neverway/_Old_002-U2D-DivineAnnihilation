//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OTU_System_TransitionManager : MonoBehaviour
{
    // Public variables
    public bool playTransition;

    // Reference variables
    private Transform exitPointTarget;
    private Transform playerTarget;
    private OTU_System_TransitionManager transitionManager;

    public Image FadeTransitionObject;
    public float FadeSpeed = 0.5f;
    public float FadeStayDelay = 0.6f;

    void Start()
    {
        FadeTransitionObject.color = new Color(0, 0, 0, 1);
        StartCoroutine("TriggerFadeIn");
    }

    IEnumerator TriggerFade()
    {
        FadeTransitionObject.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        yield return new WaitForSeconds(FadeStayDelay);
        FadeOut();
        yield return new WaitForSeconds(FadeStayDelay);
    }

    IEnumerator TriggerFadeOut()
    {
        FadeTransitionObject.canvasRenderer.SetAlpha(0.0f);

        FadeIn();
        yield return new WaitForSeconds(FadeStayDelay);
    }

    IEnumerator TriggerFadeIn()
    {
        FadeTransitionObject.canvasRenderer.SetAlpha(1.0f);


        FadeOut();
        yield return new WaitForSeconds(FadeStayDelay);
    }

    void FadeIn()
    {
        FadeTransitionObject.CrossFadeAlpha(1.0f, FadeSpeed, false);
    }

    void FadeOut()
    {
        FadeTransitionObject.CrossFadeAlpha(0.0f, FadeSpeed, false);
    }
}