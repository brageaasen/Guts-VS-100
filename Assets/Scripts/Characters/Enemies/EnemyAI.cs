using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    [Header("Pathfinding")]
    private Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    [Header("Particles")]
    public ParticleSystem dustPS;

    [Header("Other")]
    [SerializeField] private GameObject enemyGFX;
    [SerializeField] private LayerMask groundLayer;

    private Path path;
    private int currentWaypoint = 0;
    private Vector2 currentVelocity;
    RaycastHit2D isGrounded;
    Seeker seeker;
    Rigidbody2D rb;
    Animator animator;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();

        target = GameObject.Find("Player").GetComponent<Transform>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        animator.SetBool("IsGrounded", isGrounded);
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(new Vector2(rb.position.x, rb.position.y), target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // See if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<BoxCollider2D>().bounds.extents.y + jumpCheckOffset);
        //isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);
        isGrounded = Physics2D.BoxCast(GetComponent<CircleCollider2D>().bounds.center, GetComponent<CircleCollider2D>().bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        
        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        // Jump
        if (jumpEnabled && isGrounded)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                animator.SetTrigger("Jump");
                rb.AddForce(Vector2.up * speed * jumpModifier);
                //dustPS.Play();
            }
        }

        // Movement
        //rb.velocity = Vector2.SmoothDamp(rb.velocity, force, ref currentVelocity, 0.5f);
        rb.AddForce(force);

        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.05f)
            {
                enemyGFX.transform.localScale = new Vector3(-1f * Mathf.Abs(enemyGFX.transform.localScale.x), enemyGFX.transform.localScale.y, enemyGFX.transform.localScale.z);
                //dustPS.Play();
            }
            else if (rb.velocity.x < -0.05f)
            {
                enemyGFX.transform.localScale = new Vector3(Mathf.Abs(enemyGFX.transform.localScale.x), enemyGFX.transform.localScale.y, enemyGFX.transform.localScale.z);
                //dustPS.Play();
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}