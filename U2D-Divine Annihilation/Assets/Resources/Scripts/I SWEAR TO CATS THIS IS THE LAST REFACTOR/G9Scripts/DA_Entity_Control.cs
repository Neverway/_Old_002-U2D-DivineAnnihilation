//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// DA-SID: MRC
// Purpose: Give functionality to game entities
// Applied to: The entity prefab
// Editor script: DASDK_Tool_EntityEditor
// Notes: 
//
//=============================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using Pathfinding;

public class DA_Entity_Control : MonoBehaviour
{
    // Base variables
    public string entityName;
    public float maxHealth = 100f;
    public RuntimeAnimatorController entityAnimator;

    public bool useSpritesOverAnimator;
    public int choiceValue;

    // Base idle variables
    public Sprite idleUp;
    public Sprite idleDown;
    public Sprite idleLeft;
    public Sprite idleRight;

    // Base walk variables
    public float walkSpeed = 5f;

    // Base sprint variables
    public float sprintSpeed = 7f;
    public float dodgeSpeed = 11f;

    // Follower variables
    private Transform target;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    Rigidbody2D rigidbody2d;
    public float stopRange = 1.5f;
    public float nextWaypointDistance = 3f;

    // Player variables
    public Sprite shelfSprite;
    public GameObject HUD;
    public bool canMove = true;
    public bool alternateCanMove = true;
    private Vector2 movement;
    private Vector2 dodgeMovement;
    public bool dodgeCooldown;
    public float dodgeDuration;
    public float tapSpeed = 0.23f; //in seconds
    private float BlastTapTime = 0;
    private float LlastTapTime = 0;
    private float RlastTapTime = 0;

    // Character variables
    public bool isFollower;

    // Enemy variables
    public string[] enemysPartyMembers;
    public float senseRange = 20f;
    //public float attack = 15; 
    public int goldDrop = 5;
    public float expDrop = 5;

    // Private variables
    public string entityType;
    public float currentSpeed;
    private bool firstPass = true;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D Rigidbody;
    private OTU_System_InputManager inputManager;
    private OTU_System_InventoryManager inventoryManager;
    private OTU_System_TransitionManager transitionManager;
    private OTU_System_MenuManager menuManager;
    private OTU_System_SaveManager saveManager;
    private OTU_System_SaveLoader saveLoader;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        // Find references
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        inventoryManager = FindObjectOfType<OTU_System_InventoryManager>();
        transitionManager = FindObjectOfType<OTU_System_TransitionManager>();
        menuManager = FindObjectOfType<OTU_System_MenuManager>();
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
        saveLoader = FindObjectOfType<OTU_System_SaveLoader>();
        currentSpeed = walkSpeed;

            /*
        // Spawn a failsafe config object if the proper one cannot be found (by default it will save to a failsafe save file labeled as SlotZero)
        if (inputManager == null)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/Core/Config"), new Vector3(0,0,0), new Quaternion(0,0,0,0), GameObject.Find("[System]").transform);
            inputManager = FindObjectOfType<OTU_System_InputManager>();
            saveManager = FindObjectOfType<OTU_SystemCinemachineBasicMultiChannelPerlin_SaveManager>();
            saveManager.gameObject.transform.SetParent(null);
            saveManager.gameObject.name = "Config";
            saveManager.activeSave2.saveProfileName = "SlotZero";
            Debug.LogWarning("The scene was loaded abnormally and a failsafe save was created at the persitent data path! Please start the game from the title scene to fix this issue (unless you are just debugging stuff I guess.)");
            
            SceneManager.LoadScene("Main_Title");
            Debug.LogWarning("The scene was loaded abnormally so the failsafe level was loaded! In the future, please start the game from the title scene to avoid this issue (unless you are just debugging stuff I guess.)");
        }
        */

        // Set entity type
        if (choiceValue == 0)
        {
            if (isFollower)
            {
                entityType = "follower";
                seeker = GetComponent<Seeker>();
                rigidbody2d = GetComponent<Rigidbody2D>();
                //characterMovement = FindObjectOfType<Entity_Character_Movement>();
                saveManager = FindObjectOfType<OTU_System_SaveManager>();

                InvokeRepeating("UpdatePath", 0f, 0.5f);
            }
            else
            {
                entityType = "character";
            }
        }

        if (choiceValue == 1)
        {
            entityType = "enemy";
        }

