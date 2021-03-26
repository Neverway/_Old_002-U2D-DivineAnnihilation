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
    // Public Variabless
    public int partyTurnID = 0;
    public bool playersTurn = true;
    public float YOffset = 5f;
    public bool acceptingInput = true;

    public GameObject turnArrow;
    public GameObject actionMenu;
    public GameObject enemySelectionMenu;

    public string movesetPartyMember0 = "";
    public string movesetPartyMember1 = "";
    public string movesetPartyMember2 = "";
    public string movesetPartyMember3 = "";

    // Private Variables
    private GameObject partyMember0;
    private GameObject partyMember1;
    private GameObject partyMember2;
    private GameObject partyMember3;
    private GameObject Enemy0;
    private GameObject Enemy1;
    private GameObject Enemy2;
    private GameObject Enemy3;
    private bool playerPartyMember1;
    private bool playerPartyMember2;
    private bool playerPartyMember3;
    private bool enemyPartyMember1;
    private bool enemyPartyMember2;
    private bool enemyPartyMember3;
    private GameObject abzPlayer;
    private SaveManager saveManager;
    private Battle_Zone_Control battleZone;
    private Battle_ActionFunctions actionFunctions;
    private Battle_Entity_Assignment entityAssignment;

    void Start()
    {
        abzPlayer = GameObject.FindGameObjectWithTag("Player");
        //actionMenu = GameObject.Find("{Action Menu}");
        //enemySelectionMenu = GameObject.Find("{Enemy Selection}");
        saveManager = FindObjectOfType<SaveManager>(); // Set a reference to the SaveManager script on the Config object in the scene
        battleZone = FindObjectOfType<Battle_Zone_Control>();
        actionFunctions = FindObjectOfType<Battle_ActionFunctions>();
        entityAssignment = FindObjectOfType<Battle_Entity_Assignment>();

        Enemy0 = GameObject.Find("{EP0}");
        Enemy1 = GameObject.Find("{EP1}");
        Enemy2 = GameObject.Find("{EP2}");
        Enemy3 = GameObject.Find("{EP3}");

        partyMember0 = GameObject.Find("{PM0}");
        partyMember1 = GameObject.Find("{PM1}");
        partyMember2 = GameObject.Find("{PM2}");
        partyMember3 = GameObject.Find("{PM3}");
    }


    IEnumerator acceptInput()
    {
        yield return new WaitForSeconds(1); // The delay until it is accepting input again
        acceptingInput = true;              // Allow input again
    }

    IEnumerator attackWave()
    {
        yield return new WaitForSeconds(6); // The delay until it is accepting input again
        battleZone.abzActive = false;
        partyMember0.GetComponent<SpriteRenderer>().enabled = true;
        if (partyMember1 != null) partyMember1.GetComponent<SpriteRenderer>().enabled = true;
        if (partyMember2 != null) partyMember2.GetComponent<SpriteRenderer>().enabled = true;
        if (partyMember3 != null) partyMember3.GetComponent<SpriteRenderer>().enabled = true;
        abzPlayer.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        abzPlayer.gameObject.GetComponent<Entity_Character_Movement>().enabled = false;
        NextTurn();
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
                if (playerPartyMember1) { partyTurnID = 1; Debug.Log("0>1"); acceptingInput = false; actionMenu.SetActive(true); actionFunctions.acceptingInput = true; }
                else if (playerPartyMember2) { partyTurnID = 2; Debug.Log("0>2"); acceptingInput = false; actionMenu.SetActive(true); actionFunctions.acceptingInput = true; }
                else if (playerPartyMember3) { partyTurnID = 3; Debug.Log("0>3"); acceptingInput = false; actionMenu.SetActive(true); actionFunctions.acceptingInput = true; }
                else { partyTurnID = 0; playersTurn = false; Debug.Log("0>0"); acceptingInput = false; }
            }


            // Party memeber 1 to next
            if (partyTurnID == 1 && acceptingInput)
            {
                if (playerPartyMember2) { partyTurnID = 2; Debug.Log("1>2"); acceptingInput = false; actionMenu.SetActive(true); actionFunctions.acceptingInput = true; }
                else if (playerPartyMember3) { partyTurnID = 3; Debug.Log("1>3"); acceptingInput = false; actionMenu.SetActive(true); actionFunctions.acceptingInput = true; }
                else { partyTurnID = 0; playersTurn = false; Debug.Log("1>0"); acceptingInput = false; }
            }


            // Party memeber 2 to next
            if (partyTurnID == 2 && acceptingInput)
            {
                if (playerPartyMember3) { partyTurnID = 3; Debug.Log("2>3"); acceptingInput = false; actionMenu.SetActive(true); actionFunctions.acceptingInput = true; }
                else { partyTurnID = 0; playersTurn = false; Debug.Log("2>0"); acceptingInput = false; }
            }


            // Party memeber 3 to next
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
            actionMenu.SetActive(false);
            NextTurn();
        }

        else if (partyTurnID == 1)
        {
            movesetPartyMember1 = "defend";
            actionMenu.SetActive(false);
            NextTurn();
        }

        else if (partyTurnID == 2)
        {
            movesetPartyMember2 = "defend";
            actionMenu.SetActive(false);
            NextTurn();
        }

        else if (partyTurnID == 3)
        {
            movesetPartyMember3 = "defend";
            actionMenu.SetActive(false);
            NextTurn();
        }
    }


    public void SetMoveAttack(int targetEnemySlot)
    {
        Debug.Log(targetEnemySlot);
        if (partyTurnID == 0)
        {
            movesetPartyMember0 = "attack";
            battleZone.abzActive = true;
            enemySelectionMenu.SetActive(false);
            partyMember0.GetComponent<SpriteRenderer>().enabled = false;

            entityAssignment.AbzEntitySwap(0);
            abzPlayer.transform.position = new Vector2(-4, 1);
            abzPlayer.GetComponent<SpriteRenderer>().enabled = true;
            abzPlayer.GetComponent<Entity_Character_Movement>().enabled = true;
            StartCoroutine("attackWave");
        }

        else if (partyTurnID == 1)
        {
            movesetPartyMember1 = "attack";
            battleZone.abzActive = true;
            enemySelectionMenu.SetActive(false);
            partyMember1.GetComponent<SpriteRenderer>().enabled = false;

            entityAssignment.AbzEntitySwap(1);
            abzPlayer.transform.position = new Vector2(-4, 1);
            abzPlayer.GetComponent<SpriteRenderer>().enabled = true;
            abzPlayer.GetComponent<Entity_Character_Movement>().enabled = true;
            StartCoroutine("attackWave");
        }

        else if (partyTurnID == 2)
        {
            movesetPartyMember2 = "attack";
            battleZone.abzActive = true;
            enemySelectionMenu.SetActive(false);
            partyMember2.GetComponent<SpriteRenderer>().enabled = false;

            entityAssignment.AbzEntitySwap(2);
            abzPlayer.transform.position = new Vector2(-4, 1);
            abzPlayer.GetComponent<SpriteRenderer>().enabled = true;
            abzPlayer.GetComponent<Entity_Character_Movement>().enabled = true;
            StartCoroutine("attackWave");
        }

        else if (partyTurnID == 3)
        {
            movesetPartyMember3 = "attack";
            battleZone.abzActive = true;
            enemySelectionMenu.SetActive(false);
            partyMember3.GetComponent<SpriteRenderer>().enabled = false;

            entityAssignment.AbzEntitySwap(3);
            abzPlayer.transform.position = new Vector2(-4, 1);
            abzPlayer.GetComponent<SpriteRenderer>().enabled = true;
            abzPlayer.GetComponent<Entity_Character_Movement>().enabled = true;
            StartCoroutine("attackWave");
        }

    }
}
