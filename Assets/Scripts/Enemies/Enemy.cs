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

    public bool isDead;

    public MeleeEnemy combat;

    [SerializeField] private GameObject _headPrefab;
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

            if (rb.velocity.x > 0 || rb.velocity.x < 0 || rb.velocity.y > 0 || rb.velocity.y < 0)
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
        
        // Die animation / Disable enemy
        animator.SetBool("IsDead", true);
        gameObject.layer = LayerMask.NameToLayer("Dead");
        Destroy(gameObject.GetComponent<EnemyAI>());
        gameObject.GetComponent<Enemy>().enabled = false;
        gameObject.GetComponentInChildren<MeleeEnemy>().enabled = false;


        int random = Random.Range(0, 10);
        if (random < 4)
        {
            Debug.Log("NORMAL");
            //rb.gravityScale = 3;
        } else if (random < 6)
        {
            Debug.Log("BEHEAD");
            Behead();
        } else if (random > 6)
        {
            Debug.Log("CUT IN HALF");
            CutInHalf();
        }
    }

    // Spawn all upper/lower bodyparts and weapon on death
    private void CutInHalf()
    {
        // Remove sprite of enemy
        Destroy(gameObject.GetComponentInChildren<SpriteRenderer>());

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

    // Spawn head/lower bodyparts and weapon on death
    private void Behead()
    {
        // Remove sprite of enemy
        Destroy(gameObject.GetComponentInChildren<SpriteRenderer>());

        // Spawn the upper body part.
        GameObject head = Instantiate(_headPrefab, transform.position, Quaternion.identity);
        KnockbackFeedback headKnockback = head.GetComponent<KnockbackFeedback>();
        headKnockback.PlayFeedback(GameObject.FindGameObjectWithTag("Player"));

        // Spawn the weapon.
        GameObject weapon = Instantiate(_weaponPrefab, transform.position, Quaternion.identity);
        KnockbackFeedback weaponKnockback = weapon.GetComponent<KnockbackFeedback>();
        weaponKnockback.PlayFeedback(GameObject.FindGameObjectWithTag("Player"));
    }
}
