using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStateId
{
    OpenPathfind,
    RoadPathfind
}
public interface EnemyState
{
    EnemyStateId GetId();
    void Enter(EnemyController enemyCon);
    void Update(EnemyController enemyCon);
    void Exit(EnemyController enemyCon);
}
