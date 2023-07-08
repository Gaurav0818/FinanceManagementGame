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
    [SerializeField] private string m_AnswerTrue;
    [SerializeField] private string m_AnswerFalse;
    
        
    public int m_CurrencyCost;
    public int m_SatisfactionIncreaseBy;
    public int m_SatisfactionDecreaseBy;
    
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
    
    protected void IfAnswerIsTrue()
    {
        ScoreManger.Instance.DecreaseCurrency(m_CurrencyCost);
        ScoreManger.Instance.AddSatisfactionScore(m_SatisfactionIncreaseBy);
    }
    
    protected void IfAnswerIsFalse()
    {
        ScoreManger.Instance.DecreaseSatisfactionScore(m_SatisfactionDecreaseBy);
    }
}
