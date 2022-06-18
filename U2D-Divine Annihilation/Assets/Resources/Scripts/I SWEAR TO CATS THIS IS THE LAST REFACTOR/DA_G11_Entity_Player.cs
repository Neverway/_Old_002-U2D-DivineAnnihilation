//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Give player & event handler, control over an entity
// Applied to: Overworld entity with "Player" tag
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using Pathfinding;

public class DA_G11_Entity_Player : MonoBehaviour
{
    // Entity variables
    [Header ("Base")]
    public string entityName;
    public float health = 100f;
    public float walkSpeed = 5f;
    public float sprintSpeed = 7f;
    [Header ("Pathfinding")]
    public Transform pathfindingTarget;         // The transformation to move to when pathfinding
    public float stopRange = 1.5f;              // The distance the entity has to be from the pathfindingTarget before it's path is considered complete
    public float nextWaypointDistance = 3f;     // The amount of nodes to scan ahead on the navgrid
    public float repathRate = 0.001f;           // Pathfinding update rate

    // Player variables
    [Header ("Player")]
    public Sprite characterIcon;        // The sprite used for the HUD shelf, battle menu, & save icon
    public bool enablePlayerMovement;   // Set entity to player (if this is false the entity will default to following a pathfinding target)
    public bool canMove;                // Enable the use of player input to move the entity (used for stoping the player when in menus or cutscenes, READ ONLY - Set by DA_System_MenuManager)
    public float dodgeSpeed = 11f;      // How quickly the entity moves when dodging

    // Entity private variables
    private float currentSpeed;
    private Path path;
    private Seeker seeker;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;
    private Vector2 direction;
    private SpriteRenderer spriteRenderer;

    // Player private variables
    private Vector2 movement;
    private Vector2 dodgeMovement;
    public bool dodgeCooldown;
    public float dodgeDuration;
    public float tapSpeed = 0.23f; //in seconds
    private float BlastTapTime = 0;
    //private float LlastTapTime = 0;
    //private float RlastTapTime = 0;
    private bool firstPass = true;

    // Entity reference variables
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private OTU_System_InputManager inputManager;
    private OTU_System_SaveManager saveManager;
    private OTU_System_SaveLoader saveLoader;

