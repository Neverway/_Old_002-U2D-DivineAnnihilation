using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_WakeUp : MonoBehaviour
{
    public bool animationFinished;
    public GameObject playerCharacter;
    public GameObject playerCharacterShadow;
    private SaveManager saveManager;
    private System_Config_Manager configManager;

    // Start is called before the first frame update
    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        configManager = FindObjectOfType<System_Config_Manager>();
        if (saveManager.activeSave.c1s1_0 == 0)
        {
            configManager.overrideCanMove = false;
            gameObject.SetActive(false);
        }
        else
        {
            configManager.GetComponent<System_Config_Manager>().overrideCanMove = true;
            playerCharacter.GetComponent<SpriteRenderer>().enabled = false;
            playerCharacterShadow.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animationFinished)
        {
            configManager.GetComponent<System_Config_Manager>().overrideCanMove = false;
            playerCharacter.GetComponent<SpriteRenderer>().enabled = true;
            playerCharacterShadow.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.SetActive(false);
            saveManager.activeSave.c1s1_0 = 1;
        }
    }
}