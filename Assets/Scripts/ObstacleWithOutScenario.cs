using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleWithOutScenario", menuName = "ScriptedData/Obstacle/ObstacleWithOutScenario")]
public class ObstacleWithOutScenario : Obstacle
{
    public override ObstacleType Type
    {
        get { return ObstacleType.WithoutScenario; }
    }

    public override void AnswerQuestion(bool answer)
    {
        if (answer)
            IfAnswerIsTrue();
        else
            IfAnswerIsFalse();

        ObstacleManger.Instance.CloseObstacle();
    }
}
