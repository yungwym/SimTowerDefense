using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPathfindState : EnemyState
{

    private Transform targetResource;




    public EnemyStateId GetId()
    {
        return EnemyStateId.OpenPathfind;
    }

    public void Enter(EnemyController enemyController)
    {
        FindClosestResourceTile(enemyController);
    }

    public void Update(EnemyController enemyController)
    {
        
    }

    public void Exit(EnemyController enemyController)
    {

    }

    private void FindClosestResourceTile(EnemyController enemyController)
    {
        List<Tile> resourceTiles = enemyController.GetResourceTargetObjectsList();

        float shortestDistance = Mathf.Infinity;
        
        for (int i = 0; i < resourceTiles.Count; i++)
        {
            float distanceBetween = Vector3.Distance(enemyController.transform.position, resourceTiles[i].transform.position);

            if (distanceBetween < shortestDistance)
            {
                shortestDistance = distanceBetween;

                targetResource = resourceTiles[i].transform;
            }
        }


        MoveToClosestResourceTile(enemyController);
    }

    private void MoveToClosestResourceTile(EnemyController enemyController)
    {
        enemyController.GetNavMeshAgent().destination = targetResource.position;
    }
}
