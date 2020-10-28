using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWakeUp : MonoBehaviour
{
    public bool animationFinished;
    public GameObject playerCharacter;
    public GameObject playerCharacterShadow;
    public GameObject configTarget;
    private SaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        saveManager = configTarget.GetComponent<SaveManager>();
        if (saveManager.activeSave.hasWokenUp)
        {
            configTarget.GetComponent<SystemConfigManager>().overrideCanMove = false;
            gameObject.SetActive(false);
        }
        else
        {
            configTarget.GetComponent<SystemConfigManager>().overrideCanMove = true;
            playerCharacter.GetComponent<SpriteRenderer>().enabled = false;
            playerCharacterShadow.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (animationFinished)
        {
            configTarget.GetComponent<SystemConfigManager>().overrideCanMove = false;
            playerCharacter.GetComponent<SpriteRenderer>().enabled = true;
            playerCharacterShadow.GetComponent<SpriteRenderer>().enabled = true;
            gameObject.SetActive(false);
            saveManager.activeSave.hasWokenUp = true;
        }
    }
}
