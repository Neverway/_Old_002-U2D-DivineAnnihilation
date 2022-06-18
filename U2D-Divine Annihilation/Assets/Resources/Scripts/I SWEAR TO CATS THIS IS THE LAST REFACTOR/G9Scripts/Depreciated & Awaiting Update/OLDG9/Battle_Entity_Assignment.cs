//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Sets up the player's party
// Applied to: BattleController object in a battle scene
//
//=============================================================================

using UnityEngine;

public class Battle_Entity_Assignment : MonoBehaviour
{
    // Public Variabless
    public GameObject partyMember1;
    public GameObject partyMember2;
    public GameObject partyMember3;
    public SpriteRenderer partyMember1Sprite;
    public SpriteRenderer partyMember2Sprite;
    public SpriteRenderer partyMember3Sprite;

    public RuntimeAnimatorController foxAnimator;
    public RuntimeAnimatorController miyuAnimator;
    public RuntimeAnimatorController samAnimator;
    public RuntimeAnimatorController caseyAnimator;
    public Sprite idleSideFox;
    public Sprite idleSideMiyu;
    public Sprite idleSideSam;
    public Sprite idleSideCasey;

    // Private Variables
    private RuntimeAnimatorController partyMember1SpriteAnimator;
    private RuntimeAnimatorController partyMember2SpriteAnimator;
    private RuntimeAnimatorController partyMember3SpriteAnimator;

    private GameObject abzPlayer;
    private SaveManager saveManager;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>(); // Set a reference to the SaveManager script on the Config object in the scene
        abzPlayer = GameObject.FindGameObjectWithTag("Player");

        // PARTY SLOT 1
        if (saveManager.activeSave.partyMemberOne != "NULL") partyMember1.SetActive(true);
        else partyMember1.SetActive(false);
        if (saveManager.activeSave.partyMemberOne == "Fox") { partyMember1Sprite.sprite = idleSideFox; partyMember1SpriteAnimator = foxAnimator; }
        if (saveManager.activeSave.partyMemberOne == "Miyu"){ partyMember1Sprite.sprite = idleSideMiyu; partyMember1SpriteAnimator = miyuAnimator; }
        if (saveManager.activeSave.partyMemberOne == "Sam") { partyMember1Sprite.sprite = idleSideSam; partyMember1SpriteAnimator = samAnimator; }
        if (saveManager.activeSave.partyMemberOne == "Casey") { partyMember1Sprite.sprite = idleSideCasey; partyMember1SpriteAnimator = caseyAnimator; }


        // PARTY SLOT 2
        if (saveManager.activeSave.partyMemberTwo != "NULL") partyMember2.SetActive(true);
        else partyMember2.SetActive(false);
        if (saveManager.activeSave.partyMemberTwo == "Fox") { partyMember2Sprite.sprite = idleSideFox; partyMember2SpriteAnimator = foxAnimator; }
        if (saveManager.activeSave.partyMemberTwo == "Miyu") { partyMember2Sprite.sprite = idleSideMiyu; partyMember2SpriteAnimator = miyuAnimator; }
        if (saveManager.activeSave.partyMemberTwo == "Sam") { partyMember2Sprite.sprite = idleSideSam; partyMember2SpriteAnimator = samAnimator; }
        if (saveManager.activeSave.partyMemberTwo == "Casey") { partyMember2Sprite.sprite = idleSideCasey; partyMember2SpriteAnimator = caseyAnimator; }


        // PARTY SLOT 3
        if (saveManager.activeSave.partyMemberThree != "NULL") partyMember3.SetActive(true);
        else partyMember3.SetActive(false);
        if (saveManager.activeSave.partyMemberThree == "Fox") { partyMember3Sprite.sprite = idleSideFox; partyMember3SpriteAnimator = foxAnimator; }
        if (saveManager.activeSave.partyMemberThree == "Miyu") { partyMember3Sprite.sprite = idleSideMiyu; partyMember3SpriteAnimator = miyuAnimator; }
        if (saveManager.activeSave.partyMemberThree == "Sam") { partyMember3Sprite.sprite = idleSideSam; partyMember3SpriteAnimator = samAnimator; }
        if (saveManager.activeSave.partyMemberThree == "Casey") { partyMember3Sprite.sprite = idleSideCasey; partyMember3SpriteAnimator = caseyAnimator; }
    }


    public void AbzEntitySwap(int memeberID)
    {
        if (memeberID == 0)
        {
            abzPlayer.GetComponent<Animator>().runtimeAnimatorController = foxAnimator;
        }
        else if (memeberID == 1)
        {
            abzPlayer.GetComponent<Animator>().runtimeAnimatorController = partyMember1SpriteAnimator;
        }
        else if(memeberID == 2)
        {
            abzPlayer.GetComponent<Animator>().runtimeAnimatorController = partyMember2SpriteAnimator;
        }
        else if(memeberID == 3)
        {
            abzPlayer.GetComponent<Animator>().runtimeAnimatorController = partyMember3SpriteAnimator;
        }
    }
}
