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

public class DAG13_Menu_Control_Text : MonoBehaviour
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
    [SerializeField] private bool usingTextMeshPro;
    [SerializeField] private TextOptions[] textOption;
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
	    indexLimit = textOption.Length-1;
    }

    private IEnumerator RepeatPressDelay()
    {
	    yield return new WaitForSeconds(0.2f);
	    acceptingInput = true;
    }

    private void Update()
    {
	    IndexControl();
	    SetColorAndText();
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
				textOption[scrollIndex].onHovered.Invoke();
				StartCoroutine(RepeatPressDelay());
			}
			else if (input.GetKey("Menu Down") || input.GetAxis("Menu Vertical") > 0.01f)
			{
				if (!acceptingInput) { return; }
				acceptingInput = false;
				if (scrollIndex < indexLimit && !wrapAroundScrolling) { scrollIndex++; PlayAudio("Scroll"); }
				else if (scrollIndex == indexLimit && wrapAroundScrolling) { scrollIndex = 0; PlayAudio("Scroll"); }
				textOption[scrollIndex].onHovered.Invoke();
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
				textOption[scrollIndex].onHovered.Invoke();
				StartCoroutine(RepeatPressDelay());
			}
			else if (input.GetKey("Menu Right") || input.GetAxis("Menu Horizontal") > 0.01f)
			{
				if (!acceptingInput) { return; }
				acceptingInput = false;
				if (scrollIndex < indexLimit && !wrapAroundScrolling) { scrollIndex++; PlayAudio("Scroll"); }
				else if (scrollIndex == indexLimit && wrapAroundScrolling) { scrollIndex = 0; PlayAudio("Scroll"); }
				textOption[scrollIndex].onHovered.Invoke();
				StartCoroutine(RepeatPressDelay());
			}
		}
    }

    // Set the colors of the text objects based on weather they are selected or not
    private void SetColorAndText()
    {
	    for (int i = 0; i < indexLimit+1; i++)
	    {
		    if (i != scrollIndex)
		    {
			    if (!usingTextMeshPro)
			    {
				    textOption[i].textObject.GetComponent<Text>().color = unselectedColor;
				    textOption[i].textObject.GetComponent<Text>().text = textOption[i].textNormal;
			    }
			    else
			    {
				    textOption[i].textObject.GetComponent<TMP_Text>().color = unselectedColor;
				    textOption[i].textObject.GetComponent<TMP_Text>().text = textOption[i].textNormal;
			    }
		    }
		    else
		    {
			    if (!usingTextMeshPro)
			    {
				    textOption[i].textObject.gameObject.GetComponent<Text>().color = selectedColor;
				    textOption[i].textObject.GetComponent<Text>().text = textOption[i].textSelected;
			    }
			    else
			    {
				    textOption[i].textObject.gameObject.GetComponent<TMP_Text>().color = selectedColor;
				    textOption[i].textObject.GetComponent<TMP_Text>().text = textOption[i].textSelected;
			    }
		    }
	    }
    }

    // Handel selecting a menu option or hitting the back button
    private void SelectControl()
    {
	    if (input.GetKeyDown("Interact"))
	    {
		    PlayAudio("Select"); 
		    textOption[scrollIndex].onSelected.Invoke();
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
			    switch (textOption[scrollIndex].selectable)
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
    public class TextOptions
    {
	    public string textNormal;
	    public string textSelected;
	    public bool selectable = true;
	    public GameObject textObject;
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

    public void SetNormalText(int _index, string _text)
    {
	    textOption[_index].textNormal = _text;
    }

    public void SetSelectedText(int _index, string _text)
    {
	    textOption[_index].textSelected = _text;
    }
}
