using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "Scenario", menuName = "ScriptedData/Scenario")]
public class Scenario : ScriptableObject
{
    
    [SerializeField] private string m_ScenarioName;
    [SerializeField] private float m_Duration;

    [SerializeField] private List<Obstacle> m_PossibleObstacles;
    [SerializeField] public Sprite  m_ScenarioImage;
    
    private Obstacle m_ScenaioObstacle;

    public string GetScenarioName()
    {
        return m_ScenarioName;
    }
    
    public float GetDuration()
    {
        return m_Duration;
    }
    
    public void GenerateRandomObstacle()
    {
        if(m_PossibleObstacles.Count == 0)
            return;
        
        if (Random.Range(0, 100) < 100)
            m_ScenaioObstacle =  m_PossibleObstacles[Random.Range(0, m_PossibleObstacles.Count)];
        else
            m_ScenaioObstacle = null;
    }
    
    public Obstacle GetObstacle()
    {
        return m_ScenaioObstacle;
    }
    
}
