using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour
{
    //Enemy Prefab
    [SerializeField] private GameObject enemyToSpawn;

    [SerializeField] Transform enemySpawnPoint;

    //Wave Variables 
    [SerializeField] private int timeBetweenWavesMin = 10;
    [SerializeField] private int timeBetweenWavesMax = 100;
    private float timeBetweenWaves = 0;

    [SerializeField] private int enemiesInWave = 5;

    //Initial Value for when spawner is instaniated, value overwritten after first spawn
    [SerializeField] private float countDownBetweenWaves = 8;


    [SerializeField] private bool tileIsConnectedToRoad = false;


    [SerializeField]  private List<Tile> resourceTiles;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnWaveIfAvailable();
    }


    private void SpawnWaveIfAvailable()
    {
        if (countDownBetweenWaves <= 0.0f && tileIsConnectedToRoad)
        {
            GenerateRandomTimeBetweenWaves();
            StartCoroutine(SpawnEnemyWave());
        }
        countDownBetweenWaves -= Time.deltaTime;
    }

    IEnumerator SpawnEnemyWave()
    {
        //Spawn Enemy Loop
        for (int i = 0; i < enemiesInWave; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(2.0f);
        }
    }

    private void SpawnEnemy()
    {
        GameObject spawnedEnemy = Instantiate(enemyToSpawn, enemySpawnPoint.position, enemySpawnPoint.rotation);
        spawnedEnemy.GetComponent<EnemyController>().SetResourceTargetObjectsList(resourceTiles);

        Debug.Log("Spawned Enemy");
    }

    private void GenerateRandomTimeBetweenWaves()
    {
        timeBetweenWaves = Random.Range(timeBetweenWavesMin, timeBetweenWavesMax);
        countDownBetweenWaves = timeBetweenWaves;
    }

    public void SetIsConnectedToRoad(bool isConnected)
    {
        Debug.Log("Set");

        tileIsConnectedToRoad = isConnected;
    }

    public void SetResourceTilesList(List<Tile> newResourceTileList)
    {
        resourceTiles = newResourceTileList;
    }

}
