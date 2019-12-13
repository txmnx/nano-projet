using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject controlMenuUI;
    public GameObject pauseMenuUI;

    public Animator player1Animator;
    public Animator player2Animator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void Resume()
    {
        //pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        player1Animator.enabled = true;
        player2Animator.enabled = true;
    }
    public void Pause()
    {
        Debug.Log("qzdzaidazdlknazdn");
        //pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        player1Animator.enabled = false;
        player2Animator.enabled = false;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
    public void ControlMenu()
    {/*
        controlMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);*/
    }

    public void QuitControlMenu()
    {
        //controlMenuUI.SetActive(false);
        //pauseMenuUI.SetActive(true);
    }
    public void OptionMenu()
    {

    }
}
