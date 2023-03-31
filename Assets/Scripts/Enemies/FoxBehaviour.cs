using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxBehaviour : MonoBehaviour
{

    Animator animator;
    EnemyAI enemyAI;
    Enemy enemy;
    MeleeEnemy meleeEnemy;
    Rigidbody2D rb;
    AudioSource audioSource;
    Player player;

    public bool sleepOnAwake;



    public bool isSleeping = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        enemyAI = GetComponentInParent<EnemyAI>();
        enemy = GetComponentInParent<Enemy>();
        meleeEnemy = GetComponentInParent<MeleeEnemy>();
        rb = GetComponentInParent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<Player>();

        if (sleepOnAwake)
        {
            Sleep();
        }
    }

    void Update()
    {
        if (sleepOnAwake)
        {
            if (enemy.maxHealth != enemy.currentHealth && isSleeping)
            {
                WakeUp();
            } else if (GetComponentInParent<BoxCollider2D>().IsTouchingLayers(LayerMask.GetMask("Player")))
            {
                WakeUp();
            }
        }
        if (enemy.isDead || player.isDead)
        {
            audioSource.Stop();
        }
    }

    void Sleep()
    {
        meleeEnemy.canAttack = false;
        isSleeping = true;
        animator.SetBool("IsSleeping", isSleeping);
        enemyAI.followEnabled = false;
        meleeEnemy.enabled = false;
    }

    public void WakeUp()
    {
        animator.SetTrigger("WokeUp");
        isSleeping = false;
        animator.SetBool("IsSleeping", isSleeping);
    }

    public void StartChase()
    {
        meleeEnemy.canAttack = true;
        enemyAI.followEnabled = true;
        meleeEnemy.enabled = true;
        audioSource.Play();
    }
}
