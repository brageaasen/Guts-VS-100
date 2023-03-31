using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEnterWater : MonoBehaviour
{
    public ParticleSystem waterPS;
    public ParticleSystem enemyWaterPS;
    private AudioManager audioManager;

    public float drownFrequency = 1f;

    private float time = 0.0f;

    GameObject player;
    GameObject enemy;
    PlayerMovement playerMovement;
    
    private SpriteRenderer sprite;

    void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        player = GameObject.FindWithTag("Player");
        enemy = GameObject.Find("Fox");
        sprite = GetComponent<SpriteRenderer>();
    
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        time += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            player.GetComponent<Player>().isUnderwater = true;
            player.GetComponent<SpriteRenderer>().sortingLayerName = "Water";
            audioManager.Play("EnterWater");
            waterPS.Play();
            audioManager.Play("Underwater");
        }
        if (collider2D.tag == "Enemy")
        {
            enemy.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Water";
            audioManager.Play("EnterWater");
            enemyWaterPS.Play();
        }
    }
    
    void OnTriggerStay2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Enemy")
        {
            if (enemy.GetComponent<EnemyAI>() != null)
                enemy.GetComponent<EnemyAI>().jumpEnabled = false;
                
            if (time >= drownFrequency && !enemy.GetComponent<Enemy>().isDead)
            {
                time = 0.0f;
                enemy.GetComponent<Enemy>().TakeDamage(20);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player")
        {
            player.GetComponent<Player>().isUnderwater = false;
            player.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
            audioManager.Play("EnterWater");
            audioManager.StopPlaying("Underwater");
            waterPS.Play();
        }
        if (collider2D.tag == "Enemy")
        {
            enemy.GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Enemies";
            audioManager.Play("EnterWater");
            enemyWaterPS.Play();
        }
    }

}
