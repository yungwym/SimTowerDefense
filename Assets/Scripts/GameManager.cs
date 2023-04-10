using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameboardSectionGenerator gameboardSectionGenerator;

    [SerializeField] private GameboardSectionController[,] gameboardSections;

    [SerializeField] private BuildController buildController;

    //Enemy Spawner Prefabs
  //  [SerializeField] private GameObject enemySpawnerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        GameboardSectionController initialGameboardSection = gameboardSectionGenerator.GenerateBlankBoard(18, 18, BoardSectionType.HOME);

        buildController.SetCurrentGameboardSectionUnderEdit(initialGameboardSection);

      //  initialGameboardSection.SpawnEnemySpawner(enemySpawnerPrefab);
    }

  
}
