using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{

    public Animator animator;
    Rigidbody2D rb;

    public HealthBar healthBar;
    public int maxHealth = 100;
    public int currentHealth;

    public bool isDead = false;

    public MeleeEnemy combat;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
        GetComponent<EdgeCollider2D>().enabled = false;

        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

            if ((rb.velocity.x > 0 || rb.velocity.x < 0) || (rb.velocity.y > 0 || rb.velocity.y < 0))
            {
                animator.SetBool("IsMoving", true);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }

    }

    public void TakeDamage(int damage)
    {
        if (!isDead)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);

            // Play hurt animation
            animator.SetTrigger("Hurt");
            combat.ResetAttackCooldown();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;

        // Die animation
        animator.SetBool("IsDead", true);

        // Disable enemy
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        Destroy(GetComponent<EnemyAI>());
        Destroy(GetComponent<AIPath>());
        rb.gravityScale = 3;
    }

}
