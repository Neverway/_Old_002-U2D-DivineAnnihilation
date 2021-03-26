//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Sets up the enemy party
// Applied to: BattleController object in a battle scene
//
//=============================================================================

using UnityEngine;

public class Battle_Enemy_Assignment : MonoBehaviour
{
    // Public Variabless
    public Sprite idleSidePurpleCat;

    // Private Variables
    private GameObject Enemy0;
    private GameObject Enemy1;
    private GameObject Enemy2;
    private GameObject Enemy3;
    private SaveManager saveManager;

    void Start()
    {
        Enemy0 = GameObject.Find("{EP0}");
        Enemy1 = GameObject.Find("{EP1}");
        Enemy2 = GameObject.Find("{EP2}");
        Enemy3 = GameObject.Find("{EP3}");
    }


    void Update()
    {
        // PARTY SLOT 0
        if (PlayerPrefs.GetString("Enemy0") != "NULL") Enemy0.SetActive(true);
        else Enemy0.SetActive(false);
        if (PlayerPrefs.GetString("Enemy0") == "Purple Cat") Enemy0.GetComponent<SpriteRenderer>().sprite = idleSidePurpleCat;

        // PARTY SLOT 1
        if (PlayerPrefs.GetString("Enemy1") != "NULL") Enemy1.SetActive(true);
        else Enemy1.SetActive(false);
        if (PlayerPrefs.GetString("Enemy1") == "Purple Cat") Enemy1.GetComponent<SpriteRenderer>().sprite = idleSidePurpleCat;

        // PARTY SLOT 2
        if (PlayerPrefs.GetString("Enemy2") != "NULL") Enemy2.SetActive(true);
        else Enemy2.SetActive(false);
        if (PlayerPrefs.GetString("Enemy2") == "Purple Cat") Enemy2.GetComponent<SpriteRenderer>().sprite = idleSidePurpleCat;

        // PARTY SLOT 3
        if (PlayerPrefs.GetString("Enemy3") != "NULL") Enemy3.SetActive(true);
        else Enemy3.SetActive(false);
        if (PlayerPrefs.GetString("Enemy3") == "Purple Cat") Enemy3.GetComponent<SpriteRenderer>().sprite = idleSidePurpleCat;
    }
}
