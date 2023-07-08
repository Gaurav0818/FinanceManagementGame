using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : Singleton<GameManger>
{
    public GameObject startPanel;
    public GameObject endPanel;
    private void Start()
    {
        startPanel.SetActive(true);
        endPanel.SetActive(false);
        TimelineManager.Instance.gameMode = TimelineManager.GameMode.WeekBased;
    }

    public void Select7DayDifficulty()
    {
        TimelineManager.Instance.gameMode = TimelineManager.GameMode.WeekBased;
    }
    
    public void Select30DayDifficulty()
    {
        TimelineManager.Instance.gameMode = TimelineManager.GameMode.MonthBased;
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        EventManager.TriggerStartGameEvent();
    }
    
    public void GameEnd()
    {
        endPanel.SetActive(true);
    }
    
    public void GoToMenu()
    {
        startPanel.SetActive(true);
        endPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
