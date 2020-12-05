//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Sets up the enemy party
// Applied to: BattleController object in a battle scene
//
//=============================================================================

using UnityEngine;

public class Battle_Enemy_Assignment : MonoBehaviour
{
    public GameObject Enemy0;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public SpriteRenderer Enemy0Sprite;
    public SpriteRenderer Enemy1Sprite;
    public SpriteRenderer Enemy2Sprite;
    public SpriteRenderer Enemy3Sprite;

    public Sprite idleSidePurpleCat;
    public GameObject configTarget;
    private SaveManager saveManager;

    void Start()
    {
        saveManager = configTarget.GetComponent<SaveManager>(); // Set a reference to the SaveManager script on the Config object in the scene
    }


    void Update()
    {
        // PARTY SLOT 0
        if (PlayerPrefs.GetString("Enemy0") != "NULL") Enemy0.SetActive(true);
        else Enemy0.SetActive(false);
        if (PlayerPrefs.GetString("Enemy0") == "Purple Cat") Enemy0Sprite.sprite = idleSidePurpleCat;

        // PARTY SLOT 1
        if (PlayerPrefs.GetString("Enemy1") != "NULL") Enemy1.SetActive(true);
        else Enemy1.SetActive(false);
        if (PlayerPrefs.GetString("Enemy1") == "Purple Cat") Enemy1Sprite.sprite = idleSidePurpleCat;

        // PARTY SLOT 2
        if (PlayerPrefs.GetString("Enemy2") != "NULL") Enemy2.SetActive(true);
        else Enemy2.SetActive(false);
        if (PlayerPrefs.GetString("Enemy2") == "Purple Cat") Enemy2Sprite.sprite = idleSidePurpleCat;

        // PARTY SLOT 3
        if (PlayerPrefs.GetString("Enemy3") != "NULL") Enemy3.SetActive(true);
        else Enemy3.SetActive(false);
        if (PlayerPrefs.GetString("Enemy3") == "Purple Cat") Enemy3Sprite.sprite = idleSidePurpleCat;
    }
}
