using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManger : Singleton<ObstacleManger>
{
    private Obstacle m_CurrentObstacle;

    [SerializeField] private int m_TimeToStartObstacle = 10;
    [SerializeField] private int m_TimeToCloseObstacle = 50;

    private Scenario m_LinkedScenario;
    private TimelineManager.DayTypeInWhichScenarioCanBeUsed m_LinkedScenarioType;

    public List<int> possibleDays;
    public List<int> freeTimeOfDay;
    private int seletedDay;
    private int seletedTime;
    
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
    
    private void StartDaySchedulePanel()
    {
        if(m_CurrentObstacle)
            UiManager.Instance.OpenObstacle(m_CurrentObstacle);
    }
    
    public void CloseDaySchedulePanel()
    {
        UiManager.Instance.CloseObstacle();
    }
    
    private void StartTimeSchedulePanel()
    {
        if(m_CurrentObstacle)
            UiManager.Instance.OpenObstacle(m_CurrentObstacle);
    }
    
    public void CloseTimeSchedulePanel()
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

    public void NeedToScheduleScenario(Scenario scenario, TimelineManager.DayTypeInWhichScenarioCanBeUsed scenarioType)
    {
        m_LinkedScenarioType = scenarioType;
        m_LinkedScenario = scenario;
        possibleDays =  TimelineManager.Instance.GetAllPossibleDays(m_LinkedScenarioType);
        UiManager.Instance.OpenDaySchedulePanel();
    }
    
    public void SelectedDaySchedule(int day)
    {
        seletedDay = day;
        freeTimeOfDay = TimelineManager.Instance.GetFreeTimeOfDay(day, (int)m_LinkedScenario.GetDuration());
        UiManager.Instance.OpenTimeSchedulePanel();
    }    

    public void SelectedTimeSchedule(int time)
    {
        TimelineManager.Instance.AddScenarioToDay(seletedDay ,time, m_LinkedScenario);
    }

}
