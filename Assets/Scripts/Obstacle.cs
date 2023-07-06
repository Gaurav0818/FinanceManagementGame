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
    
    public abstract void AnswerQuestion(bool answer);
}
