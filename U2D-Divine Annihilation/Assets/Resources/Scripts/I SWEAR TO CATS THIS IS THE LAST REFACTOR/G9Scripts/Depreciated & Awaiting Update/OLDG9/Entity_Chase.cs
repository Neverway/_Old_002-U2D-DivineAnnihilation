//=========== Written by Arthur W. Sheldon AKA Lizband_UCC ====================
//
// Purpose: Chase the player character
// Applied to: An entity in an overworld scene
//
//=============================================================================

using UnityEngine;
using Pathfinding;

public class Entity_Chase : MonoBehaviour
{
    private Transform target;
    public float senseRange = 20f;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public Transform spriteGraphic;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rigidbody2d;

    private void Awake(){Debug.LogWarning("AN OLD SCRIPT IS IN USE! [" + this.GetType().ToString() + "] Is Located on [" + gameObject.name + "]");}

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        // Auto find the player in the scene
        target = GameObject.FindWithTag("Player").transform;
        if (target == null)
        {
            Debug.LogError("[ID002 DA]: " + "An enemy was unable to find the player target. Make sure your player has the tag 'Player' set.");
        }

        InvokeRepeating("UpdatePath", 0f, 0.5f);
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
        
        if (Vector2.Distance(rigidbody2d.position, target.position) <= senseRange)
        {
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rigidbody2d.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rigidbody2d.AddForce(force);

            float distance = Vector2.Distance(rigidbody2d.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (rigidbody2d.velocity.x >= 0.01f)
            {
                //spriteGraphic.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rigidbody2d.velocity.x <= -0.01f)
            {
                //spriteGraphic.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
