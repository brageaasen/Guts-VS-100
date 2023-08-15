using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWonScreen : MonoBehaviour
{

    public GameObject gameWonMenuUI;

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
        if (false) // Currently no win condition
        {
            player.GetComponent<PlayerMovementWithDash>().enabled = false;
            gameWonMenuUI.SetActive(true);
            timer.StopTimer();
        }
    }

    public void PlaySelectSound()
    {
         audioManager.Play("Select");
    }
}