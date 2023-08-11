using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    private Rigidbody2D rb;
    private PlayerMovementWithDash pm;

    [SerializeField] private CinemachineVirtualCamera cinemachine;


    public float attackRate = 2f;
    float nextAttackTime = 0f;
    public enum LastAttackDirection
    {
        FORWARD, UP, DOWN
    }
    public LastAttackDirection lastAttackDirection;



    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public int currentCombo;
    public bool isAttacking;

    public bool canAttack = false;

    public float stepForce = 200;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovementWithDash>();
    }

    // Update is called once per frame
    void Update() {
        
        //if (Time.time >= nextAttackTime)
        //{
            //if (Input.GetButtonDown("Attack") && canAttack)
            //{
            //    animator.SetTrigger("Attack");
            //    nextAttackTime = Time.time + 1f / attackRate;
            //}
            AttackUp();
            ComboAttack();
        //}
    }

    // Used in animation events
    void Attack() {
        // Apply camera shake
        CinemachineShake.Instance.ShakeCamera(5f, .1f);

        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies
        foreach (Collider2D enemy in hitEnemies) {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }

    // Used in animation events
    void StartComboAttack()
    {
        SetIsAttacking(false);
        if (currentCombo < 2)
        {
            currentCombo++;
        }
        else {
            currentCombo = 1;
        }
    }

    // Used in animation events
    void FinishComboAttack()
    {
        SetIsAttacking(false);
        currentCombo = 0;
    }
    
    void ComboAttack()
    {
        if (Input.GetButtonDown("Attack") && !isAttacking && (pm.LastOnGroundTime > 0))
        {
            lastAttackDirection = LastAttackDirection.FORWARD;
            //Debug.Log($"Attacked Forward! Combo: {currentCombo}");
            SetIsAttacking(true);
            animator.SetTrigger("GroundAttack" + currentCombo);
        }
    }

    void AttackUp()
    {
        if (Input.GetButtonDown("Attack") && Input.GetButton("Up") && !isAttacking && (pm.LastOnGroundTime > 0))
        {
            lastAttackDirection = LastAttackDirection.UP;
            //Debug.Log("Attacked Up!}");
            SetIsAttacking(true);
            animator.SetTrigger("GroundAttackUp");
        }
    }

    void PushPlayer()
    {
        rb.AddForce(new Vector2((stepForce * transform.localScale.x), 0));
    }

    void OnDrawGizmosSelected() {
        if (attackPoint == null) {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void SetIsAttacking(bool state)
    {
        //Debug.Log($"Changed state to {state}");
        isAttacking = state;
        animator.SetBool("IsAttacking", state);
    }
}