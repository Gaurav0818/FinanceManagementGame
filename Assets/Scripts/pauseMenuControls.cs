using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenuControls : MonoBehaviour
{
    public GameObject pauseMenuPanel;
    void Start()
    {
        pauseMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuPanel.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                pauseMenuPanel.SetActive(true);
                TimelineManager.Instance.PauseTimeScale();
            }
        }
    }
    
    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        TimelineManager.Instance.ResumeTimeScale();
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
