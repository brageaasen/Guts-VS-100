using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{

    public GameObject gameOverMenuUI;

    [SerializeField] private Timer timer;

    private GameObject player;
    private AudioManager audioManager;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().isDead || (timer.GetTime() <= 0))
        {
            player.GetComponent<PlayerMovementWithDash>().enabled = false;
            player.GetComponent<PlayerCombat>().enabled = false;
            gameOverMenuUI.SetActive(true);
        }
    }

    public void PlaySelectSound()
    {
         audioManager.Play("Select");
    }
}