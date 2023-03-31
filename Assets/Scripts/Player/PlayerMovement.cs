using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private Rigidbody2D rb;
    private CharacterController2D controller;
    private Animator animator;

    LayerMask obstacleLayer, groundLayer;


    [Header("Player movement")]
    public float runSpeed = 40f;
    public float playerClimbSpeed = 1;

    [Header("Fall Damage")]
    public int fallDamageMultiplier = 100;
    public float fallThresholdVelocity = 5f;

    float horizontalMove = 0f;
    bool jump = false;
    bool climb = false;
    [HideInInspector] public bool eating = false;
    
    [Header("Bools")]
    public bool crouch;
    public bool behindForeGround;
    public bool canClimb;
    public bool canCrouch;

    private NutCollecter nuts;
    private AudioManager audioManager;

    private float timePassed = 0f;

    private void Start()
	{
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        player = GetComponent<Player>();
        controller = GetComponent<CharacterController2D>();
		rb = GetComponent<Rigidbody2D>();
        obstacleLayer = LayerMask.GetMask("Obstacle");
        groundLayer = LayerMask.GetMask("Ground");
        nuts = GetComponent<NutCollecter>();
        animator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        animator.SetBool("Grounded", controller.m_Grounded);

        if (player.isSleeping && Input.anyKey)
        {
            player.Invoke("WakePlayer", 3);
        }



        // Jump
        if (Input.GetButtonDown("Jump") && !player.isSleeping && controller.m_Grounded) {
             if (!player.isUnderwater)
		    {
                controller.CreateDust();
		    }
            jump = true;
            animator.SetBool("IsJumping", true);
        }

        // Crouch
        if (Input.GetButtonDown("Crouch") && canCrouch) {
            crouch = true;
        } else if (Input.GetButtonUp("Crouch")) {
            crouch = false;
        }

        // Climb
        if (Input.GetButtonDown("Climb") && canClimb)
        {
            climb = true;
        } else if (Input.GetButtonUp("Climb")) {
            OnClimbing(false);
            climb = false;
            rb.gravityScale = 3;
        }
        
        // Eat
        if (Input.GetButtonDown("Eat") && !player.isSleeping)
        {
            if (nuts.nuts <= 0)
            return;
            audioManager.Play("Eating");
            eating = true;
            animator.SetBool("IsEating", true);
        }
    }

    private bool IsClimbable()
    {
        return (GetComponent<BoxCollider2D>().IsTouchingLayers(obstacleLayer));
    }


    public void OnCrouching()
    {
        animator.SetBool("IsCrouching", true);
    }

    public void OnClimbing(bool IsClimbing)
    {
        animator.SetBool("IsClimbing", IsClimbing);
    }
    public void OnLanding() {
        TakeFallDamage();
        animator.SetBool("IsFalling", false);
    }

    public void Stealth()
    {
        if (crouch && behindForeGround)
        {
            // Change sortinglayer to default
            player.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Default");
            // Layer 7 = enemy layer
            gameObject.layer = 7;
        }
    }

    public void UnStealth()
    {
        // Change sortinglayer to player
        player.GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Player");
        // Layer 6 = player layer
        gameObject.layer = 6;
    }



    void FixedUpdate() {
        // Move our character
        if (!player.isSleeping)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, eating);
            jump = false;
        }
        
        if (controller.m_Grounded && animator.GetBool("IsJumping") && rb.velocity.y == 0)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", false);
        }
        else if (rb.velocity.y < -2)
        {
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsFalling", true);
        }
        else
        {
            animator.SetBool("IsFalling", false);
        }

        if (climb)
        {
            if (IsClimbable())
            {
                OnClimbing(true);
                rb.gravityScale = 0;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                transform.position = new Vector3(transform.position.x, transform.position.y + (playerClimbSpeed / 100), transform.position.z);
            } else
            {
                OnClimbing(false);
                rb.gravityScale = 3;
            }
        }
    }

    public void TakeFallDamage()
    {
        if (rb.velocity.y < -fallThresholdVelocity)
        {
            int damage = Mathf.CeilToInt(Mathf.Abs(rb.velocity.y + fallThresholdVelocity)) * fallDamageMultiplier;
            player.TakeDamage(damage);
        }
    }
}