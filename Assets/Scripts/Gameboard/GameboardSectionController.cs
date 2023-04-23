using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameboardSectionController : MonoBehaviour
{
    [SerializeField] private int column;
    [SerializeField] private int row;

    //Types of Tile Lists
   // List<GameObject> pendingRoadList;

    public List<Tile> allTilesList;

    //Individual Tile Set Lists
    public List<Tile> blankTilesList;
    public List<Tile> elevatedTilesList;
    public List<Tile> roadTilesList;
    public List<Tile> towerTilesList;
    public List<Tile> resourceTilesList;
    public List<Tile> enemySpawnTilesList;

    //public List<Tile> occupiedTiles;

    //Enemy Prefabs 
    [SerializeField] private GameObject enemySpawnerPrefab;

    private float timeToWaitForEnemySpawner = 5.0f;

    //ResourceTiles
    [SerializeField] List<GameObject> availableResourcesToSpawnList;


    private bool b_GameboardSectionIsUnderEdit = false;

    //NavMeshSurface
    [SerializeField] private NavMeshSurface navMeshSurface;

    private void Start()
    {
        StartCoroutine(SpawnEnemySpawner());
    }


    public void AddToAllTileList(Tile tile)
    {
        allTilesList.Add(tile);
        blankTilesList.Add(tile);
    }

    public void AddRoadTiles(Tile tile)
    {
        blankTilesList.Remove(tile);
        roadTilesList.Add(tile);
    }

    public void RebuildNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }


    public void SetColumnAndRow(int _col, int _row) 
    {
        column = _col;
        row = _row;
    }

    
    public void SetGameboardSectionUnderEdit(bool isUnderEdit)
    {
        b_GameboardSectionIsUnderEdit = isUnderEdit;

        foreach (Tile tile in allTilesList)
        {
            tile.SetBoolParentSectionUnderEdit(isUnderEdit);
        }
    }
     


    //Function: To Determine the tiles that are spawnable 

    //Functions to Added Specific Tiles to the Gameboard

    //Enemy Spawner
    private IEnumerator SpawnEnemySpawner()
    {
        yield return new WaitForSeconds(timeToWaitForEnemySpawner);

        //Get random tile index from available tiles
        int randomTileIndex = Random.Range(0, blankTilesList.Count);

        //Get random tile from list
        Tile tileToSpawnOn = blankTilesList[randomTileIndex];

        //Make new position for spawner and instantiate spawner
        Vector3 spawnerPosition = new Vector3(tileToSpawnOn.transform.position.x, tileToSpawnOn.transform.position.y + 0.40f, tileToSpawnOn.transform.position.z);
        GameObject newEnemySpawner = Instantiate(enemySpawnerPrefab, spawnerPosition, tileToSpawnOn.transform.rotation);

        newEnemySpawner.transform.parent = tileToSpawnOn.gameObject.transform;

        //Set availability and tile Item
        tileToSpawnOn.SetTileAvailability(false);
        tileToSpawnOn.tileItem = newEnemySpawner;
        tileToSpawnOn.tileType = TileType.ENEMY;

        tileToSpawnOn.GetComponentInChildren<EnemySpawnerController>().SetResourceTilesList(resourceTilesList);

        //Remove and add tile to proper list 
        blankTilesList.Remove(tileToSpawnOn);
        enemySpawnTilesList.Add(tileToSpawnOn);

        //Increase Time and Call Again
        timeToWaitForEnemySpawner += 5.0f;

        StartCoroutine(SpawnEnemySpawner());

    }


    public void SpawnResourceTile()
    {
        int chanceForNeighbour = 60;

        //Generate random amount of resource tiles to spawn
        int randomAmount = Random.Range(4, 8);

        for (int i = 0; i < randomAmount; i++)
        {
            int randResourceIndex = Random.Range(0, availableResourcesToSpawnList.Count);
            GameObject randResource = availableResourcesToSpawnList[randResourceIndex];

            //Get Random index
            int randomTileIndex = Random.Range(0, blankTilesList.Count);
            Tile tileToSpawnOn = blankTilesList[randomTileIndex];

            tileToSpawnOn.InstantiateNewResourceOnTile(randResource);

            //While Loop - To elevate multiple neighbouring tiles
            while (true)
            {
                //Generate num for chance to Elevated a neighbour tile 
                int zeroToHundred = Random.Range(0, 101);
                //If so
                if (zeroToHundred <= chanceForNeighbour)
                {
                    //Get Neighbour Tiles of the newly elevated Tile
                    List<Tile> newResourceAdjacents = tileToSpawnOn.GetNeighbourTiles();

                    //Generate Random Index for the neighbour tile
                    int randomNeighIndex = Random.Range(0, newResourceAdjacents.Count);

                    //Get random neighbour tile 
                    Tile newResourceNeighbour = newResourceAdjacents[randomNeighIndex];

                    //Check if null
                    if (newResourceNeighbour != null && newResourceNeighbour.tileType == TileType.BLANK)
                    {
                        newResourceNeighbour.ElevateTile(randResource);

                        blankTilesList.Remove(newResourceNeighbour);
                        elevatedTilesList.Add(newResourceNeighbour);

                        tileToSpawnOn = newResourceNeighbour;

                        chanceForNeighbour -= 5;
                    }
                }
                else
                {
                    break;
                }
            }
            //Remove and add to proper list 
            blankTilesList.Remove(tileToSpawnOn);
            elevatedTilesList.Add(tileToSpawnOn);
            //UpdateEnemySpawnersResourceList(tileToSpawnOn);
        }
    }

    public void SpawnElevatedTile(GameObject elevatedTilePrefab)
    {
        //Chance for a neighbour tile to be elevated
        int chanceForNeighbour = 80;

        //Get random amount of elevated tiles to spawn 
        int randAmount = Random.Range(16, 22);

        for (int i = 0; i < randAmount; i++)
        {
            //Get Random number for index of random Elevated Tile 
            int randomIndex = Random.Range(0, blankTilesList.Count);
            Tile newElevatedTile = blankTilesList[randomIndex];
            //Elevated Tile
            newElevatedTile.ElevateTile(elevatedTilePrefab);

            //While Loop - To elevate multiple neighbouring tiles
            while (true)
            {
                //Generate num for chance to Elevated a neighbour tile 
                int zeroToHundred = Random.Range(0, 101);
                //If so
                if (zeroToHundred <= chanceForNeighbour)
                {
                    //Get Neighbour Tiles of the newly elevated Tile
                    List<Tile> newElevatedAdjacents = newElevatedTile.GetNeighbourTiles();

                    //Generate Random Index for the neighbour tile
                    int randomNeighIndex = Random.Range(0, newElevatedAdjacents.Count);

                    //Get random neighbour tile 
                    Tile newElevatedNeighbour = newElevatedAdjacents[randomNeighIndex];

                    //Check if null
                    if (newElevatedNeighbour != null && newElevatedNeighbour.tileType == TileType.BLANK)
                    {
                        newElevatedNeighbour.ElevateTile(elevatedTilePrefab);

                        blankTilesList.Remove(newElevatedNeighbour);
                        elevatedTilesList.Add(newElevatedNeighbour);

                        newElevatedTile = newElevatedNeighbour;

                        chanceForNeighbour -= 5;
                    }
                }
                else
                {
                    break;
                }
            }

            blankTilesList.Remove(newElevatedTile);
            elevatedTilesList.Add(newElevatedTile);
        }
    }

    /*
    private void UpdateEnemySpawnersResourceList(Tile newResourceList)
    {
        foreach (Tile enemyTile in enemySpawnTilesList)
        {
            enemyTile.GetComponent<EnemySpawnerController>().AddToResourceTilesList(newResourceList);
        }

    }
    */

}
