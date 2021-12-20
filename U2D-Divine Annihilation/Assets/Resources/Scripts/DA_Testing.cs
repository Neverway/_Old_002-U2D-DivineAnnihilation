//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// SID: 
// Purpose: 
// Applied to: 
// Editor script: 
// Notes: 
//
//=============================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DA_Testing : MonoBehaviour
{
    // Public variables
    public string entityName;
    public float currentSpeed;
    public float senseRange = 20f;

    public float stopRange = 1.5f;
    public float slowdownMultiplier = 1;
    public float repathRate = 1.5f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    public float nextWaypointDistance = 3f;
    Seeker seeker;
    public Vector2 testvector;
    public bool isMoving;
    public Vector2 direction;

    // Private variables
    private Transform target;
    private Animator characterAnimator;
    private Rigidbody2D rigidbody2d;

    //private Entity_Character_Movement characterMovement;

    // Reference variables
    private OTU_System_SaveManager saveManager;


    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        saveManager = FindObjectOfType<OTU_System_SaveManager>();
        characterAnimator = gameObject.GetComponent<Animator>();
        InvokeRepeating("UpdatePath", 0f, repathRate);
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


    void FixedUpdate()
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
            target = GameObject.FindWithTag("Player").transform;
            currentSpeed = GameObject.FindWithTag("Player").GetComponent<DA_Entity_Control>().currentSpeed+0.5f;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;


            // Move toward target
            if (Vector2.Distance(rigidbody2d.position, target.position) <= senseRange && Vector2.Distance(rigidbody2d.position, target.position) >= stopRange)
            {

                // Draw direction based off A* point vector
                direction = ((Vector2)path.vectorPath[currentWaypoint+1] - rigidbody2d.position).normalized;

                if (Vector2.Distance(rigidbody2d.position, target.position) <= stopRange+0.5f)
                {
                    rigidbody2d.MovePosition(rigidbody2d.position + direction * (currentSpeed-slowdownMultiplier) * Time.fixedDeltaTime);    // Update the movement for the character
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

                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

                
                
            // Character animator
            if (isMoving)
            {
                if (direction.x > 0.5)
                {
                    characterAnimator.SetFloat("MoveX", 0.2f);
                    characterAnimator.SetFloat("MoveY", 0.0f);
                    testvector.x = 0.2f;
                }
                else if (direction.x < -0.5)
                {
                    characterAnimator.SetFloat("MoveX", -0.2f);
                    characterAnimator.SetFloat("MoveY", 0.0f);
                    testvector.x = -0.2f;
                }
                if (direction.y > 0.5)
                {
                    characterAnimator.SetFloat("MoveY", 0.2f);
                    testvector.y = 0.2f;
                }
                else if (direction.y < -0.5)
                {
                    characterAnimator.SetFloat("MoveY", -0.2f);
                    testvector.y = -0.2f;
                }
            }
            else if (!isMoving)
            {
                if (direction.x > 0.5)
                {
                    characterAnimator.SetFloat("MoveX", 0.1f);
                    characterAnimator.SetFloat("MoveY", 0.0f);
                    testvector.x = 0.1f;
                }
                else if (direction.x < -0.5)
                {
                    characterAnimator.SetFloat("MoveX", -0.1f);
                    characterAnimator.SetFloat("MoveY", 0.0f);
                    testvector.x = -0.1f;
                }
                if (direction.y > 0.5)
                {
                    characterAnimator.SetFloat("MoveY", 0.1f);
                    testvector.y = 0.1f;
                }
                else if (direction.y < -0.5)
                {
                    characterAnimator.SetFloat("MoveY", -0.1f);
                    testvector.y = -0.1f;
                }
            }
            

            // Teleport to target if stuck
            if (Vector2.Distance(rigidbody2d.position, target.position) >= senseRange)
            {
                rigidbody2d.transform.position = new Vector2(target.transform.position.x, target.transform.position.y);
            }
        }
    }
}
