using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ScenarioManger : Singleton<ScenarioManger>
{
    private TimelineManager.ScenarioData m_CurrentScenario;
    private Obstacle m_CurrentObstacle;

    private void Awake()
    {
        EventManager.OnHourIncreaseEvent += HourIncreaseEvent;
    }

    public void StartScenario(TimelineManager.ScenarioData scenario)
    {
        m_CurrentScenario = scenario;
        m_CurrentObstacle = scenario.scenario.GetObstacle();    
    }
    
    public Scenario GetScenario()
    {
        return m_CurrentScenario.scenario;
    }
    
    private void StartNextScenario()
    {
        int Index = TimelineManager.Instance.GetCurrentDay().scenarioList.IndexOf(m_CurrentScenario);
        Index++;
        
        if (Index < TimelineManager.Instance.GetCurrentDay().scenarioList.Count)
        {
            StartScenario(TimelineManager.Instance.GetCurrentDay().scenarioList[Index]);
        }
        else
        {
            TimelineManager.Instance.SkipHour();
        }
    }

    private void HourIncreaseEvent()
    {
        if( m_CurrentScenario.StartTime + m_CurrentScenario.scenario.GetDuration() >= TimelineManager.Instance.GetCurrentTimeInHr())
            StartNextScenario();
    }
    
}
