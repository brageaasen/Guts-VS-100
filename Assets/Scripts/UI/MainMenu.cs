using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;

    void Awake()
    {
        this.audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        this.audioManager.StopPlaying("MusicGame");
		if (SceneManager.GetActiveScene().name == "MainMenu")
			this.audioManager.Play("MusicMenu");
    }

    public void PlaySelectSound()
    {
        audioManager.Play("Select");
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void StopMusic()
    {
        this.audioManager.StopPlaying("MusicMenu");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
