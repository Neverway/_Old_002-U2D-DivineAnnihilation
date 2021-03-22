using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemFadeTransition : MonoBehaviour
{
    public Image FadeTransitionObject;
    public float FadeSpeed;
    public float FadeStayDelay;

    void Start()
    {
        FadeTransitionObject.color = new Color(0, 0, 0, 1);
        StartCoroutine("TriggerFadeIn");
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown("j"))
        {
            Debug.Log("Trigger fade");
            StartCoroutine("TriggerFade");
        }
        if (Input.GetKeyDown("k"))
        {
            Debug.Log("Trigger fade in");
            StartCoroutine("TriggerFadeIn");
        }
        if (Input.GetKeyDown("l"))
        {
            Debug.Log("Trigger fade out");
            StartCoroutine("TriggerFadeOut");
        }
        */
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
        FadeTransitionObject.CrossFadeAlpha (0.0f, FadeSpeed, false);  
    }

}
