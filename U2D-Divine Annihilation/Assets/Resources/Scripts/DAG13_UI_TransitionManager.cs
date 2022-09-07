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

public class DAG13_UI_TransitionManager : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    [SerializeField] private bool fadeinOnStart;


    //=-----------------=
    // Private variables
    //=-----------------=
    
    public bool fadeoutActive;
    public bool fadeinActive;
    
    public float fadeSpeed; // The value to add or subtract from the alpha on each loop
    public float holdTime; // How long in seconds to wait before fading back from black in FadeTransition()


    //=-----------------=
    // Reference variables
    //=-----------------=
    private Image blackoutImage;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void Start()
    {
	    blackoutImage = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
    }

    private IEnumerator HoldBlack()
    {
	    yield return new WaitForSeconds(holdTime);
	    Fadein(fadeSpeed);
    }

    private void Update()
    {
	    if (fadeinOnStart)
	    {
		    blackoutImage.color = new Color(0, 0, 0, 1);
		    Fadein(fadeSpeed);
		    fadeinOnStart = false;
	    }

	    if (fadeoutActive)
	    {
		    var imageColor = blackoutImage.color;
		    switch (imageColor.a < 1)
		    {
			    case true:
			    {
				    print("case true");
				    blackoutImage.color = new Color(0, 0, 0, imageColor.a+fadeSpeed);
				    break;
			    }
			    default:
				    print("case false");
				    fadeoutActive = false;
				    break;
		    }
	    }

	    if (fadeinActive)
	    {
		    var imageColor = blackoutImage.color;
		    switch (imageColor.a > 0)
		    {
			    case true:
			    {
				    print("case true");
				    blackoutImage.color = new Color(0, 0, 0, imageColor.a-fadeSpeed);
				    break;
			    }
			    default:
				    print("case false");
				    fadeinActive = false;
				    break;
		    }
	    }
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=


    //=-----------------=
    // External Functions
    //=-----------------=
    // Fade to black then fade black
    public void FadeTransition(float _fadeSpeed, float _holdTime)
    {
	    Fadeout(_fadeSpeed);
	    StartCoroutine(HoldBlack());
    }
    
    // Fade the screen to black and hold
    public void Fadeout(float _fadeSpeed)
    {
	    if (fadeinActive) return;
	    fadeoutActive = true;
    }
    
    // Fade the screen from black and hold
    public void Fadein(float _fadeSpeed)
    {
	    if (fadeoutActive) return;
	    fadeinActive = true;
    }
}
