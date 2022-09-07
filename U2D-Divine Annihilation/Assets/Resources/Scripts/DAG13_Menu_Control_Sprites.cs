//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Allow button inputs to scroll through and interacting with a menu
// Applied to: The root of a UI menu
// Editor script: 
// Notes: 
//
//=============================================================================

using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DAG13_Menu_Control_Sprites : MonoBehaviour
{
    //=-----------------=
    // Public variables
    //=-----------------=
    [Header("Colors")]
    [SerializeField] private Color selectedColor = new Color(1,1,1,1);
    [SerializeField] private Color unselectedColor = new Color(0.25f,0.25f,0.25f,1);
    
    [Header("Options")]
    [SerializeField] private bool wrapAroundScrolling;
    [SerializeField] private bool horizontalScrolling;
    [SerializeField] private SpriteOptions[] spriteOptions;
    [SerializeField] private UnityEvent onBack;
    
    [Header("Sound Effects")]
    [SerializeField] private AudioSource scroll;
    [SerializeField] private AudioSource selectSuccess;
    [SerializeField] private AudioSource selectDenied;


    //=-----------------=
    // Private variables
    //=-----------------=
    private int scrollIndex;
    private int indexLimit;
    private bool acceptingInput = true;


    //=-----------------=
    // Reference variables
    //=-----------------=
    private NUPInput input;


    //=-----------------=
    // Mono Functions
    //=-----------------=
    private void OnDisable()
    {
	    StopCoroutine(RepeatPressDelay());
	    acceptingInput = true;
    }
    
    private void Start()
    {
	    input = FindObjectOfType<NUPInput>();
	    indexLimit = spriteOptions.Length-1;
    }

    private IEnumerator RepeatPressDelay()
    {
	    yield return new WaitForSeconds(0.2f);
	    acceptingInput = true;
    }

    private void Update()
    {
	    IndexControl();
	    SetColorAndImage();
	    SelectControl();
    }
    
    
    //=-----------------=
    // Internal Functions
    //=-----------------=
    // Control scrolling through the menu and firing onHover events
    private void IndexControl()
    {
	    // Scroll up and down through the menu
	    if (!horizontalScrolling)
		{
			if (input.GetKey("Menu Up") || input.GetAxis("Menu Vertical") < -0.01f)
			{
				if (!acceptingInput) { return; }
				acceptingInput = false;
				if (scrollIndex > 0 && !wrapAroundScrolling) { scrollIndex--; PlayAudio("Scroll"); }
				else if (scrollIndex == 0 && wrapAroundScrolling) { scrollIndex = indexLimit; PlayAudio("Scroll"); }
				spriteOptions[scrollIndex].onHovered.Invoke();
				StartCoroutine(RepeatPressDelay());
			}
			else if (input.GetKey("Menu Down") || input.GetAxis("Menu Vertical") > 0.01f)
			{
				if (!acceptingInput) { return; }
				acceptingInput = false;
				if (scrollIndex < indexLimit && !wrapAroundScrolling) { scrollIndex++; PlayAudio("Scroll"); }
				else if (scrollIndex == indexLimit && wrapAroundScrolling) { scrollIndex = 0; PlayAudio("Scroll"); }
				spriteOptions[scrollIndex].onHovered.Invoke();
				StartCoroutine(RepeatPressDelay());
			}
		}
	    
	    // Scroll left and right through the menu
		else
		{
			if (input.GetKey("Menu Left") || input.GetAxis("Menu Horizontal") < -0.01f)
			{
				if (!acceptingInput) { return; }
				acceptingInput = false;
				if (scrollIndex > 0 && !wrapAroundScrolling) { scrollIndex--; PlayAudio("Scroll"); }
				else if (scrollIndex == 0 && wrapAroundScrolling) { scrollIndex = indexLimit; PlayAudio("Scroll"); }
				spriteOptions[scrollIndex].onHovered.Invoke();
				StartCoroutine(RepeatPressDelay());
			}
			else if (input.GetKey("Menu Right") || input.GetAxis("Menu Horizontal") > 0.01f)
			{
				if (!acceptingInput) { return; }
				acceptingInput = false;
				if (scrollIndex < indexLimit && !wrapAroundScrolling) { scrollIndex++; PlayAudio("Scroll"); }
				else if (scrollIndex == indexLimit && wrapAroundScrolling) { scrollIndex = 0; PlayAudio("Scroll"); }
				spriteOptions[scrollIndex].onHovered.Invoke();
				StartCoroutine(RepeatPressDelay());
			}
		}
    }

    // Set the colors of the sprite objects based on weather they are selected or not
    private void SetColorAndImage()
    {
	    for (int i = 0; i < indexLimit+1; i++)
	    {
		    if (i != scrollIndex)
		    {
			    spriteOptions[i].spriteObject.GetComponent<Image>().color = unselectedColor;
			    spriteOptions[i].spriteObject.GetComponent<Image>().sprite = spriteOptions[i].spriteNormal;
		    }
		    else
		    {
			    spriteOptions[i].spriteObject.gameObject.GetComponent<Image>().color = selectedColor;
			    spriteOptions[i].spriteObject.GetComponent<Image>().sprite = spriteOptions[i].spriteSelected;
		    }
	    }
    }

    // Handel selecting a menu option or hitting the back button
    private void SelectControl()
    {
	    if (input.GetKeyDown("Interact"))
	    {
		    PlayAudio("Select"); 
		    spriteOptions[scrollIndex].onSelected.Invoke();
	    }
	    else if (input.GetKeyDown("Action"))
	    {
		    onBack.Invoke();
	    }
    }

    private void PlayAudio(string audioSource)
    {
	    switch (audioSource)
	    {
		    case "Scroll":
		    {
			    if (scroll != null) { scroll.Play(); }
			    return;
		    }
		    case "Select":
		    {
			    switch (spriteOptions[scrollIndex].selectable)
			    {
				    case true when selectSuccess:
					    selectSuccess.Play();
					    break;
				    case false when selectDenied:
					    selectDenied.Play();
					    break;
			    }
			    return;
		    }
	    }
    }
    
    [Serializable]
    public class SpriteOptions
    {
	    public Sprite spriteNormal;
	    public Sprite spriteSelected;
	    public bool selectable = true;
	    public GameObject spriteObject;
	    public UnityEvent onHovered;
	    public UnityEvent onSelected;
    }
    
    
    //=-----------------=
    // External Functions
    //=-----------------=
    public void ResetScrollIndex()
    {
	    scrollIndex = 0;
    }
}
