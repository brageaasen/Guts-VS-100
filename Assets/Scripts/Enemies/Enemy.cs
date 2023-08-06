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
        isDead = true;

        // Die animation
        animator.SetBool("IsDead", true);

        // Disable enemy
        //GetComponent<BoxCollider2D>().enabled = false;
        //GetComponent<CircleCollider2D>().enabled = false;
        gameObject.layer = LayerMask.NameToLayer("Dead");
        Destroy(GetComponent<EnemyAI>());
        Destroy(GetComponent<AIPath>());
        rb.gravityScale = 3;

        // Disable the main sprite renderer (or any other visual components of the original enemy).
        // Optionally, you can also play a death animation or sound effect here.

        forceDirection = new(1.5f, 1.5f);

        // Spawn the upper body part.
        GameObject upperBody = Instantiate(_upperBodyPrefab, transform.position, Quaternion.identity);
        Rigidbody2D upperRigidbody = upperBody.GetComponent<Rigidbody2D>();
        upperRigidbody.AddForce(forceDirection, ForceMode2D.Impulse);

        // Spawn the lower body part.
        GameObject lowerBody = Instantiate(_lowerBodyPrefab, transform.position, Quaternion.identity);
        Rigidbody2D lowerRigidbody = lowerBody.GetComponent<Rigidbody2D>();
        lowerRigidbody.AddForce(-forceDirection, ForceMode2D.Impulse);

        // Destroy the original enemy GameObject.
        Destroy(gameObject);
    }

}
