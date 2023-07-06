using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleWithLinkedScenario", menuName = "ScriptedData/Obstacle/ObstacleWithLinkedScenario")]
public class ObstacleWithLinkedScenario : Obstacle
{
    public override ObstacleType Type
    {
        get { return ObstacleType.WithScenario; }
    }
    
    [SerializeField] private Scenario m_LinkedScenario;
}
