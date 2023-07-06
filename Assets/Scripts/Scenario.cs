using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario", menuName = "ScriptedData/Scenario")]
public class Scenario : ScriptableObject
{
    [System.Serializable]
    public enum ScenarioType
    {
        InsideHouse,
        InsideUniversity,
        InsideBus,
        InsideCinema,
        InsideRestaurant,
        InsideShop
    }
    
    [SerializeField] private ScenarioType m_ScenarioType;
    [SerializeField] private float m_Duration;
    
    public ScenarioType GetScenarioType()
    {
        return m_ScenarioType;
    }
    
    public float GetDuration()
    {
        return m_Duration;
    }
}
