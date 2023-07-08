using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManger : Singleton<ObstacleManger>
{
    private Obstacle m_CurrentObstacle;

    [SerializeField] private int m_TimeToStartObstacle = 10;
    [SerializeField] private int m_TimeToCloseObstacle = 50;
    
    private void Awake()
    {
        EventManager.OnMinuteIncreaseEvent += MinuteIncreaseEvent;
    }

    public void SetCurrentObstacle(Obstacle obstacle)
    {
        m_CurrentObstacle = obstacle;
    }

    private void MinuteIncreaseEvent()
    {
        if(TimelineManager.Instance.GetCurrentTimeInMin() == m_TimeToStartObstacle )
        {
            StartObstacle();
        }
        
        if(TimelineManager.Instance.GetCurrentTimeInMin() == m_TimeToCloseObstacle )
        {
            CloseObstacle();  
        }
    }

    private void StartObstacle()
    {
        if(m_CurrentObstacle)
            UiManager.Instance.OpenObstacle(m_CurrentObstacle);
    }
    
    public void CloseObstacle()
    {
        UiManager.Instance.CloseObstacle();
    }
    
    public void TrueSelected()
    {
        m_CurrentObstacle.AnswerQuestion(true);
    }
    
    public void FalseSelected()
    {
        m_CurrentObstacle.AnswerQuestion(false);
    }
}
