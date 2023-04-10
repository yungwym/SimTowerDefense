using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPathfindState : EnemyState
{
    public EnemyStateId GetId()
    {
        return EnemyStateId.RoadPathfind;
    }

    public void Enter(EnemyController enemyController)
    {
        //enemyController.GetNavMeshAgent().autoBraking = false;
        enemyController.GetNavMeshAgent().destination = enemyController.GetCastleTargetTransform().position;
    }

    public void Update(EnemyController enemyController)
    {

    }

    public void Exit(EnemyController enemyController)
    {

    }
}
