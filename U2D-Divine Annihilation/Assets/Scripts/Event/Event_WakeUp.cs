using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_WakeUp : MonoBehaviour
{
    public bool allowInput;
    public bool animationFinished;
    public Animator animator;
    public GameObject playerCharacter;
    public GameObject playerCharacterShadow;
    private System_Config_Manager configManager;
    private SaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        configManager = FindObjectOfType<System_Config_Manager>();
        if (saveManager.activeSave.c1s1_0 == 1)
        {
            configManager.overrideCanMove = false;
            gameObject.SetActive(false);
        }
        else
        {
            configManager.overrideCanMove = true;
            playerCharacter.GetComponent<SpriteRenderer>().enabled = false;
            playerCharacterShadow.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical") || Input.GetButtonDown("Interact"))
        {
            if (allowInput)
            {
                animator.SetFloat("animationSpeed", 1f);
            }
        }
        if (animationFinished)
        {
            configManager.overrideCanMove = false;
            playerCharacter.GetComponent<SpriteRenderer>().enabled = true;
            playerCharacterShadow.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.SetActive(false);
            saveManager.activeSave.c1s1_0 = 1;
        }
    }
}