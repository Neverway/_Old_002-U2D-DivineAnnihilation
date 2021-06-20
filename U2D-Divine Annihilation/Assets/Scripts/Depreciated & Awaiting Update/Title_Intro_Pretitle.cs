//=========== Written by Arthur W. Sheldon AKA Lizband_UCC =============================
//
// Purpose: Animate the pre-title message and transition to the opening on a keypress
// Applied to: Config object in a scene
//
//======================================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Title_Intro_Pretitle : MonoBehaviour
{
    public Text[] pretitleText;
    public Text continueText;
    public Image fadeObject;
    public float initialDelay;
    [Range (0, 1)]
    public float TextFadeSpeed;
    public float continueDelay;
    public bool continueFirstFade;
    public UnityEvent onFinish;

    private bool startedPretitleTextAppear;
    private bool startedContinueTextAppear;
    private bool introComplete;

    private System_InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        inputManager = FindObjectOfType<System_InputManager>();
        //continueText.text = "[" + inputManager.controls["Interact"].ToString() + "]";

        // Make all text invisible at start
        continueText.color = new Color(255, 255, 255, 0);
        foreach (var obj in pretitleText)
        {
            obj.color = new Color(255, 255, 255, 0);
        }
        StartCoroutine("startPretitleTextAppear");
        StartCoroutine("startContinueTextAppear");
    }


    IEnumerator startPretitleTextAppear()
    {
        yield return new WaitForSeconds(initialDelay);
        startedPretitleTextAppear = true;
    }

    IEnumerator startContinueTextAppear()
    {
        yield return new WaitForSeconds(continueDelay);
        continueText.text = "Press [" + inputManager.controls["Interact"].ToString() + "] to continue";
        startedContinueTextAppear = true;
    }


    IEnumerator pretitleTextAppear()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (var obj in pretitleText)
        {
            obj.color = new Color(255, 255, 255, pretitleText[0].color.a + TextFadeSpeed);
        }
    }


    IEnumerator continueTextAppear()
    {
        yield return new WaitForSeconds(0.2f);
        continueText.color = new Color(255, 255, 255, continueText.color.a + TextFadeSpeed);
    }


    IEnumerator fadeout()
    {
        yield return new WaitForSeconds(0.001f);
        fadeObject.color = new Color(0, 0, 0, fadeObject.color.a + (TextFadeSpeed + 0.04f));
        if (fadeObject.color.a < 1)
        {
            StartCoroutine("fadeout");
        }
        else
        {
            onFinish.Invoke();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!introComplete)
        {
            if (pretitleText[0].color.a < 1f && startedPretitleTextAppear)
            {
                StartCoroutine("pretitleTextAppear");
            }
            if (continueText.color.a < 1f && startedContinueTextAppear && !continueFirstFade)
            {
                StartCoroutine("continueTextAppear");
            }
            if (continueText.color.a >= 1f && startedContinueTextAppear)
            {
                continueFirstFade = true;
                continueText.gameObject.GetComponent<Animator>().enabled = true;
                introComplete = true;
            }
        }

        if (Input.GetKeyDown(inputManager.controls["Interact"]) && introComplete)
        {
            StartCoroutine("fadeout");
        }

        if (Input.GetKeyDown(inputManager.controls["Interact"]) && !introComplete)
        {
            StopAllCoroutines();
            continueText.color = new Color(255, 255, 255, 1);
            continueText.text = "Press [" + inputManager.controls["Interact"].ToString() + "] to continue";
            continueText.gameObject.GetComponent<Animator>().enabled = true;
            foreach (var obj in pretitleText)
            {
                obj.color = new Color(255, 255, 255, 1);
            }
            introComplete = true;
        }
    }
}
