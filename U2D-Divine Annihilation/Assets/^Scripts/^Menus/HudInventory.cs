using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_menu_inventoryManager : MonoBehaviour
{
    // Referances
    public GameObject inventoryBoxObject;   // A referance to the inventory box object
    public Text inventoryNameObject;        // A referance to the inventory name object
    public Image inventoryCharacterObject;  // A referance to the inventory character object
    public Text inventoryGoldObject;        // A referance to the inventory gold object
    public Text inventorylevelObject;       // A referance to the inventory level object
    public Button selectedButton;

    public bool inventoryBoxActive;
    public bool acceptingInput;
    private scr_character_movement characterMovement; // A referance to the player movement so the dialogue box can freeze the player when it opens
    private scr_system_configurationManager global;


    // Start is called before the first frame update
    void Start()
    {
        selectedButton.Select();
        characterMovement = FindObjectOfType<scr_character_movement>(); // Find the character movment script
        global = FindObjectOfType<scr_system_configurationManager>(); // Find the config script
    }

    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(0.05f);
        acceptingInput = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Close inventory
        if (inventoryBoxActive && Input.GetKeyDown("c"))
        {
            if (acceptingInput)
            { 
                characterMovement.movementSpeed = characterMovement.storedSpeed; // Set the players speed so they won't get stuck with a movement speed of zero
                inventoryBoxActive = false;
                inventoryBoxObject.SetActive(false); // Make the box disappear
                acceptingInput = false;
                StartCoroutine("acceptInput");
            }
        }

        // Open inventory
        if (!inventoryBoxActive && Input.GetKeyDown("c") && !global.menuActive)
        {
            if (acceptingInput)
            {
                inventoryBoxActive = true;
                inventoryBoxObject.SetActive(true); // Make the box appear
                acceptingInput = false;
                StartCoroutine("acceptInput");
            }
        }
    }
}
