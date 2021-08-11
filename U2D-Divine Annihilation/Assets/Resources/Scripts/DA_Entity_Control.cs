//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Give functionality to game entities
// Applied to: The entity prefab
// Editor script: DASDK_Tool_EntityEditor
// Notes: 
//
//=============================================================================

using UnityEngine;
using UnityEngine.UI;

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

    // Player variables
    public Sprite shelfSprite;
    public GameObject inventoryType;
    public GameObject HUD;
    public bool canMove = true;
    private Vector2 movement;

    // Character variables
    public bool isFollower;

    // Enemy variables
    public float senseRange = 20f;
    public float attack = 15; 
    public int goldDrop = 5;
    public float expDrop = 5;

    // Private variables
    public string entityType;
    public float currentSpeed;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D Rigidbody;
    private OTU_System_InputManager inputManager;
    private OTU_System_SaveManager saveManager;


    void Start()
    {
        // Find references
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        inputManager = FindObjectOfType<OTU_System_InputManager>();
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
        currentSpeed = walkSpeed;

        // Spawn a failsafe config object if the proper one cannot be found (by default it will save to a failsafe save file labeled as SlotZero)
        if (inputManager == null)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/Core/Config"), new Vector3(0,0,0), new Quaternion(0,0,0,0), GameObject.Find("[System]").transform);
            inputManager = FindObjectOfType<OTU_System_InputManager>();
            saveManager = FindObjectOfType<OTU_System_SaveManager>();
            saveManager.gameObject.transform.SetParent(null);
            saveManager.gameObject.name = "Config";
            saveManager.activeSave2.saveProfileName = "SlotZero";
            Debug.LogWarning("The scene was loaded abnormally and a failsafe save was created at the persitent data path! Please start the game from the title scene to fix this issue (unless you are just debugging stuff I guess.)");
        }

        // Set entity type
        if (choiceValue == 0)
        {
            if (isFollower)
            {
                entityType = "follower";
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
        Rigidbody.MovePosition(Rigidbody.position + movement * currentSpeed * Time.fixedDeltaTime);    // Update the movement for the character
    }

    void PlayerEntity()
    {
        if (canMove)
        {
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

    }

    void CharacterEntity()
    {

    }

    void FollowerEntity()
    {

    }

    void EnemyEntity()
    {

    }
}
