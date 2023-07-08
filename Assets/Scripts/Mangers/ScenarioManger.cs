using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ScenarioManger : Singleton<ScenarioManger>
{
    private TimelineManager.ScenarioData m_CurrentScenario;

    private void Awake()
    {
        EventManager.OnHourIncreaseEvent += HourIncreaseEvent;
        EventManager.OnStartGameEvent += StartGameEvent;
    }

    private void StartGameEvent()
    {
        
    }
    
    public void StartScenario(TimelineManager.ScenarioData scenario)
    {
        m_CurrentScenario = scenario;
        scenario.scenario.GenerateRandomObstacle();
        ObstacleManger.Instance.SetCurrentObstacle(scenario.scenario.GetObstacle());    
        
        UiManager.Instance.SetScenarioImage();
        UiManager.Instance.RefreshSchedule();
    }
    
    public Scenario GetCurrentScenario()
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
            TimelineManager.Instance.StartNextDay();
        }
    }

    private void HourIncreaseEvent()
    {
        if (m_CurrentScenario.StartTime + m_CurrentScenario.scenario.GetDuration() <= TimelineManager.Instance.GetCurrentTimeInHr())
        {
            //TimelineManager.Instance.MarkScenarioAsDone(m_CurrentScenario);
            StartNextScenario();
        }
        else
        {
            m_CurrentScenario.scenario.GenerateRandomObstacle();
        }
        
    }
    
    


}