        if (choiceValue == 2)
        {
            entityType = "player";
            if (HUD != null )
            {
                if (HUD.transform.GetChild(2).GetComponent<Text>() != null)
                {
                    HUD.transform.GetChild(2).GetComponent<Text>().text = entityName;
                }
                if (HUD.transform.GetChild(3).GetComponent<Image>() != null)
                {
                    HUD.transform.GetChild(3).GetComponent<Image>().sprite = shelfSprite;
                }
            }
            if (inventoryManager != null)
            {
                inventoryManager.UpdatePlayerDescription(spriteRenderer.sprite, entityName);
            }
        }
    }

    IEnumerator DodgeRoll()
    {
        yield return new WaitForSeconds(dodgeDuration);
        dodgeMovement = new Vector2(0,0);
        dodgeCooldown = true;
        yield return new WaitForSeconds(dodgeDuration+0.3f);
        dodgeCooldown = false;
    }


    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rigidbody2d.position, target.position, OnPathComplete);
        }
    }


    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    void Update()
    {
        if (entityType == "player")
        {
            PlayerEntity();
        }
        else if (entityType == "character")
        {
            CharacterEntity();
        }
        else if (entityType == "follower")
        {
            FollowerEntity();
        }
        else if (entityType == "enemy")
        {
            EnemyEntity();
        }
    }

    void FixedUpdate()
    {
        if (dodgeMovement.x == 0 && dodgeMovement.y == 0)
        {
            Rigidbody.MovePosition(Rigidbody.position + movement * currentSpeed * Time.fixedDeltaTime);    // Update the movement for the character
        }
        else
        {
            Rigidbody.MovePosition(Rigidbody.position + dodgeMovement * dodgeSpeed * Time.fixedDeltaTime);    // Update the movement for the character
        }
    }

    void PlayerEntity()
    {
        if (canMove)
        {
            if (saveLoader.hasLoadedCurrentLevel)
            {
                saveManager.activeSave2.playerSavePosition = gameObject.transform.position;
            }
            // Horizontal control
            if (Input.GetKey(inputManager.controls["Right"]) && movement.x < 1)
            {
                movement.x += 1;
            }
            if (Input.GetKey(inputManager.controls["Left"]) && movement.x > -1)
            {
                movement.x -= 1;
            }
            if (!Input.GetKey(inputManager.controls["Left"]) && !Input.GetKey(inputManager.controls["Right"]))
            {
                movement.x = 0;
            }

            // Vertical control
            if (Input.GetKey(inputManager.controls["Up"]) && movement.y < 1)
            {
                movement.y += 1;
            }
            if (Input.GetKey(inputManager.controls["Down"]) && movement.y > -1)
            {
                movement.y -= 1;
            }
            if (!Input.GetKey(inputManager.controls["Down"]) && !Input.GetKey(inputManager.controls["Up"]))
            {
                movement.y = 0;
            }

            if (Input.GetKey(inputManager.controls["Up"]) || Input.GetKey(inputManager.controls["Down"]) || Input.GetKey(inputManager.controls["Left"]) || Input.GetKey(inputManager.controls["Right"]))
            {
                animator.SetFloat("LastX", movement.x);
                animator.SetFloat("LastY", movement.y);

            }
            if (Input.GetKey(inputManager.controls["Special 3"]) && saveManager.activeSave2.playerHealth >=0)
            {
                saveManager.activeSave2.playerHealth -= 1;
            }
            if (Input.GetKey(inputManager.controls["Special 4"]) && saveManager.activeSave2.playerHealth <= maxHealth)
            {
                saveManager.activeSave2.playerHealth += 1;
            }

            // Roll
            if(Input.GetKeyDown(inputManager.controls["Action"]) && dodgeMovement.x == 0 && dodgeMovement.y == 0 && !dodgeCooldown)
            {
                if((Time.time - BlastTapTime) < tapSpeed)
                { 
                    // Check facing direction
                    dodgeDuration = 0.3f;
                    StartCoroutine("DodgeRoll");
                    if (animator.GetFloat("LastX") <= -0.1){dodgeMovement.x = -1;animator.Play("Roll Left");}
                    else if (animator.GetFloat("LastX") >= 0.1){dodgeMovement.x = 1;animator.Play("Roll Right");}
                    if (animator.GetFloat("LastY") <= -0.1){dodgeMovement.y = -1;}
                    else if (animator.GetFloat("LastY") >= 0.1){dodgeMovement.y = 1;}
                }

                BlastTapTime = Time.time; 
            }
            
            if(Input.GetKeyDown(inputManager.controls["L"]) && dodgeMovement.x == 0 && dodgeMovement.y == 0 && !dodgeCooldown)
            {
                if((Time.time - LlastTapTime) < tapSpeed)
                { 
                    // Check facing direction
                    dodgeDuration = 0.15f;
                    StartCoroutine("DodgeRoll");
                    if (animator.GetFloat("LastX") <= -0.1){dodgeMovement.y = -1;}
                    else if (animator.GetFloat("LastX") >= 0.1){dodgeMovement.y = 1; }
                    if (animator.GetFloat("LastY") <= -0.1){dodgeMovement.x = -1;}
                    else if (animator.GetFloat("LastY") >= 0.1){dodgeMovement.x = -1;}
                }
                LlastTapTime = Time.time; 
            }
            
            if(Input.GetKeyDown(inputManager.controls["R"]) && dodgeMovement.x == 0 && dodgeMovement.y == 0 && !dodgeCooldown)
            {
                if((Time.time - RlastTapTime) < tapSpeed)
                { 
                    // Check facing direction
                    dodgeDuration = 0.15f;
                    StartCoroutine("DodgeRoll");
                    if (animator.GetFloat("LastX") <= -0.1){dodgeMovement.y = 1;}
                    else if (animator.GetFloat("LastX") >= 0.1){dodgeMovement.y = -1;}
                    if (animator.GetFloat("LastY") <= -0.1){dodgeMovement.x = 1;}
                    else if (animator.GetFloat("LastY") >= 0.1){dodgeMovement.x = 1;}
                }
                RlastTapTime = Time.time; 
            }

        }
        else
        {
            movement.x = 0;
            movement.y = 0;
        }

        // Entity animator
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);

        // Sprinting
        if (Input.GetKey(inputManager.controls["Action"]))
        {
            currentSpeed = sprintSpeed;
            //animator.speed = 1.5f;
        }
        else
        {
            currentSpeed = walkSpeed;
            //animator.speed = 1;
        }

        // can/can't move check
        if (!menuManager.menuActive && alternateCanMove)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }

        if (saveManager.activeSave2.playerHealth <= 0 && firstPass)
        {
            menuManager.alternateMenuActive = true;
            animator.Play("Knockout");
            transitionManager.FadeIn("knockout");
            spriteRenderer.sortingLayerName = "UI";
            spriteRenderer.sortingOrder = 5;
            firstPass = false;
        }
    }

    void CharacterEntity()
    {

    }

    void FollowerEntity()
    {
        target = GameObject.FindWithTag("Player").transform;

        if (path == null)
        {
            return;
        }

        if (reachedEndOfPath)
        {
            // This is not currently in use but the if statment get rid of a warning in the log
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }

        else
        {
            reachedEndOfPath = false;
        }

        if (saveManager.activeSave2.partyMembers[0] == entityName || saveManager.activeSave2.partyMembers[1] == entityName || saveManager.activeSave2.partyMembers[2] == entityName)
        {
            // Move towards the target (usually the party leader)
            if (Vector2.Distance(rigidbody2d.position, target.position) <= senseRange && Vector2.Distance(rigidbody2d.position, target.position) >= stopRange)
            {
                Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody2d.position).normalized;
                Vector2 force = direction * walkSpeed * Time.deltaTime;
                rigidbody2d.AddForce(force);

                float distance = Vector2.Distance(rigidbody2d.position, path.vectorPath[currentWaypoint]);

                if (distance < nextWaypointDistance)
                {
                    currentWaypoint++;
                }

            }

            // Teleport to target if stuck
            if (Vector2.Distance(rigidbody2d.position, target.position) >= senseRange)
            {
                rigidbody2d.transform.position = new Vector2(target.transform.position.x, target.transform.position.y);
            }
        }
    }

    void EnemyEntity()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && entityType == "enemy") 
        {
            transitionManager.BattleTransition(0);
            PlayerPrefs.SetString("EP0", entityName);
            PlayerPrefs.SetString("EP1", enemysPartyMembers[0]);
            PlayerPrefs.SetString("EP2", enemysPartyMembers[1]);
            PlayerPrefs.SetString("EP3", enemysPartyMembers[2]);
        }
    }

    public void SetCameraIdleNoise(float amplitude)
    {
        gameObject.transform.GetChild(0).gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        CinemachineVirtualCamera vcam;
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
    }
}