    // Player reference variables
    private OTU_System_TransitionManager transitionManager;
    private OTU_System_MenuManager menuManager;
    private OTU_System_InventoryManager inventoryManager;
    private GameObject hud;     // Used to get the HUD shelf
    private Text hudName;       // Used to display HUD shelf character name
    private Image hudIcon;      // Used to display HUD shelf character name

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}


    private void Start()
    {
        // Find references
        seeker = GetComponent<Seeker>();
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        inputManager = FindObjectOfType<OTU_System_InputManager>();
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
        saveLoader = FindObjectOfType<OTU_System_SaveLoader>();
        transitionManager = FindObjectOfType<OTU_System_TransitionManager>();
        menuManager = FindObjectOfType<OTU_System_MenuManager>();
        inventoryManager = FindObjectOfType<OTU_System_InventoryManager>();
        hud = GameObject.FindWithTag("DA_hud");
        hudName = hud.transform.GetChild(2).GetComponent<Text>();
        hudIcon = hud.transform.GetChild(3).GetComponent<Image>();

        // Establish start functions
        currentSpeed = walkSpeed;
        InvokeRepeating("UpdatePath", 0f, repathRate);
        hudName.text = entityName;
        hudIcon.sprite = characterIcon;
        if (inventoryManager != null)
        {
            inventoryManager.UpdatePlayerDescription(spriteRenderer.sprite, entityName);
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
    

    private void Update()
    {
        if (enablePlayerMovement) { PlayerMovement(); }
        else { PathfindingMovement(); }
        PlayerHealthHandeler();
    }
    

    private void FixedUpdate()
    {
        // Update the movement for the player        
        if (dodgeMovement.x == 0 && dodgeMovement.y == 0) { rigidbody2d.MovePosition(rigidbody2d.position + movement * currentSpeed * Time.fixedDeltaTime); }
        else { rigidbody2d.MovePosition(rigidbody2d.position + dodgeMovement * dodgeSpeed * Time.fixedDeltaTime); }
    }


    // Entity functions
    private void PathfindingMovement()
    {
        // Set collider to trigger so entity doesn't get stuck on walls when pathfinding (Still avoid walls if possible when pathfinding though)
        if (GetComponent<Collider2D>().isTrigger == false) { GetComponent<Collider2D>().isTrigger = true; }

        if (path == null) { return; }
        if (reachedEndOfPath) {} // This is not currently in use but the if statment get rid of a warning in the log

        if (currentWaypoint >= path.vectorPath.Count) { reachedEndOfPath = true; return; }
        else { reachedEndOfPath = false; }


        // Move towards the target if their is one
        // Move toward target
        if (Vector2.Distance(rigidbody2d.position, pathfindingTarget.position) >= stopRange-stopRange+0.1)
        {
            // Draw direction based off A* point vector
            direction = ((Vector2)path.vectorPath[currentWaypoint+1] - rigidbody2d.position).normalized;

            if (Vector2.Distance(rigidbody2d.position, pathfindingTarget.position) <= stopRange+0.5f)
            {
                rigidbody2d.MovePosition(rigidbody2d.position + direction * (currentSpeed-1) * Time.fixedDeltaTime);    // Update the movement for the character
            }
            else
            {
                rigidbody2d.MovePosition(rigidbody2d.position + direction * currentSpeed * Time.fixedDeltaTime);    // Update the movement for the character
            }

            float distance = Vector2.Distance(rigidbody2d.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }
        }
    }

    void UpdatePath() { if (seeker.IsDone() && !enablePlayerMovement) { seeker.StartPath(rigidbody2d.position, pathfindingTarget.position, OnPathComplete); } }
    void OnPathComplete(Path p) { if (!p.error) { path = p;currentWaypoint = 0; } }


    private void PlayAnimation(string animationTag)
    {
        animator.Play(animationTag);
    }


    // Player entity functions
    private void PlayerMovement()
    {
        // Set trigger to collider so collides with walls (Used to restore collisions if player is coming out of entity mode)
        if (GetComponent<Collider2D>().isTrigger == true) { GetComponent<Collider2D>().isTrigger = false; }

        if (canMove)
        {
            // Load save position on level start
            if (saveLoader.hasLoadedCurrentLevel) { saveManager.activeSave2.playerSavePosition = gameObject.transform.position; }
            

            // Move horizontal
            if (Input.GetKey(inputManager.controls["Right"]) && movement.x < 1) { movement.x += 1; } 
            if (Input.GetKey(inputManager.controls["Left"]) && movement.x > -1) { movement.x -= 1; }
            if (!Input.GetKey(inputManager.controls["Left"]) && !Input.GetKey(inputManager.controls["Right"])) { movement.x = 0; }

            // Move vertical
            if (Input.GetKey(inputManager.controls["Up"]) && movement.y < 1) { movement.y += 1; }
            if (Input.GetKey(inputManager.controls["Down"]) && movement.y > -1) { movement.y -= 1; }
            if (!Input.GetKey(inputManager.controls["Down"]) && !Input.GetKey(inputManager.controls["Up"])) { movement.y = 0; }
            if (Input.GetKey(inputManager.controls["Up"]) || Input.GetKey(inputManager.controls["Down"]) || Input.GetKey(inputManager.controls["Left"]) || Input.GetKey(inputManager.controls["Right"]))
            { animator.SetFloat("LastX", movement.x);animator.SetFloat("LastY", movement.y); }


            // Debug controls for health variable
            if (Input.GetKey(inputManager.controls["Special 3"]) && saveManager.activeSave2.playerHealth >=0)
            { saveManager.activeSave2.playerHealth -= 1; }
            if (Input.GetKey(inputManager.controls["Special 4"]) && saveManager.activeSave2.playerHealth <= health)
            { saveManager.activeSave2.playerHealth += 1; }


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


            // Roll
            if(Input.GetKeyDown(inputManager.controls["Action"]) && dodgeMovement.x == 0 && dodgeMovement.y == 0 && !dodgeCooldown)
            {
                if((Time.time - BlastTapTime) < tapSpeed)
                { 
                    dodgeDuration = 0.3f;
                    StartCoroutine("DodgeRoll");
                    if (animator.GetFloat("LastX") <= -0.1){dodgeMovement.x = -1;animator.Play("Roll Left");}
                    else if (animator.GetFloat("LastX") >= 0.1){dodgeMovement.x = 1;animator.Play("Roll Right");}
                    if (animator.GetFloat("LastY") <= -0.1){dodgeMovement.y = -1;}
                    else if (animator.GetFloat("LastY") >= 0.1){dodgeMovement.y = 1;}
                }
                BlastTapTime = Time.time; 
            }
            
            // Dodge Left
            /*
            if(Input.GetKeyDown(inputManager.controls["L"]) && dodgeMovement.x == 0 && dodgeMovement.y == 0 && !dodgeCooldown)
            {
                if((Time.time - LlastTapTime) < tapSpeed)
                { 
                    dodgeDuration = 0.15f;
                    StartCoroutine("DodgeRoll");
                    if (animator.GetFloat("LastX") <= -0.1){dodgeMovement.y = -1;}
                    else if (animator.GetFloat("LastX") >= 0.1){dodgeMovement.y = 1; }
                    if (animator.GetFloat("LastY") <= -0.1){dodgeMovement.x = -1;}
                    else if (animator.GetFloat("LastY") >= 0.1){dodgeMovement.x = -1;}
                }
                LlastTapTime = Time.time; 
            }
            
            // Dodge Right
            if(Input.GetKeyDown(inputManager.controls["R"]) && dodgeMovement.x == 0 && dodgeMovement.y == 0 && !dodgeCooldown)
            {
                if((Time.time - RlastTapTime) < tapSpeed)
                { 
                    dodgeDuration = 0.15f;
                    StartCoroutine("DodgeRoll");
                    if (animator.GetFloat("LastX") <= -0.1){dodgeMovement.y = 1;}
                    else if (animator.GetFloat("LastX") >= 0.1){dodgeMovement.y = -1;}
                    if (animator.GetFloat("LastY") <= -0.1){dodgeMovement.x = 1;}
                    else if (animator.GetFloat("LastY") >= 0.1){dodgeMovement.x = 1;}
                }
                RlastTapTime = Time.time; 
            }*/
        }
        else
        {
            movement.x = 0;
            movement.y = 0;
        }

        // can/can't move check
        if (!menuManager.menuActive) { canMove = true; }
        else { canMove = false; }

        // Entity animator
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
    }


    private void PlayerHealthHandeler()
    {
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


    public void PlayerCameraNoise(float amplitude)
    {
        gameObject.transform.GetChild(0).gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
        CinemachineVirtualCamera vcam;
        vcam = FindObjectOfType<CinemachineVirtualCamera>();
        vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
    }
}