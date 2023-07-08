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

    [SerializeField] private List<Obstacle> m_PossibleObstacles;
    [SerializeField] public Sprite  m_ScenarioImage;
    
    private Obstacle m_ScenaioObstacle;
    private bool m_IsDone = false;
    
    public ScenarioType GetScenarioType()
    {
        return m_ScenarioType;
    }
    
    public float GetDuration()
    {
        return m_Duration;
    }

    private void Awake()
    {
        if (Random.Range(0, 100) < 50)
            m_ScenaioObstacle = GetRandomObstacle();
        else
            m_ScenaioObstacle = null;
        
    }

    private Obstacle GetRandomObstacle()
    {
        return m_PossibleObstacles[Random.Range(0, m_PossibleObstacles.Count)];
    }
    
    public Obstacle GetObstacle()
    {
        return m_ScenaioObstacle;
    }
    
    public void SetScenarioToDone()
    {
        m_IsDone = true;
    }
    
    public bool IsScenarioDone()
    {
        return m_IsDone;
    }
}
