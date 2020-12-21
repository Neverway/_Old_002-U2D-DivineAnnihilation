//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Manage who's turn it is
// Applied to: BattleController object in a battle scene
//
//=============================================================================

using System.Collections;
using UnityEngine;

public class Battle_Turn_Manager : MonoBehaviour
{
    public int partyTurnID = 0;
    public bool playersTurn = true;
    public float YOffset = 5f;
    private bool playerPartyMember1;
    private bool playerPartyMember2;
    private bool playerPartyMember3;
    private bool enemyPartyMember1;
    private bool enemyPartyMember2;
    private bool enemyPartyMember3;
    public bool acceptingInput = true;

    public GameObject turnArrow;
    public GameObject partyMember0;
    public GameObject partyMember1;
    public GameObject partyMember2;
    public GameObject partyMember3;
    public GameObject Enemy0;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject configTarget;

    public string movesetPartyMember0 = "";
    public string movesetPartyMember1 = "";
    public string movesetPartyMember2 = "";
    public string movesetPartyMember3 = "";
    private SaveManager saveManager;
    private Battle_Zone_Control battleZone;

    void Start()
    {
        saveManager = configTarget.GetComponent<SaveManager>(); // Set a reference to the SaveManager script on the Config object in the scene
        battleZone = FindObjectOfType<Battle_Zone_Control>();
    }


    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(1); // The delay until it is accepting input again
        acceptingInput = true;              // Allow input again
    }


    void Update()
    {
        // Set active player party
        if (saveManager.activeSave.partyMemberOne != "NULL")
        {
            playerPartyMember1 = true;
        }

        else
        {
            playerPartyMember1 = false;
        }

        if (saveManager.activeSave.partyMemberTwo != "NULL")
        {
            playerPartyMember2 = true;
        }

        else
        {
            playerPartyMember2 = false;
        }

        if (saveManager.activeSave.partyMemberThree != "NULL")
        {
            playerPartyMember3 = true;
        }

        else
        {
            playerPartyMember3 = false;
        }


        // Set active enemy party
        if (PlayerPrefs.GetString("Enemy1") != "NULL")
        {
            enemyPartyMember1 = true;
        }
        else
        {
            enemyPartyMember1 = false;
        }
        if (PlayerPrefs.GetString("Enemy2") != "NULL")
        {
            enemyPartyMember2 = true;
        }
        else
        {
            enemyPartyMember2 = false;
        }
        if (PlayerPrefs.GetString("Enemy3") != "NULL")
        {
            enemyPartyMember3 = true;
        }
        else
        {
            enemyPartyMember3 = false;
        }

        // Set turn arrow positions
        if (playersTurn && partyTurnID == 0) turnArrow.transform.position = new Vector3(partyMember0.transform.position.x, partyMember0.transform.position.y + YOffset);
        if (playersTurn && partyTurnID == 1) turnArrow.transform.position = new Vector3(partyMember1.transform.position.x, partyMember1.transform.position.y + YOffset);
        if (playersTurn && partyTurnID == 2) turnArrow.transform.position = new Vector3(partyMember2.transform.position.x, partyMember2.transform.position.y + YOffset);
        if (playersTurn && partyTurnID == 3) turnArrow.transform.position = new Vector3(partyMember3.transform.position.x, partyMember3.transform.position.y + YOffset);

        if (!playersTurn && partyTurnID == 0) turnArrow.transform.position = new Vector3(Enemy0.transform.position.x, Enemy0.transform.position.y + YOffset);
        if (!playersTurn && partyTurnID == 1) turnArrow.transform.position = new Vector3(Enemy1.transform.position.x, Enemy1.transform.position.y + YOffset);
        if (!playersTurn && partyTurnID == 2) turnArrow.transform.position = new Vector3(Enemy2.transform.position.x, Enemy2.transform.position.y + YOffset);
        if (!playersTurn && partyTurnID == 3) turnArrow.transform.position = new Vector3(Enemy3.transform.position.x, Enemy3.transform.position.y + YOffset);
    }


    public void NextTurn()
    {
        if (playersTurn && acceptingInput)
        {
            // Party memeber 0 to next
            if (partyTurnID == 0 && acceptingInput)
            {
                if (playerPartyMember1) { partyTurnID = 1; Debug.Log("0>1"); acceptingInput = false; }
                else if (playerPartyMember2) { partyTurnID = 2; Debug.Log("0>2"); acceptingInput = false; }
                else if (playerPartyMember3) { partyTurnID = 3; Debug.Log("0>3"); acceptingInput = false; }
                else { partyTurnID = 0; playersTurn = false; Debug.Log("0>0"); acceptingInput = false; }
            }


            // Party memeber 1 to next
            if (partyTurnID == 1 && acceptingInput)
            {
                if (playerPartyMember2) { partyTurnID = 2; Debug.Log("1>2"); acceptingInput = false; }
                else if (playerPartyMember3) { partyTurnID = 3; Debug.Log("1>3"); acceptingInput = false; }
                else { partyTurnID = 0; playersTurn = false; Debug.Log("1>0"); acceptingInput = false; }
            }


            // Party memeber 1 to next
            if (partyTurnID == 2 && acceptingInput)
            {
                if (playerPartyMember3) { partyTurnID = 3; Debug.Log("2>3"); acceptingInput = false; }
            else { partyTurnID = 0; playersTurn = false; Debug.Log("2>0"); acceptingInput = false; }
            }


            // Party memeber 1 to next
            if (partyTurnID == 3 && acceptingInput)
            {
                partyTurnID = 0; playersTurn = false; Debug.Log("3>0"); acceptingInput = false;
            }
            StartCoroutine("acceptInput");                                  // Activate the keypress delay
        }


        if (!playersTurn && acceptingInput)
        {
            // Party memeber 0 to next
            if (partyTurnID == 0 && acceptingInput)
            {
                if (enemyPartyMember1) { partyTurnID = 1; Debug.Log("0>1"); acceptingInput = false; }
                else if (enemyPartyMember2) { partyTurnID = 2; Debug.Log("0>2"); acceptingInput = false; }
                else if (enemyPartyMember3) { partyTurnID = 3; Debug.Log("0>3"); acceptingInput = false; }
                else { partyTurnID = 0; playersTurn = true; Debug.Log("0>0"); acceptingInput = false; }
            }


            // Party memeber 1 to next
            if (partyTurnID == 1 && acceptingInput)
            {
                if (enemyPartyMember2) { partyTurnID = 2; Debug.Log("1>2"); acceptingInput = false; }
                else if (enemyPartyMember3) { partyTurnID = 3; Debug.Log("1>3"); acceptingInput = false; }
                else { partyTurnID = 0; playersTurn = true; Debug.Log("1>0"); acceptingInput = false; }
            }


            // Party memeber 1 to next
            if (partyTurnID == 2 && acceptingInput)
            {
                if (enemyPartyMember3) { partyTurnID = 3; Debug.Log("2>3"); acceptingInput = false; }
                else { partyTurnID = 0; playersTurn = true; Debug.Log("2>0"); acceptingInput = false; }
            }


            // Party memeber 1 to next
            if (partyTurnID == 3 && acceptingInput)
            {
                partyTurnID = 0; playersTurn = true; Debug.Log("3>0"); acceptingInput = false;
            }
            StartCoroutine("acceptInput");                                  // Activate the keypress delay

        }

    }


    public void SetMoveDefend()
    {
        if (partyTurnID == 0)
        {
            movesetPartyMember0 = "defend";
        }

        else if (partyTurnID == 1)
        {
            movesetPartyMember1 = "defend";
        }

        else if (partyTurnID == 2)
        {
            movesetPartyMember2 = "defend";
        }

        else if (partyTurnID == 3)
        {
            movesetPartyMember3 = "defend";
        }
    }


    public void SetMoveAttack(int targetEnemySlot)
    {
        if (partyTurnID == 0)
        {
            //partyMember0.moveState = "attack";
        }

        else if (partyTurnID == 1)
        {
            //partyMember1.moveState = "attack";
        }

        else if (partyTurnID == 2)
        {
            //partyMember2.moveState = "attack";
        }

        else if (partyTurnID == 3)
        {
            //partyMember3.moveState = "attack";
        }

    }
}
