using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : ScriptableObject
{
    [System.Serializable]
    public enum ObstacleType
    {
        WithoutScenario,
        WithScenario
    }

    public abstract ObstacleType Type { get; }
    
    [SerializeField] private string m_Question;
    [SerializeField] private string m_AnswerTrue = "True";
    [SerializeField] private string m_AnswerFalse = "False";
    
    public abstract void AnswerQuestion(bool answer);
    
    public string GetQuestion()
    {
        return m_Question;
    }

    public string GetAnsTrue()
    {
        return m_AnswerTrue;
    }
    
    public string GetAnsFalse()
    {
        return m_AnswerFalse;
    }
}
