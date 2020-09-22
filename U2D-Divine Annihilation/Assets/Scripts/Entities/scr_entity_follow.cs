using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class scr_entity_follow : MonoBehaviour
{
    public Transform target;
    public float senseRange = 20f;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public Transform spriteGraphic;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rigidbody2d;


    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rigidbody2d = GetComponent<Rigidbody2D>();

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


    // Update is called once per frame
    void FixedUpdate()
    {
        if (path == null)
        {
            return;
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
                spriteGraphic.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rigidbody2d.velocity.x <= -0.01f)
            {
                spriteGraphic.localScale = new Vector3(1f, 1f, 1f);
            }
        }
    }
}
