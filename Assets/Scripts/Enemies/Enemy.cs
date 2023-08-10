using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{

    public Animator animator;
    Rigidbody2D rb;
    KnockbackFeedback knockback;

    public HealthBar healthBar;
    public int maxHealth = 100;
    public int currentHealth;

    public bool isDead = false;

    public MeleeEnemy combat;

    [SerializeField] private GameObject _upperBodyPrefab;
    [SerializeField] private GameObject _lowerBodyPrefab;
    [SerializeField] private GameObject _weaponPrefab;
    public Vector2 forceDirection; // Set this vector to determine the direction in which the body parts should split.

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        
        GetComponent<EdgeCollider2D>().enabled = false;

        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<KnockbackFeedback>();
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
            knockback.PlayFeedback(GameObject.FindGameObjectWithTag("Player"));
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
        if (isDead) // Check if the enemy is already dead
            return;
        isDead = true;

        // Die animation
        animator.SetBool("IsDead", true);

        // Disable enemy
        //GetComponent<BoxCollider2D>().enabled = false;
        //GetComponent<CircleCollider2D>().enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Dead");
        Destroy(GetComponent<EnemyAI>());
        Destroy(GetComponent<AIPath>());
        Destroy(GetComponentInChildren<SpriteRenderer>());
        rb.gravityScale = 3;

        // Spawn the upper body part.
        GameObject upperBody = Instantiate(_upperBodyPrefab, transform.position, Quaternion.identity);
        KnockbackFeedback upperKnockback = upperBody.GetComponent<KnockbackFeedback>();
        upperKnockback.PlayFeedback(GameObject.FindGameObjectWithTag("Player"));

        // Spawn the lower body part.
        GameObject lowerBody = Instantiate(_lowerBodyPrefab, transform.position, Quaternion.identity);
        KnockbackFeedback lowerKnockback = lowerBody.GetComponent<KnockbackFeedback>();
        lowerKnockback.PlayFeedback(GameObject.FindGameObjectWithTag("Player"));

        // Spawn the weapon.
        GameObject weapon = Instantiate(_weaponPrefab, transform.position, Quaternion.identity);
        KnockbackFeedback weaponKnockback = weapon.GetComponent<KnockbackFeedback>();
        weaponKnockback.PlayFeedback(GameObject.FindGameObjectWithTag("Player"));
    }

}
