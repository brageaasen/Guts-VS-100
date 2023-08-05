using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    public HealthBar healthBar;

    public int maxHealth = 100;
    public int currentHealth;
    public int nutHealing = 40;
    public bool isDead = false;

    public bool isUnderwater = false;

    public static Vector2 lastCheckpointPos = new Vector2(0f, 0f);

    private NutCollecter nuts;
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;

    private Vector3 startPos = new Vector3(0f, 0, 0f);

    [HideInInspector] public bool isSleeping = false;

    void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = lastCheckpointPos;
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
/*         if (this.transform.position == startPos)
        {
            isSleeping = true;
            animator.SetBool("IsSleeping", isSleeping);
        } */
        // currentHealth = maxHealth - 40;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currentHealth);
        nuts = GetComponent<NutCollecter>();
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        // Play hurt animation
        animator.SetTrigger("Hurt");
        
        playerCombat.SetIsAttacking(false);

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

        // Disable player movement
        GetComponent<PlayerMovement>().enabled = false;
    }

    public void Eat()
    {
        playerMovement.eating = false;
        nuts.DecrementNuts();
        currentHealth += nutHealing;
        if (currentHealth > maxHealth)
        {
            currentHealth -= (currentHealth - maxHealth);
        }
        healthBar.SetHealth(currentHealth);
        animator.SetBool("IsEating", false);
    }

    public void WakePlayer()
    {
        isSleeping = false;
        animator.SetBool("IsSleeping", isSleeping);
        playerCombat.canAttack = true;
    }
}
