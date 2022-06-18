// Included Libraries
//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Sets up the battle party HUD
// Applied to: BattleController object in a battle scene
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;

public class Battle_Shelf_Assignment : MonoBehaviour
{
    // Public Variabless
    public GameObject shelf1;
    public GameObject shelf2;
    public GameObject shelf3;
    public Image shelf1Icon;
    public Image shelf2Icon;
    public Image shelf3Icon;
    public Text shelf1Name;
    public Text shelf2Name;
    public Text shelf3Name;
    public Sprite iconFox;
    public Sprite iconMiyu;
    public Sprite iconSam;
    public Sprite iconCasey;

    // Private Variables
    private SaveManager saveManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>(); // Set a reference to the SaveManager script on the Config object in the scene
    }


    void Update()
    {
        // PARTY SLOT 1
        if (saveManager.activeSave.partyMemberOne != "NULL")
        {
            shelf1.SetActive(true);
            shelf1Name.text = saveManager.activeSave.partyMemberOne;
        }

        else shelf1.SetActive(false);
        if (saveManager.activeSave.partyMemberOne == "Fox") shelf1Icon.sprite = iconFox;
        if (saveManager.activeSave.partyMemberOne == "Miyu") shelf1Icon.sprite = iconMiyu;
        if (saveManager.activeSave.partyMemberOne == "Sam") shelf1Icon.sprite = iconSam;
        if (saveManager.activeSave.partyMemberOne == "Casey") shelf1Icon.sprite = iconCasey;


        // PARTY SLOT 2
        if (saveManager.activeSave.partyMemberTwo != "NULL")
        {
            shelf2.SetActive(true);
            shelf2Name.text = saveManager.activeSave.partyMemberTwo;
        }

        else shelf2.SetActive(false);
        if (saveManager.activeSave.partyMemberTwo == "Fox") shelf2Icon.sprite = iconFox;
        if (saveManager.activeSave.partyMemberTwo == "Miyu") shelf2Icon.sprite = iconMiyu;
        if (saveManager.activeSave.partyMemberTwo == "Sam") shelf2Icon.sprite = iconSam;
        if (saveManager.activeSave.partyMemberTwo == "Casey") shelf2Icon.sprite = iconCasey;


        // PARTY SLOT 3
        if (saveManager.activeSave.partyMemberThree != "NULL")
        {
            shelf3.SetActive(true);
            shelf3Name.text = saveManager.activeSave.partyMemberThree;
        }

        else shelf3.SetActive(false);
        if (saveManager.activeSave.partyMemberThree == "Fox") shelf3Icon.sprite = iconFox;
        if (saveManager.activeSave.partyMemberThree == "Miyu") shelf3Icon.sprite = iconMiyu;
        if (saveManager.activeSave.partyMemberThree == "Sam") shelf3Icon.sprite = iconSam;
        if (saveManager.activeSave.partyMemberThree == "Casey") shelf3Icon.sprite = iconCasey;
    }
}
