using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    private bool canPause = true;

    public GameObject pauseMenuUI;
    [SerializeField] private GameObject gameOverMenuUI;
    [SerializeField] private GameObject dialoguePanelUI;
    [SerializeField] private GameObject NPC;
    
    private AudioManager audioManager;
    private LevelChanger levelChanger;

    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        levelChanger = GameObject.Find("LevelChanger").GetComponent<LevelChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (NPC != null)
            canPause = !NPC.GetComponent<NPC>().isTalking;

        if (Input.GetKeyDown(KeyCode.Escape) && (gameOverMenuUI.activeSelf == false) && (gameOverMenuUI.activeSelf == false) && canPause)
        {
            if (GameIsPaused) 
            {
                Resume();
            }
            else 
            {
                Pause();
            }
        }
    }

    public void PlaySelectSound()
    {
         audioManager.Play("Select");
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        if (NPC != null)
            if (NPC.GetComponent<NPC>().isTalking)
                dialoguePanelUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        dialoguePanelUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        levelChanger.FadeToLevel(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
