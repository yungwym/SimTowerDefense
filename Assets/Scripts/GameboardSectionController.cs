using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameboardSectionController : MonoBehaviour
{
    //Road System Controller
    private RoadSystemController roadSystemController;

    //Gameboard Grid Variables 
    public GameObject[,] grid;
    public Transform gridStartPosition;
    private int GridRowSize;
    private int GridColumnSize;
    private int halfGridSize;
    private float tileSize = 1.0f;

    //Tile Prefabs 
    public GameObject blankTilePrefab;
    public GameObject blankElevatedPrefab;
    public GameObject quadPrefab;


    //Types of Tile Lists
    List<GameObject> pendingRoadList;

    List<GameObject> roadTiles;
    List<GameObject> blankTiles;

    public void GenerateBlankBoard(int rows, int columns)
    {
        roadTiles = new List<GameObject>();
        blankTiles = new List<GameObject>();

        roadSystemController = FindObjectOfType<RoadSystemController>();

        GridRowSize = rows;
        GridColumnSize = columns;

        int tileNumber = 0;

        grid = new GameObject[rows, columns];

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                float posY = (gridStartPosition.position.z) - row * tileSize;
                float posX = (gridStartPosition.position.x) + column * tileSize;

                grid[row, column] = SpawnBlankTile(row, column, tileNumber, posX, posY);

                tileNumber += 1;

            }
        }

        SetNeighbouringTiles();
    }

    private GameObject SpawnBlankTile(int row, int column, int tileNum, float posX, float posY)
    {
        GameObject blankTile = Instantiate(blankTilePrefab, new Vector3(posX, 0.0f, posY), Quaternion.identity);

        Tile blankT = blankTile.GetComponent<Tile>();

        blankT.SetTileLocation(row, column);
        blankT.SetTileNumber(tileNum);
        blankT.SetTileType(TileType.BLANK);
        blankTile.transform.parent = gameObject.transform;

        //Add Blank Tile to List 
        blankTiles.Add(blankTile);

        return blankTile;
    }

    private void SetNeighbouringTiles()
    {
        for (int row = 0; row < GridRowSize; row++)
        {
            for (int col = 0; col < GridColumnSize; col++)
            {
                Tile currentTile = grid[row, col].GetComponent<Tile>();

                //Top -row, col
                if (GetTileAtRowAndColumn(row - 1, col))
                {
                    currentTile.topNeighbour = GetTileAtRowAndColumn(row - 1, col).GetComponent<Tile>();
                }
                //Right row, +col
                if (GetTileAtRowAndColumn(row, col + 1))
                {
                    currentTile.rightNeighbour = GetTileAtRowAndColumn(row, col + 1).GetComponent<Tile>();
                }
                //Bottom +row, col
                if (GetTileAtRowAndColumn(row + 1, col))
                {
                    currentTile.bottomNeighbour = GetTileAtRowAndColumn(row + 1, col).GetComponent<Tile>();
                }
                //Left row, -col
                if (GetTileAtRowAndColumn(row, col - 1))
                {
                    currentTile.leftNeighbour = GetTileAtRowAndColumn(row, col - 1).GetComponent<Tile>();
                }
            }
        }
    }

    private GameObject GetTileAtRowAndColumn(int row, int col)
    {
        //Check if Row and Column are Valid 
        if (row >= 0 && row < GridRowSize)
        {
            if (col >= 0 && col < GridColumnSize)
            {
                return grid[row, col];
            }
        }
        return null;
    }


    public void BuildInnerCircleRoad()
    {
        //Build Inner Circle Roads 

        pendingRoadList = new List<GameObject>();

        //Horizontal 
        for (int i = 12; i < 20; i++)
        {
            pendingRoadList.Add(GetTileAtRowAndColumn(12, i));
            pendingRoadList.Add(GetTileAtRowAndColumn(19, i));
        }

        //Vertical
        for (int i = 13; i < 19; i++)
        {
            pendingRoadList.Add(GetTileAtRowAndColumn(i, 12));
            pendingRoadList.Add(GetTileAtRowAndColumn(i, 19));
        }

        roadSystemController.CalculatePendingRoadTiles(pendingRoadList);

        Debug.Log(pendingRoadList.Count);

        //Remove Road Tiles from Blank List and Re-add them to Road Tile List
        foreach (GameObject roadTile in pendingRoadList)
        {
            blankTiles.Remove(roadTile);
            roadTiles.Add(roadTile);
        }
        pendingRoadList.Clear();

        CalculateFourQuadrants();
        //  GenerateElevatedTiles();
    }

    /*
   // private void GenerateElevatedTiles()
    {
        Debug.Log("Generating Elevated Tiles");

        //Generate Random Num for amount of elevated Tiles to be generated
        int randAmount = Random.Range(10, 20);
        Debug.Log("Random Amount Of Elevated Tiles: " + randAmount);

        for (int i = 0; i < randAmount; i++)
        {
            //Get Random Num for Accessing Index 
            int randIndex = Random.Range(0, blankTiles.Count);
            Debug.Log("Random Index in Tile List: " + randIndex);

            //Access Random Index
            Tile newElevatedHighTile = blankTiles[randIndex].GetComponent<Tile>();

            newElevatedHighTile.GetComponent<MeshFilter>().mesh = blankElevatedPrefab.GetComponent<MeshFilter>().sharedMesh;
            newElevatedHighTile.GetComponent<MeshRenderer>().materials = blankElevatedPrefab.GetComponent<MeshRenderer>().sharedMaterials;
            newElevatedHighTile.transform.localScale = new Vector3(1f, 5f, 1f);

            List<Tile> elevatedTileNeighbours = newElevatedHighTile.GetNeighbourTiles();

            Debug.Log(elevatedTileNeighbours.Count);

            int randElevatedIndex = Random.Range(0, 4);

            Tile randNeighbourTile = elevatedTileNeighbours[randElevatedIndex];
            randNeighbourTile.GetComponent<MeshFilter>().mesh = blankElevatedPrefab.GetComponent<MeshFilter>().sharedMesh;
            randNeighbourTile.GetComponent<MeshRenderer>().materials = blankElevatedPrefab.GetComponent<MeshRenderer>().sharedMaterials;
            randNeighbourTile.transform.localScale = new Vector3(1f, 2.5f, 1f);

        }
    }
    */

    private void CalculateFourQuadrants()
    {
        halfGridSize = GridRowSize / 2;

        Debug.Log("Half Grid Size: " + halfGridSize);

        Vector2[] quadArray = { new Vector2(halfGridSize - 1, halfGridSize - 1),
                                new Vector2(halfGridSize - 1, halfGridSize),
                                new Vector2(halfGridSize, halfGridSize - 1),
                                new Vector2(halfGridSize, halfGridSize) };

        Debug.Log(quadArray.Length);

        foreach (Vector2 quadrant in quadArray)
        {
            Tile quadTile = GetTileAtRowAndColumn((int)quadrant.x, (int)quadrant.y).GetComponent<Tile>();

            quadTile.GetComponent<MeshRenderer>().materials = quadPrefab.GetComponent<MeshRenderer>().sharedMaterials;
        }



    }
}
