using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;

    public Transform attackPoint;
    public LayerMask enemyLayers;

    private Rigidbody2D rb;
    private PlayerMovementWithDash pm;


    public float attackRate = 2f;
    float nextAttackTime = 0f;


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

            ComboAttack();
        //}

    }

    void Attack() {
        // Detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage enemies
        foreach (Collider2D enemy in hitEnemies) {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

    }

    void StartComboAttack()
    {
        isAttacking = false;
        if (currentCombo < 3)
        {
            currentCombo++;
        }
    }

    void FinishComboAttack()
    {
        isAttacking = false;
        currentCombo = 0;
    }
    void ComboAttack()
    {
        if (Input.GetButtonDown("Attack") && !isAttacking)
        {
            isAttacking = true;
            animator.SetTrigger("GroundAttack" + currentCombo);
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
}