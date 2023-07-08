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
    [SerializeField] private TimelineManager.DayTypeInWhichScenarioCanBeUsed m_ScenarioType;
    [SerializeField] private ScheduleType m_ScheduleType;

    public TimelineManager.DayTypeInWhichScenarioCanBeUsed GetScenarioType()
    {
        return m_ScenarioType;
    }
    
    public override void AnswerQuestion(bool answer)
    {
        ObstacleManger.Instance.CloseObstacle();
        if (answer)
        {
            ObstacleManger.Instance.NeedToScheduleScenario(m_LinkedScenario, m_ScenarioType);
            IfAnswerIsTrue();
        }
        else
        {
            IfAnswerIsFalse();
        }
    }
    
}
