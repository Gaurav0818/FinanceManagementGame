using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleWithLinkedScenario", menuName = "ScriptedData/Obstacle/ObstacleWithLinkedScenario")]
public class ObstacleWithLinkedScenario : Obstacle
{
    
    [System.Serializable]
    public enum ScheduleType
    {
        SameDay,             
        AnotherDay               
    }
    
    public override ObstacleType Type
    {
        get { return ObstacleType.WithScenario; }
    }
    
    [SerializeField] private Scenario m_LinkedScenario;
    [SerializeField] private Scenario.ScenarioType m_ScenarioType;
    [SerializeField] private ScheduleType m_ScheduleType;

    public override void AnswerQuestion(bool answer)
    {
        if (answer)
        {
            
        }
        else
        {
            
        }
    }
    
    private void IfAnswerIsTrue()
    {
        if (m_ScheduleType == ScheduleType.SameDay)
        {
            ImmediateSchedule();
        }
        else
        {
            DelayedSchedule();
        }
    }
    
    private void ImmediateSchedule()
    {
        
    }
    
    private void DelayedSchedule()
    {
        
    }
    
    
}
